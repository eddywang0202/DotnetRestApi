using FourtitudeAsiaTest.Model;

namespace FourtitudeAsiaTest.BLL
{
    public interface IItemBLL
    {
        PartnerTransRes CalculateFinalAmount(long totalamount);
    }
}
