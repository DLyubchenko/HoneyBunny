using HoneyBunny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HoneyBunny.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<ViewResult> Index()
        {
            var categories = await _repository.CategoryListAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<PartialViewResult> AddCategory(int parentId)
        {
            await InitializeCategoryDropDown(parentId);

            return PartialView("_AddCategory", new Category { ParentId = parentId });
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(Category category)
        {
            await SetCategoryFullName(category);

            await _repository.InsertCategoryAsync(category);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> EditCategory(int id)
        {
            var category = await _repository.GetCategoryAsync(id);

            if (category == null)
                return HttpNotFound();

            await InitializeCategoryDropDown(category.ParentId);

            return PartialView("_EditCategory", category);
        }

        [HttpPost]
        public async Task<ActionResult> EditCategory(Category category)
        {
            await SetCategoryFullName(category);

            await _repository.UpdateCategoryAsync(category);

            return RedirectToAction("Index");
        }


        private async Task InitializeCategoryDropDown(int defaultSelectedCategoryId)
        {
            var categories = new Dictionary<int, string> { { 0, "(Пусто)" } };
            var list = await _repository.CategoryListAsync();

            foreach (var d in list)
            {
                categories.Add(d.Id, d.FullName);
            }

            ViewBag.AllCategories = new SelectList(categories, "Key", "Value", categories[defaultSelectedCategoryId]);
        }

        private async Task SetCategoryFullName(Category category)
        {
            if (category.ParentId != 0)
            {
                var parentCategory = await _repository.GetCategoryAsync(category.ParentId);
                category.FullName = string.Concat(parentCategory.FullName, "/", category.Name);
            }
            else
            {
                category.FullName = category.Name;
            }
        }

        //public async Task<ActionResult> Delete(string lastName)
        //{
        //    Workers worker = await Task<Workers>.Factory.StartNew(() =>
        //    {
        //        return db.Workers.FirstOrDefault(e => e.Last_Name == lastName);
        //    });

        //    if (worker == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    await Task<Workers>.Factory.StartNew(() =>
        //    {
        //        return db.Workers.Remove(worker);
        //    });
        //    await db.SaveChangesAsync();

        //    return RedirectToAction("Employees");
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}