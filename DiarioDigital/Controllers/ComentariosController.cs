using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;



namespace DiarioDigital.Controllers
{
    public class ComentariosController : Controller
    {

        private DiarioOnlineEntities db = new DiarioOnlineEntities();

        // GET: Comentarios
        public ActionResult Index()
        {
            return View();
        }
         

        public ActionResult Coment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var articulo = db.Articulo.Find(id);

 
            ViewModel.ArticuloComentarioViewModel datos = new ViewModel.ArticuloComentarioViewModel
            {
                Articulo = articulo
                
            };

         return View(datos);

        }
          [Authorize]
          [HttpPost]
          public ActionResult Coment(ViewModel.ArticuloComentarioViewModel model)
        {
           
            var useractual = db.Usuarios.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).IdUser;
                
   
            var coment = new Comentarios();
            if (model != null)
            {
                coment.Idcoment = model.comentarios.Idcoment;
                coment.DateComent = DateTime.Now;
                coment.postID = model.Articulo.IdArticulo;
                coment.userID = useractual;           
                coment.comentario = model.comentarios.comentario;
                db.Comentarios.Add(coment);
                db.SaveChanges();

                return RedirectToAction("Index", "Articulo");
            }
                     
              return View(coment);
        }

        public ActionResult Delete(int? id)
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

        // POST: Articulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateInput(false)]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentarios comentarios = db.Comentarios.Find(id);
            db.Comentarios.Remove(comentarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        public ActionResult MostrarComent(int? id)        
        {

            var mostrar = db.Comentarios.Include("Articulo").Where(x => x.postID == id);

            return View(mostrar);
        }
    }
}