using System.Text.Json.Serialization;

namespace FourtitudeAsiaTest.Model
{
    public class ModelRes
    {
    }

    public class BaseResponse
    {
        public int Result { get; set; }
        public string Resultmessage { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public long TotalAmount { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? TotalDiscount { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int FinalAmount { get; set; }
    }
}
