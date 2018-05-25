/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-24
 * Godzina: 13:48
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
	/// Description of rw.
	/// </summary>
	public class Rw
	{	
		string year;
		public Rw(string fileNameTXT, string fileLogName)
		{
			conf Config = new conf();
			FileStream fs = new FileStream(fileNameTXT,
			                               FileMode.Open, FileAccess.ReadWrite);
			DataTable docNames = new DataTable();
			docNames.Columns.Add("WZ", typeof(string));
			String[] documents;
			string pdfName = fileNameTXT.Replace(".txt", ".pdf");
//			DataTable documents2it = new DataTable();
//			documents2it.Columns.Add("WZ", typeof(string));
//
			try
			{
				StreamReader sr = new StreamReader(fs);
				while (!sr.EndOfStream)
				{
					string text = sr.ReadLine().Replace(" ", "");
					if (
						text.Contains("rk") && text.Contains("us") && text.Contains("LP/")
						|| text.Contains("Ar") && text.Contains("k") && text.Contains("s")
					)
					{
						compilerDocName.Rw RwName = new ocr_wz.compilerDocName.Rw();
						RwName.RWfirst(text);
						documents = RwName.resultRW.Split(';');
						foreach (var document in documents)
						{
							if (document.Contains("LP_"))
							{
								RwName.LP(document);
								docNames.Rows.Add(RwName.resultRW);
							}
							else if (document.Contains("RW_"))
							{
								RwName.RW(document);
								docNames.Rows.Add(RwName.resultRW);
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
				int rw = 0;
				int lp = 0;
				
				foreach (DataRow row in uniqDocNames.Rows)
				{
					StreamWriter SW1;
					SW1 = File.AppendText(fileLogName);
					SW1.WriteLine(row.Field<string>(0));
					SW1.Close();
					if (row.Field<string>(0).Contains("LP_"))
					{
						lp++;
						yearDocs yearOut = new yearDocs(row.Field<string>(0));
						yearOut.yearLP();
						year = yearOut.year;
					}
					else if (row.Field<string>(0).Contains("RW_"))
					{
						rw++;
					}
				}
				
				if (rw == lp || rw  > lp)
				{
					foreach (DataRow row in uniqDocNames.Rows)
					{
						if  (row.Field<string>(0).Contains("RW_"))
						{
							Copy CopyNewName = new Copy(pdfName, year, row.Field<string>(0), fileLogName);
							CopyNewName.CopyRW();
						}
					}
				}
				else
				{
					foreach (DataRow row in uniqDocNames.Rows)
					{
						if  (row.Field<string>(0).Contains("LP_"))
						{
							Copy CopyNewName = new Copy(pdfName, year, row.Field<string>(0), fileLogName);
							CopyNewName.CopyRW();
						}
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
