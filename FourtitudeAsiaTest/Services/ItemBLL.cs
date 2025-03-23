using FourtitudeAsiaTest.Model;

namespace FourtitudeAsiaTest.BLL
{
    public class ItemBLL : IItemBLL
    {
        public ItemBLL()
        {
        }

        public PartnerTransRes CalculateFinalAmount(long totalamount)
        {
            decimal totalamountMYR = (decimal)(totalamount / 100.0);
            var discount = GetDiscount(totalamountMYR);
            var discountAmount = totalamountMYR * discount;
            var finalamount = totalamountMYR - discountAmount;

            return new PartnerTransRes
            {
                TotalDiscount = discountAmount * 100,
                FinalAmount = finalamount * 100
            };
        }

        private static decimal GetDiscount(decimal totalAmount)
        {
            var baseDiscount =
                totalAmount < 200 ? 0 :
                totalAmount >= 200 && totalAmount <= 500 ? 5 :
                totalAmount >= 501 && totalAmount <= 800 ? 7 :
                totalAmount >= 801 && totalAmount <= 1200 ? 10 :
                totalAmount > 1200 ? 15 : 0;

            var specialDiscount =
                (totalAmount > 900 & totalAmount % 10 == 5) ? 10 :
                (totalAmount > 500 & totalAmount % 2 != 0) ? 8 : 0;

            var finalDiscount = baseDiscount + specialDiscount >= 20 ?
                20 : baseDiscount + specialDiscount;

            return finalDiscount >= 0 ? (decimal)(finalDiscount / 100.0) : 0;
        }
    }
}
