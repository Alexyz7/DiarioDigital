using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiarioDigital;
using DiarioDigital.ViewModel;

namespace DiarioDigital.Controllers
{
    public class ArticuloController : Controller
    {
        private DiarioOnlineEntities db = new DiarioOnlineEntities();

        // GET: Articulo
        public ActionResult Index()
        {

        
            var articulo = db.Articulo.Include("Categoria").Select(x => new ArticuloViewmodel
            {
                IdArticulo = x.IdArticulo,
                Titulo = x.Titulo,
                Fecha = x.Fecha,
                Contenido = x.Contenido,
                Vista_previa = x.Vista_previa,
                categoriaID = x.categoriaID,
                categoriaNombre = db.Categoria.Where(e => e.Idcategoria == x.categoriaID).Select(f => f.Nombre).FirstOrDefault().ToString()
            });


          

            return View(articulo); 
        }

        // GET: Articulo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dato = db.Articulo.Find(id);

           
            var ss = new ArticuloViewmodel
            {
                IdArticulo = dato.IdArticulo,
                Titulo = dato.Titulo,
                Contenido = dato.Contenido,
                Vista_previa = dato.Vista_previa,
                categoriaID = dato.categoriaID,
                Fecha = dato.Fecha
            };

                    

            if (dato == null)
            {
                return HttpNotFound();
            }

          
      
            return View(ss);
        }





        // GET: Articulo/Create
        public ActionResult Create()
        {
            ViewBag.categoriaID = new SelectList(db.Categoria, "Idcategoria", "Nombre");
            return View();
        }

        // POST: Articulo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArticulo,Titulo,Fecha,Contenido,categoriaID")] Articulo articulo, HttpPostedFileBase img)
        {


            if (ModelState.IsValid)
            {
                articulo.Fecha = DateTime.UtcNow;
                articulo.Vista_previa = new byte[img.ContentLength];
                img.InputStream.Read(articulo.Vista_previa, 0, img.ContentLength);
                db.Articulo.Add(articulo);
                db.SaveChanges();
                return RedirectToAction("Details", "Articulo", new { id = articulo.IdArticulo });
            }



            ViewBag.categoriaID = new SelectList(db.Categoria, "Idcategoria", "Nombre", articulo.categoriaID);
            return View(articulo);
        }


      

        // GET: Articulo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulo articulo = db.Articulo.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoriaID = new SelectList(db.Categoria, "Idcategoria", "Nombre", articulo.categoriaID);
            return View(articulo);
        }

        // POST: Articulo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArticulo,Titulo,Fecha,Contenido,categoriaID")] Articulo articulo, HttpPostedFileBase img)
        {
            byte[] imageNactual = null;


            if (img == null)
            {

                imageNactual = db.Articulo.AsNoTracking().FirstOrDefault(t => t.IdArticulo == articulo.IdArticulo).Vista_previa;
                articulo.Vista_previa = imageNactual;
            }

            else
            {

                articulo.Vista_previa = new byte[img.ContentLength];
                img.InputStream.Read(articulo.Vista_previa, 0, img.ContentLength);

            }

            if (ModelState.IsValid)
            {

                db.Entry(articulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Articulo", new { id = articulo.IdArticulo });
            }
      
            return View(articulo);
        }


        // GET: Articulo/Delete/5
        public ActionResult BorrarArticulo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulo articulo = db.Articulo.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // POST: Articulo/Delete/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BorrarArticulo(int id)
        {
            Articulo articulo = db.Articulo.Find(id);
            db.Articulo.Remove(articulo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sociedad()
        {

            var soc = db.Articulo.Where(t => t.Categoria.Nombre == "Sociedad");

            return View(soc);
        }


        public ActionResult CienciaYtecnologia()
        {

            var soc = db.Articulo.Where(t => t.Categoria.Nombre == "Ciencia Y Tecnologia");

            return View(soc);

        }

        public ActionResult Economia()
        {

            var soc = db.Articulo.Where(t => t.Categoria.Nombre == "Economia");

            return View(soc);
        }

        public ActionResult Politica()
        {

            var soc = db.Articulo.Where(t => t.Categoria.Nombre == "Politica");

            return View(soc);
        }
        public ActionResult Deportes()
        {

            var soc = db.Articulo.Where(t => t.Categoria.Nombre == "Deportes");

            return View(soc);
        }
        public ActionResult Internacional()
        {

            var soc = db.Articulo.Where(t => t.Categoria.Nombre == "internacional");

            return View(soc);
        }
        public ActionResult Cultura()
        {

            var soc = db.Articulo.Where(t => t.Categoria.Nombre == "Cultura");

            return View(soc);
        }
        public ActionResult opinion()
        {

            var soc = db.Articulo.Where(t => t.Categoria.Nombre == "Opinion");

            return View(soc);
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
