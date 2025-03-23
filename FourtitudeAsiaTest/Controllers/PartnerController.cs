using FourtitudeAsiaTest.BLL;
using FourtitudeAsiaTest.Loggers;
using FourtitudeAsiaTest.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FourtitudeAsiaTest.Controllers
{
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly IAppLogger<PartnerController> _logger;
        private readonly IItemBLL _partnerBLL;

        public PartnerController(IAppLogger<PartnerController> logger, IItemBLL partnerBLL)
        {
            _logger = logger;
            _partnerBLL = partnerBLL;
        }

        [HttpPost]
        [Route("api/submittrxmessage")]
        public IActionResult Submittrxmessage(SubmittrxmessageReq req)
        {
            _logger.LogInfo($"Submittrxmessage start, req:{JsonConvert.SerializeObject(req)}");

            try
            {
                var finalResult = _partnerBLL.CalculateFinalAmount(req.Totalamount);

                var result = new BaseResponse
                {
                    Result = 1,
                    TotalAmount = req.Totalamount,
                    TotalDiscount = (int)finalResult.TotalDiscount,
                    FinalAmount = (int)finalResult.FinalAmount
                };

                _logger.LogInfo($"Submittrxmessage end, res:{JsonConvert.SerializeObject(result)}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Submittrxmessage error, ex: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse { Result = 0, Resultmessage = ex.Message });
            }
        }
    }
}
