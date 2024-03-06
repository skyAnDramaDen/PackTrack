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
        //inject IParcelService and IUserService dependencies
        public ParcelExposedController(IParcelService parcelService, IUserService userService)
        {
            _parcelService = parcelService;
            _userService = userService;
        }
        // GET: api/ParcelExposed/GetParcelById
        // method to retrieve parcel details by Id.
        [HttpGet("GetParcelById")]
        public async Task<ActionResult<Response<string>>> GetParcelById(string id)
        {
            //encase attempt to return parcel by id in try block
            try
            {
                //retrieve parcel by id
                var entity = await _parcelService.GetParcelById(id);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //method to retrieve list of parcels
        [HttpGet("GetParcelLists")]
        public async Task<ActionResult<Response<IEnumerable<ParcelViewModel>>>> GetParcelLists()
        {
            try
            {

                //retrieve list of parcels
                var entity = await _parcelService.GetParcelLists();
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/ParcelExposed/CreateParcel
        // method to create a new parcel.
        [HttpPost("CreateParcel")]
        public async Task<ActionResult<Response<IEnumerable<ParcelViewModel>>>> CreateParcel(CreateParcel model)
        {
            try
            {
                //create parcel with model thats passed
                var entity = await _parcelService.CreateParcel(model);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/ParcelExposed/UpdateParcel
        // method to update parcel details.
        [HttpPut("UpdateParcel")]
        public async Task<ActionResult<Response<string>>> UpdateParcel(string trackingNumber, UpdateParcel model)
        {
            try
            {
                //update parcel using tracking number
                var entity = await _parcelService.UpdateParcel(trackingNumber, model);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/ParcelExposed/CreateAdmin
        // method to create a new admin user.
        [HttpPost("CreateAdmin")]
        public async Task<ActionResult<Response<string>>> CreateAdmin(RegisterAdminViewModel model)
        {
            try
            {
                //create new admin user using user service
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
