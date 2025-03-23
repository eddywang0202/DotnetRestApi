using FluentValidation;
using FourtitudeAsiaTest.Helper;
using FourtitudeAsiaTest.Model;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FourtitudeAsiaTest.Validators
{
    public class SubmitTransReqValidator : AbstractValidator<SubmittrxmessageReq>
    {
        private readonly IConfiguration _configuration;
        private readonly IActionContextAccessor _actionContextAccessor;

        public SubmitTransReqValidator(IConfiguration configuration, IActionContextAccessor actionContextAccessor)
        {
            _configuration = configuration;
            _actionContextAccessor = actionContextAccessor;

            var actionContext = _actionContextAccessor.ActionContext;
            var isModelStateValid = actionContext != null && actionContext.ModelState.IsValid;
            var modelStateErrorMessage = actionContext.ModelState.Select(x => x.Value?.Errors.FirstOrDefault()?.ErrorMessage).FirstOrDefault() ?? "No Error";

            RuleFor(x => x)
                .Cascade(CascadeMode.Stop)
                .Must(x => isModelStateValid).WithMessage($"{modelStateErrorMessage}")
                .Must(x => IsValidTimestamp(x.Timestamp)).WithMessage("Invalid timestamp or Expired.")
                .Must(x => IsAuthorized(x)).WithMessage("Access Denied!.")
                .Must(x => IsValidTotalAmount(x)).WithMessage("Invalid Total Amount.");
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

        public bool IsValidTimestamp(string timestamp)
        {
            return !(!DateTime.TryParse(timestamp, out DateTime parsedTime) ||
                    Math.Abs((DateTime.UtcNow - parsedTime.ToUniversalTime()).TotalMinutes) > 5);
        }

        public bool IsValidTotalAmount(SubmittrxmessageReq req)
        {
            return req.Items.Sum(x => x.Unitprice * x.Qty) == req.Totalamount;
        }
    }
}
