namespace ConsultationService.Core.Classes
{
    public class Snils
    {
        // собственный номер
        public string PersonalNumber { get; set; }

        //  контрольное число
        public int ControlNumber { get; set; }

        //  контрольная сумма (считается из номера)
        public int ControlSum { get; set; }

        public bool IsValid 
        {
            get 
            {
                return ControlSum == ControlNumber;
            }
        }
    }
}
