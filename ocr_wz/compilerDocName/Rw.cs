/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-25
 * Godzina: 13:50
 * 
 */
using System;
using System.Text.RegularExpressions;

namespace ocr_wz.compilerDocName
{
	/// <summary>
	/// Description of Rw.
	/// </summary>
	public class Rw
	{
		public string resultRW;
		public void RWfirst(string result)
		{
			Regex regex = new Regex(@"Wyd");
			result = result.Replace("Arkusz:", "RW_");
			result = Regex.Replace(result, @"[~`!@#$%^&\*()+A-KM-OS-UZęóąśłżźćń;:'\|,<.>?""\]\.\-]", "");
			result = result.Replace("/", "_");
			resultRW = Regex.Replace(result, "LP_", ";LP_");
		}
		public void RW(string result)
		{
			if (result.Length > 10)
			{
				resultRW = result.Remove(10);
			}
			else
			{
				resultRW = result;
			}
		}
		public void LP(string result)
		{
			if (result.Length > 11)
			{
				result = result.Remove(11);
			}
			yearDocs year = new yearDocs(result);
			year.yearLP();
			resultRW = year.docNameN;
			
		}
	}
}
