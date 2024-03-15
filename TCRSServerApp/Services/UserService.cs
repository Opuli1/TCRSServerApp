using Microsoft.EntityFrameworkCore;

namespace TCRSServerApp.Services
{
    public class UserService
    {
        private readonly TCRSContext _context;

        public UserService(TCRSContext context)
        {
            _context = context;
        }

        public async Task<SignedInUser?> SignInAsync(SignInModel model)
        {
            var dbUser = await _context.Users
                            .AsNoTracking()
                            .FirstOrDefaultAsync(u => u.Email == model.Username);

            if (dbUser is not null)
            {
                return new SignedInUser(dbUser.Id, $"{dbUser.FirstName} {dbUser.LastName}".Trim());
            } else
            {
                return null;
            }
        }
    }
}
