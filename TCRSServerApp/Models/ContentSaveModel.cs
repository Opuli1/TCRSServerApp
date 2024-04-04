using TCRSServerApp.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace TCRSServerApp.Models
{
    public class ContentSaveModel
    {
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Title { get; set; }

        [Required, MaxLength(150)]
        public string Slug { get; set; }

        public int CategoryId { get; set; }

        public string? Content { get; set; }

        public bool IsPublished { get; set; }

        public ContentPost ToContentEntity(int userId) =>
            new()
            {
                Id = Id,
                Title = Title,
                Slug = Slug,
                CategoryId = CategoryId,
                Content = Content!,
                IsPublished = IsPublished,
                UserId = userId
            };

        public ContentPost Merge(ContentPost entity)
        {
            entity.Title = Title;
            entity.CategoryId = CategoryId;
            entity.Content = Content!;
            entity.IsPublished = IsPublished;
            return entity;
        }

        public static Expression<Func<ContentPost, ContentSaveModel>> Selector =>
            cp => new ContentSaveModel
            {
                Id = cp.Id,
                Title = cp.Title,
                Slug = cp.Slug,
                CategoryId = cp.CategoryId,
                Content = cp.Content,
                IsPublished = cp.IsPublished
            };
    }
}
