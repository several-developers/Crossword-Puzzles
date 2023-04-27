using System.Threading.Tasks;
using Core.Crossword;
using Core.Enums;
using Core.Infrastructure.Data.Crossword;
using Core.Infrastructure.Services.GameScene;
using Core.Infrastructure.Services.Global;
using Core.UI.Utilities;
using Core.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Crossword
{
    public class CrosswordViewLogic
    {
        public CrosswordViewLogic(ICrosswordService crosswordService, IGameDataService gameDataService,
            CellItemUI cellItemPrefab, Transform cellsContainer, MonoBehaviour coroutineRunner,
            GridLayoutGroup gridLayoutGroup, TMP_InputField playerIF)
        {
            _crosswordService = crosswordService;
            _gameDataService = gameDataService;
            _playerIF = playerIF;
            gridLayoutGroup.enabled = false;
            _crosswordBuilder = new(cellItemPrefab, cellsContainer);
            _layoutFixHelper = new(coroutineRunner, gridLayoutGroup);
        }
        
        private readonly ICrosswordService _crosswordService;
        private readonly IGameDataService _gameDataService;
        private readonly CrosswordBuilder _crosswordBuilder;
        private readonly LayoutFixHelper _layoutFixHelper;
        private readonly TMP_InputField _playerIF;
        
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
                return;
            
            AnswerData answerData = new();
        }
        
        private void UpdateCrosswordView()
        {
            WordData[] wordsDataArray = _gameDataService.GetCrosswordData().wordsData;
            
            foreach (WordData wordData in wordsDataArray)
            {
                AnswerData answerData = _crosswordService.GetAnswerData(wordData.column, wordData.row);
                ShowWord(wordData, answerData);
            }
        }

        private void ShowWord(WordData wordData, AnswerData answerData)
        {
            for (int i = 0; i < answerData.Length; i++)
            {
                int column = answerData.Column;
                int row = answerData.Row;

                if (answerData.Direction == Direction.Across)
                    column += i;
                else
                    row += i;

                ShowChar(column, row, wordData.answer[i]);
            }
        }

        private void ShowChar(int column, int row, char letter)
        {
            if (!_crosswordBuilder.TryGetCellItem(column, row, out CellItemUI cellItem))
                return;
                        
            cellItem.SetChar(letter);
            cellItem.SetColor(isGray: false);
        }
    }
}