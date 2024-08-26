
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity;
using AplicacionWebNomina.Models;
using AplicacionWebNomina.Data;

namespace AplicacionWebNomina.Controllers
{
    public class OrdenController : Controller
    {
        private ApplicationDContext db = new ApplicationDContext();

        // GET: Registrar Orden
        public ActionResult RegistrarOrden()
        {
            ViewBag.Clientes = ObtenerClientes();
            ViewBag.Tiendas = ObtenerTiendas();
            ViewBag.Empleados = ObtenerEmpleados();
            return View();
        }

        // POST: Registrar Orden
        [HttpPost]
        public ActionResult RegistrarOrden(OrdenCompra model, string action)
        {
            if (action == "Cancelar")
            {
                // Refrescar la misma página
                return RedirectToAction("RegistrarOrden");
            }

            if (ModelState.IsValid)
            {
                // Crear la orden
                var orden = new OrdenCompra
                {
                    ClienteId = model.ClienteId,
                    TiendaId = model.TiendaId,
                    EmpleadoId = model.EmpleadoId,
                    FechaOrden = DateTime.Now
                };

                db.Orders.Add(orden);
                db.SaveChanges();

                foreach (var detalle in model.DetalleOrden)
                {
                    var producto = db.Products.SingleOrDefault(p => p.Codigo == detalle.CodigoProducto);

                    if (producto != null)
                    {
                        if (producto.Stock >= detalle.Cantidad)
                        {
                            // Agregar detalles de la orden
                            var detalleOrden = new DetalleOrdenCompra
                            {
                                OrdenCompraId = orden.OrdenCompraId, // Usa la propiedad correcta
                                CodigoProducto = detalle.CodigoProducto,
                                Cantidad = detalle.Cantidad,
                                Descuento = detalle.Descuento,
                                Precio = producto.Precio
                            };

                            db.DetalleOrdenCompra.Add(detalleOrden);

                            // Actualizar stock del producto
                            producto.Stock -= detalle.Cantidad;
                            db.Entry(producto).State = EntityState.Modified;
                        }
                        else
                        {
                            ViewBag.ErrorMessage = $"No hay stock suficiente para el producto {detalle.CodigoProducto}";
                            ViewBag.Clientes = ObtenerClientes();
                            ViewBag.Tiendas = ObtenerTiendas();
                            ViewBag.Empleados = ObtenerEmpleados();
                            return View(model);
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = $"Producto con código {detalle.CodigoProducto} no encontrado";
                        ViewBag.Clientes = ObtenerClientes();
                        ViewBag.Tiendas = ObtenerTiendas();
                        ViewBag.Empleados = ObtenerEmpleados();
                        return View(model);
                    }
                }

                db.SaveChanges();

                ViewBag.Mensaje = "Orden registrada con éxito.";
                return RedirectToAction("RegistrarOrden");
            }

            ViewBag.Clientes = ObtenerClientes();
            ViewBag.Tiendas = ObtenerTiendas();
            ViewBag.Empleados = ObtenerEmpleados();
            return View(model);
        }

        private List<SelectListItem> ObtenerClientes()
        {
            return db.Users.Select(c => new SelectListItem
            {
                Value = c.UsuarioId.ToString(),
                Text = c.Username // Ajusta según el nombre del cliente en tu modelo
            }).ToList();
        }

        private List<SelectListItem> ObtenerTiendas()
        {
            return db.Stores.Select(t => new SelectListItem
            {
                Value = t.StoreId.ToString(),
                Text = t.StoreName
            }).ToList();
        }

        private List<SelectListItem> ObtenerEmpleados()
        {
            return db.Users.Select(e => new SelectListItem
            {
                Value = e.UsuarioId.ToString(),
                Text = e.Username // Ajusta según el nombre del empleado en tu modelo
            }).ToList();
        }
    }
}

