
using Conso_API_Gami_ASP.DAL.Entities;
using Conso_API_Gami_ASP.DAL.Interfaces;
using Demo_ASP_MVC_Modele.WebApp.Infrastructure;
using Demo_ASP_MVC_Modele.WebApp.Models;
using Demo_ASP_MVC_Modele.WebApp.Models.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Demo_ASP_MVC_Modele.WebApp.Controllers
{
    public class MemberController : Controller
    {
        private IMemberRepository _service;
        private SessionManager _session;
        private IGameRepository _gameService;

        public MemberController(IMemberRepository service, SessionManager session, IGameRepository gameService)
        {
            _service = service;
            _session = session;
            _gameService = gameService;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register([FromForm] MemberRegForm form)
        {
            if (!ModelState.IsValid) return View(form);
            try
            {
                //todo autoconnect
                _service.Insert(form.ToBll());
                return RedirectToAction("Index", "Game");
            }
            catch (Exception e)
            {
                ViewData["error"] = e.Message;
                return View(form);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(MemberLoginForm form)
        {
            if (!ModelState.IsValid) return View(form);

            try
            {
                string token = _service.Login(form.Pseudo, form.Pwd);

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

                JwtSecurityToken jwt = handler.ReadJwtToken(token);

                string ids= jwt.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
                int id = int.Parse(ids);

                MemberDetail currentMember = _service.GetById(id, token);
                currentMember.Token = token;
                _session.CurrentUser = currentMember;

                return RedirectToAction("Index", "Game");
            }
            catch (Exception e)
            {
                ViewData["error"] = e.Message;
                return View(form);
            }


        }

        public IActionResult Logout()
        {
            _session.Logout();
            return RedirectToAction("Index", "Home");
        }

        [AuthRequired]
        public IActionResult Profil()
        {
            MemberDetail model = _session.CurrentUser;

            return View(model);
        }

    }
}
