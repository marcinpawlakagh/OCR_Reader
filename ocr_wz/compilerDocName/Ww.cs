/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-25
 * Godzina: 08:43
 * 
 */
using System;
using System.Text.RegularExpressions;

namespace ocr_wz.compilerDocName
{
	/// <summary>
	/// Description of Ww.
	/// </summary>
	public class Ww
	{
		public string resultWW;
		public Ww(string result)
		{
			Regex regex = new Regex(@"Wyd"); //@"\D"
			result = regex.Replace(result, "");
			result = Regex.Replace(result, @"oduWW", "");
			result = Regex.Replace(result, @"ww", "WW");
			result = Regex.Replace(result, "[a-z]" , "");
			int ile = result.Length;
			for (int i = 0; i < ile + 10; i++ )
			{
				result = Regex.Replace(result, @"[:punct:]WW", "WW");
				result = Regex.Replace(result, @"[[]]WW", "WW");
				result = Regex.Replace(result, @"[.]WW", "WW");
				result = Regex.Replace(result, @"[:numeric:]WW", "WW");
				result = Regex.Replace(result, @"[-|0-9A-Za-ząęółśżźćń\=„*+',;\._<>""()«%]WW", "WW");
			}
			result = Regex.Replace(result, @"WW8", "WW18");
			result = Regex.Replace(result, @"WW[i!l]8/", "WW18/");
			result = Regex.Replace(result, @"[S]WW/", "WW/");
			result = Regex.Replace(result, "WWWW", "WW");
			resultWW = result;
			
		}
	}
}
