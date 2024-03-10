using BethanyPieShop.Models;

namespace WebApplication1.Models
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly BethanyPieShopDbContext _BethanyPieShopDbContext;

        public CategoryRepository(BethanyPieShopDbContext bethanysPieShopDbContext)
        {
            _BethanyPieShopDbContext = bethanysPieShopDbContext;
        }

        public IEnumerable<Category> AllCategories => _BethanyPieShopDbContext.Categories.OrderBy(p => p.CategoryName);
    }
}
