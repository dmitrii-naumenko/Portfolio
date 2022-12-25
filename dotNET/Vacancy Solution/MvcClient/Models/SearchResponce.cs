using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcClient.Models
{
	public class SearchResponce
	{
		[Required(ErrorMessage = "Пожалуйста введите текст")]
		public string SearchText { get; set; }
	}
}