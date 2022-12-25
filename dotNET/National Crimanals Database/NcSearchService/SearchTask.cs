using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalCriminalDatabase
{
    public class SearchTask
    {
        public SearchParameter Search { get; set; }

        public string Email { get; set; }
        public int MaxResultCount { get; set; }
    }
}
