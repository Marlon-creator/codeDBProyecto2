#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseDatosTarea1.Models;
using Microsoft.Data.SqlClient;

namespace BaseDatosTarea1.Controllers
{
    public class PuestoesPruebaController : Controller
    {
        private readonly BaseDatosTareaContext _context;

        public PuestoesPruebaController(BaseDatosTareaContext context)
        {
            _context = context;
        }

        // GET: PuestoesPrueba
        public IActionResult Index()
        {
            List<Puesto> puestos = this._context.GetPuestos().ToList();
            return View(puestos);
        }


        // GET: PuestoesPrueba/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PuestoesPrueba/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nombre,SalarioXhora,Activo")] Puesto puesto)
        {
            if (PuestoExistsNombre(puesto.Nombre))
            {
                ViewBag.Name = "Puesto Ya existe";
                return View(puesto);    
             
            }
            else
            {
                SqlParameter inName = new ("@inNombre", puesto.Nombre);
                SqlParameter inSalario = new ("@inSalario", puesto.SalarioXhora);
                _context.Database.ExecuteSqlRaw("EXECUTE dbo.AgregarPuesto @inNombre,@inSalario", inName, inSalario);
                return RedirectToAction(nameof(Index));
            }


        }

        // GET: PuestoesPrueba/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var puesto = await _context.Puestos.FindAsync(id);
            if (puesto == null)
            {
                return NotFound();
            }
            return View(puesto);
        }

        // POST: PuestoesPrueba/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre,SalarioXhora,Activo")] Puesto puesto)
        {
            SqlParameter inId = new ("@inId", id);
            SqlParameter inName = new ("@inNombre", puesto.Nombre);
            SqlParameter inSalario = new ("@inSalario", puesto.SalarioXhora);
            SqlParameter inActivo = new ("@inActivo", puesto.Activo);
            _context.Database.ExecuteSqlRaw("EXECUTE dbo.EditarPuesto @inId,@inNombre,@inSalario,@inActivo", inId,inName,inSalario,inActivo);

            return RedirectToAction(nameof(Index));
        }

        // GET: PuestoesPrueba/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puesto = await _context.Puestos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (puesto == null)
            {
                return NotFound();
            }

            return View(puesto);
        }

        // POST: PuestoesPrueba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            SqlParameter pContactName = new ("@inId", id);
            _context.Database.ExecuteSqlRaw("EXECUTE dbo.BorrarPuesto @inId", pContactName);
            return RedirectToAction(nameof(Index));
        }

        private bool PuestoExists(int id)
        {
            return _context.Puestos.Any(e => e.Id == id);
        }

        private bool PuestoExistsNombre(string nombre)
        {
            return _context.Puestos.Any(e => e.Nombre == nombre);
        }

    }
}
