/*
 * Wykonał Marcin Pawlak
 * Użytkownik: mpawlak
 * Data: 2018-03-26
 * Godzina: 15:04
 */

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using ImageMagick;
using System.Threading.Tasks;

namespace ocr_wz
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.SetWindowSize(110, 30);
			ExistPath CheckIn = new ExistPath();
//			ReadTxt reading = new ReadTxt("C:\\ARCHIWUM_WZ\\!skany\\!ocr\\po_ocr\\WWWZ.txt", "C:\\ARCHIWUM_WZ\\!skany\\!ocr\\logi\\files_20180516_115210.txt");
//			Console.WriteLine("Program do automatycznego przetwarzania skanów dokumentów wykonany przez Marcin Pawlak tel. 797-155-154");
//			conf Config = new conf();	// pobieram konfigurację katalogów wynikowych i źródłowych
//			DateTime thisTime = DateTime.Now;
//			string filesSurfix = thisTime.ToString().Replace(" ", "_").Replace("-", "").Replace(":","");
//			string fileLogName;
//			try
//			{
//				string [] extensions = new [] { ".jpg", ".png", ".tif", ".tiff", ".pdf", ".gif", ".bmp" }; //tworzę listę plików do przetworzenia
//				string [] files = Directory.GetFiles(Config.inPath, "*.*")
//					.Select(Path.GetFileName)
//					.Where(f => extensions.Contains(new FileInfo(f).Extension.ToLower())).ToArray();
//					fileLogName = Config.inPath + "\\!ocr\\logi\\files_" + filesSurfix + ".txt"; //Nazwa pliku txt z zawartością skanów (files_RRRRMMDD_HHMMSS.txt)
//						foreach(string file in files)
//						{
//						File.WriteAllLines(fileLogName, files);
//						}
//					int lines = File.ReadAllLines(fileLogName).Length;
//					Console.WriteLine("Wyeksportowano listę z " + lines.ToString() + " plikami do " + Config.inPath + fileLogName);
//			
//					string [] linesFileLog = File.ReadAllLines(fileLogName);
//						foreach (string fileName in linesFileLog)
//						{
//						Scan Scan = new Scan(fileName, fileLogName);
//						}
//			}
//			catch (Exception)
//			{
//				File.WriteAllText(Config.inPath + "\\!ocr\\logi\\error_" + filesSurfix + ".txt", "Brak plików do przetworzenia!"); //zapisuje log z informacją, że nie było plików do przetworzenia
//			}
				MagickReadSettings settings = new MagickReadSettings();
				settings.Density = new Density(300,300);
				settings.Compression = Compression.Fax;
				var t1 = new Task( () => {MagickImageCollection filePDF = new MagickImageCollection();
										filePDF.Read("C:\\ARCHIWUM_WZ\\!skany\\F_18_04546.pdf", settings);
										filePDF.Write("C:\\ARCHIWUM_WZ\\!skany\\F_18_04546.tif");
										});
				var t2 = new Task( () => {MagickImageCollection filePDF1 = new MagickImageCollection();
										filePDF1.Read("C:\\ARCHIWUM_WZ\\!skany\\raben.pdf", settings);
										filePDF1.Write("C:\\ARCHIWUM_WZ\\!skany\\raben.tif");});
				
				t1.Start();
				t2.Start();
						
				
			Console.ReadKey();
		}
	}
}