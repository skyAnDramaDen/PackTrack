using AbcParcel.Common.Contract;
using AbcParcel.Common;
using AbcParcel.Services.ParcelServices;
using Microsoft.AspNetCore.Mvc;
using AbcParcel.Services.UserServices;
using AbcParcel.Models;

namespace AbcParcel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelExposedController : ControllerBase
    {
        private readonly IParcelService _parcelService;
        private readonly IUserService _userService;
        public ParcelExposedController(IParcelService parcelService, IUserService userService)
        {
            _parcelService = parcelService;
            _userService = userService;
        }
        [HttpGet("GetParcelById")]
        public async Task<ActionResult<Response<string>>> GetParcelById(string id)
        {
            try
            {
                var entity = await _parcelService.GetParcelById(id);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetParcelLists")]
        public async Task<ActionResult<Response<IEnumerable<ParcelViewModel>>>> GetParcelLists()
        {
            try
            {
                var entity = await _parcelService.GetParcelLists();
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateParcel")]
        public async Task<ActionResult<Response<IEnumerable<ParcelViewModel>>>> CreateParcel(CreateParcel model)
        {
            try
            {
                var entity = await _parcelService.CreateParcel(model);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateParcel")]
        public async Task<ActionResult<Response<string>>> UpdateParcel(string trackingNumber, UpdateParcel model)
        {
            try
            {
                var entity = await _parcelService.UpdateParcel(trackingNumber, model);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateAdmin")]
        public async Task<ActionResult<Response<string>>> CreateAdmin(RegisterAdminViewModel model)
        {
            try
            {
                var entity = await _userService.RegisterAdmin(model);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
