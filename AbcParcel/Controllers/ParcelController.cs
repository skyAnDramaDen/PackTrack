using AbcParcel.Common;
using AbcParcel.Services.ParcelServices;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.ComponentModel.DataAnnotations;

namespace AbcParcel.Controllers
{
    public class ParcelController : Controller
    {
        // Constructor to inject IParcelService dependency.
        private readonly IParcelService _parcelService;
        public ParcelController(IParcelService parcelService)
        {
            _parcelService = parcelService;
        }

        //display list of parcels
        public async Task<IActionResult> Index()
        {
            //retrieve parcels from parcel service
            var entities = await _parcelService.GetParcelLists();
            //display results
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

        //method to retrieve parcels by tracking number
        public async Task<IActionResult> ViewParcelByTrackingNumber(string trackingNumber)
        {
            var entity = await _parcelService.GetParcelByTrackingNumber(trackingNumber);
            return View(entity.Result);
        }

        public IActionResult CreateParcel()
        {
            return View();
        }

        //handle parcel registration
        public async Task<IActionResult> RegisterParcel(CreateParcel model)
        {
            //validate model and create parcel service
            if (ModelState.IsValid)
            {
                //if successful redirect to index
                var result = await _parcelService.CreateParcel(model);
                if (result.Successful)
                {
                    return RedirectToAction("Index", "Parcel");
                }
            }
            //return view if registration fails
            return View(model);
        }
        public async Task<IActionResult> EditParcel(string Id)
        {
            // Retrieve parcel details by Id and display the edit view.
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
        //method to update parcel details
        public async Task<IActionResult> UpdateParcel(string trackingNumber, UpdateParcel model)
        {
            // validate model and update parcel using parcel service.
            if (ModelState.IsValid)
            {
                var result = await _parcelService.UpdateParcel(trackingNumber, model);

                // redirect to index view if update is successful.
                if (result.Successful)
                {
                    return RedirectToAction("Index", "Parcel");
                }
            }

            // return the view with model errors if update fails.
            return View(model);
        }
        //method to delete parcel
        public IActionResult DeleteById(long id)
        {
            //fetch and delete parcel from record using id property
            var result = _parcelService.DeleteRecordbyId(id);
            if (result)
                return RedirectToAction("Index", "Parcel");

            return RedirectToAction("Index", "Parcel");
        }

        //method to update parcel
        public async Task<IActionResult> UpdateParcelView(UpdateParcelView model)
        {

            //validate model 
            if (ModelState.IsValid)
            {
                var result = await _parcelService.UpdateParcelView(model);

                //redirect ti index if successful
                if (result.Successful)
                {
                    return RedirectToAction("Index", "Parcel");
                }
            }
            return View(model);
        }
    }
}
