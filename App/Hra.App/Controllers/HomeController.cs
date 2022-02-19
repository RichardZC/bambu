using Hra.App.Models;
using Hra.Infraestructure.Data;
using Hra.Transversal.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Hra.App.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly BAMBUContext context;

        public HomeController(BAMBUContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.grupo = await context.Grupo.CountAsync();
            ViewBag.Miembro = await context.Persona.CountAsync(x => x.GrupoId != null);
            ViewBag.Pendiente = await context.Persona.CountAsync(x => x.EstadoId == Constante.EstadoPersona.Pendiente);
            ViewBag.EnProceso = await context.Persona.CountAsync(x => x.EstadoId == Constante.EstadoPersona.EnProceso);
            ViewBag.Graduado = await context.Persona.CountAsync(x => x.EstadoId == Constante.EstadoPersona.Graduado);
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}