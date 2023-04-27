using System;
using System.Collections.Generic;
using Core.Crossword;
using Core.Enums;
using Core.Infrastructure.Data.Crossword;
using Core.Infrastructure.Data.Player;
using Core.Infrastructure.Services.Global;
using Core.Utilities;
using UnityEngine;

namespace Core.Infrastructure.Services.GameScene
{
    public class CrosswordService : ICrosswordService
    {
        public CrosswordService(IGameDataService gameDataService)
        {
            _gameDataService = gameDataService;
            _answersDataDictionary = new();
            _errorBehaviour = gameDataService.GetPlayerData().errorBehaviour;
            
            SetupAnswersDataDictionary();
        }
        
        private readonly IGameDataService _gameDataService;
        private readonly ErrorBehaviour _errorBehaviour;
        private readonly Dictionary<int, AnswerData> _answersDataDictionary;

        public bool TryFindMatchWord(int hashCode) =>
            _answersDataDictionary.ContainsKey(hashCode);

        public AnswerData GetAnswerData(int hashCode) =>
            TryFindMatchWord(hashCode) ? _answersDataDictionary[hashCode] : new AnswerData();

        public AnswerData GetAnswerData(int column, int row)
        {
            foreach (AnswerData answerData in _answersDataDictionary.Values)
            {
                if (answerData.Column != column || answerData.Row != row)
                    continue;

                return answerData;
            }
            
            return new AnswerData();
        }

        public ErrorBehaviour GetErrorBehaviour() => _errorBehaviour;

        public void UpdateAnswersData()
        {
            _answersDataDictionary.Clear();
            SetupAnswersDataDictionary();
        }

        private void SetupAnswersDataDictionary()
        {
            CrosswordData crosswordData = _gameDataService.GetCrosswordData();

            foreach (WordData wordData in crosswordData.wordsData)
            {
                int hashCode = wordData.answer.ToLower().GetHashCode();

                if (CheckDictionaryKeyCollision(hashCode))
                    break;

                int length = wordData.answer.Length;
                bool isDown = string.Equals(wordData.direction, "down", StringComparison.OrdinalIgnoreCase);
                Direction direction = isDown ? Direction.Down : Direction.Across;
                AnswerData answerData = new(direction, wordData.column, wordData.row, length);
                _answersDataDictionary.Add(hashCode, answerData);
            }
        }

        private bool CheckDictionaryKeyCollision(int hashCode)
        {
            if (!_answersDataDictionary.ContainsKey(hashCode))
                return false;
            
            string errorLog = Log.Print("<rb>Collision</rb> was found!");
            Debug.LogError(errorLog);
            
            return true;
        }
    }
}