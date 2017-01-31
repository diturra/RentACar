using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentaCar.Datos;
using RentaCar.Areas.Admin.Models;
using ImageProcessor.Imaging.Formats;
using ImageProcessor;
using System.Drawing;
using System.IO;

namespace RentaCar.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]

    public class VehiculosController : Controller
    {
        private RentaCarEntities db = new RentaCarEntities();

        // GET: Admin/Vehiculos
        public ActionResult Index()
        {
            var vehiculo = db.Vehiculo.Include(v => v.Categoria);
            return View(vehiculo.ToList());
        }

        // GET: Admin/Vehiculos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // GET: Admin/Vehiculos/Create
        public ActionResult Create()
        {
            ViewBag.categoriaID = new SelectList(db.Categoria, "id", "nombre");
            return View();
        }

        // POST: Admin/Vehiculos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(VehiculoModelo vehiculo,HttpPostedFileBase files)
        {

            if (ModelState.IsValid)
            {
                Vehiculo vehiculodb = new Vehiculo()
                {
                    abono = vehiculo.abono,
                    aire_acondicionado = vehiculo.aire_acondicionado,
                    capacidad = vehiculo.capacidad,
                    valor = vehiculo.valor,
                    categoriaID = vehiculo.categoriaID,
                    cierre_centralizado = vehiculo.cierre_centralizado,
                    disponible = vehiculo.disponible,
                    modelo = vehiculo.modelo,
                    puertas = vehiculo.puertas,
                    transmision = vehiculo.transmision,
                    url_foto = ""
                };

                db.Vehiculo.Add(vehiculodb);
                db.SaveChanges();



                //fotography
                ISupportedImageFormat format = new JpegFormat { Quality = 90 };
                Size size = new Size(650, 0);
                //https://naimhamadi.wordpress.com/2014/06/25/processing-images-in-c-easily-using-imageprocessor/
                // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                {
                    var path = Path.Combine(Server.MapPath("~/imagenes/Autos"), string.Format("{0}.{1}", vehiculodb.id.ToString("00000"), "jpg"));

                    // Load, resize, set the format and quality and save an image.
                    imageFactory.Load(files.InputStream)
                                .Resize(size)
                                .Format(format)
                                .Save(path);
                }
                vehiculodb.url_foto = string.Format("/imagenes/Autos/{0}.jpg", vehiculodb.id.ToString("00000"));
                Edit(vehiculodb);


                return RedirectToAction("Index");
            }

            ViewBag.categoriaID = new SelectList(db.Categoria, "id", "nombre", vehiculo.categoriaID);
            return View(vehiculo);
        }

        // GET: Admin/Vehiculos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoriaID = new SelectList(db.Categoria, "id", "nombre", vehiculo.categoriaID);
            return View(vehiculo);
        }

        // POST: Admin/Vehiculos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,categoriaID,modelo,capacidad,transmision,aire_acondicionado,cierre_centralizado,abono,valor,url_foto,disponible,puertas")] Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoriaID = new SelectList(db.Categoria, "id", "nombre", vehiculo.categoriaID);
            return View(vehiculo);
        }

        // GET: Admin/Vehiculos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // POST: Admin/Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            db.Vehiculo.Remove(vehiculo);
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
