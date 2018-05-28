/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-18
 * Godzina: 13:55
 * 
 */
using System;
using System.IO;
using System.Diagnostics;

namespace ocr_wz
{
	/// <summary>
	/// Description of tif.
	/// </summary>
	public class Jpg
	{
		public string fileNamePDF;
		public Jpg(string scanName, string fileLogName)
		{
			conf Config = new conf();
			
			if ((File.Exists(Config.inPath + "\\!ocr\\oryginal_files\\jpg\\" +scanName)) == true)
			{
				DateTime thisTime = DateTime.Now;
				string filesSurfix = thisTime.ToString().Replace(" ", "_").Replace("-", "").Replace(":","");
				string fileDuble = scanName.Replace(".jpg", "") + "_" + filesSurfix + ".jpg";
				string fileName = fileDuble.Replace(".jpg", "");
				fileNamePDF = Config.inPath + "\\!ocr\\po_ocr\\" + fileName + ".pdf";
				
				File.Move(Config.inPath + "\\" + scanName, Config.inPath + "\\!ocr\\oryginal_files\\jpg\\" + fileDuble);
				
						ProcessStartInfo tesseract = new ProcessStartInfo();
						tesseract.WorkingDirectory = ".\\tesseract";
						tesseract.WindowStyle = ProcessWindowStyle.Hidden;
						tesseract.UseShellExecute = false;
						tesseract.FileName = "cmd.exe";
						tesseract.Arguments =
								"/c tesseract.exe " +
								"\"" + Config.inPath + "\\!ocr\\oryginal_files\\jpg\\" +fileName+ ".jpg" + "\""+ " " +
								"\"" + Config.inPath + "\\!ocr\\po_ocr\\" +fileName+ "\"" +
								" -l " + "pol " + "pdf" ;
						// Start tesseract.
						Process process = Process.Start(tesseract);
						process.WaitForExit();
			}
			else
			{
				File.Move(Config.inPath + "\\" + scanName, Config.inPath + "\\!ocr\\oryginal_files\\jpg\\" +scanName);
				string fileName = scanName.Replace(".jpg", "");
				fileNamePDF = Config.inPath + "\\!ocr\\po_ocr\\" + fileName + ".pdf";
				
						ProcessStartInfo tesseract = new ProcessStartInfo();
						tesseract.WorkingDirectory = ".\\tesseract";
						tesseract.WindowStyle = ProcessWindowStyle.Hidden;
						tesseract.UseShellExecute = false;
						tesseract.FileName = "cmd.exe";
						tesseract.Arguments =
								"/c tesseract.exe " +
								"\"" + Config.inPath + "\\!ocr\\oryginal_files\\jpg\\" +fileName+ ".jpg" + "\""+ " " +
								"\"" + Config.inPath + "\\!ocr\\po_ocr\\" +fileName+ "\"" +
								" -l " + "pol " + "pdf" ;
						// Start tesseract.
						Process process = Process.Start(tesseract);
						process.WaitForExit();
			}
			StreamWriter SW;
			SW = File.AppendText(fileLogName);
			SW.WriteLine("Przetworzenie do przeszukiwalnego pliku PDF ...   OK");
			SW.Close();
		}
	}
}
