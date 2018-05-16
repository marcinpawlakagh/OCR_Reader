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
	/// Description of fv.
	/// </summary>
	public class Fv
	{
		public Fv(string fileNameTXT, string fileLogName)
		{
			string pdfName = fileNameTXT.Replace(".txt", ".pdf");
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
						  text.Contains("F/")
					    || text.Contains("Nu") && text.Contains("dow") && text.Contains("du")
					   )
					{
						if (text.Contains("F/"))
						{
							if (text.Length > 10)
							{
								text = text.Remove(10);
								docNames.Rows.Add(text);
							}
							else
							{
								docNames.Rows.Add(text);
							}
						}
						else if (text.Contains("Nu") && text.Contains("dow") && text.Contains("du"))
						{
							Regex regex = new Regex(@"Wyd"); //@"\D"
							string result = regex.Replace(text, "");
							result = Regex.Replace(result, @"oduWZ", "");
							result = Regex.Replace(result, "[a-z]" , "");
							result = Regex.Replace(result, @"[~`!@#$%^&\*()_+B-EG-RT-Uęóąśłżźćń;:'\|,<.>?""\]\.\-]", "");
							result = Regex.Replace(result, @"[a-z0-9A-Z]WZ/", "WZ/");
							if (result.Length > 11)
							{
								result = result.Remove(11);
								docNames.Rows.Add(result);
							}
							else
							{
								docNames.Rows.Add(result);
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
					StreamWriter SW2;
					SW2 = File.AppendText(fileLogName);
					SW2.WriteLine(row.Field<string>(0));
					SW2.Close();
				}
				int ileFV = 0;
				int ileWZ = 0;
				foreach (DataRow row in uniqDocNames.Rows)
				{
					string docName = row.Field<string>(0).Replace("/", "_");
					if (docName.Contains("F_"))
					{
						ileFV++;
					}
					else if (docName.Contains("WZ_"))
					{
						ileWZ++;
					}
					Console.WriteLine(docName);
				}
				Console.WriteLine("Fv = " + ileFV + "    WZ = " + ileWZ);
				
				if (ileFV == ileWZ || ileWZ > ileFV)
				{
					foreach (DataRow row in uniqDocNames.Rows)
					{
						string docName = row.Field<string>(0).Replace("/", "_");
						if (docName.Contains("WZ_"))
						{
							string year = row.Field<string>(0).Remove(5);
							year = year.Replace("WZ/", "");
							Copy CopyNewName = new Copy(pdfName, year, docName, fileLogName);
							CopyNewName.CopyFV();
						}
					}
				}
				else
				{
					foreach (DataRow row in uniqDocNames.Rows)
					{
						string docName = row.Field<string>(0).Replace("/", "_");
						if (docName.Contains("F_"))
						{
							string year = row.Field<string>(0).Remove(4);
							year = year.Replace("F/", "");
							try
							{
								StreamWriter SW3;
								SW3 = File.AppendText(fileLogName);
								SW3.WriteLine(Config.outPath +"Rok_20"+ year + "\\FV\\" + docName + ".pdf");
								SW3.Close();
								File.Copy(pdfName, Config.outPath +"Rok_20"+ year + "\\FV\\" + docName + ".pdf" );
							}
							catch (IOException)
							{
								FileInfo infoFirst = new FileInfo(Config.outPath +"Rok_20"+ year + "\\FV\\" + docName + ".pdf");
								long byteFirst = infoFirst.Length;
								FileInfo infoSecond = new FileInfo(pdfName);
								long byteSecond = infoSecond.Length;
								if (byteFirst != byteSecond)
								{
									StreamWriter SW4;
									SW4 = File.AppendText(fileLogName);
									SW4.WriteLine(Config.outPath +"Rok_20"+ year + "\\duplikaty\\" + docName + ".pdf");
									SW4.Close();
									File.Copy(pdfName, Config.outPath +"Rok_20"+ year + "\\duplikaty\\" + docName + ".pdf" );
								}
							}
						}
					}
				}
					string przetworzone = pdfName.Replace("po_ocr\\", "po_ocr\\przetworzone\\");
					File.Move(pdfName, przetworzone);
					fs.Close();
					File.Delete(fileNameTXT);
			}
			catch(Exception ex)
			{
				Console.WriteLine("Nie odnaleziono pliku txt!" + ex);
			}
		}
	}
}
