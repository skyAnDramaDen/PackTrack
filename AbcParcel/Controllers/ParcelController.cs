using AbcParcel.Common;
using AbcParcel.Services.ParcelServices;
using Microsoft.AspNetCore.Mvc;

namespace AbcParcel.Controllers
{
    public class ParcelController : Controller
    {
        private readonly IParcelService _parcelService;
        public ParcelController(IParcelService parcelService)
        {
            _parcelService = parcelService;
        }

        public async Task<IActionResult> Index()
        {
            var entities = await _parcelService.GetParcelLists();
            if (entities.HasResult)
            {
                return View(entities.Result);
            }
            return View();

        }
        public IActionResult ViewParcel()
        {
            return View();
        }
        public async Task<IActionResult> ViewParcelByTrackingNumber(string trackingNumber)
        {
            var entity = await _parcelService.GetParcelByTrackingNumber(trackingNumber);
            return View(entity.Result);
        }

        public IActionResult CreateParcel()
        {
            return View();
        }
        public async Task<IActionResult> RegisterParcel(CreateParcel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _parcelService.CreateParcel(model);
                if (result.Successful)
                {
                    return RedirectToAction("Index", "Parcel");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> EditParcel(string Id)
        {
            try
            {
                if (Convert.ToInt32(Id) < 0)
                {
                    return NotFound();
                }
                var response = await _parcelService.GetParcelById(Id);
                if (response.Successful)
                {
                    var editView = response.Result;
                    return View(editView);
                }
                else
                {
                    return BadRequest(response.ValidationMessages);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> UpdateParcel(string trackingNumber, UpdateParcel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _parcelService.UpdateParcel(trackingNumber, model);
                if (result.Successful)
                {
                    return RedirectToAction("Index", "Parcel");
                }
            }
            return View(model);
        }
        public IActionResult DeleteById(long id)
        {
            var result = _parcelService.DeleteRecordbyId(id);
            if (result)
                return RedirectToAction("Index", "Parcel");

            return RedirectToAction("Index", "Parcel");
        }
        public async Task<IActionResult> UpdateParcelView(UpdateParcelView model)
        {
            if (ModelState.IsValid)
            {
                var result = await _parcelService.UpdateParcelView(model);
                if (result.Successful)
                {
                    return RedirectToAction("Index", "Parcel");
                }
            }
            return View(model);
        }
    }
}
