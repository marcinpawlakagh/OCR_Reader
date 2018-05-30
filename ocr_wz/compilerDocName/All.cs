/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-30
 * Godzina: 07:56
 * 
 */
using System;
using System.Text.RegularExpressions;

namespace ocr_wz.compilerDocName
{
	/// <summary>
	/// Description of All.
	/// </summary>
	public class All
	{
		public string resultText;
		public All(string text)
		{
			Regex regex = new Regex(@"Wyd");
			text = Regex.Replace(text, "2AS", "ZAS");
			text = Regex.Replace(text, "wz8", "WZ/18");
			text = Regex.Replace(text, "ŻAS", "ZAS");
			text = Regex.Replace(text, "WŻ/", "WZ/");
			text = Regex.Replace(text, "wz/", "WZ/");
			text = Regex.Replace(text, "ww", "WW");
			text = Regex.Replace(text, "Ww", "WW");
			text = Regex.Replace(text, "WZJ/", "WZ/");
			text = Regex.Replace(text, "WZ4", "WZ/");
			text = Regex.Replace(text, "W2/", "WZ/");
			text = Regex.Replace(text, "WŻ/", "WZ/");
			text = Regex.Replace(text, "”", "");
			text = Regex.Replace(text, "WWZ/", "WZ/");
			resultText = Regex.Replace(text, "WŻZ/", "WZ/");
		}
	}
}
