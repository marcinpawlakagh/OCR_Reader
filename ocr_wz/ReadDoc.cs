/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-24
 * Godzina: 13:50
 * 
 */
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data;

namespace ocr_wz
{
	/// <summary>
	/// Description of read_doc.
	/// </summary>
	public class ReadDoc
	{
		public ReadDoc()
		{
			FileStream fs = new FileStream("C:\\ARCHIWUM_WZ\\!skany\\!ocr\\po_ocr\\F_18_04546.txt",
			FileMode.Open, FileAccess.ReadWrite);
			DataTable docNames = new DataTable();
			docNames.Columns.Add("WZ", typeof(string));
			//docNames.Columns.Add("ZAS", typeof(string));
			
				try
				{
					StreamReader sr = new StreamReader(fs);
					int ileWZ = 0;
					
					while (!sr.EndOfStream)
					{
						string text = sr.ReadLine().Replace(" ", "");
							if (
								text.Contains("mówienie")
								| (text.Contains("ze") && text.Contains("wn") && text.Contains("me"))
								| (text.Contains("trz") && text.Contains("num"))
								| text.Contains("ydanie")
								| text.Contains("numer:")
								| text.Contains("WZ/")
								| text.Contains("WW 17")
								| text.Contains("WW 18")
								| (text.Contains("F/"))
								| (text.Contains("mer") && text.Contains("dow"))
								)
						{ 
									Regex regex = new Regex(@"Wyd"); //@"\D"
									string result = regex.Replace(text, "");
									result = Regex.Replace(result, @"oduWZ", "");
									result = Regex.Replace(result, "[a-z]" , "");
									result = Regex.Replace(result, @"[~`!@#$%^&\*()_+B-EG-RT-Uęóąśłżźćń;:'\|,<.>?""\]\.\-]", "");
									
									result = Regex.Replace(result, "2AS", "ZAS");
									
									Regex checkWZ =  new Regex(@"^WZ_[0-9][0-9]_[0-9][0-9][0-9][0-9][0-9]$");
									bool validation = checkWZ.IsMatch(result);
									
									//Console.WriteLine(result);
									
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
												ileZnakow = result.Count();
												Console.WriteLine(result + "  " + ileZnakow + "  " + "Wydanie Zewnętrzne");
												docNames.Rows.Add(result);
											}
											else
											{
												licznikWZ = licznikWZ.Remove(5);
												result = result.Remove(startIndex:6) + licznikWZ;
												ileZnakow = result.Count();
												Console.WriteLine(result + "  " + ileZnakow + "  " + "Wydanie Zewnętrzne");
												docNames.Rows.Add(result);
											}
											
										}
										else if(ileZnakow == 11)
										{
											Console.WriteLine(result + "  " + ileZnakow + "  " + "Wydanie Zewnętrzne");
											docNames.Rows.Add(result);
										}
									}
									else if(result.Contains("ZAS"))
									{
										result = Regex.Replace(result, @"[a-z0-9A-Z]ZAS/", "ZAS/");
										result = result.Remove(startIndex:13);
										int ileZnakow = result.Count();
										Console.WriteLine(result + "  " + ileZnakow + "  " + "Zamówienie Sprzedaży");
										docNames.Rows.Add(result);
									}
									else if(result.Contains("WW"))
									{
										int ileZnakow = result.Count();
										Console.WriteLine(result + "  " + ileZnakow + "  " + "Wydanie Wewnętrzne");
										docNames.Rows.Add(result);
									}
									else if(result.Contains("F/"))
									{
										int ileZnakow = result.Count();
										if (ileZnakow > 10)
										{
											result = result.Remove(10);
										}
										Console.WriteLine(result + "  " + ileZnakow + "  " + "Faktura Vat");
										docNames.Rows.Add(result);
									}
									ileWZ++;
									
								}
					}
					Console.WriteLine("..................................");

					var UniqueRows = docNames.AsEnumerable().Distinct(DataRowComparer.Default);
					DataTable uniqDocNames = UniqueRows.CopyToDataTable();
					foreach (DataRow row in uniqDocNames.Rows)
					{
						Console.WriteLine(row.Field<string>(0));
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Nie odnaleziono pliku txt!" + ex);
				}
		}
	}
}
