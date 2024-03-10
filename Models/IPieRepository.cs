namespace WebApplication1.Models
{
    public interface IPieRepository
    {
        IEnumerable<Pie> AllPies { get; }
        IEnumerable<Pie> PiesOfTheWeek { get; }

        void AddPie(Pie newPie);
        Pie? GetPieById(int pieId);
        IEnumerable<Pie> SearchPies(string searchQuery);
    }
}
