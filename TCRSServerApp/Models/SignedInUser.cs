namespace TCRSServerApp.Models
{
    public record struct SignedInUser(int UserId, string DisplayName)
    {
        public readonly bool IsEmpty => UserId == 0;
    }
}
