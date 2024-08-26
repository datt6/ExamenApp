using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using AplicacionWebNomina.Models;
using AplicacionWebNomina.Data;

namespace AplicacionWebNomina.Controllers
{
    public class AccesoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Autenticación
        public ActionResult Autenticar()
        {
            return View();
        }

        // POST: Autenticación
        [HttpPost]
        public ActionResult Autenticar(User oUsuario)
        {
            if (ModelState.IsValid)
            {
                var usuario = db.Users.SingleOrDefault(u => u.Username == oUsuario.Username && u.Pass == oUsuario.Pass);

                if (usuario != null)
                {
                    if (usuario.Activo)
                    {
                        // Autenticación exitosa, redirigir a la página de inicio
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Usuario inactivo
                        ViewBag.ErrorMessage = "Usuario inactivo. Por favor contacte al administrador.";
                    }
                }
                else
                {
                    // Usuario no encontrado o credenciales incorrectas
                    ViewBag.ErrorMessage = "Usuario y/o contraseña incorrecta";
                }
            }

            return View();
        }

        // GET: Registro
        public ActionResult Registrar()
        {
            ViewBag.Tiendas = ObtenerTiendas();
            return View();
        }

        // POST: Registro
        [HttpPost]
        public ActionResult Registrar(User oUsuario, string action)
        {
            if (action == "Cancelar")
            {
                // Redirigir a la página de autenticación
                return RedirectToAction("Autenticar");
            }

            if (ModelState.IsValid)
            {
                var usuarioExistente = db.Users.SingleOrDefault(u => u.UsuarioId == oUsuario.UsuarioId);

                if (usuarioExistente != null)
                {
                    // Actualizar el usuario existente
                    usuarioExistente.Username = oUsuario.Username;
                    usuarioExistente.CorreoElectronico= oUsuario.CorreoElectronico;
                    usuarioExistente.Pass = oUsuario.Pass;
                    usuarioExistente.StoreId = oUsuario.StoreId;
                    usuarioExistente.Activo = oUsuario.Activo;
                    db.Entry(usuarioExistente).State = EntityState.Modified;
                    db.SaveChanges();

                    ViewBag.Mensaje = "Información actualizada con éxito.";
                }
                else
                {
                    // Registrar nuevo usuario
                    db.Users.Add(oUsuario);
                    db.SaveChanges();

                    ViewBag.Mensaje = "Usuario registrado con éxito.";
                }

                return RedirectToAction("Autenticar");
            }

            ViewBag.Tiendas = ObtenerTiendas();
            return View(oUsuario);
        }

        private List<SelectListItem> ObtenerTiendas()
        {
            var tiendas = new List<SelectListItem>();

            try
            {
                using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString))
                {
                    string query = "SELECT StoreId, Nombre FROM Store";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            tiendas.Add(new SelectListItem
                            {
                                Value = dr["StoreId"].ToString(),
                                Text = dr["Nombre"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar el error adecuadamente (por ejemplo, registrar el error)
                Console.WriteLine(ex.Message);
                // También puedes asignar un mensaje de error a ViewBag si es necesario
            }

            return tiendas;
        }
    }
}

