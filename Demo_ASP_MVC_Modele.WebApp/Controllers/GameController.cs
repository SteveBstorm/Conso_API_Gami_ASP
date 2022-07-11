﻿using Conso_API_Gami_ASP.DAL.Entities;
using Conso_API_Gami_ASP.DAL.Interfaces;
using Demo_ASP_MVC_Modele.WebApp.Infrastructure;
using Demo_ASP_MVC_Modele.WebApp.Models;
using Demo_ASP_MVC_Modele.WebApp.Models.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Demo_ASP_MVC_Modele.WebApp.Controllers
{
    public class GameController : Controller
    {
        private IGameRepository _service;
        private SessionManager _session;

        public GameController(IGameRepository service, SessionManager session)
        {
            _service = service;
            _session = session;
        }

        public IActionResult Index()
        {
            return View(_service.GetAll().ToViewModel());
        }

        [AuthRequired]
        public IActionResult Add()
        {
            return View(new GameForm());
        }
        [AuthRequired]
        [HttpPost]
        public IActionResult Add([FromForm] GameForm gameForm)
        {
            if (gameForm.NbPlayerMin > gameForm.NbPlayerMax)
            {
                ModelState.AddModelError("NbPlayerMax", "Le nombre de joueur Maximum doit être superieur ou égale au nombre de joueur minmum");
            }

            if (!ModelState.IsValid)
            {
                return View(gameForm);
            }

            _service.Insert(gameForm.ToModel(), _session.CurrentUser.Token);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details([FromRoute] int id)
        {
            try
            {
                Game game = _service.GetById(id).ToViewModel();
                return View(game);
            }
            catch (ArgumentNullException e)
            {
                TempData["ErrorMessage"] = "Le jeu n'a pas été trouvé !!!";
                return RedirectToAction(nameof(Index));
            }
        }
        [AuthRequired]
        public IActionResult Delete([FromRoute] int id)
        {
            _service.Delete(id, _session.CurrentUser.Token);
            return RedirectToAction(nameof(Index));
        }
        [AuthRequired]
        public IActionResult Update([FromRoute]int id)
        {
            try
            {
                GameEntity gameModel = _service.GetById(id);

                GameForm gameForm = new GameForm()
                {
                    Name = gameModel.Name,
                    Age = gameModel.Age,
                    Description = gameModel.Description,
                    NbPlayerMin = gameModel.NbPlayerMin,
                    NbPlayerMax = gameModel.NbPlayerMax,
                    IsCoop = gameModel.IsCoop
                };

                return View(gameForm);
            }
            catch (ArgumentNullException e)
            {
                TempData["ErrorMessage"] = "Le jeu n'a pas été trouvé !!!";
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public IActionResult Update([FromRoute] int id, [FromForm]GameForm gameForm)
        {
            if (gameForm.NbPlayerMin > gameForm.NbPlayerMax)
            {
                ModelState.AddModelError("NbPlayerMax", "Le nombre de joueur Maximum doit être superieur ou égale au nombre de joueur minmum");
            }

            if (!ModelState.IsValid)
            {
                return View(gameForm);
            }

            GameEntity data = gameForm.ToModel();
            data.Id = id;

            bool isOk = _service.Update(data, _session.CurrentUser.Token);

            if(isOk)
            {
                return RedirectToAction(nameof(Details), new { Id = id });
            }
            else
            {
                TempData["ErrorMessage"] = "Une erreur est survenu durant la mise à jour !!!";
                return RedirectToAction(nameof(Index));
            }
        }

        [AuthRequired]
        public IActionResult AddFav()
        {
            FavoriteForm form = new FavoriteForm();
            form.GameList = _service.GetAll().Select(x => x.ToViewModel());

            return View(form);
        }

        [HttpPost]
        public IActionResult AddFav(FavoriteForm form)
        {
            try
            {
                _service.AddFavoriteGame(_session.CurrentUser.Id, form.SelectedId, _session.CurrentUser.Token);

                return RedirectToAction("Profil", "Member");
            }
            catch(Exception e)
            {
                ViewData["error"] = e.Message;
                form.GameList = _service.GetAll().Select(x => x.ToViewModel());
                return View(form);
            }
            
        }

    }
}
