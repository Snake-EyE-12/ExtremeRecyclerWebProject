using ExtremeRecycler.Data.DALs;
using ExtremeRecycler.Interfaces;
using ExtremeRecycler.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExtremeRecycler.Controllers
{
    public class DatabaseTestController : Controller
    {

        // Max-
        // I am using this controller to test database stuff so idk if will be used



        // I do not know how we will be getting the dal stuff so whatever, the example ways we did in class do not work with this templated interface
        DataAccessLayer<Item> dal;

        public DatabaseTestController(DataAccessLayer<Item> indal)
        {
            dal = indal;
        }

        // I can use this but I don't understand
        //ItemDataList itemDAL = new ItemDataList();

        [HttpGet]
        public IActionResult Add()
        {
            return View("AddItems");
        }

        [HttpPost]
        public IActionResult Add(Item i)
        {
            if (ModelState.IsValid)
            {
                dal.Add(i);
                return RedirectToAction("Items");
            }
            return View("AddItems");
        }

        public IActionResult Items()
        {
            return View(dal.GetAll());
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
