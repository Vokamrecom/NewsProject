using Microsoft.EntityFrameworkCore;
using NewsProject.Data;
using NewsProject.Interfaces;
using NewsProject.Models;

namespace NewsProject.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> GetAllAsync(int skip = 0, int take = 10)
        {
            return await _context.News
                .OrderByDescending(n => n.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<News?> GetByIdAsync(int id)
        {
            return await _context.News.FindAsync(id);
        }

        public async Task<IEnumerable<News>> GetLatestAsync(int count)
        {
            return await _context.News
                .OrderByDescending(n => n.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<News> CreateAsync(News news)
        {
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return news;
        }

        public async Task<News?> UpdateAsync(int id, News news)
        {
            var existingNews = await _context.News.FindAsync(id);
            if (existingNews == null)
            {
                return null;
            }

            existingNews.Title = news.Title;
            existingNews.ImageUrl = news.ImageUrl;
            existingNews.Subtitle = news.Subtitle;
            existingNews.Content = news.Content;
            existingNews.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingNews;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return false;
            }

            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.News.AnyAsync(e => e.Id == id);
        }
    }
}


