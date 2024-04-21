using Microsoft.CodeAnalysis.CSharp.Syntax;
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
                                    .Select(c => c.CategoryId)
                                    .FirstOrDefaultAsync();

                if(categoryId >= 0)
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
                        .FirstOrDefaultAsync(cp => cp.PostId == contentPostId);

        public async Task<MethodResult> SaveAsync(ContentSaveModel post, int userId)
        {
            if(post.PostId == 0)
            {
                var entity = post.ToContentEntity(userId);

                var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == post.CategoryId);

                entity.Slug = entity.Slug.Slugify();
                
                entity.CreatedOn = DateTime.Now;

                if(!categoryExists)
                {
                    return MethodResult.Failure("Invalid Category ID");
                }

                if(entity.IsPublished)
                {
                    entity.PublishedOn = DateTime.Now;
                }

                await _context.AddAsync(entity);
            } else
            {
                ContentPost? entity = await _context.ContentPosts
                                        .FirstOrDefaultAsync (cp => cp.PostId == post.PostId);

                if(entity is not null)
                {
                    var wasContentPostPublished = entity.IsPublished;

                    entity = post.Merge(entity);

                    entity.EditedOn = DateTime.Now;

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

        public async Task<MethodResult> DeletePostsAsync(int contentId)
        {
            try
            {

                var content = await _context.ContentPosts.FindAsync(contentId);

                if (content == null)
                {
                    return MethodResult.Failure("Post not found");
                }

                _context.ContentPosts.Remove(content);

                await _context.SaveChangesAsync();

                return MethodResult.Success();
            }
            catch (Exception ex)
            {
                return MethodResult.Failure(ex.Message);
            }
        }

        public async Task<MethodResult> DeleteCategoryFromPost(int categoryId)
        {
            try
            {
                var categoriesToDelete = await _context.ContentPosts
                                            .Where(cp => cp.CategoryId == categoryId)
                                            .ToListAsync();

                foreach (var category in categoriesToDelete)
                {
                    category.CategoryId = 1;
                }

                await _context.SaveChangesAsync();

                return MethodResult.Success();
            } catch(Exception e)
            {
                return MethodResult.Failure("An error occurred while deleting category: " + e.Message);
            }
        }

        public async Task<ContentPost?> GetPostBySlugAsync(string slug) =>
            await _context.ContentPosts
                        .Include(cp => cp.Category)
                        .Include(f => f.Files)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(cp => cp.IsPublished && cp.Slug == slug);
    }
}
