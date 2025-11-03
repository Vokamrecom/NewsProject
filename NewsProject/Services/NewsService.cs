using NewsProject.Interfaces;
using NewsProject.Models;

namespace NewsProject.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _repository;

        public NewsService(INewsRepository repository)
        {
            _repository = repository;
        }

        public async Task<(bool Success, IEnumerable<News>? News, string? ErrorMessage)> GetAllAsync(int skip, int take)
        {
            try
            {
                if (skip < 0 || take <= 0 || take > 100)
                {
                    return (false, null, "Неверные параметры пагинации");
                }

                var news = await _repository.GetAllAsync(skip, take);
                return (true, news, null);
            }
            catch (Exception ex)
            {
                return (false, null, $"Ошибка при получении новостей: {ex.Message}");
            }
        }

        public async Task<(bool Success, News? News, string? ErrorMessage)> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return (false, null, "Неверный ID новости");
                }

                var news = await _repository.GetByIdAsync(id);
                if (news == null)
                {
                    return (false, null, "Новость не найдена");
                }

                return (true, news, null);
            }
            catch (Exception ex)
            {
                return (false, null, $"Ошибка при получении новости: {ex.Message}");
            }
        }

        public async Task<(bool Success, IEnumerable<News>? News, string? ErrorMessage)> GetLatestAsync(int count)
        {
            try
            {
                if (count <= 0 || count > 100)
                {
                    return (false, null, "Количество новостей должно быть от 1 до 100");
                }

                var news = await _repository.GetLatestAsync(count);
                return (true, news, null);
            }
            catch (Exception ex)
            {
                return (false, null, $"Ошибка при получении последних новостей: {ex.Message}");
            }
        }

        public async Task<(bool Success, News? News, string? ErrorMessage)> CreateAsync(News news)
        {
            try
            {
                if (news == null)
                {
                    return (false, null, "Новость не может быть null");
                }

                var createdNews = await _repository.CreateAsync(news);
                return (true, createdNews, null);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner: {ex.InnerException.Message}";
                }
                return (false, null, $"Ошибка при создании новости: {errorMessage}");
            }
        }

        public async Task<(bool Success, News? News, string? ErrorMessage)> UpdateAsync(int id, News news)
        {
            try
            {
                if (id <= 0)
                {
                    return (false, null, "Неверный ID новости");
                }

                if (news == null)
                {
                    return (false, null, "Новость не может быть null");
                }

                var updatedNews = await _repository.UpdateAsync(id, news);
                if (updatedNews == null)
                {
                    return (false, null, "Новость не найдена");
                }

                return (true, updatedNews, null);
            }
            catch (Exception ex)
            {
                return (false, null, $"Ошибка при обновлении новости: {ex.Message}");
            }
        }

        public async Task<(bool Success, string? ErrorMessage)> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return (false, "Неверный ID новости");
                }

                var result = await _repository.DeleteAsync(id);
                if (!result)
                {
                    return (false, "Новость не найдена");
                }

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Ошибка при удалении новости: {ex.Message}");
            }
        }
    }
}


