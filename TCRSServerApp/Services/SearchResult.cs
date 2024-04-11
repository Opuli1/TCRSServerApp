using Microsoft.AspNetCore.Components;

namespace TCRSServerApp.Services
{
    public class SearchResult
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public MarkupString Description { get; set; }

        public string Url { get; set; }

        public string Type { get; set; }
    }
}
