namespace FourtitudeAsiaTest.Model
{
    public class PartnerTransReq
    {
    }

    public class SubmittrxmessageReq
    {
        public string? Partnerkey { get; set; }
        public string? Partnerrefno { get; set; }
        public string? Partnerpassword { get; set; }
        public int Totalamount { get; set; }
        public List<Itemdetail> Items { get; set; } = new List<Itemdetail>();
        public string? Timestamp { get; set; }
        public string? Sig { get; set; }
    }

    public class Itemdetail
    {
        public string Partneritemref { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public int Unitprice { get; set; }
    }
}
