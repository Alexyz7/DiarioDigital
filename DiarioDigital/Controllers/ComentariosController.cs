using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiarioDigital;

namespace DiarioDigital.Controllers
{
    public class ComentariosController : Controller
    {
        private DiarioOnlineEntities db = new DiarioOnlineEntities();

        // GET: Comentarios
        public ActionResult Index()
        {
            var comentarios = db.Comentarios.Include(c => c.Usuarios).Include(c => c.Articulo);
            return View(comentarios.ToList());
        }

        // GET: Comentarios/MostrarComent/
        public ActionResult MostrarComent(int? id)
        {


            var mostcoment = db.Comentarios.Include("Articulo").Where(x => x.postID == id);


            return View(mostcoment);
        }

        // GET: Comentarios/Create
        public ActionResult Coment(int? id)
        {

            var articulo = db.Articulo.Find(id);

            ViewModel.ArticuloComentarioViewModel datoArticulo = new ViewModel.ArticuloComentarioViewModel
            {

                Articulo = articulo

            };

            return View(datoArticulo);
        }

        // POST: Comentarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Coment(ViewModel.ArticuloComentarioViewModel coment)
        {
            var useractual = db.Usuarios.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).IdUser;

            var cm = new Comentarios();

            if (coment != null)
            {
                cm.Idcoment = coment.comentarios.Idcoment;
                cm.DateComent = DateTime.UtcNow;
                cm.comentario = coment.comentarios.comentario;
                cm.userID = useractual;
                cm.postID = coment.Articulo.IdArticulo;
                db.Comentarios.Add(cm);
                db.SaveChanges();
                return RedirectToAction("Index", "Articulo");
            }


            return View(cm);
        }

        // GET: Comentarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentarios comentarios = db.Comentarios.Find(id);
            if (comentarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.Usuarios, "IdUser", "Email", comentarios.userID);
            ViewBag.postID = new SelectList(db.Articulo, "IdArticulo", "Titulo", comentarios.postID);
            return View(comentarios);
        }

        // POST: Comentarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Idcoment,comentario,DateComent,postID,userID")] Comentarios comentarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
     
            ViewBag.postID = new SelectList(db.Articulo, "IdArticulo", "Titulo", comentarios.postID);
            return View(comentarios);
        }

        // GET: Comentarios/Delete/5
        public ActionResult BorrarComent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentarios comentarios = db.Comentarios.Find(id);
            if (comentarios == null)
            {
                return HttpNotFound();
            }
            return View(comentarios);
        }

        // POST: Comentarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BorrarComent(int id)
        {
            Comentarios comentarios = db.Comentarios.Find(id);
            db.Comentarios.Remove(comentarios);
            db.SaveChanges();
            return RedirectToAction("Index","Articulo");
        }





        public ActionResult Subcoment(int? id)
        {

            Comentarios cm = db.Comentarios.Find(id);


            ViewModel.ArticuloComentarioViewModel dato = new ViewModel.ArticuloComentarioViewModel
            {

                comentarios = cm

            };


            return View(dato);

        }


        [HttpPost]
        public ActionResult Subcoment(ViewModel.ArticuloComentarioViewModel sub)
        {
            var useractual = db.Usuarios.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).IdUser;


            var sub1 = new Subcomentarios();
            if (sub != null)
            {
                sub1.IdSubcoment = sub.subcomentario.IdSubcoment;
                sub1.fechaSub = DateTime.UtcNow;
                sub1.Usuario_id = useractual;
                sub1.ComID = sub.comentarios.Idcoment;
                sub1.subComentario = sub.subcomentario.subComentario;
                db.Subcomentarios.Add(sub1);
                db.SaveChanges();
                return RedirectToAction("index", "Articulo");
            }

            return View(sub);

        }



        public ActionResult Mostrarsub(int? id)
        {

            var mostcoment = db.Subcomentarios.Include("Comentarios").Where(x => x.ComID == id);


            return View(mostcoment);
        }






        public ActionResult Borrarsub(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcomentarios subcoment = db.Subcomentarios.Find(id);
            if (subcoment == null)
            {
                return HttpNotFound();
            }
            return View(subcoment);
        }

        // POST: Comentarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Borrarsub(int id)
        {
            Subcomentarios subcoment = db.Subcomentarios.Find(id);
            db.Subcomentarios.Remove(subcoment);
            db.SaveChanges();
            return RedirectToAction("Index", "Articulo");
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
