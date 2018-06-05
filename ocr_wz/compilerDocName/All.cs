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
            text = text.Replace("W$18/", "WZ/18/");
			text = Regex.Replace(text, "WŻ/", "WZ/");
			text = Regex.Replace(text, "wz/", "WZ/");
            text = Regex.Replace(text, "Wz/", "WZ/");
            text = Regex.Replace(text, "VvZ/", "WZ/");
			text = Regex.Replace(text, "ww", "WW");
			text = Regex.Replace(text, "Ww", "WW");
			text = Regex.Replace(text, "WZJ/", "WZ/");
			text = Regex.Replace(text, "WZ4", "WZ/");
			text = Regex.Replace(text, "W2/", "WZ/");
			text = Regex.Replace(text, "WŻ/", "WZ/");
			text = Regex.Replace(text, "”", "");
			text = Regex.Replace(text, "WWZ/", "WZ/");
            text= Regex.Replace(text, @"vv2/", "WZ/");
            text = Regex.Replace(text, "WVZ/", "WZ/");
            text = Regex.Replace(text, "2A", "ZA");
            text = Regex.Replace(text, "ZA[0-9A-Za-z]/", "ZAS/");
            text = Regex.Replace(text, "ZAS[0-9A-Za-z]/", "ZAS/");
			resultText = Regex.Replace(text, "WŻZ/", "WZ/");
		}
	}
}
