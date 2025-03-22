using FourtitudeAsiaTest.Helper;
using FourtitudeAsiaTest.Model;

namespace FourtitudeAsiaTest.BLL
{
    public class PartnerBLL : IPartnerBLL
    {
        private readonly IConfiguration _configuration;

        public PartnerBLL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PartnerTransRes CalculateFinalAmount(SubmittrxmessageReq req)
        {
            decimal totalamount = (decimal)(req.Totalamount / 100.0);
            var discount = GetDiscount(totalamount);
            var discountAmount = totalamount * discount;
            var finalamount = totalamount - discountAmount;

            return new PartnerTransRes
            {
                TotalDiscount = discountAmount * 100,
                FinalAmount = finalamount * 100
            };
        }

        public bool IsAuthorized(SubmittrxmessageReq req)
        {
            var partnerkeys = _configuration.GetSection("PartnerSettings:Partnerkeys").Get<List<string>>();
            if (!partnerkeys.Any(x => x == req?.Partnerkey))
            {
                return false;
            }

            var timeStampString = DateTime.Parse(req.Timestamp).ToUniversalTime().ToString("yyyyMMddHHmmss");
            var trxInfo = $"{timeStampString}{req.Partnerkey}{req.Partnerrefno}{req.Totalamount}{req.Partnerpassword}";
            var trxInfoEncrypt = EncryptHelper.ComputeSha256Base64(trxInfo);

            return trxInfoEncrypt == req.Sig;
        }

        public bool IsValidTotalAmount(SubmittrxmessageReq req)
        {
            var itemsTotalAmounts = req.Items.Sum(x => x.Unitprice * x.Qty);
            return itemsTotalAmounts == req.Totalamount;
        }

        private decimal GetDiscount(decimal totalAmount)
        {
            var baseDiscount =
                totalAmount < 200 ? 0 :
                totalAmount >= 200 && totalAmount <= 500 ? 5 :
                totalAmount >= 501 && totalAmount <= 800 ? 7 :
                totalAmount >= 801 && totalAmount <= 1200 ? 10 :
                totalAmount > 1200 ? 15 : 0;

            var specialDiscount =
                (totalAmount > 500 & totalAmount % 2 != 0) ? 8 :
                (totalAmount > 900 & totalAmount % 10 == 5) ? 10 : 0;

            var finalDiscount = baseDiscount + specialDiscount >= 20 ?
                20 : baseDiscount + specialDiscount;

            return finalDiscount >= 0 ? (decimal)(finalDiscount / 100.0) : 0;
        }
    }
}
