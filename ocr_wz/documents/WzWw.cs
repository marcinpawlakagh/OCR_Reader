/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-26
 * Godzina: 11:13
 * 
 */
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data;

namespace ocr_wz.documents
{
	/// <summary>
	/// Description of WzWw.
	/// </summary>
	public class WzWw
	{
		public WzWw(string fileNameTXT, string fileLogName)
		{
			conf Config = new conf();
			FileStream fs = new FileStream(fileNameTXT,
			FileMode.Open, FileAccess.ReadWrite);
			DataTable docNames = new DataTable();
			docNames.Columns.Add("WZ", typeof(string));
			
			try
			{
				StreamReader sr = new StreamReader(fs);
				while (!sr.EndOfStream)
				{
					string text = sr.ReadLine().Replace(" ", "");
					if (
					text.Contains("mówienie")
					|| (text.Contains("ze") && text.Contains("wn") && text.Contains("me"))
					|| (text.Contains("trz") && text.Contains("num"))
					|| text.Contains("ydanie")
					|| text.Contains("numer:")
					|| text.Contains("WW1")
					|| text.Contains("WZ/")
					)
					{
						
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Nie odnaleziono pliku txt!" + ex);
			}
			
			Console.ReadKey();
			
		}
	}
}
