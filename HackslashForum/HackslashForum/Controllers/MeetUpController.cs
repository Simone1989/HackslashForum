using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackslashForum.Controllers
{
    public class MeetUpController : Controller
    {
        // GET: MeetUp
        public ActionResult Index()
        {
            return View();
        }

        // GET: MeetUp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MeetUp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeetUp/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MeetUp/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MeetUp/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MeetUp/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MeetUp/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}