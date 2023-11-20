using Microsoft.AspNetCore.Mvc;
using System;
using TestingApp.Data;

namespace TestingApp.Controllers
{
    public class PeopleController : Controller
    {
        private readonly TestingAppDbContext _context;

        public PeopleController(TestingAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var people = _context.People.ToList();
            return View(people);
        }
    }

}
