using Microsoft.AspNetCore.Mvc;
using OkulProjesi.Connect;
using OkulProjesi.Models;

namespace OkulProjesi.Controllers
{
    public class OgretmenController : Controller
    {
        public IActionResult Index()
        {
            using (var ctx = new OkulDbContext())
            {
                var teachers = ctx.Ogretmenler.ToList();
                return View(teachers);
            }
        }

        [HttpGet]
        public IActionResult OgretmenEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OgretmenEkle(Ogretmen ogretmen)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Ogretmenler.Add(ogretmen);
                    ctx.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(ogretmen);
        }

        public IActionResult OgretmenDuzenle(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                var ogretmen = ctx.Ogretmenler.Find(id);
                if (ogretmen != null)
                {
                    return View(ogretmen);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult OgretmenDuzenle(Ogretmen ogretmen)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Entry(ogretmen).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    ctx.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(ogretmen);
        }

        public IActionResult OgretmenSil(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                var ogretmen = ctx.Ogretmenler.Find(id);
                if (ogretmen != null)
                {
                    ctx.Ogretmenler.Remove(ogretmen);
                    ctx.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}

