using GitHubSearch.ApplicationCore.Enums;
using System.Collections.Generic;

namespace GitHubSearch.ApplicationCore.Models
{
    public class GenericResult<T>
    {
        public T KeyID { get; set; }

        public StatusCode Status { get; set; }

        public string Message { get; set; }

        public T Item { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}