﻿using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace DiarioDigital.Controllers

{
    public class ArticuloController : Controller
    {
        private DiarioOnlineEntities db = new DiarioOnlineEntities();

        // GET: Articulo
        public async Task<ActionResult> Index()
        {

            var art = db.Articulo.Include(a => a.Categoria).Include(a => a.Comentarios);
            return View(await art.ToListAsync());
        }

        // GET: Articulo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulo articulo = await db.Articulo.FindAsync(id);
           

            if (articulo == null)
            {
                return HttpNotFound();
            }



          
            return View(articulo);
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
                articulo.Vista_previa = new byte[img.ContentLength];
                img.InputStream.Read(articulo.Vista_previa, 0, img.ContentLength);
                db.Articulo.Add(articulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            ViewBag.categoriaID = new SelectList(db.Categoria, "Idcategoria", "Nombre", articulo.categoriaID);
            return View(articulo);
        }

        // GET: Articulo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulo articulo = await db.Articulo.FindAsync(id);
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

                imageNactual = db.Articulo.SingleOrDefault(t => t.IdArticulo == articulo.IdArticulo).Vista_previa;

            }

            else
            {

                articulo.Vista_previa = new byte[img.ContentLength];
                img.InputStream.Read(articulo.Vista_previa, 0, img.ContentLength);

            }


            if (ModelState.IsValid)
            {
               
                var art = db.Articulo.Find(articulo.IdArticulo);
                db.Entry(art).CurrentValues.SetValues(articulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoriaID = new SelectList(db.Categoria, "Idcategoria", "Nombre", articulo.categoriaID);
            return View(articulo);
        }

        // GET: Articulo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulo articulo = await db.Articulo.FindAsync(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // POST: Articulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateInput(false)]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Articulo articulo = await db.Articulo.FindAsync(id);
            db.Articulo.Remove(articulo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult Sociedad()
        {

            var soc = db.Articulo.Where(t => t.Categoria.Nombre == "Sociedad");

            return View(soc);
        }
        /*
        public async Task<ActionResult> CienciaYtecnologia() { 
        
        }
        public async Task<ActionResult> Politica() { 
        
        }
        public async Task<ActionResult> Deportes() { 
        
        }
        public async Task<ActionResult> Internacional() {
        
        }
        public async Task<ActionResult> Cultura() { 
        
        }
        public async Task<ActionResult> Opinion() {
        
        }
        public async Task<ActionResult> Economia() { 
       
        }
        */


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
