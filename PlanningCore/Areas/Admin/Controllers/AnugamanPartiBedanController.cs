using Microsoft.AspNetCore.Mvc;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Data;

namespace PlanningCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AnugamanPartiBedanController : Controller
    {
        private readonly IAnugamanPartibedan _anugamanPartibedan;
        private readonly IPlanningSamjhauta _planningSamjhauta;

        public AnugamanPartiBedanController(IAnugamanPartibedan anugamanPartibedan, IPlanningSamjhauta planningSamjhauta)
        {
            _anugamanPartibedan = anugamanPartibedan;
            _planningSamjhauta = planningSamjhauta;
        }
        public async  Task<IActionResult> AnugamanPartibedan(int id)
        {
            var model = await _anugamanPartibedan.GetAnugamanPartiBedan(id)??new AnugamanPartibedanViewModel();
            model.PlanningSamjhauta = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            return View(model);
        }
        public async Task<IActionResult> CreateAnugamanPartibedan(int planningSamjhautaId)
        {
            var model = new AnugamanPartibedanViewModel();
            model.PlanningSamjhautaId = planningSamjhautaId;
            model.PlanningSamjhauta = await _planningSamjhauta.GetPlanningSamjhautaById(planningSamjhautaId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAnugamanPartibedan(AnugamanPartibedanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.PlanningSamjhauta = await _planningSamjhauta.GetPlanningSamjhautaById(model.PlanningSamjhautaId);
                TempData["error"] = "Please check the form for errors.";
                return View(model);
            }

            try
            {
                bool isSuccess;

                if (model.Id == 0)
                {
                    isSuccess = await _anugamanPartibedan.InsertAnugamanParitbedan(model);
                    TempData["success"] = "Anugaman Partibedan successfully created.";
                }
                else
                {
                    isSuccess = await _anugamanPartibedan.InsertAnugamanParitbedan(model);
                    TempData["success"] = "Anugaman Partibedan successfully updated.";
                }

                if (!isSuccess)
                {
                    TempData["error"] = "Something went wrong. Please try again.";
                    return View(model);
                }

                return RedirectToAction("PlanningsamjhautaIndex", "Planningsamjhauta", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Error: {ex.Message}";
                return View(model);
            }
        }

    }
}
