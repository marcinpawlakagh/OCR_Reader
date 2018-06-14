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
using System.Threading.Tasks;

namespace ocr_wz
{
	public class Program
	{
		public static void Main(string[] args)
		{
            //Console.WriteLine("Program do automatycznego przetwarzania zeskanowanych dokumentów wykonany przez Marcin Pawlak tel. 797-155-154");
            //ExistPath CheckIn = new ExistPath();
            ReadTxt reading = new ReadTxt("C:\\ARCHIWUM_WZ\\!skany\\!ocr\\po_ocr\\WZ_18_03298.txt", "C:\\ARCHIWUM_WZ\\!skany\\!ocr\\logi\\files_20180516_115210.txt");
            //conf Config = new conf();
            //DateTime thisTime = DateTime.Now;
            //string filesSurfix = thisTime.ToString().Replace(" ", "_").Replace("-", "").Replace(":", "");
            //string fileLogName;
            //try
            //{
            //    string[] extensions = new[] { ".jpg", ".png", ".tif", ".tiff", ".pdf", ".gif", ".bmp" }; //tworzę listę plików do przetworzenia
            //    string[] files = Directory.GetFiles(Config.inPath, "*.*")
            //        .Select(Path.GetFileName)
            //        .Where(f => extensions.Contains(new FileInfo(f).Extension.ToLower())).ToArray();
            //    fileLogName = Config.inPath + "\\!ocr\\logi\\files_" + filesSurfix + ".txt"; //Nazwa pliku txt z zawartością skanów (files_RRRRMMDD_HHMMSS.txt)
            //    string pathLogName = fileLogName.Replace(".txt", "");
            //    Directory.CreateDirectory(pathLogName);
            //    foreach (string file in files)
            //    {
            //        File.WriteAllLines(fileLogName, files);
            //    }
            //    int lines = File.ReadAllLines(fileLogName).Length;
            //    Console.WriteLine("Wyeksportowano listę z " + lines.ToString() + " plikami do " + Config.inPath + fileLogName);
            //    string[] linesFileLog = File.ReadAllLines(fileLogName);
            //    foreach (string fileName in linesFileLog)
            //    {
            //        string fileLogNameDone = pathLogName + "\\" + fileName + ".txt";// !!sprawdzić działanie!!
            //        var t = new Task(() => { Scan Scan = new Scan(fileName, fileLogNameDone); });
            //        t.Start();
            //    }
            //}
            //catch (Exception)
            //{
            //    File.WriteAllText(Config.inPath + "\\!ocr\\logi\\error_" + filesSurfix + ".txt", "Brak plików do przetworzenia!"); //zapisuje log z informacją, że nie było plików do przetworzenia
            //}

            //foreach (var file in Directory.GetFiles(Path.GetTempPath(), "*.*"))
            //{
            //    try
            //    {
            //        File.Delete(file);
            //    }
            //    catch
            //    {

            //    }
            //}
			
			Console.ReadKey();
			// pętla sprawdza folder tmp jeżeli są jakieś pliki to sprawdza czy istnie plik pdf w lokalizacji po_pdf, jeżeli istnieje to usuń.
			//dopisać warunek jeżeli w \po_ocr\ są pliki 0kb przenies oryginały do skany
		}
	}
}