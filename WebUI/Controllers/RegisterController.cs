using System.Diagnostics;
using Core.Models.User;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers;

public class RegisterController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    public RegisterController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(RegisterViewModel model)
    {
        if(ModelState.IsValid)
        {
            User user = new User { Email = model.Email, UserName = model.Email};
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return View(model);
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}