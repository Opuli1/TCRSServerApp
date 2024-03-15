using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.Json;

namespace TCRSServerApp.Authentication
{
    public class AuthenticationService
    {
        private readonly UserService _userService;
        private readonly ProtectedLocalStorage _protectedLocalStorage;

        public AuthenticationService(UserService userService, ProtectedLocalStorage protectedLocalStorage)
        {
            _userService = userService;
            _protectedLocalStorage = protectedLocalStorage;
        }

        public async Task<SignedInUser?> SignInUserAsync(SignInModel signInModel)
        {
            var signedInUser = await _userService.SignInAsync(signInModel);

            if (signedInUser is not null)
            {
                await SaveUserToBrowserStorageAsync(signedInUser.Value);
            }

            return signedInUser;
        }

        private const string UserStorageKey = "app_user";

        private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {

        };

        public async Task SaveUserToBrowserStorageAsync(SignedInUser user) =>
            await _protectedLocalStorage.SetAsync(UserStorageKey, JsonSerializer.Serialize(user, _jsonSerializerOptions));

        public async Task<SignedInUser?> GetUserFromBrowserStorageAsync()
        {
            try
            {
                var result = await _protectedLocalStorage.GetAsync<string>(UserStorageKey);

                if (result.Success && !string.IsNullOrWhiteSpace(result.Value))
                {
                    var signedInUser = JsonSerializer.Deserialize<SignedInUser>(result.Value, _jsonSerializerOptions);

                    return signedInUser;
                }
            } catch(InvalidOperationException)
            {
                //not important;
                //called from client side after;
                //safe to ignore
            }
            
            return null;
        }

        public async Task RemoveUserFromBrowserStorageAsync() =>
            await _protectedLocalStorage.DeleteAsync(UserStorageKey);
    }
}
