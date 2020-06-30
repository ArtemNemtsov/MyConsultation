using System;

namespace ConsultationService.Core.Classes
{
    public static class SnilsParser
    {
        private static readonly int countSnilsNumber = 9;

        public static int GetControlNumber(string snils)
        {
            var controlNumber = snils.Substring(countSnilsNumber);
            bool success = Int32.TryParse(controlNumber, out int number);

            return success ? number : 0;
        }

        public static string GetPersonalNumber(string snils)
        {
            return snils.Substring(0, countSnilsNumber);
        }

        public static int CalcControlSum(string personalNumbers)
        {
            int controlSum = 0;

            for (var i = 0; i < personalNumbers.Length; i++)
            {
                //  обратный номер позиции равен длине строки - номер текущей позиции
                int positionNum = personalNumbers.Length - i;

                // получаем текущий элемент
                int currrentNumber = (int)Char.GetNumericValue(personalNumbers[i]);

                // контрольная сумма = обратный номер позици * текущий элемент
                controlSum += positionNum * currrentNumber;
            }
            
            switch (controlSum)
            {
                case int sum when sum == 100 || sum == 101:
                    controlSum = 00;
                    break;

                case int sum when sum > 100:
                    controlSum %= 101;
                    break;
            }

            return controlSum;
        }
    }
}
