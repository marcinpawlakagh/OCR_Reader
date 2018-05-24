/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-24
 * Godzina: 10:43
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
	/// Description of WzRaben.
	/// </summary>
	public class WzRaben
	{
		string pdfName;
		public WzRaben(string fileNameTXT, string fileLogName)
		{
			conf Config = new conf();
			FileStream fs = new FileStream(fileNameTXT,
			                               FileMode.Open, FileAccess.ReadWrite);
			DataTable docNames = new DataTable();
			docNames.Columns.Add("WZ", typeof(string));
			try
			{
				DateTime thisTime = DateTime.Now;
				string year = (thisTime.ToString().Replace(" ", "_").Replace("-", "").Replace(":","")).Remove(4).Replace("20", "");
				string yearBack = Convert.ToString((Convert.ToInt32(year) - 1));
				string yearNext = Convert.ToString((Convert.ToInt32(year) + 1));
				string pdfPath = fileNameTXT.Replace(".txt", ".pdf");
				StreamReader sr = new StreamReader(fs);
				StreamWriter SW;
				SW = File.AppendText(fileLogName);
				SW.WriteLine("Tablica dokumentów:");
				SW.Close();
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
						|| text.Contains("wz/" + yearBack + "/")
						|| text.Contains("wz/" + year + "/")
						|| text.Contains("wz/" + yearNext + "/")
						|| text.Contains("ww" + yearBack + "/")
						|| text.Contains("ww" + year + "/")
						|| text.Contains("ww" + yearNext + "/")
						|| text.Contains("WZ/" + yearBack + "/")
						|| text.Contains("WZ/" + year + "/")
						|| text.Contains("WZ/" + yearNext + "/")
						|| text.Contains("WW" + yearBack + "/")
						|| text.Contains("WW" + year + "/")
						|| text.Contains("WW" + yearNext + "/")
					)
					{
						Regex regex = new Regex(@"Wyd"); //@"\D"
						string result = regex.Replace(text, "");
						result = Regex.Replace(result, "ww", "WW");
						result = Regex.Replace(result, "wz", "WZ");
						result = Regex.Replace(result, @"oduWZ", "");
						result = Regex.Replace(result, "[a-z]" , "");
						result = Regex.Replace(result, @"[~`!@#$%^&\*()_+B-EG-RT-Uęóąśłżźćń;:'\|<.>?""\]\.\-]", "");
						result = Regex.Replace(result, @"[a-z0-9A-Z]WZ/", "WZ/");
						String[] documents;
						documents = result.Split(',');
						DataTable documentsTrue = new DataTable();
						documentsTrue.Columns.Add("WZ", typeof(string));
						foreach (string row in documents)
						{
							if (row.Contains("WZ") || row.Contains("WW") || row.Contains("ZAS"))
							{
								documentsTrue.Rows.Add(row);
							}

						}
						foreach (DataRow row in documentsTrue.Rows)
						{
							StreamWriter SW2;
							SW2 = File.AppendText(fileLogName);
							SW2.WriteLine(row.Field<string>(0));
							SW2.Close();
						}
					}
				}
			}
			catch
			{
				
			}
		}
	}
}
