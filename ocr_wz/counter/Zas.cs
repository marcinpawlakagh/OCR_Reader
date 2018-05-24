/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-24
 * Godzina: 14:07
 * 
 */
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ocr_wz.counter
{
	/// <summary>
	/// Description of Zas.
	/// </summary>
	public class Zas
	{	public string result0;
		public Zas(string result)
		{
			Regex regex = new Regex(@"Wyd");
			if (result.Length > 13)
			{
				result = result.Remove(startIndex:13);
			}
			result0 = Regex.Replace(result, "/", "_");
		}
	}
}
