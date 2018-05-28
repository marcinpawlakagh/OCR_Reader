/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-24
 * Godzina: 12:47
 * 
 */
using System;
using System.Text.RegularExpressions;

namespace ocr_wz.compilerDocName
{
	/// <summary>
	/// Description of Wz.
	/// </summary>
	public class Wz
	{
		public string resultWZ;
		public Wz(string result)
		{
			Regex regex = new Regex(@"Wyd"); //@"\D"
			result = regex.Replace(result, "");
			result = Regex.Replace(result, @"oduWZ", "");
			result = Regex.Replace(result, "2AS", "ZAS");
			result = Regex.Replace(result, "WŻ/", "WZ/");
			result = Regex.Replace(result, "wz/", "WZ/");
			result = Regex.Replace(result, "WZJ/", "WZ/");
			result = Regex.Replace(result, "WZ4", "WZ/");
			int ile = result.Length;
			for (int i = 0; i < ile + 10; i++ )
			{
				result = Regex.Replace(result, @"[:punct:]WZ/", "WZ/");
				result = Regex.Replace(result, @"[[]]WZ/", "WZ/");
				result = Regex.Replace(result, @"[.]WZ/", "WZ/");
				result = Regex.Replace(result, @"[:numeric:]WZ/", "WZ/");
				result = Regex.Replace(result, @"[-|0-9A-Za-ząęółśżźćń\=„*+',;\._<>""()«%]WZ/", "WZ/");
				result = result.Replace("[","");
				result = result.Replace("]","");
			}
			result = Regex.Replace(result, @"WZ/8/", "WZ/18/");
			result = Regex.Replace(result, @"WZ/[i!l]8/", "WZ/18/");
			result = Regex.Replace(result, @"[S]WZ/", "WZ/");
			result = Regex.Replace(result, "WZWZ/", "WZ/");
			resultWZ = result;
			
		}
	}
}
