using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            if (!CheckForDifferentCharsInCommonCell())
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

                wordData.direction.GetWordDirection(out Direction direction);
                bool isDirectionWrong = direction == Direction.Wrong;

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

                int value;
                bool isWordOutOfBounce = false;
                wordData.direction.GetWordDirection(out Direction direction);

                switch (direction)
                {
                    case Direction.Across:
                        value = x + wordData.answer.Length;
                        isWordOutOfBounce = value >= Columns;
                        break;

                    case Direction.Down:
                        value = y + wordData.answer.Length;
                        isWordOutOfBounce = value >= Rows;
                        break;
                }

                if (isWordOutOfBounce)
                {
                    CrosswordValidationLogs.LogWordOutOfBounceError(wordData.answer);
                    OnValidationFinished?.Invoke(ValidateResult.Fail);
                    return false;
                }
            }

            return true;
        }

        private bool CheckForDifferentCharsInCommonCell()
        {
            WordData[] wordsData = _crosswordConfig.wordsData;
            Dictionary<GridPosition, char> charsDictionary = new(capacity: 100);
            string answer;
            int column;
            int row;

            foreach (WordData wordData in wordsData)
            {
                answer = wordData.answer.ToLower();
                int length = answer.Length;
                column = wordData.column;
                row = wordData.row;
                wordData.direction.GetWordDirection(out Direction direction);

                for (int i = 0; i < length; i++)
                {
                    bool isCharsEqual = CheckCharInDictionary(i);
                    
                    if (!isCharsEqual)
                    {
                        CrosswordValidationLogs.LogDifferentCharsInCommonCellError(wordData.answer, column, row);
                        OnValidationFinished?.Invoke(ValidateResult.Fail);
                        return false;
                    }

                    AddDirection(direction);
                }
            }

            bool CheckCharInDictionary(int i)
            {
                GridPosition gridPosition = new(column, row);
                bool containsChar = charsDictionary.ContainsKey(gridPosition);
                    
                if (containsChar)
                {
                    bool isCharsEqual = charsDictionary[gridPosition] == answer[i];
                    return isCharsEqual;
                }

                charsDictionary.Add(gridPosition, answer[i]);
                return true;
            }

            void AddDirection(Direction direction)
            {
                switch (direction)
                {
                    case Direction.Across:
                        column++;
                        break;
                        
                    case Direction.Down:
                        row++;
                        break;
                }
            }
            
            return true;
        }
    }
}