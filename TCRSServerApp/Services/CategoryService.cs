using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace TCRSServerApp.Services
{
    public class CategoryService
    {
        private readonly TCRSContext _context;

        public CategoryService(TCRSContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync() =>
            await _context.Categories
            .AsNoTracking()
            .ToListAsync();

        public async Task<MethodResult> SaveCategoryAsync(Category model)
        {
            try
            {
                if (model.Id > 0)
                {
                    _context.Categories.Update(model);
                }
                else
                {
                    await _context.Categories.AddAsync(model);
                }

                await _context.SaveChangesAsync();

                return MethodResult.Success();
            } catch(Exception e)
            {
                return MethodResult.Failure(e.Message);
            }
        }
    }
}
