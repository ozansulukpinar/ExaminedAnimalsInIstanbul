using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExaminedAnimalsInIstanbul.Models;

namespace ExaminedAnimalsInIstanbul.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAnimalService _animalService;

        public HomeController(ILogger<HomeController> logger, IAnimalService animalService)
        {
            _logger = logger;
            _animalService = animalService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.SpeciesName = _animalService.GetDistinctSpeciesNames();

            ViewBag.MainColour = _animalService.GetDistinctMainColours();

            ViewBag.Gender = _animalService.GetDistinctGenders();
            
            TempData["Animals"] = _animalService.GetExaminedAnimals();            

            return View();
        }

        [HttpPost]
        public IActionResult Index(SearchViewModel model)
        {
            ViewBag.SpeciesName = _animalService.GetDistinctSpeciesNames();

            ViewBag.MainColour = _animalService.GetDistinctMainColours();

            ViewBag.Gender = _animalService.GetDistinctGenders();

            if(ModelState.IsValid)            
                TempData["SearchedAnimals"] = _animalService.GetSearchedExaminedAnimals(model.SpeciesName, model.MainColour, model.Gender);            

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}