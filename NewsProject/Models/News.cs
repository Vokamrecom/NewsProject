using System.ComponentModel.DataAnnotations;

namespace NewsProject.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заголовок обязателен")]
        [StringLength(200, ErrorMessage = "Заголовок не может быть длиннее 200 символов")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "URL изображения не может быть длиннее 500 символов")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Подзаголовок обязателен")]
        [StringLength(500, ErrorMessage = "Подзаголовок не может быть длиннее 500 символов")]
        public string Subtitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Текст новости обязателен")]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}


