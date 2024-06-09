using Microsoft.AspNetCore.Mvc;
using OkulProjesi.Connect;
using OkulProjesi.Models;
using System.Linq;
using System.Collections.Generic;

namespace OkulProjesi.Controllers
{
    public class OgrenciController : Controller
    {
        public IActionResult Index()
        {
            using (var ctx = new OkulDbContext())
            {
                var lst = ctx.Ogrenciler.ToList();
                return View(lst);
            }
        }

        [HttpGet]
        public IActionResult OgrenciEkle()
        {
            using (var ctx = new OkulDbContext())
            {
                ViewBag.Dersler = ctx.Dersler.ToList();
            }
            return View();
        }

        [HttpPost]
        public IActionResult OgrenciEkle(Ogrenci ogr, int[] selectedDersler)
        {
            if (ogr != null)
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Ogrenciler.Add(ogr);
                    ctx.SaveChanges();

                    foreach (var dersId in selectedDersler)
                    {
                        ctx.OgrenciDersler.Add(new OgrenciDers { OgrenciId = ogr.OgrenciId, DersId = dersId });
                    }
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult OgrenciDuzenle(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                var ogr = ctx.Ogrenciler.Find(id);
                return View(ogr);
            }
        }

        [HttpPost]
        public IActionResult OgrenciDuzenle(Ogrenci ogr)
        {
            if (ogr != null)
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Entry(ogr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteStudent(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                ctx.Ogrenciler.Remove(ctx.Ogrenciler.Find(id));
                ctx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult DersEkleSil(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                var ogr = ctx.Ogrenciler.Find(id);
                var mevcutDersler = ctx.Dersler.ToList();

                var alinanDersler = ctx.OgrenciDersler
                                        .Where(od => od.OgrenciId == id)
                                        .Select(od => od.Ders)
                                        .ToList();

                var model = new DersEkleSilViewModel
                {
                    Ogrenci = ogr,
                    MevcutDersler = mevcutDersler,
                    AlinanDersler = alinanDersler
                };

                return View(model);
            }
        }

        [HttpPost]
        public IActionResult DersEkle(int ogrenciId, int dersId)
        {
            using (var ctx = new OkulDbContext())
            {
                if (ctx.OgrenciDersler.Any(od => od.OgrenciId == ogrenciId && od.DersId == dersId))
                {
                    return RedirectToAction("DersEkleSil", new { id = ogrenciId });
                }

                var ogrenciDers = new OgrenciDers
                {
                    OgrenciId = ogrenciId,
                    DersId = dersId
                };
                ctx.OgrenciDersler.Add(ogrenciDers);
                ctx.SaveChanges();
                return RedirectToAction("DersEkleSil", new { id = ogrenciId });
            }
        }

        [HttpPost]
        public IActionResult DersSil(int ogrenciId, int dersId)
        {
            using (var ctx = new OkulDbContext())
            {
                var ogrenciDers = ctx.OgrenciDersler.FirstOrDefault(od => od.OgrenciId == ogrenciId && od.DersId == dersId);
                if (ogrenciDers != null)
                {
                    ctx.OgrenciDersler.Remove(ogrenciDers);
                    ctx.SaveChanges();
                }
                return RedirectToAction("DersEkleSil", new { id = ogrenciId });
            }
        }

        [HttpPost]
        public IActionResult KaydetDersler(int ogrenciId, List<int> alinanDersler)
        {
            using (var ctx = new OkulDbContext())
            {
                var mevcutDersler = ctx.OgrenciDersler.Where(od => od.OgrenciId == ogrenciId).ToList();

                foreach (var dersId in alinanDersler)
                {
                    if (!mevcutDersler.Any(md => md.DersId == dersId))
                    {
                        ctx.OgrenciDersler.Add(new OgrenciDers { OgrenciId = ogrenciId, DersId = dersId });
                    }
                }

                foreach (var mevcutDers in mevcutDersler)
                {
                    if (!alinanDersler.Contains(mevcutDers.DersId))
                    {
                        ctx.OgrenciDersler.Remove(mevcutDers);
                    }
                }

                ctx.SaveChanges();
                return RedirectToAction("DersEkleSil", new { id = ogrenciId });
            }
        }
    }
}
