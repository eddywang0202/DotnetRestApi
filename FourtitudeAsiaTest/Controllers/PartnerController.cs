using FourtitudeAsiaTest.BLL;
using FourtitudeAsiaTest.Model;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace FourtitudeAsiaTest.Controllers
{
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PartnerController));
        private readonly IPartnerBLL _partnerBLL;

        public PartnerController(ILogger<PartnerController> logger, IPartnerBLL partnerBLL)
        {
            _partnerBLL = partnerBLL;
        }

        [HttpPost]
        [Route("api/submittrxmessage")]
        public IActionResult Submittrxmessage(SubmittrxmessageReq req)
        {
            _log.Info($"Submittrxmessage start, req:{JsonConvert.SerializeObject(req)}");

            var result = new BaseResponse { Result = 0 };

            //validation
            result.Resultmessage =
                string.IsNullOrEmpty(req.Partnerkey) ? "Partnerkey is required" :
                string.IsNullOrEmpty(req.Partnerrefno) ? "Partnerrefno is required" :
                string.IsNullOrEmpty(req.Partnerpassword) ? "Partnerpassword is required" :
                string.IsNullOrEmpty(req.Timestamp) ? "Timestamp is required" :
                string.IsNullOrEmpty(req.Sig) ? "Sig is required" :
                Math.Abs((DateTime.UtcNow - DateTime.Parse(req.Timestamp).ToUniversalTime()).Minutes) > 5 ? "Expired" :
                !_partnerBLL.IsAuthorized(req) ? "Access Denied!" :
                !_partnerBLL.IsValidTotalAmount(req) ? "Total amount not match" :
                req.Totalamount <= 0 ? "Totalamount must positive value" : "";

            if (!string.IsNullOrEmpty(result.Resultmessage))
            {
                _log.Info($"Submittrxmessage end, res:{JsonConvert.SerializeObject(result)}");
                return Ok(result);
            }

            var finalResult = _partnerBLL.CalculateFinalAmount(req);

            result.TotalAmount = req.Totalamount;
            result.TotalDiscount = (int)finalResult.TotalDiscount;
            result.FinalAmount = (int)finalResult.FinalAmount;
            result.Result = 1;

            _log.Info($"Submittrxmessage end, res:{JsonConvert.SerializeObject(result)}");
            return Ok(result);
        }
    }
}
