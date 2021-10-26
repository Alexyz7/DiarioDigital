using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiarioDigital.ViewModel;

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


        public ActionResult MostrarComent(int? id)
        {
           var info = db.Comentarios.Where(x => x.postID == id).Select(d => new ComentViewmodel
            {

                Idcoment = d.Idcoment,
                comentario = d.comentario,
                DateComent = d.DateComent,
                postID = d.postID,
                userID = d.userID,
                autor = d.Usuarios.Nombre

            }

             );

            return View(info.ToList());

        }






        // GET: 
        public ActionResult Coment(int? id)
        {

            var articulo = db.Articulo.Find(id);
           ComentViewmodel datoArticulo = new ComentViewmodel
            {

                postID = articulo.IdArticulo

            };

            return View(datoArticulo);
        }

        // POST: Comentarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Coment(ComentViewmodel coment, string principal)
        {
            var useractual = db.Usuarios.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).IdUser;

            var cm = new Comentarios();

            if (ModelState.IsValid)
            {
                cm.Idcoment = coment.Idcoment;
                cm.DateComent = DateTime.UtcNow;
                cm.comentario = coment.comentario;
                cm.userID = useractual;
                cm.postID = coment.postID;
                db.Comentarios.Add(cm);
                db.SaveChanges();
                return RedirectToAction("Details", "Articulo", new { id = cm.postID });
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

            var comentarios = db.Comentarios.Find(id);

            ComentViewmodel gg = new ComentViewmodel
            {
                Idcoment = comentarios.Idcoment,
                DateComent = comentarios.DateComent,
                postID = comentarios.postID,
                comentario = comentarios.comentario,
                userID = comentarios.userID
            };

            if (gg == null)
            {
                return HttpNotFound();
            }

            return View(gg);
        }

        // POST: Comentarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(ComentViewmodel cm, string identificacion)
        {
            Comentarios coment = new Comentarios();

            if (cm != null)
            {
                coment.Idcoment = cm.Idcoment;
                coment.comentario = cm.comentario;
                coment.userID = cm.userID;
                coment.DateComent = DateTime.Now;
                coment.postID = cm.postID;
                db.Entry(coment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Articulo", new { id = coment.postID });

            }

            return View();
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
        [ValidateInput(false)]
        public ActionResult BorrarComent(int id, string borrado)
        {
            Comentarios comentarios = db.Comentarios.Find(id);
            var postactual = db.Articulo.Where(x => x.IdArticulo == comentarios.postID).Select(x => x.IdArticulo).SingleOrDefault();
            db.Comentarios.Remove(comentarios);
            db.SaveChanges();
            return RedirectToAction("Details", "Articulo", new { id = postactual });
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
