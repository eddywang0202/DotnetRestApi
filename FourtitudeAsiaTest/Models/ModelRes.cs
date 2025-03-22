namespace FourtitudeAsiaTest.Model
{
    public class ModelRes
    {
    }

    public class BaseResponse
    {
        public int Result { get; set; }
        public string Resultmessage { get; set; }
        public int TotalAmount { get; set; }
        public int TotalDiscount { get; set; }
        public int FinalAmount { get; set; }
    }
}
