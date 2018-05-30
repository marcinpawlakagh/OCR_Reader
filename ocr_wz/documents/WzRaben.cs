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
		public WzRaben(string fileNameTXT, string fileLogName)
		{
			conf Config = new conf();
			FileStream fs = new FileStream(fileNameTXT,
			                               FileMode.Open, FileAccess.ReadWrite);
			DataTable docNames = new DataTable();
			docNames.Columns.Add("WZ", typeof(string));
			String[] documents;
			try
			{
				DateTime thisTime = DateTime.Now;
				string year = (thisTime.ToString().Replace(" ", "_").Replace("-", "").Replace(":","")).Remove(4).Replace("20", "");
				string yearBack = Convert.ToString((Convert.ToInt32(year) - 1));
				string yearNext = Convert.ToString((Convert.ToInt32(year) + 1));
				string pdfName = fileNameTXT.Replace(".txt", ".pdf");
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
						|| text.Contains("WW1")
						|| text.Contains("WZ/")
						|| text.Contains("WZ/" + yearBack + "/")
						|| text.Contains("WZ/" + year + "/")
						|| text.Contains("WZ/" + yearNext + "/")
						|| text.Contains("WW" + yearBack + "/")
						|| text.Contains("WW" + year + "/")
						|| text.Contains("WW" + yearNext + "/")
					)
					{
						compilerDocName.Raben RabenName = new ocr_wz.compilerDocName.Raben(text);
						documents = RabenName.resultRaben.Split(',');
						foreach (string row in documents)
						{
							if (row.Contains("WZ"))
							{
								compilerDocName.Wz WzName = new ocr_wz.compilerDocName.Wz(row);
								counter.Wz licznikWz = new ocr_wz.counter.Wz(WzName.resultWZ);
								if (licznikWz.result0 != null)
								{
									docNames.Rows.Add(licznikWz.result0);
								}
							}
							else if (row.Contains("WW"))
							{
								compilerDocName.Ww WwName = new ocr_wz.compilerDocName.Ww(row);
								counter.Ww licznikWw = new ocr_wz.counter.Ww(WwName.resultWW);
								if (licznikWw.result0 != null)
								{
									docNames.Rows.Add(licznikWw.result0);
								}
							}
							else if(row.Contains("ZAS") || row.Contains("2AS") || row.Contains("ŻAS"))
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
				}
				var UniqueRows = docNames.AsEnumerable().Distinct(DataRowComparer.Default);
				DataTable uniqDocNames = UniqueRows.CopyToDataTable();
				int ileWZ = 0;
				int ileWW = 0;
				int ileZAS = 0;
				StreamWriter SW1;
				SW1 = File.AppendText(fileLogName);
				SW1.WriteLine("Tablica dokumentów:");
				SW1.Close();
				foreach (DataRow row in uniqDocNames.Rows)
				{
					StreamWriter SW2;
					SW2 = File.AppendText(fileLogName);
					SW2.WriteLine(row.Field<string>(0));
					SW2.Close();
					if (row.Field<string>(0).Contains("WZ"))
					{
						ileWZ++;
					}
					else if (row.Field<string>(0).Contains("WW"))
					{
						ileWW++;
					}
					else if (row.Field<string>(0).Contains("ZAS"))
					{
						ileZAS++;
					}
				}
				if (ileWZ + ileWW == ileZAS || ileWZ + ileWW > ileZAS)
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
						else if (row.Field<string>(0).Contains("WW_"))
						{
							yearDocs WW = new yearDocs(row.Field<string>(0));
							WW.yearWW();
							Copy CopyNewName = new Copy(pdfName, WW.year, row.Field<string>(0), fileLogName);
							CopyNewName.CopyWW();
						}
					}
				}
				else
				{
					int tableElements = uniqDocNames.Rows.Count;
					for (int i = 0; i < tableElements; i++)
					{
						pdfName = fileNameTXT.Replace(".txt", ".pdf");
						string docName = Convert.ToString(uniqDocNames.Rows[i]["WZ"]);
						if (i == 0 && docName.Contains("WW"))
						{
							yearDocs WW = new yearDocs(docName);
							WW.yearWW();
							Copy CopyNewName = new Copy(pdfName, WW.year, docName, fileLogName);
							CopyNewName.CopyWW();
						}
						else if (i == 0 && docName.Contains("WZ"))
						{
							yearDocs WZ = new yearDocs(docName);
							WZ.yearWZ();
							Copy CopyNewName = new Copy(pdfName, WZ.year, docName, fileLogName);
							CopyNewName.CopyWW();
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
							yearDocs WZ = new yearDocs(docName);
							WZ.yearWZ();
							Copy CopyNewName = new Copy(pdfName, WZ.year, docName, fileLogName);
							CopyNewName.CopyWW();
						}
						else if (i > 0 && docName.Contains("WW"))
						{
							yearDocs WW = new yearDocs(docName);
							WW.yearWW();
							Copy CopyNewName = new Copy(pdfName, WW.year, docName, fileLogName);
							CopyNewName.CopyWW();
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
