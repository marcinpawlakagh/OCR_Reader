/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-26
 * Godzina: 11:13
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
	/// Description of WzWw.
	/// </summary>
	public class WzWw
	{
		public WzWw(string fileNameTXT, string fileLogName)
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
					|| (text.Contains("ze") && text.Contains("wn") && text.Contains("me"))
					|| (text.Contains("trz") && text.Contains("num"))
					|| text.Contains("ydanie")
					|| text.Contains("numer:")
					|| text.Contains("WW1")
					|| text.Contains("WZ/")
					)
					{
						Regex regex = new Regex(@"Wyd"); //@"\D"
						string result = regex.Replace(text, "");
						result = Regex.Replace(result, @"oduWZ", "");
						result = Regex.Replace(result, "[a-z]" , "");
						result = Regex.Replace(result, @"[~`!@#$%^&\*()_+B-EG-RT-Uęóąśłżźćń;:'\|,<.>?""\]\.\-]", "");
						result = Regex.Replace(result, "2AS", "ZAS");
						
						if (result.Contains("WZ"))
						{
							result = Regex.Replace(result, @"[a-z0-9A-Z]WZ/", "WZ/");
							int ileZnakow = result.Count();
							string licznikWZ;
							if (ileZnakow > 11)
							{
								licznikWZ = Regex.Replace(result, @"WZ/[0-9][0-9]/", "");
								int licznikWZCount = licznikWZ.Count();
								if (licznikWZCount > 6)
								{
									licznikWZ = licznikWZ.Remove(6);
								}
								int licznik = int.Parse(licznikWZ);
								
								if (licznik > 99999)
								{
									licznikWZ = Regex.Replace(licznikWZ, @"^1" , "");
									result = result.Remove(startIndex:6) + licznikWZ;
									result = Regex.Replace(result, "/", "_");
									ileZnakow = result.Count();
									docNames.Rows.Add(result);
								}
								else
								{
									licznikWZ = licznikWZ.Remove(5);
									result = result.Remove(startIndex:6) + licznikWZ;
									result = Regex.Replace(result, "/", "_");
									ileZnakow = result.Count();
									docNames.Rows.Add(result);
								}
								
							}
							else if(ileZnakow == 11)
							{
								result = Regex.Replace(result, "/", "_");
								docNames.Rows.Add(result);
							}
							
						}
						else if (result.Contains("WW"))
						{
							result = Regex.Replace(result, @"[a-z0-9A-Z]WW", "WW");
							int ileZnakow = result.Count();
							string licznikWW;
							if (ileZnakow > 11)
							{
								licznikWW = Regex.Replace(result, @"WW[0-9][0-9]/", "");
								int licznikWZCount = licznikWW.Count();
								if (licznikWZCount > 6)
								{
									licznikWW = licznikWW.Remove(6);
								}
								int licznik = int.Parse(licznikWW);
							
								if (licznik > 999999)
								{
									licznikWW = Regex.Replace(licznikWW, @"^1" , "");
									result = result.Remove(startIndex:6) + licznikWW;
									result = Regex.Replace(result, "/", "_");
									ileZnakow = result.Count();
									docNames.Rows.Add(result);
								}
							
								else
								{
									licznikWW = licznikWW.Remove(5);
									result = result.Remove(startIndex:6) + licznikWW;
									result = Regex.Replace(result, "/", "_");
									ileZnakow = result.Count();
									docNames.Rows.Add(result);
								}
							}
							else if(ileZnakow == 11)
							{
								result = Regex.Replace(result, "/", "_");
								docNames.Rows.Add(result);
							}
						}
						else if(result.Contains("ZAS"))
						{
							result = Regex.Replace(result, @"[a-z0-9A-Z]ZAS/", "ZAS/");
							result = result.Remove(startIndex:13);
							result = Regex.Replace(result, "/", "_");
							int ileZnakow = result.Count();
							docNames.Rows.Add(result);
						}
					}
				}
				var UniqueRows = docNames.AsEnumerable().Distinct(DataRowComparer.Default);
				DataTable uniqDocNames = UniqueRows.CopyToDataTable();
//				StreamWriter SW;
//				SW = File.AppendText(fileLogName);
//				SW.WriteLine("Tablica dokumentów:");
//				SW.Close();
					
				foreach (DataRow row in uniqDocNames.Rows)
				{
					Console.WriteLine(row.Field<string>(0));
//					StreamWriter SW1;
//					SW1 = File.AppendText(fileLogName);
//					SW1.WriteLine(row.Field<string>(0));
//					SW1.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Nie odnaleziono pliku txt!" + ex);
			}
			
			Console.ReadKey();
		}
	}
}
