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
	/// Description of ww.
	/// </summary>
	public class Ww
	{
		string pdfName;
		public Ww(string fileNameTXT, string fileLogName)
		{
			conf Config = new conf();
			FileStream fs = new FileStream(fileNameTXT,
			                               FileMode.Open, FileAccess.ReadWrite);
			DataTable docNames = new DataTable();
			docNames.Columns.Add("WW", typeof(string));
			try
			{
				StreamReader sr = new StreamReader(fs);
				
				while (!sr.EndOfStream)
				{
					string text1 = sr.ReadLine().Replace(" ", "");
					compilerDocName.All allCompiler = new ocr_wz.compilerDocName.All(text1);
					string text = allCompiler.resultText;
					if (
						text.Contains("mówienie")
						|| (text.Contains("ze") && text.Contains("wn") && text.Contains("me"))
						|| (text.Contains("trz") && text.Contains("num"))
						|| text.Contains("ydanie")
						|| text.Contains("numer:")
						|| text.Contains("WW")
					)
					{
						if (text.Contains("WW") && text.Contains("/"))
						{
							compilerDocName.Ww WwName = new ocr_wz.compilerDocName.Ww(text);
							counter.Ww licznikWw = new ocr_wz.counter.Ww(WwName.resultWW);
							if (licznikWw.result0 != null)
							{
								docNames.Rows.Add(licznikWw.result0);
							}
						}
						else if(text.Contains("ZAS"))
						{
							compilerDocName.Zas ZasName = new ocr_wz.compilerDocName.Zas(text);
							counter.Zas licznikZas = new ocr_wz.counter.Zas(ZasName.resultZas);
							if (licznikZas.result0 != null)
							{
								docNames.Rows.Add(licznikZas.result0);
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
				int ileWW = 0;
				int ileZAS = 0;
				foreach (DataRow row in uniqDocNames.Rows)
				{
					StreamWriter SW1;
					SW1 = File.AppendText(fileLogName);
					SW1.WriteLine(row.Field<string>(0));
					SW1.Close();
					if (row.Field<string>(0).Contains("WW"))
					{
						ileWW++;
					}
					else if (row.Field<string>(0).Contains("ZAS_"))
					{
						ileZAS++;
					}
				}
				
				pdfName = fileNameTXT.Replace(".txt", ".pdf");
				if (ileWW == ileZAS || ileWW > ileZAS)
				{
					foreach (DataRow row in uniqDocNames.Rows)
					{
						if (row.Field<string>(0).Contains("WW"))
						{
							yearDocs WW = new yearDocs(row.Field<string>(0));
							WW.yearWW();
							Copy CopyNewName = new Copy(pdfName, WW.year, row.Field<string>(0), fileLogName);
							CopyNewName.CopyWW();
						}
					}
					PdfOcrDone pdfDone = new PdfOcrDone(pdfName);
				}
				else
				{
					int tableElements = uniqDocNames.Rows.Count;
					for (int i = 0; i < tableElements; i++)
					{
						string docName = Convert.ToString(uniqDocNames.Rows[i]["WW"]);
						if ( i == 0 && docName.Contains("WW"))
						{
							yearDocs readYear = new yearDocs(docName);
							readYear.yearWW();
							Copy CopyNewName = new Copy(pdfName, readYear.year, docName, fileLogName);
							CopyNewName.CopyWW();
						}
						else if (i == 0 && docName.Contains("ZAS_") && tableElements < 2)
						{
							yearDocs readYear = new yearDocs(docName);
							readYear.yearZas();
							Copy CopyNewName = new Copy(pdfName, readYear.year, docName, fileLogName);
							CopyNewName.CopyZAS();
						}
						else if(i > 0 && docName.Contains("ZAS_") && (Convert.ToString(uniqDocNames.Rows[i-1]["WW"])).Contains("ZAS_"))
						{
							yearDocs readYear = new yearDocs(docName);
							readYear.yearZas();
							Copy CopyNewName = new Copy(pdfName, readYear.year, docName, fileLogName);
							CopyNewName.CopyZAS();
						}
						else if (i > 0 && docName.Contains("WW"))
						{
							yearDocs readYear = new yearDocs(docName);
							readYear.yearWW();
							Copy CopyNewName = new Copy(pdfName, readYear.year, docName, fileLogName);
							CopyNewName.CopyWW();
						}
						
					}
					PdfOcrDone pdfDone = new PdfOcrDone(pdfName);
				}
				
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
