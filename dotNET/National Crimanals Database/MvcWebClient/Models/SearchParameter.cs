using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcWebClient.Models
{
    public class SearchParameter
    {
        private string _email = "user.testservice@yandex.ru";
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter meail")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [Range(1, 1000, ErrorMessage = "Max result count must be in range from 1 to 1000")]
        public int MaxResultCount { get; set; }
    }
}