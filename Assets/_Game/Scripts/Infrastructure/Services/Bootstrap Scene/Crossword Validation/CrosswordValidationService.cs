using System;
using System.Collections;
using System.Collections.Generic;
using Core.Crossword;
using Core.Enums;
using Core.Infrastructure.Config.Crossword;
using Core.Infrastructure.Providers.Global.Config;
using Core.Utilities;
using UnityEngine;

namespace Core.Infrastructure.Services.BootstrapScene
{
    public class CrosswordValidationService : ICrosswordValidationService
    {
        public CrosswordValidationService(IConfigProvider configProvider, ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _crosswordConfig = configProvider.GetCrosswordConfig();
        }

        public event Action<ValidateResult> OnValidationFinished;

        private readonly ICoroutineRunner _coroutineRunner;
        private readonly CrosswordConfig _crosswordConfig;
        
        private const int Columns = 10;
        private const int Rows = 10;

        public void Validate() =>
            _coroutineRunner.StartCoroutine(ValidationCO());

        private IEnumerator ValidationCO()
        {
            if (!CheckForEmptyAnswers())
                yield break;
            
            yield return new WaitForEndOfFrame();
            
            if (!CheckForSimilarWords())
                yield break;
            
            yield return new WaitForEndOfFrame();

            if (!CheckForCorrectDirections())
                yield break;
            
            yield return new WaitForEndOfFrame();

            if (!CheckForOutOfBounce())
                yield break;
            
            yield return new WaitForEndOfFrame();
            
            OnValidationFinished?.Invoke(ValidateResult.Success);

        }

        private bool CheckForEmptyAnswers()
        {
            foreach (WordData wordData in _crosswordConfig.wordsData)
            {
                bool isAnswerEmpty = string.IsNullOrWhiteSpace(wordData.answer);

                if (isAnswerEmpty)
                {
                    CrosswordValidationLogs.LogEmptyAnswerError(wordData.column, wordData.row);
                    OnValidationFinished?.Invoke(ValidateResult.Fail);
                    return false;
                }
            }

            return true;
        }

        private bool CheckForSimilarWords()
        {
            var hashes = new LinkedList<int>();

            foreach (WordData wordData in _crosswordConfig.wordsData)
            {
                int hashCode = wordData.answer.ToLower().GetHashCode();
                bool hasHashCollision = hashes.Contains(hashCode);

                if (hasHashCollision)
                {
                    CrosswordValidationLogs.LogHashCollisionError(wordData.answer);
                    OnValidationFinished?.Invoke(ValidateResult.Fail);
                    return false;
                }

                hashes.AddFirst(hashCode);
            }

            return true;
        }
        
        private bool CheckForCorrectDirections()
        {
            foreach (WordData wordData in _crosswordConfig.wordsData)
            {
                bool isDirectionEmpty = string.IsNullOrWhiteSpace(wordData.direction);
                
                if (isDirectionEmpty)
                {
                    CrosswordValidationLogs.LogWrongDirectionError(wordData.answer, wordData.direction);
                    OnValidationFinished?.Invoke(ValidateResult.Fail);
                    return false;
                }

                bool isDirectionWrong =
                    !string.Equals(wordData.direction, "down", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(wordData.direction, "across", StringComparison.OrdinalIgnoreCase);

                if (isDirectionWrong)
                {
                    CrosswordValidationLogs.LogWrongDirectionError(wordData.answer, wordData.direction);
                    OnValidationFinished?.Invoke(ValidateResult.Fail);
                    return false;
                }
            }
            
            return true;
        }

        private bool CheckForOutOfBounce()
        {
            foreach (WordData wordData in _crosswordConfig.wordsData)
            {
                int x = wordData.column;
                int y = wordData.row;
                bool isPositionOutOfBounce = x < 0 || y < 0 || x >= Columns || y >= Rows;

                if (isPositionOutOfBounce)
                {
                    CrosswordValidationLogs.LogPositionOutOfBounceError(wordData.answer);
                    OnValidationFinished?.Invoke(ValidateResult.Fail);
                    return false;
                }

                bool isDownDirection = string.Equals(wordData.direction.ToLower(), "down");
                int value;

                if (isDownDirection)
                    value = y + wordData.answer.Length;
                else
                    value = x + wordData.answer.Length;

                bool isWordOutOfBounce = isDownDirection ? value >= Rows : value >= Columns;
                
                if (isWordOutOfBounce)
                {
                    CrosswordValidationLogs.LogWordOutOfBounceError(wordData.answer);
                    OnValidationFinished?.Invoke(ValidateResult.Fail);
                    return false;
                }
            }

            return true;
        }
    }
}