/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-25
 * Godzina: 12:47
 * 
 */
using System;
using System.Text.RegularExpressions;

namespace ocr_wz.compilerDocName
{
	/// <summary>
	/// Description of Raben.
	/// </summary>
	public class Raben
	{
		public string resultRaben;
		public Raben(string result)
		{
			Regex regex = new Regex(@"Wyd"); //@"\D"
			result = regex.Replace(result, "");
			result = Regex.Replace(result, "ww", "WW");
			result = Regex.Replace(result, "wz", "WZ");
			result = Regex.Replace(result, @"oduWZ", "");
			result = Regex.Replace(result, "[a-z]" , "");
			result = Regex.Replace(result, @"[~`!@#$%^&\*()_+B-EG-RT-Uęóąśłżźćń;:'\|<.>?""\]\.\-]", "");
			resultRaben = Regex.Replace(result, @"[a-z0-9A-Z]WZ/", "WZ/");
			
		}
	}
}
