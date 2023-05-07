using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingAggSiteApp.Models;
using ShoppingAggSiteHub.SDK;
using ShoppingAggSiteHub.DTO.Item;

namespace ShoppingAggSiteApp.Controllers
{
    public class ItemController : Controller
    {
        private ItemSDK itemSDK = new ItemSDK("https://localhost:44390/api");

        public async Task<IActionResult> Index()
        {
            List<ItemDTO> data = await itemSDK.GetAllAsync();
            var itemVms = data.ConvertAll(x => new ItemVm
            {
                CurrentPrice = x.CurrentPrice,
                Id = x.Id,
                ItemImageUrl = x.ItemImageUrl,
                ItemName = x.ItemName,
                QualityRatingId = x.QualityRatingId,
                StoreId = x.StoreId,
                Weight = x.Weight
            });

            return View(itemVms);
        }

        // GET: ItemController/Details/5
        public async Task<IActionResult> Details(ItemVm itemVm, int id)
        {
            var data = await itemSDK.GetByIdAsync(id);

            itemVm.Id = data.Id;
            itemVm.StoreId = data.StoreId;
            itemVm.QualityRatingId = data.QualityRatingId;
            itemVm.ItemName = data.ItemName;
            itemVm.ItemImageUrl = data.ItemImageUrl;
            itemVm.CurrentPrice = data.CurrentPrice;
            itemVm.Weight = data.Weight;

            return View(itemVm);
        }

        // GET: ItemController/Add
        public IActionResult Add()
        {
            ItemAddVm itemAddVm = new ItemAddVm();
            return View(itemAddVm);
        }

        // POST: ItemController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ItemAddVm itemAddVm)
        {
            ItemAddDTO itemAddDTO = new ItemAddDTO
            {
                StoreId = itemAddVm.StoreId,
                QualityRatingId = itemAddVm.QualityRatingId,
                ItemName = itemAddVm.ItemName,
                ItemImageUrl = itemAddVm.ItemImageUrl,
                CurrentPrice = itemAddVm.CurrentPrice,
                Weight = itemAddVm.Weight
            };

            var data = await itemSDK.AddAsync(itemAddDTO);
            return RedirectToAction("Details", new { id = data });
        }

        // GET: ItemController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var data = await itemSDK.GetByIdAsync(id);

            ItemUpdateVm updateVm = new ItemUpdateVm();
            updateVm.CurrentPrice = data.CurrentPrice;
            updateVm.ItemId = data.Id;
            updateVm.ItemImageUrl= data.ItemImageUrl;
            updateVm.ItemName = data.ItemName;
            updateVm.StoreId = data.StoreId;
            updateVm.QualityRatingId = data.QualityRatingId;
            updateVm.Weight = data.Weight;

            return View(updateVm);
        }

        // POST: ItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ItemUpdateVm itemUpdateVm, int id)
        {
            var data = await itemSDK.GetByIdAsync(id);

            ItemUpdateDTO updatedItemDTO = new ItemUpdateDTO()
            {
                ItemName = itemUpdateVm.ItemName,
                ItemId = itemUpdateVm.ItemId,
                CurrentPrice = itemUpdateVm.CurrentPrice,
                ItemImageUrl = itemUpdateVm.ItemImageUrl,
                StoreId = itemUpdateVm.StoreId,
                QualityRatingId = itemUpdateVm.QualityRatingId,
                Weight = itemUpdateVm.Weight
            };

            var updatedData = await itemSDK.UpdateAsync(updatedItemDTO);
            return RedirectToAction("Index");
        }

        //GET: ItemController/Delete/5
        public async Task<IActionResult> Delete(ItemVm itemVm, int id)
        {
            var data = await itemSDK.GetByIdAsync(id);

            itemVm.Id = data.Id;
            itemVm.StoreId = data.StoreId;
            itemVm.QualityRatingId = data.QualityRatingId;
            itemVm.ItemName = data.ItemName;
            itemVm.ItemImageUrl = data.ItemImageUrl;
            itemVm.CurrentPrice = data.CurrentPrice;
            itemVm.Weight = data.Weight;

            return View(itemVm);
        }

        //POST: ItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var data = await itemSDK.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
