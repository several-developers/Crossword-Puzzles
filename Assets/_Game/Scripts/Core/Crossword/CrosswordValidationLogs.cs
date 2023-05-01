using Core.Utilities;
using UnityEngine;

namespace Core.Crossword
{
    public static class CrosswordValidationLogs
    {
        private const string Fail = "Crossword validation <rb>failed</rb>. ";
        
        public static void LogEmptyAnswerError(int column, int row)
        {
            string errorLog = Log.Print(Fail + "Reason: <rb>Hash Code Collision</rb>, " + 
                                        $"Column: <gb>{column}</gb>, Row: <gb>{row}</gb>.");
                    
            Debug.LogError(errorLog);
        }
        
        public static void LogHashCollisionError(string answer)
        {
            string errorLog = Log.Print(Fail + "Reason: <rb>Hash Code Collision</rb>, " + 
                                        $"answer: <gb>{answer}</gb>.");
                    
            Debug.LogError(errorLog);
        }

        public static void LogWrongDirectionError(string answer, string direction)
        {
            string errorLog = Log.Print(Fail + "Reason: <rb>Wrong Direction</rb>, " + 
                                        $"answer: <gb>{answer}</gb>, direction: <gb>{direction}</gb>.");
                    
            Debug.LogError(errorLog);
        }
        
        public static void LogPositionOutOfBounceError(string answer)
        {
            string errorLog = Log.Print(Fail + "Reason: <rb>Position Out Of Bounce</rb>, " + 
                                        $"answer: <gb>{answer}</gb>.");
                    
            Debug.LogError(errorLog);
        }
        
        public static void LogWordOutOfBounceError(string answer)
        {
            string errorLog = Log.Print(Fail + "Reason: <rb>Word Out Of Bounce</rb>, " + 
                                        $"answer: <gb>{answer}</gb>.");
                    
            Debug.LogError(errorLog);
        }

        public static void LogDifferentCharsInCommonCellError(string answer, int column, int row)
        {
            string errorLog = Log.Print(Fail + "Reason: <rb>Different Letters In Common Cell</rb>, " + 
                                        $"answer: <gb>{answer}</gb>, column: <gb>{column}</gb>, row: <gb>{row}</gb>.");
                    
            Debug.LogError(errorLog);
        }
    }
}