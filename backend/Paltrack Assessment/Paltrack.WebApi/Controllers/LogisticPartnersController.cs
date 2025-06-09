using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Paltrack.Application.Contracts;

namespace Paltrack.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogisticPartnersController : ControllerBase
    {
        private readonly ILogisticsPartnerService _logisticsPartnerService;

        public LogisticPartnersController(ILogisticsPartnerService logisticsPartnerService)
        {
            _logisticsPartnerService = logisticsPartnerService;
        }

        //[HttpGet("logistics-partners")]
        //public async Task<IActionResult> GetAll([FromQuery] LogisticsPartnerFilter filter)
        //{
        //    var result = await _logisticsPartnerService.GetAllAsync(filter);

        //    if (result.IsSuccess)
        //    {
        //        return Ok(new
        //        {
        //            data = result.Data.Items, 
        //            totalCount = result.Data.TotalCount 
        //        });
        //    }

        //    return BadRequest(result.Message); 
        //}

        [HttpGet("logistics-partners")]
        public async Task<LoadResult> GetQueryable([FromQuery] DataSourceLoadOptionsBase loadOptions)
        {
            var query = _logisticsPartnerService.GetQueryable();
            return await DataSourceLoader.LoadAsync(query, loadOptions);
        }
    }
}
