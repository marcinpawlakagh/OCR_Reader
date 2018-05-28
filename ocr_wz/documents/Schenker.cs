/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-07
 * Godzina: 11:25
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
	/// Description of Schenker.
	/// </summary>
	public class Schenker
	{
		public Schenker(string fileNameTXT, string fileLogName)
		{
			conf Config = new conf();
			FileStream fs = new FileStream(fileNameTXT,
			                               FileMode.Open, FileAccess.ReadWrite);
			DataTable docNames = new DataTable();
			docNames.Columns.Add("WZ", typeof(string));
			string pdfName = fileNameTXT.Replace(".txt", ".pdf");
			
			try
			{
				StreamReader sr = new StreamReader(fs);
				DateTime thisTime = DateTime.Now;
				string year = (thisTime.ToString().Replace(" ", "_").Replace("-", "").Replace(":","")).Remove(4).Replace("20", "");
				string yearBack = Convert.ToString((Convert.ToInt32(year) - 1));
				string yearNext = Convert.ToString((Convert.ToInt32(year) + 1));
				String[] documents;
				
				while (!sr.EndOfStream)
				{
					string text = sr.ReadLine().Replace(" ", "");
					if (
						text.Contains("wz/" + yearBack + "/")
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
						compilerDocName.Schenker SchenkerName = new ocr_wz.compilerDocName.Schenker(text);
						documents = SchenkerName.resultSchenker.Split(',');
						
						foreach (string row in documents)
						{
							if (row.Contains("WZ/"))
							{
								compilerDocName.Wz WzName = new ocr_wz.compilerDocName.Wz(row);
								counter.Wz licznikWz = new ocr_wz.counter.Wz(WzName.resultWZ);
								docNames.Rows.Add(licznikWz.result0);
							}
							else if (row.Contains("WW"))
							{
								compilerDocName.Ww WwName = new ocr_wz.compilerDocName.Ww(row);
								counter.Ww licznikWw = new ocr_wz.counter.Ww(WwName.resultWW);
								docNames.Rows.Add(licznikWw.result0);
							}
						}
					}
				}
				
				var UniqueRows = docNames.AsEnumerable().Distinct(DataRowComparer.Default);
				DataTable uniqDocNames = UniqueRows.CopyToDataTable();
				StreamWriter SW;
				SW = File.AppendText(fileLogName);
				SW.WriteLine("Tablica dokumentów:");
				SW.Close();
				foreach (DataRow row in uniqDocNames.Rows)
				{
					StreamWriter SW1;
					SW1 = File.AppendText(fileLogName);
					SW1.WriteLine(row.Field<string>(0));
					SW1.Close();
					if (row.Field<string>(0).Contains("WZ_"))
					{
						yearDocs WZ = new yearDocs(row.Field<string>(0));
						WZ.yearWZ();
						Copy CopyNewName = new Copy(pdfName, WZ.year, row.Field<string>(0), fileLogName);
						CopyNewName.CopyWZdeliveryDoc();
					}
					else if (row.Field<string>(0).Contains("WW"))
					{
						yearDocs WW = new yearDocs(row.Field<string>(0));
						WW.yearWW();
						Copy CopyNewName = new Copy(pdfName, WW.year, row.Field<string>(0), fileLogName);
						CopyNewName.CopyWWdeliveryDoc();
					}
				}
				PdfOcrDone pdfDone = new PdfOcrDone(pdfName);
				fs.Close();
				File.Delete(fileNameTXT);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Nie odnaleziono pliku txt!" + ex);
			}
		}
	}
}
