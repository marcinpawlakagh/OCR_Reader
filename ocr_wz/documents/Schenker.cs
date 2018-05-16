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
			
				try
				{
					StreamReader sr = new StreamReader(fs);
					DateTime thisTime = DateTime.Now;
					string year = (thisTime.ToString().Replace(" ", "_").Replace("-", "").Replace(":","")).Remove(4).Replace("20", "");
					string yearBack = Convert.ToString((Convert.ToInt32(year) - 1));
					string yearNext = Convert.ToString((Convert.ToInt32(year) + 1));
					string pdfPath = fileNameTXT.Replace(".txt", ".pdf");
					DataTable documents2it = new DataTable();
					documents2it.Columns.Add("WZ", typeof(string));
					
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
									Regex regex = new Regex(@"Wyd"); //@"\D"
									string result = regex.Replace(text, "");
									result = Regex.Replace(result, "ww", "WW");
									result = Regex.Replace(result, "wz", "WZ");
									result = Regex.Replace(result, @"oduWZ", "");
									result = Regex.Replace(result, "[a-z]" , "");
									result = Regex.Replace(result, @"[~`!@#$%^&\*()_+B-EG-RT-Uęóąśłżźćń;:'\|<.>?""\]\.\-]", "");
									result = Regex.Replace(result, "WZWZ", "WZ");
									for (int i = 1; i < 20; i++)
									{
										result = Regex.Replace(result, @"[a-z0-9A-Z]WZ/", "WZ/");
									}
									
									String[] documents;
									documents = result.Split(',');
									
									foreach (var document in documents)
									{
										if (document.Length > 11)
										{
											string docName = document.Remove(11).Replace("/", "_");
											documents2it.Rows.Add(docName);
										}
										else if (document.Length == 11)
										{
											string docName = document.Replace("/", "_");
											documents2it.Rows.Add(docName);
										}
										
									}
							}
					}
				var UniqueRows = documents2it.AsEnumerable().Distinct(DataRowComparer.Default);
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
				foreach (DataRow row in uniqDocNames.Rows)
				{
					string docName = row.Field<string>(0);
					if (docName.Contains("WZ_"))
					{
						string yearDoc = docName.Remove(startIndex:5).Replace("WZ_", "");
						if (File.Exists(Config.outPath + "Rok_20" + yearDoc + "\\" + docName +".pdf"))
						{
							File.Copy(pdfPath, Config.outPath + "Rok_20" + yearDoc + "\\do_polaczenia\\" + docName +".pdf");
							StreamWriter SW2;
							SW2 = File.AppendText(fileLogName);
							SW2.WriteLine(Config.outPath + "Rok_20" + yearDoc + "\\do_polaczenia\\" + docName +".pdf");
							SW2.Close();
						}
						else
						{
							File.Copy(pdfPath, Config.outPath + "Rok_20" + yearDoc + "\\" + docName +".pdf");
							StreamWriter SW3;
							SW3 = File.AppendText(fileLogName);
							SW3.WriteLine(Config.outPath + "Rok_20" + yearDoc + "\\" + docName +".pdf");
							SW3.Close();
						}
					}
					else if (docName.Contains("WW"))
					{
						string yearDoc = docName.Remove(startIndex:4).Replace("WW", "");
						if (File.Exists(Config.outPath + "Rok_20" + yearDoc + "\\WW\\" + docName +".pdf"))
						{
							File.Copy(pdfPath, Config.outPath + "Rok_20" + yearDoc + "\\do_polaczenia\\" + docName +".pdf");
							StreamWriter SW4;
							SW4 = File.AppendText(fileLogName);
							SW4.WriteLine(Config.outPath + "Rok_20" + yearDoc + "\\" + docName +".pdf");
							SW4.Close();
						}
						else
						{
							File.Copy(pdfPath, Config.outPath + "Rok_20" + yearDoc + "\\WW\\" + docName +".pdf");
							StreamWriter SW5;
							SW5 = File.AppendText(fileLogName);
							SW5.WriteLine(Config.outPath + "Rok_20" + yearDoc + "\\WW\\" + docName +".pdf");
							SW5.Close();
						}
					}
				}
						string przetworzone = pdfPath.Replace("po_ocr\\", "po_ocr\\przetworzone\\");
						File.Move(pdfPath, przetworzone);
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
