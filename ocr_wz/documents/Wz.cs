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
									if (text.Contains("WZ"))
									{
										compilerDocName.Wz WzName = new ocr_wz.compilerDocName.Wz(text);
										string result = WzName.resultWZ;
										counter.Wz licznikWz = new ocr_wz.counter.Wz(result);
										docNames.Rows.Add(licznikWz.result0);
									}
									else if(text.Contains("ZAS") || text.Contains("2AS") || text.Contains("ŻAS"))
									{
										compilerDocName.Zas ZasName = new ocr_wz.compilerDocName.Zas(text);
										string result = ZasName.resultZas;
										counter.Zas licznikZas = new ocr_wz.counter.Zas(result);
										docNames.Rows.Add(licznikZas.result0);
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
					}
					int ileWZ = 0;
					int ileZAS = 0;
					foreach (DataRow row in uniqDocNames.Rows)
					{
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
						DataTable endTable = new DataTable();
						foreach (DataRow row in uniqDocNames.Rows)
						{
							if (row.Field<string>(0).Contains("WZ_"))
							{
								string year = row.Field<string>(0).Remove(5);
								year = year.Replace("WZ_", "");
								string docName = row.Field<string>(0);
								Copy CopyNewName = new Copy(pdfName, year, docName, fileLogName);
								CopyNewName.CopyWZ();
							}
						}
						string przetworzone = pdfName.Replace("po_ocr\\", "po_ocr\\przetworzone\\");
						File.Move(pdfName, przetworzone);
						
					}
					else
					{
						DataTable endTable = new DataTable();
						int tableElements = uniqDocNames.Rows.Count;
						string tableRow = Convert.ToString(uniqDocNames.Rows[0]["WZ"]);
						for (int i = 0; i < tableElements; i++)
						{
							string year = Convert.ToString(uniqDocNames.Rows[i]["WZ"]).Remove(6);
							string docName = Convert.ToString(uniqDocNames.Rows[i]["WZ"]);
							if ( i == 0 && docName.Contains("WZ_"))
							{
								year = year.Replace("WZ_", "");
								year = year.Replace("_", "");
								Copy CopyNewName = new Copy(pdfName, year, docName, fileLogName);
								CopyNewName.CopyWZ();
							}
							else if (i == 0 && docName.Contains("ZAS_") && tableElements < 2)
							{
								year = year.Replace("ZAS_", "");
								Copy CopyNewName = new Copy(pdfName, year, docName, fileLogName);
								CopyNewName.CopyZAS();
							}
							else if(i > 0 && docName.Contains("ZAS_") && (Convert.ToString(uniqDocNames.Rows[i-1]["WZ"])).Contains("ZAS_"))
							{
								year = year.Replace("ZAS_", "");
								Copy CopyNewName = new Copy(pdfName, year, docName, fileLogName);
								CopyNewName.CopyZAS();
							}
							else if (i > 0 && docName.Contains("WZ"))
							{
								year = year.Replace("WZ_", "");
								year = year.Replace("_", "");
								Copy CopyNewName = new Copy(pdfName, year, docName, fileLogName);
								CopyNewName.CopyWZ();
							}
							
						}
						string przetworzone = pdfName.Replace("po_ocr\\", "po_ocr\\przetworzone\\");
						File.Move(pdfName, przetworzone);
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
