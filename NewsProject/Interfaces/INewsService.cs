using NewsProject.Models;

namespace NewsProject.Interfaces
{
    public interface INewsService
    {
        Task<(bool Success, IEnumerable<News>? News, string? ErrorMessage)> GetAllAsync(int skip, int take);
        Task<(bool Success, News? News, string? ErrorMessage)> GetByIdAsync(int id);
        Task<(bool Success, IEnumerable<News>? News, string? ErrorMessage)> GetLatestAsync(int count);
        Task<(bool Success, News? News, string? ErrorMessage)> CreateAsync(News news);
        Task<(bool Success, News? News, string? ErrorMessage)> UpdateAsync(int id, News news);
        Task<(bool Success, string? ErrorMessage)> DeleteAsync(int id);
    }
}


