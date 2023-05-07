using Microsoft.AspNetCore.Mvc;
using ShoppingAggSiteApp.Models;
using ShoppingAggSiteHub.DTO.Store;
using ShoppingAggSiteHub.SDK;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ShoppingAggSiteApp.Controllers
{
    public class StoreController : Controller
    {
        private StoreSDK storeSDK = new StoreSDK("https://localhost:44390/api");
        public async Task<IActionResult> Index()
        {
            List<StoreDTO> data = await storeSDK.GetAllAsync();
            var storeVms = data.ConvertAll(x => new StoreVm { StoreName = x.StoreName, BrandId = x.BrandId, Id = x.Id, LocationId = x.LocationId, StoreImageUrl = x.StoreImageUrl });

            return View(storeVms);
        }
        public async Task<IActionResult> Details(StoreVm storeVm, int id)
        {
            var data = await storeSDK.GetByIdAsync(id);

            storeVm.Id = data.Id;
            storeVm.BrandId = data.BrandId;
            storeVm.LocationId = data.LocationId;
            storeVm.StoreImageUrl = data.StoreImageUrl;
            storeVm.StoreName = data.StoreName;

            return View(storeVm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        // GET: StoreController/Add
        public IActionResult Add()
        {
            StoreAddVm storeAddVm = new StoreAddVm();
            return View(storeAddVm);
        }
        // POST: StoreController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(StoreAddVm storeAddVm)
        {
            StoreAddDTO storeAddDTO = new StoreAddDTO
            {
                BrandId = storeAddVm.BrandId,
                LocationId = storeAddVm.LocationId,
                StoreImageUrl = storeAddVm.StoreImageUrl,
                StoreName = storeAddVm.StoreName
            };

            var data = await storeSDK.AddAsync(storeAddDTO);
            return RedirectToAction("Details", new { id = data });
        }
        // GET: StoreController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var data = await storeSDK.GetByIdAsync(id);

            StoreVm updateVm = new StoreVm();
            updateVm.Id = data.Id;
            updateVm.BrandId = data.BrandId;
            updateVm.LocationId = data.LocationId;
            updateVm.StoreImageUrl = data.StoreImageUrl;
            updateVm.StoreName = data.StoreName;

            return View(updateVm);
        }

        // POST: StoreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StoreVm storeVm, int id)
        {
            StoreUpdateDTO updatedStoreDTO = new StoreUpdateDTO()
            {
                StoreId = storeVm.Id,
                BrandId = storeVm.BrandId,
                LocationId = storeVm.LocationId,
                StoreImageUrl = storeVm.StoreImageUrl,
                StoreName = storeVm.StoreName,
            };

            var updatedData = await storeSDK.UpdateAsync(updatedStoreDTO);
            return RedirectToAction("Index");
        }
    }
}
