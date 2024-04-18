using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace TCRSServerApp.Services
{
    public class SearchResult
    {
        [Key]
        public int SearchId { get; set; }

        public string Title { get; set; }

        public MarkupString Description { get; set; }

        public string Url { get; set; }

        public string Type { get; set; }
    }
}
