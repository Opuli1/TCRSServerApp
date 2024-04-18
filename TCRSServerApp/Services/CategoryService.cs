using Microsoft.EntityFrameworkCore;

namespace TCRSServerApp.Services
{
    public class CategoryService
    {
        private readonly TCRSContext _context;
        private readonly ContentPostService _contentPostService;

        public CategoryService(TCRSContext context, ContentPostService contentPostService)
        {
            _context = context;
            _contentPostService = contentPostService;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync() =>
            await _context.Categories
            .AsNoTracking()
            .ToListAsync();

        public async Task<MethodResult> SaveCategoryAsync(Category model)
        {
            try
            {
                if (model.CategoryId > 0)
                {
                    _context.Categories.Update(model);
                }
                else
                {
                    model.Slug = model.Slug.Slugify();

                    await _context.Categories.AddAsync(model);
                }

                await _context.SaveChangesAsync();

                return MethodResult.Success();
            } catch(Exception e)
            {
                return MethodResult.Failure(e.Message);
            }
        }

        public async Task<MethodResult> DeleteCategoryAsync(int categoryId)
        {
            try
            {

                var category = await _context.Categories.FindAsync(categoryId);

                if (category == null)
                {
                    return MethodResult.Failure("Category not found");
                }

                var postsWithCategory = await _context.ContentPosts.Where(cp => cp.CategoryId == categoryId).ToListAsync();
                
                foreach (var post in postsWithCategory)
                {
                    post.CategoryId = 0;
                }

                _context.Categories.Remove(category);

                await _context.SaveChangesAsync();

                return MethodResult.Success();
            }
            catch (Exception ex)
            {
                return MethodResult.Failure(ex.Message);
            }
        }
    }
}
