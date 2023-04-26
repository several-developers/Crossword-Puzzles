using System.IO;
using Core.Infrastructure.Data.Crossword;
using UnityEngine;

namespace Core.Infrastructure.Providers.GameScene
{
    public class CrosswordProvider : ICrosswordProvider
    {
        public CrosswordProvider()
        {
        }

        public string[] GetGridHelper()
        {
            string[] gridHelper = {
                "0 1 2 3 4 5 6 7 8 9",
                "1 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "2 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "3 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "4 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "5 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "6 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "7 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "8 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "9 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
            };

            return gridHelper;
        }
    }
}