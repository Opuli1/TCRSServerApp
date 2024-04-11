using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace TCRSServerApp.Services
{
    public class SearchService
    {
        private readonly TCRSContext _context;

        public SearchService(TCRSContext context)
        {
            _context = context;
        }

        public async Task<List<SearchResult>> SearchAsync(string searchText)
        {
            var postResults = await _context.ContentPosts
                        .Where(post => EF.Functions.Like(post.Title, $"%{searchText}%") || EF.Functions.Like(post.Content, $"%{searchText}%"))
                        .Select(post => new SearchResult
                        {
                            Id = post.Id,
                            Title = post.Title,
                            Description = new MarkupString(post.Content.Length > 100 ? $"{post.Content.Substring(0, 100)}..." : $"{post.Content}"),
                            Url = $"/posts/{post.Slug}",
                            Type = "Post"
                        })
                        .ToListAsync();

            var categoryResults = _context.Categories
                            .Where(category => EF.Functions.Like(category.Name, $"%{searchText}%"))
                            .Select(category => new SearchResult
                            {
                                Id = category.Id,
                                Title = category.Name,
                                Url = $"/categories/{category.Slug}/posts",
                                Type = "Category"
                            });

            return postResults.Concat(categoryResults).ToList();
        }
    }
}
