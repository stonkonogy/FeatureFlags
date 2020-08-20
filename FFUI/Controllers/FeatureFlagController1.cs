using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Interfaces;
using FFModels;
using Repositories;


namespace FFUI.Controllers
{
    public class FeatureFlagController1 : Controller
    {
        private IRepository _repository = new AzureFFRepository();
        // GET: FeatureFlagController1

        
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult>  GetEnabledFlags()
        {
            var activeFlags = await _repository.GetActiveFeatureFlagNames();

            return View(activeFlags.FeatureFlagNames);
        }

        // GET: FeatureFlagController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FeatureFlagController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FeatureFlagController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FeatureFlagController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FeatureFlagController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FeatureFlagController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FeatureFlagController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
