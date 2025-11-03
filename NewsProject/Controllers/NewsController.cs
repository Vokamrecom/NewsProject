using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsProject.Interfaces;
using NewsProject.Models;

namespace NewsProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        // GET: api/News
        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> GetNews(int skip = 0, int take = 10)
        {
            var (success, news, errorMessage) = await _newsService.GetAllAsync(skip, take);
            
            if (!success)
            {
                return BadRequest(new { message = errorMessage });
            }

            return Ok(news);
        }

        // GET: api/News/5
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNews(int id)
        {
            var (success, news, errorMessage) = await _newsService.GetByIdAsync(id);
            
            if (!success)
            {
                return NotFound(new { message = errorMessage });
            }

            return Ok(news);
        }

        // GET: api/News/latest
        [HttpGet("latest")]
        public async Task<ActionResult<IEnumerable<News>>> GetLatestNews(int count = 5)
        {
            var (success, news, errorMessage) = await _newsService.GetLatestAsync(count);
            
            if (!success)
            {
                return BadRequest(new { message = errorMessage });
            }

            return Ok(news);
        }

        // POST: api/News
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<News>> PostNews(News news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (success, createdNews, errorMessage) = await _newsService.CreateAsync(news);
            
            if (!success)
            {
                return BadRequest(new { message = errorMessage });
            }

            return CreatedAtAction(nameof(GetNews), new { id = createdNews!.Id }, createdNews);
        }

        // PUT: api/News/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutNews(int id, News news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (success, updatedNews, errorMessage) = await _newsService.UpdateAsync(id, news);
            
            if (!success)
            {
                return NotFound(new { message = errorMessage });
            }

            return NoContent();
        }

        // DELETE: api/News/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var (success, errorMessage) = await _newsService.DeleteAsync(id);
            
            if (!success)
            {
                return NotFound(new { message = errorMessage });
            }

            return NoContent();
        }
    }
}
