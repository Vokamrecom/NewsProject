using NewsProject.Models;

namespace NewsProject.Interfaces
{
    public interface INewsRepository : IRepository<News>
    {
        Task<IEnumerable<News>> GetLatestAsync(int count);
    }
}


