using CibertecDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CibertecDemo.Controllers
{
    public class AccountController : Controller
    {
        //GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //POST: /Account/Register
        [HttpPost]
        public IActionResult Register(UsuarioModel model) 
        {
            if (!ModelState.IsValid) 
            {
                //Si la validación falla vuelve a mostrar el formulario pero con errores
                return View(model);
            }
            //Simulación de registro de usuario exitoso
            TempData["Message"]=$"Usuario {model.Nombres} registrado correctamente";
            //Redirección
            return RedirectToAction("Index","Home");
        }
    }
}
