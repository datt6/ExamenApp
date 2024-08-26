using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AplicacionWebNomina.Data;
using AplicacionWebNomina.Models;

namespace AplicacionWebNomina.Controllers
{
    public class UsuariosController : Controller
    {
        private AplicacionWebNominaContext db = new AplicacionWebNominaContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.Usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,IdUsuario,nombre,apellido,correo,genero,fechaIngreso,clave,Codempleado")] User usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,IdUsuario,nombre,apellido,correo,genero,fechaIngreso,clave,Codempleado")] User usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Método para autenticación (Login)
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            // Simula una consulta a la base de datos para encontrar al usuario
            var user = db.Usuarios
                         .Where(u => u.Username == username && u.Pass == password)
                         .FirstOrDefault();

            if (user != null)
            {
                // Si el usuario existe y está activo, redirige al módulo de generación de órdenes de compra
                return RedirectToAction("GenerarOrdenCompra", "OrdenCompra");
            }
            else
            {
                // Si el usuario no existe, está inactivo o los datos son incorrectos, muestra un mensaje de error
                ViewBag.ErrorMessage = "Usuario y/o contraseña incorrecta";
                return View("Login");
            }
        }

        // Método para redirigir al registro de usuarios
        [HttpGet]
        public ActionResult Register()
        {
            // Redirige a la pantalla de registro de usuarios
            return RedirectToAction("Create", "Usuarios");
        }

        // Método para manejar el botón Cancelar
        public ActionResult Cancel()
        {
            // Redirige a la misma pantalla de autenticación o a la página de inicio
            return RedirectToAction("Login");
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