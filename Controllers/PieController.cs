using BethanyPieShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.ViewModels;
namespace WebApplication1.Controllers
{
    public class PieController : Controller
    {
        public IActionResult Index() {
            ViewBag.CurrentCategory = "Cheese cakes";

            var pies = _pieRepository.AllPies;
            var viewModel = new PieListViewModel(pies, ViewBag.CurrentCategory);

            return View(viewModel);
        }
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly BethanyPieShopDbContext dbContext;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository,BethanyPieShopDbContext dbContext)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
            this.dbContext = dbContext;
        }

        public IActionResult List()
        {

            PieListViewModel piesListViewModel = new PieListViewModel(_pieRepository.AllPies, "Cheese cakes");
            return View(piesListViewModel);
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();

            return View(pie);
        }


        public ActionResult AddPie()
        {

            var viewModel = new AddPieViewModel();

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPie(AddPieViewModel model)
        {

            Pie newPie = new Pie
            {
                Name = model.Name,
                Price = model.Price,
                ShortDescription = model.ShortDescription,
                LongDescription = model.LongDescription,
                CategoryId = model.CategoryId,
                //Category = category,
                InStock = model.InStock,
                ImageUrl = model.ImageUrl,
                ImageThumbnailUrl = model.ImageThumbnailUrl,
                IsPieOfTheWeek = model.IsPieOfTheWeek
            };
            _pieRepository.AddPie(newPie);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Update(int id)
        {
            var pie = await dbContext.Pies.FindAsync(id);
            return View(pie);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Pie viewModel)
        {
            var pie = await dbContext.Pies.FindAsync(viewModel.PieId);
            if (pie == null)

            {
                return NotFound();
            }
            pie.Name = viewModel.Name;
            pie.ShortDescription = viewModel.ShortDescription;
            pie.LongDescription = viewModel.LongDescription;
            pie.ImageThumbnailUrl = viewModel.ImageThumbnailUrl;
            pie.ImageUrl = viewModel.ImageUrl;

            await dbContext.SaveChangesAsync();



            return RedirectToAction("List", "Pie");
        }
        public async Task<IActionResult> Delete(int id)
        {
           
            var pie = await dbContext.Pies.FindAsync(id);
            if (pie == null)
            {
                return NotFound();

            }
            dbContext.Pies.Remove(pie);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Pie");
        }
    }

}
