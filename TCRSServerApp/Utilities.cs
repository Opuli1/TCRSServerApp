namespace TCRSServerApp
{
    public class Utilities
    {
        private static readonly string[] _colors = new string[] { "primary", "danger" };

        public static string GetRandomColor() =>
            _colors.OrderBy(c => Guid.NewGuid()).First();

        public static string GetInitials(string text)
        {
            const string DefaultInitials = "TCRS";

            if(!string.IsNullOrEmpty(text))
            {
                var parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                if(parts.Length > 1)
                {
                    return $"{parts[0][0]}{parts[1][0]}";
                } else
                {
                    return text.Length > 1 ? text[..2] : text;
                }
            }

            return DefaultInitials;
        }
    }
}
