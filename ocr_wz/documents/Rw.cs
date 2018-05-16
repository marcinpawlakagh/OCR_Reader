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
			DataTable documents2it = new DataTable();
			documents2it.Columns.Add("WZ", typeof(string));
			string pdfName = fileNameTXT.Replace(".txt", ".pdf");
			
			
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
						string result = text.Replace("Arkusz:", "RW_");
						Regex regex = new Regex(@"Wyd");
						result = Regex.Replace(result, @"[~`!@#$%^&\*()+A-KM-OS-UZęóąśłżźćń;:'\|,<.>?""\]\.\-]", "");
						result = Regex.Replace(result, "LP/", ";LP/");
						String[] documents;
						documents = result.Split(';');
						foreach (var document in documents)
						{
							string docName = document.Replace("/", "_");
							
							if (docName.Contains("LP_"))
							{
								if (docName.Length > 11)
								{
									docName = docName.Remove(11);
									documents2it.Rows.Add(docName);
								}
								else
								{
									documents2it.Rows.Add(docName);
								}
							}
							else if (docName.Contains("RW_"))
							{
								if (docName.Length > 10)
								{
									docName = docName.Remove(10);
									documents2it.Rows.Add(docName);
								}
								else
								{
									documents2it.Rows.Add(docName);
								}
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
					
					int rw = 0;
					int lp = 0;
					foreach (DataRow row in uniqDocNames.Rows)
					{
						if (row.Field<string>(0).Contains("LP_"))
						{
							lp++;
							year = row.Field<string>(0).Remove(startIndex:5).Replace("LP_", "");
						}
						else if (row.Field<string>(0).Contains("RW_") && row.Field<string>(0).Length == 10)
						{
							rw++;
						}
						Console.WriteLine(row.Field<string>(0));
					}
					if (rw == lp || rw  > lp)
					{
						foreach (DataRow row in uniqDocNames.Rows)
						{
							string docName = row.Field<string>(0);
							if  (docName.Contains("RW_"))
							{
								Copy CopyNewName = new Copy(pdfName, year, docName, fileLogName);
								CopyNewName.CopyRW();
							}
						}
					}
					else
					{
						foreach (DataRow row in uniqDocNames.Rows)
						{
							string docName = row.Field<string>(0);
							if  (docName.Contains("LP_"))
							{
								Copy CopyNewName = new Copy(pdfName, year, docName, fileLogName);
								CopyNewName.CopyRW();
							}
						}
					}
					string przetworzone = pdfName.Replace("po_ocr\\", "po_ocr\\przetworzone\\");
					File.Move(pdfName, przetworzone);
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
