using System.Linq;

namespace ConsultationService.Core.Classes
{
    public static class SnilsBuilder
    {
        public static Snils BuldSnils(string dirtySnils)
        {
            // оставляем только цифры
            string snils = new string(dirtySnils
                .Where(symbol => char.IsDigit(symbol))
                .ToArray());

            // получаем собственный номер снилса
            var personalNumber = SnilsParser.GetPersonalNumber(snils);

            return new Snils
            {
                PersonalNumber = personalNumber,
                ControlNumber = SnilsParser.GetControlNumber(snils),
                ControlSum = SnilsParser.CalcControlSum(personalNumber),
            };
        } 
    }
}
