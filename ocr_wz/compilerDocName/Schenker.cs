/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-28
 * Godzina: 07:10
 * 
 */
using System;
using System.Text.RegularExpressions;

namespace ocr_wz.compilerDocName
{
	/// <summary>
	/// Description of Schenker.
	/// </summary>
	public class Schenker
	{
		public string resultSchenker;
		public Schenker(string result)
		{
			Regex regex = new Regex(@"Wyd"); //@"\D"
			result = regex.Replace(result, "");
			result = Regex.Replace(result, "ww", "WW");
			result = Regex.Replace(result, "wz", "WZ");
			result = Regex.Replace(result, @"oduWZ", "");
			result = Regex.Replace(result, "[a-z]" , "");
            result = Regex.Replace(result, "-", "");
			result = Regex.Replace(result, @"[~`!@#$%^&\*()_+B-EG-RT-Uęóąśłżźćń:'\|<.>?""\]\.\-]", "");
			resultSchenker = Regex.Replace(result, "WZWZ", "WZ");
		}
	}
}
