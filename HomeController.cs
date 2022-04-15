using coretest.DBdata;
using coretest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace coretest.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {

            mytestContext database = new mytestContext();
            List<MyModel> mm = new List<MyModel>();
            var res = database.Mytables.ToList();

            foreach (var item in res)
            {
                mm.Add(new MyModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    City = item.City,
                });


            }
                    return View(mm);
        }
            [HttpGet]
            public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Add(MyModel obj)
        {

            mytestContext database = new mytestContext();
          //  MyModel mm = new MyModel();
            Mytable tt = new Mytable();
            tt.Name= obj.Name;
            tt.Email=obj.Email;
            tt.City = obj.City;

            if (obj.Id == 0)
            {
                database.Mytables.Add(tt);
                database.SaveChanges();
            }
            else
            {
                database.Entry(tt).State= Microsoft.EntityFrameworkCore.EntityState.Modified;
                database.SaveChanges();
            }

            return RedirectToAction("Home", "Index");
        }

        public IActionResult Del(int id)
        {
            mytestContext database = new mytestContext();
          //Mytable tt = new Mytable();

            var de = database.Mytables.Where(i => i.Id == id).FirstOrDefault();
            database.Mytables.Remove(de);
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            mytestContext database = new mytestContext();
            Mytable tt = new Mytable();
          //  MyModel mm = new MyModel();
            var editing = database.Mytables.Where(e => e.Id == id).FirstOrDefault();
            tt.Name = editing.Name;
            tt.Email = editing.Email;
            tt.City=editing.City;
          
            return View("Add",editing);
        }

    }
}