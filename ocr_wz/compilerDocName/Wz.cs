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
            result = Regex.Replace(result, @"WZ/188/", "WZ/18/");
            result = Regex.Replace(result, @"WZ/T8/", "WZ/18/");
            result = Regex.Replace(result, @"WZ/188", "WZ/18/");
            result = Regex.Replace(result, @"WZ/8/", "WZ/18/");
            result = Regex.Replace(result, @"WZ[!l]", "WZ/");
            result = Regex.Replace(result, @"WZ11", "WZ/1");
            result = Regex.Replace(result, @"WZ/18.*/", "WZ/18/");
			int ile = result.Length;
			for (int i = 0; i < ile + 20; i++ )
			{
                result = Regex.Replace(result, @".*WZ/", "WZ/");
				result = result.Replace("[","");
				result = result.Replace("]","");
                result = result.Replace("—", "");
                result = result.Replace("*", "");
			}
			result = Regex.Replace(result, @"WZ/8/", "WZ/18/");
			result = Regex.Replace(result, @"WZ/[i!l]8/", "WZ/18/");
			result = Regex.Replace(result, @"[S]WZ/", "WZ/");
			result = Regex.Replace(result, "WZWZ/", "WZ/");
			resultWZ = result;
			
		}
	}
}
