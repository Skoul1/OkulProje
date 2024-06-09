using Microsoft.AspNetCore.Mvc;
using OkulProjesi.Connect;
using OkulProjesi.Models;

namespace OkulProjesi.Controllers
{
    public class DersController : Controller
    {
        public IActionResult Index()
        {
            using (var ctx = new OkulDbContext())
            {
                var lst = ctx.Dersler.ToList();
                return View(lst);
            }
        }

        [HttpGet]
        public IActionResult DersEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DersEkle(Ders drs)
        {
            if (drs != null)
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Dersler.Add(drs);
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult DersDuzenle(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                var drs = ctx.Dersler.Find(id);
                return View(drs);
            }
        }

        [HttpPost]
        public IActionResult DersDuzenle(Ders drs)
        {
            if (drs != null)
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Entry(drs).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult DersSil(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                ctx.Dersler.Remove(ctx.Dersler.Find(id));
                ctx.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
