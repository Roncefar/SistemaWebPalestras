using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPalestras.Models;

namespace WebPalestras.Controllers
{
    public class PalestrasController : Controller
    {
        private palestrasEntities db = new palestrasEntities();

        // GET: Palestras
        public ActionResult Index()
        {
            var tB_PALESTRA = db.TB_PALESTRA.Include(p => p.TB_LOCAL);
            return View(tB_PALESTRA.ToList());
        }

        // GET: Palestras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palestra palestra = db.TB_PALESTRA.Find(id);
            if (palestra == null)
            {
                return HttpNotFound();
            }
            return View(palestra);
        }

        // GET: Palestras/Create
        public ActionResult Create()
        {
            ViewBag.id_local = new SelectList(db.TB_LOCAL, "id_local", "nome");
            return View();
        }

        // POST: Palestras/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_palestra,titulo,dt_inicio,duracao,id_local")] Palestra palestra)
        {
            if (ModelState.IsValid)
            {
                db.TB_PALESTRA.Add(palestra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_local = new SelectList(db.TB_LOCAL, "id_local", "nome", palestra.id_local);
            return View(palestra);
        }

        // GET: Palestras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palestra palestra = db.TB_PALESTRA.Find(id);
            if (palestra == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_local = new SelectList(db.TB_LOCAL, "id_local", "nome", palestra.id_local);
            return View(palestra);
        }

        // POST: Palestras/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_palestra,titulo,dt_inicio,duracao,id_local")] Palestra palestra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(palestra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_local = new SelectList(db.TB_LOCAL, "id_local", "nome", palestra.id_local);
            return View(palestra);
        }

        // GET: Palestras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palestra palestra = db.TB_PALESTRA.Find(id);
            if (palestra == null)
            {
                return HttpNotFound();
            }
            return View(palestra);
        }

        // POST: Palestras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Palestra palestra = db.TB_PALESTRA.Find(id);
            db.TB_PALESTRA.Remove(palestra);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
