﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancyService
{
	public static class StringHelper
	{
		/// <summary>
		/// Method that limits the length of text to a defined length.
		/// </summary>
		/// <param name="source">The source text.</param>
		/// <param name="maxLength">The maximum limit of the string to return.</param>
		public static string LimitLength(this string source, int maxLength)
		{
			if (source.Length <= maxLength)
			{
				return source;
			}

			return source.Substring(0, maxLength);
		}
	}
}
