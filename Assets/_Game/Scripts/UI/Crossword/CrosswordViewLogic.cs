using System.Collections.Generic;
using Core.Crossword;
using Core.Enums;
using Core.Infrastructure.Config.Crossword;
using Core.Infrastructure.Providers.Global.Config;
using Core.Infrastructure.Services.GameScene;
using Core.UI.Utilities;
using Core.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Crossword
{
    public class CrosswordViewLogic
    {
        public CrosswordViewLogic(ICrosswordService crosswordService, IConfigProvider configProvider,
            CellItemUI cellItemPrefab, Transform cellsContainer, MonoBehaviour coroutineRunner,
            GridLayoutGroup gridLayoutGroup, TMP_InputField playerIF, AudioSource errorSoundAs,
            CrosswordViewShakeAnimation crosswordViewShakeAnimation)
        {
            _crosswordService = crosswordService;
            _configProvider = configProvider;
            _playerIF = playerIF;
            _errorSoundAs = errorSoundAs;
            _crosswordViewShakeAnimation = crosswordViewShakeAnimation;
            
            _crosswordBuilder = new(cellItemPrefab, cellsContainer);
            _crosswordAnimation = new(_crosswordBuilder);
            _layoutFixHelper = new(coroutineRunner, gridLayoutGroup);
            gridLayoutGroup.enabled = false;
        }
        
        private readonly ICrosswordService _crosswordService;
        private readonly IConfigProvider _configProvider;
        private readonly CrosswordBuilder _crosswordBuilder;
        private readonly CrosswordAnimation _crosswordAnimation;
        private readonly LayoutFixHelper _layoutFixHelper;
        private readonly TMP_InputField _playerIF;
        private readonly AudioSource _errorSoundAs;
        private readonly CrosswordViewShakeAnimation _crosswordViewShakeAnimation;
        
        public void CreateCrossword()
        {
            _crosswordBuilder.BuildCrossword();
            _layoutFixHelper.FixLayout();
            UpdateCrosswordView();
        }

        public void ClickLogic()
        {
            if (_playerIF.text.Length == 0)
                return;
            
            string word = _playerIF.text.ToLower();
            int hashCode = word.GetHashCode();
            bool foundMatch = _crosswordService.TryFindMatchWord(hashCode);

            string log = Log.Print($"Word = <gb>{word}</gb>, hash code = <gb>{hashCode}</gb>, " +
                                   $"found match = {(foundMatch ? "<gb>True</gb>" : "<rb>False</rb>")}");
            Debug.Log(log);

            if (!foundMatch)
            {
                HandlePlayerMistake();
                return;
            }

            PrepareCrosswordAnimation(hashCode);
        }
        
        private void UpdateCrosswordView()
        {
            WordData[] wordsDataArray = _configProvider.GetCrosswordConfig().wordsData;
            
            foreach (WordData wordData in wordsDataArray)
            {
                AnswerData answerData = _crosswordService.GetAnswerData(wordData.column, wordData.row);
                SetWord(wordData, answerData);
            }
        }

        private void SetWord(WordData wordData, AnswerData answerData)
        {
            for (int i = 0; i < answerData.Length; i++)
            {
                int column = answerData.Column;
                int row = answerData.Row;

                if (answerData.Direction == Direction.Across)
                    column += i;
                else
                    row += i;

                SetChar(column, row, wordData.answer[i]);
            }
        }

        private void SetChar(int column, int row, char letter)
        {
            GridPosition gridPosition = new(column, row);
            
            if (!_crosswordBuilder.TryGetCellItem(gridPosition, out CellItemUI cellItem))
                return;
                        
            cellItem.SetChar(letter);
            cellItem.SetColor(isGray: false);
        }

        private void PrepareCrosswordAnimation(int hashCode)
        {
            AnswerData answerData = _crosswordService.GetAnswerData(hashCode);
            List<GridPosition> gridPositionsList = new(capacity: answerData.Length);

            for (int i = 0; i < answerData.Length; i++)
            {
                int column = answerData.Column;
                int row = answerData.Row;

                if (answerData.Direction == Direction.Across)
                    column += i;
                else
                    row += i;

                GridPosition gridPosition = new(column, row);
                gridPositionsList.Add(gridPosition);
            }
            
            _crosswordAnimation.StartAnimation(gridPositionsList);
        }
        
        private void HandlePlayerMistake()
        {
            ErrorBehaviour errorBehaviour = _configProvider.GetGameConfig().errorBehaviour;

            if (errorBehaviour.shakeOnError)
                _crosswordViewShakeAnimation.StartAnimation();

            if (errorBehaviour.playSoundOnError)
                _errorSoundAs.Play();
        }
    }
}