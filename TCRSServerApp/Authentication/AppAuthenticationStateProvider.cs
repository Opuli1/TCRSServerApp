using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace TCRSServerApp.Authentication
{
    public class AppAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
    {
        private const string AppAuthenticationType = "app-auth";

        private readonly AuthenticationService _authenticationService;

        public AppAuthenticationStateProvider(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            AuthenticationStateChanged += AppAuthenticationStateProvider_AuthenticationStateChanged;
        }

        private async void AppAuthenticationStateProvider_AuthenticationStateChanged(Task<AuthenticationState> task)
        {
            var authState = await task;

            if(authState is not null)
            {
                var userId = Convert.ToInt32(authState.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var displayName = authState.User.FindFirstValue(ClaimTypes.Name);

                SignedInUser = new(userId, displayName!);
            }
        }

        public SignedInUser SignedInUser { get; private set; } = new(0, string.Empty);

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claimsPrincipal = new ClaimsPrincipal();

            var user = await _authenticationService.GetUserFromBrowserStorageAsync();

            if (user is not null)
            {
                claimsPrincipal = GetClaimsPrincipalFromUser(user.Value);
            }

            var authState = new AuthenticationState(claimsPrincipal);
            
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
            
            return authState;
        }

        public async Task<string?> SignInAsync(SignInModel signInModel)
        {
            var signedInUser = await _authenticationService.SignInUserAsync(signInModel);

            if (signedInUser is null)
            {
                return "Invalid Credentials!";
            }

            var authState = new AuthenticationState(GetClaimsPrincipalFromUser(signedInUser.Value));

            NotifyAuthenticationStateChanged(Task.FromResult(authState));

            return null;
        }

        public async Task SignOutAsync()
        {
            await _authenticationService.RemoveUserFromBrowserStorageAsync();

            var authState = new AuthenticationState(new ClaimsPrincipal());

            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        private static ClaimsPrincipal GetClaimsPrincipalFromUser(SignedInUser user)
        {
            var identity = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.DisplayName)
                    }, AppAuthenticationType);

            return new ClaimsPrincipal(identity);
        }

        public void Dispose() =>
            AuthenticationStateChanged -= AppAuthenticationStateProvider_AuthenticationStateChanged;
    }
}
