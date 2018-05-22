/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-02
 * Godzina: 11:10
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
	/// Description of Zas.
	/// </summary>
	public class Zas
	{
		string pdfName;
		public Zas(string fileNameTXT, string fileLogName)
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
								|| (text.Contains("za") && text.Contains("wie") && text.Contains("mer"))
								)
							{ 
									Regex regex = new Regex(@"Wyd"); //@"\D"
									string result = regex.Replace(text, "");
									result = Regex.Replace(result, @"oduWZ", "");
									result = Regex.Replace(result, "[a-z]" , "");
									result = Regex.Replace(result, @"[~`!@#$%^&\*()_+B-EG-RT-Uęóąśłżźćń;:'\|,<.>?""\]\.\-]", "");
									
									result = Regex.Replace(result, "2AS", "ZAS");
									
									if (result.Contains("ZAS"))
									{
										result = Regex.Replace(result, @"[a-z0-9A-Z]ZAS/", "ZAS/");
										int ileZnakow = result.Count();
										if (ileZnakow > 13)
										{
											result = result.Remove(13);
											result = Regex.Replace(result, "/", "_");
											docNames.Rows.Add(result);
										}
										else if(ileZnakow == 11)
										{
											result = Regex.Replace(result, "/", "_");
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
						StreamWriter SW1;
						SW1 = File.AppendText(fileLogName);
						SW1.WriteLine(row.Field<string>(0));
						SW1.Close();
					}
					
					int ileZAS = 0;
					foreach (DataRow row in uniqDocNames.Rows)
					{
						if (row.Field<string>(0).Contains("ZAS_"))
						{
							ileZAS++;
						}
					}
					if (ileZAS > 0)
					{
						DataTable endTable = new DataTable();
						foreach (DataRow row in uniqDocNames.Rows)
						{
							if (row.Field<string>(0).Contains("ZAS_"))
							{
								pdfName = fileNameTXT.Replace(".txt", ".pdf");
								string year = "0";
								string docName = row.Field<string>(0);
								Copy CopyNewName = new Copy(pdfName, year, docName, fileLogName);
								CopyNewName.CopyZAS();
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
								year = year.Replace("ZAS_", "");
								string docName = pdfName.Replace("C:\\ARCHIWUM_WZ\\!skany\\!ocr\\po_ocr\\", "");
								Copy CopyNewName = new Copy(pdfName, year, docName, fileLogName);
								CopyNewName.CopyZAS();
							
						}
						string przetworzone = pdfName.Replace("po_ocr\\", "po_ocr\\przetworzone\\");
						File.Move(pdfName, przetworzone);
					}
					
					fs.Close();
					File.Delete(fileNameTXT);
			}
			catch 
			{
				fs.Close();
				string year = "0";
				pdfName = fileNameTXT.Replace(".txt", ".pdf");
				string docName = pdfName.Replace(Config.inPath + "\\!ocr\\po_ocr\\", "");
				Copy CopyNewName = new Copy(pdfName, year, docName, fileLogName);
				CopyNewName.CopyOther();
				string przetworzone = pdfName.Replace("po_ocr\\", "po_ocr\\przetworzone\\");
				File.Move(pdfName, przetworzone);
				File.Delete(fileNameTXT);
			}
		}
	}
}
