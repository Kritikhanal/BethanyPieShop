using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace BethanyPieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly BethanyPieShopDbContext _bethanyPieShopDbContext;

        public PieRepository(BethanyPieShopDbContext bethanyPieShopDbContext)
        {
            _bethanyPieShopDbContext = bethanyPieShopDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _bethanyPieShopDbContext.Pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _bethanyPieShopDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public void AddPie(Pie pie)
        {
            pie.CategoryId = 1;
            _bethanyPieShopDbContext.Pies.Add(pie);
            _bethanyPieShopDbContext.SaveChanges();
        }


        public Pie? GetPieById(int pieId)
        {
            return _bethanyPieShopDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            throw new NotImplementedException();
        }
    }
}