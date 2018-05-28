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
	/// Description of wz.
	/// </summary>
	public class Wz
	{
		string pdfName;
		public Wz(string fileNameTXT, string fileLogName)
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
						|| (text.Contains("am") && text.Contains("wi") && text.Contains("nie") && text.Contains("AS"))
						|| text.Contains("ZAS")
						|| (text.Contains("ze") && text.Contains("wn") && text.Contains("me"))
						|| (text.Contains("trz") && text.Contains("num"))
						|| text.Contains("ydanie")
						|| text.Contains("numer:")
						|| text.Contains("WZ/")
						|| text.Contains("WŻ/")
					)
					{
						if (text.Contains("WZ") || text.Contains("WŻ/") || text.Contains("wz"))
						{
							compilerDocName.Wz WzName = new ocr_wz.compilerDocName.Wz(text);
							counter.Wz licznikWz = new ocr_wz.counter.Wz(WzName.resultWZ);
							if (licznikWz.result0 != null)
							{
								docNames.Rows.Add(licznikWz.result0);
							}
						}
						else if(text.Contains("ZAS") || text.Contains("2AS") || text.Contains("ŻAS"))
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
				int ileWZ = 0;
				int ileZAS = 0;
				foreach (DataRow row in uniqDocNames.Rows)
				{
					StreamWriter SW1;
					SW1 = File.AppendText(fileLogName);
					SW1.WriteLine(row.Field<string>(0));
					SW1.Close();
					if (row.Field<string>(0).Contains("WZ_"))
					{
						ileWZ++;
					}
					else if (row.Field<string>(0).Contains("ZAS_"))
					{
						ileZAS++;
					}
				}
				pdfName = fileNameTXT.Replace(".txt", ".pdf");
				if (ileWZ == ileZAS || ileWZ > ileZAS)
				{
					foreach (DataRow row in uniqDocNames.Rows)
					{
						if (row.Field<string>(0).Contains("WZ_"))
						{
							yearDocs WZ = new yearDocs(row.Field<string>(0));
							WZ.yearWZ();
							Copy CopyNewName = new Copy(pdfName, WZ.year, row.Field<string>(0), fileLogName);
							CopyNewName.CopyWZ();
						}
					}
					PdfOcrDone pdfDone = new PdfOcrDone(pdfName);
				}
				else
				{
					int tableElements = uniqDocNames.Rows.Count;
					for (int i = 0; i < tableElements; i++)
					{
						string docName = Convert.ToString(uniqDocNames.Rows[i]["WZ"]);
						if ( i == 0 && docName.Contains("WZ_"))
						{
							yearDocs readYear = new yearDocs(docName);
							readYear.yearWZ();
							Copy CopyNewName = new Copy(pdfName, readYear.year, docName, fileLogName);
							CopyNewName.CopyWZ();
						}
						else if (i == 0 && docName.Contains("ZAS_") && tableElements < 2)
						{
							yearDocs readYear = new yearDocs(docName);
							readYear.yearZas();
							Copy CopyNewName = new Copy(pdfName, readYear.year, docName, fileLogName);
							CopyNewName.CopyZAS();
						}
						else if(i > 0 && docName.Contains("ZAS_") && (Convert.ToString(uniqDocNames.Rows[i-1]["WZ"])).Contains("ZAS_"))
						{
							yearDocs readYear = new yearDocs(docName);
							readYear.yearZas();
							Copy CopyNewName = new Copy(pdfName, readYear.year, docName, fileLogName);
							CopyNewName.CopyZAS();
						}
						else if (i > 0 && docName.Contains("WZ"))
						{
							yearDocs readYear = new yearDocs(docName);
							readYear.yearWZ();
							Copy CopyNewName = new Copy(pdfName, readYear.year, docName, fileLogName);
							CopyNewName.CopyWZ();
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
