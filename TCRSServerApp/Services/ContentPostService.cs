using Microsoft.EntityFrameworkCore;

namespace TCRSServerApp.Services
{
    public class ContentPostService
    {
        private readonly TCRSContext _context;

        public ContentPostService(TCRSContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContentPost>> GetPostsAsync(bool publishedOnly = false, string? categorySlug = null)
        {
            var query = _context.ContentPosts
                            .Include(cp => cp.Category)
                            .AsNoTracking();

            if(!string.IsNullOrWhiteSpace(categorySlug))
            {
                var categoryId = await _context.Categories
                                    .AsNoTracking()
                                    .Where(c => c.Slug == categorySlug)
                                    .Select(c => c.Id)
                                    .FirstOrDefaultAsync();

                if(categoryId > 0)
                {
                    query = query.Where(cp => cp.CategoryId == categoryId);
                }
            }

            if(publishedOnly)
            {
                query = query.Where(cp => cp.IsPublished);
            }

            return await query.ToListAsync();
        }
            

        public async Task<ContentSaveModel?> GetPostAsync(int contentPostId) =>
            await _context.ContentPosts
                        .Include(cp => cp.Category)
                        .AsNoTracking()
                        .Select(ContentSaveModel.Selector)
                        .FirstOrDefaultAsync(cp => cp.Id == contentPostId);

        public async Task<MethodResult> SaveAsync(ContentSaveModel post, int userId)
        {
            if(post.Id == 0)
            {
                var entity = post.ToContentEntity(userId);

                entity.Slug = entity.Slug.Slugify();
                
                entity.CreatedOn = DateTime.Now;

                if(entity.IsPublished)
                {
                    entity.PublishedOn = DateTime.Now;
                }

                await _context.AddAsync(entity);
            } else
            {
                ContentPost? entity = await _context.ContentPosts
                                        .FirstOrDefaultAsync (cp => cp.Id == post.Id);

                if(entity is not null)
                {
                    var wasContentPostPublished = entity.IsPublished;

                    entity = post.Merge(entity);

                    entity.PublishedOn = DateTime.Now;

                    if(entity.IsPublished)
                    {
                        if(wasContentPostPublished)
                        {
                        } else
                        {
                            entity.PublishedOn = DateTime.Now;
                        }
                    } else if(wasContentPostPublished)
                    {
                        entity.PublishedOn = null;
                    }
                } else
                {
                    return MethodResult.Failure("This post does not exist...");
                }
            }

            try
            {
                if(await _context.SaveChangesAsync() > 0)
                {
                    return MethodResult.Success();
                } else
                {
                    return MethodResult.Failure("Unknown error while saving post...");
                }
            } catch (Exception e)
            {
                return MethodResult.Failure(e.Message);
            }
        }

        public async Task<ContentPost?> GetPostBySlugAsync(string slug) =>
            await _context.ContentPosts
                        .Include(cp => cp.Category)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(cp => cp.IsPublished && cp.Slug == slug);
    }
}
