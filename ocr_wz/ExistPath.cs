/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-30
 * Godzina: 08:43
 * 
 */
using System;
using System.IO;
using System.Collections;

namespace ocr_wz
{
	/// <summary>
	/// Description of ExistPath.
	/// </summary>
	public class ExistPath
	{
		public ExistPath()
		{
			conf Config = new conf();
			string[] path;
			path = new string[35];
			path[0] = Config.inPath;
			path[1] = Config.inPath + "\\!do_przetworzenia_recznego";
			path[2] = Config.inPath + "\\!do_przetworzenia_recznego\\duplikaty";
			path[3] = Config.inPath + "\\!ocr";
			path[4] = Config.inPath + "\\!ocr\\logi";
			path[5] = Config.inPath + "\\!ocr\\oryginal_files";
			path[6] = Config.inPath + "\\!ocr\\oryginal_files\\bmp";
			path[7] = Config.inPath + "\\!ocr\\oryginal_files\\gif";
			path[8] = Config.inPath + "\\!ocr\\oryginal_files\\jpg";
			path[9] = Config.inPath + "\\!ocr\\oryginal_files\\pdf";
			path[10] = Config.inPath + "\\!ocr\\oryginal_files\\png";
			path[11] = Config.inPath + "\\!ocr\\oryginal_files\\tif";
			path[12] = Config.inPath + "\\!ocr\\oryginal_files\\tiff";
			path[13] = Config.inPath + "\\!ocr\\po_ocr";
			path[14] = Config.inPath + "\\!ocr\\po_ocr\\przetworzone";
			path[15] = Config.inPath + "\\!ocr\\tmp";
			path[16] = Config.outPath;
			DateTime date = DateTime.Today;
			int year = date.Year;
			path[17] = Config.outPath + "\\Rok_" + (year - 1).ToString();
			path[18] = Config.outPath + "\\Rok_" + (year - 1).ToString() + "\\duplikaty";
			path[19] = Config.outPath + "\\Rok_" + (year - 1).ToString() + "\\WW";
			path[20] = Config.outPath + "\\Rok_" + (year - 1).ToString() + "\\FV";
			path[21] = Config.outPath + "\\Rok_" + (year - 1).ToString() + "\\RW";
			path[22] = Config.outPath + "\\Rok_" + (year - 1).ToString() + "\\do_polaczenia";
			path[23] = Config.outPath + "\\Rok_" + year.ToString();
			path[24] = Config.outPath + "\\Rok_" + year.ToString() + "\\duplikaty";
			path[25] = Config.outPath + "\\Rok_" + year.ToString() + "\\WW";
			path[26] = Config.outPath + "\\Rok_" + year.ToString() + "\\FV";
			path[27] = Config.outPath + "\\Rok_" + year.ToString() + "\\RW";
			path[28] = Config.outPath + "\\Rok_" + year.ToString() + "\\do_polaczenia";
			path[29] = Config.outPath + "\\Rok_" + (year + 1).ToString();
			path[30] = Config.outPath + "\\Rok_" + (year + 1).ToString() + "\\duplikaty";
			path[31] = Config.outPath + "\\Rok_" + (year + 1).ToString() + "\\WW";
			path[32] = Config.outPath + "\\Rok_" + (year + 1).ToString() + "\\FV";
			path[33] = Config.outPath + "\\Rok_" + (year + 1).ToString() + "\\RW";
			path[34] = Config.outPath + "\\Rok_" + (year + 1).ToString() + "\\do_polaczenia";
			
			foreach (string pathElement in path)
			{
				if (Directory.Exists(pathElement))
				{
					
					Console.Write("Istnienie folderu " + pathElement + "\t");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write("OK");
					Console.ResetColor();
					Console.WriteLine();
				}
				else
				{
					DirectoryInfo inPath = Directory.CreateDirectory(pathElement);
					Console.WriteLine("Utworzono folder: " + pathElement);
				}
			}

		}
	}
}
