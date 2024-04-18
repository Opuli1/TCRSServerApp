namespace TCRSServerApp
{
    public class Utilities
    {
        private static readonly string[] _colors = new string[] { "primary", "danger" };

        public static string GetRandomColor() =>
            _colors.OrderBy(c => Guid.NewGuid()).First();
    }
}
