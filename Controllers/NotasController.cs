using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Notas.Data;
using Notas.Models;
using System.Globalization;
using CsvHelper;
using System.IO;

public class NotasController : Controller
{
    private readonly ApplicationDbContext _context;

    public NotasController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var calificaciones = _context.Notas
            .Include(c => c.Estudiante)
            .Include(c => c.Materia);
        return View(await calificaciones.ToListAsync());
    }

    public IActionResult Create()
    {
        ViewData["EstudianteID"] = new SelectList(_context.Estudiantes, "EstudianteID", "Nombre");
        ViewData["MateriaID"] = new SelectList(_context.Materias, "MateriaID", "Nombre");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Nota nota) // Variable renombrada a 'nota'
    {
        if (ModelState.IsValid)
        {
            _context.Add(nota); // Usando 'nota'
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["EstudianteID"] = new SelectList(_context.Estudiantes, "EstudianteID", "Nombre", nota.EstudianteID);
        ViewData["MateriaID"] = new SelectList(_context.Materias, "MateriaID", "Nombre", nota.MateriaID);
        return View(nota);
    }

    public async Task<IActionResult> Editar(int? id)
    {
        if (id == null) return NotFound();

        var calificacion = await _context.Notas.FindAsync(id);
        if (calificacion == null) return NotFound();

        ViewData["EstudianteID"] = new SelectList(_context.Estudiantes, "EstudianteID", "Nombre", calificacion.EstudianteID);
        ViewData["MateriaID"] = new SelectList(_context.Materias, "MateriaID", "Nombre", calificacion.MateriaID);
        return View(calificacion);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Nota nota) // Variable renombrada a 'nota'
    {
        if (id != nota.NotasID) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(nota); // Usando 'nota'
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(nota);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var calificacion = await _context.Notas
            .Include(c => c.Estudiante)
            .Include(c => c.Materia)
            .FirstOrDefaultAsync(m => m.NotasID == id);

        if (calificacion == null) return NotFound();

        return View(calificacion);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var calificacion = await _context.Notas.FindAsync(id);
        _context.Notas.Remove(calificacion);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var calificacion = await _context.Notas
            .Include(c => c.Estudiante)
            .Include(c => c.Materia)
            .FirstOrDefaultAsync(m => m.NotasID == id);

        if (calificacion == null) return NotFound();

        return View(calificacion);
    }

    public async Task<IActionResult> ExportarCSV()
    {
        var calificaciones = await _context.Notas
            .Include(c => c.Estudiante)
            .Include(c => c.Materia)
            .ToListAsync();

        using var ms = new MemoryStream();
        using var writer = new StreamWriter(ms);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        csv.WriteRecords(calificaciones);
        writer.Flush();
        ms.Position = 0;

        return File(ms, "text/csv", "calificaciones.csv");
    }
}