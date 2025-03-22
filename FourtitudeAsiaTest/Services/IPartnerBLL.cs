using FourtitudeAsiaTest.Model;

namespace FourtitudeAsiaTest.BLL
{
    public interface IPartnerBLL
    {
        PartnerTransRes CalculateFinalAmount(SubmittrxmessageReq req);
        bool IsAuthorized(SubmittrxmessageReq req);
        bool IsValidTotalAmount(SubmittrxmessageReq req);
    }
}
