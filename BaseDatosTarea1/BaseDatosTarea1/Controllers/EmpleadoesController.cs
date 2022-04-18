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
    public class EmpleadoesController : Controller
    {
        private readonly BaseDatosTareaContext _context;

        public EmpleadoesController(BaseDatosTareaContext context)
        {
            _context = context;
        }

        // GET: Empleadoes
        public IActionResult Index()
        {
            List<Empleado> empleados = this._context.GetEmpleados().ToList();
            return View(empleados);

        }

        // GET: Empleadoes/Create
        public IActionResult Create()
        {
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre");
            ViewData["IdTipoDocuIdentidad"] = new SelectList(_context.TipoDocuIdentidads, "Id", "Nombre");
            ViewData["Puesto"] = new SelectList(_context.GetPuestos(), "Nombre", "Nombre");
            return View();
        }

        // POST: Empleadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdDepartamento,IdTipoDocuIdentidad,ValorDocuIdentidad,Puesto,Nombre,FechaNacimiento,Activo")] Empleado empleado)
        {
            SqlParameter inIdDepartamento = new("@inDepartamento", empleado.IdDepartamento);
            SqlParameter inIdTipoDocuIdentidad = new("@inTipoDocu", empleado.IdTipoDocuIdentidad);
            SqlParameter inValorDocuIdentidad = new("@inValorDocu", empleado.ValorDocuIdentidad);
            SqlParameter inPuesto = new("@inPuesto", empleado.Puesto);
            SqlParameter inNombre = new("@inNombre", empleado.Nombre);
            SqlParameter inFechaNacimiento = new("@inFecha", empleado.FechaNacimiento);
            _context.Database.ExecuteSqlRaw("EXECUTE dbo.AgregarEmpleado @inDepartamento,@inTipoDocu,@inValorDocu,@inPuesto,@inNombre,@inFecha"
                ,inIdDepartamento, inIdTipoDocuIdentidad, inValorDocuIdentidad, inPuesto, inNombre, inFechaNacimiento);
            return RedirectToAction(nameof(Index));
        }

        // GET: Empleadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre", empleado.IdDepartamento);
            ViewData["IdTipoDocuIdentidad"] = new SelectList(_context.TipoDocuIdentidads, "Id", "Nombre", empleado.IdTipoDocuIdentidad);
            ViewData["Puesto"] = new SelectList(_context.Puestos, "Nombre", "Nombre", empleado.Puesto);


            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,IdDepartamento,IdTipoDocuIdentidad,ValorDocuIdentidad,Puesto,Nombre,FechaNacimiento,Activo")] Empleado empleado)
        {
            SqlParameter inId = new("@inId", id);
            SqlParameter inIdDepartamento = new("@inDepartamento", empleado.IdDepartamento);
            SqlParameter inIdTipoDocuIdentidad = new("@inTipoDocu", empleado.IdTipoDocuIdentidad);
            SqlParameter inValorDocuIdentidad = new("@inValorDocu", empleado.ValorDocuIdentidad);
            SqlParameter inPuesto = new("@inPuesto", empleado.Puesto);
            SqlParameter inNombre = new("@inNombre", empleado.Nombre);
            SqlParameter inFechaNacimiento = new("@inFecha", empleado.FechaNacimiento);
            _context.Database.ExecuteSqlRaw("EXECUTE dbo.EditarEmpleado @inId,@inDepartamento,@inTipoDocu,@inValorDocu,@inPuesto,@inNombre,@inFecha",
                inId, inIdDepartamento, inIdTipoDocuIdentidad, inValorDocuIdentidad, inPuesto, inNombre, inFechaNacimiento);

            return RedirectToAction(nameof(Index));
        }

        // GET: Empleadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.IdDepartamentoNavigation)
                .Include(e => e.IdTipoDocuIdentidadNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            SqlParameter pContactName = new("@inId", id);
            _context.Database.ExecuteSqlRaw("EXECUTE dbo.BorrarEmpleado @inId", pContactName);
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }
    }
}
