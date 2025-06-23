using ControleFinanceiro.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ControleFinanceiro.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
