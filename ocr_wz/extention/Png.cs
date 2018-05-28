/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-18
 * Godzina: 11:53
 * 
 */
using System;
using System.IO;
using System.Diagnostics;

namespace ocr_wz
{
	/// <summary>
	/// Description of png.
	/// </summary>
	public class Png
	{
		public string fileNamePDF;
		public Png(string scanName, string fileLogName)
		{
			conf Config = new conf();
			
			if ((File.Exists(Config.inPath + "\\!ocr\\oryginal_files\\png\\" +scanName)) == true)
			{
				DateTime thisTime = DateTime.Now;
				string filesSurfix = thisTime.ToString().Replace(" ", "_").Replace("-", "").Replace(":","");
				string fileDuble = scanName.Replace(".png", "") + "_" + filesSurfix + ".png";
				string fileName = fileDuble.Replace(".png", "");
				fileNamePDF = Config.inPath + "\\!ocr\\po_ocr\\" + fileName + ".pdf";
				
				File.Move(Config.inPath + "\\" + scanName, Config.inPath + "\\!ocr\\oryginal_files\\png\\" + fileDuble);
				
						ProcessStartInfo tesseract = new ProcessStartInfo();
						tesseract.WorkingDirectory = ".\\tesseract";
						tesseract.WindowStyle = ProcessWindowStyle.Hidden;
						tesseract.UseShellExecute = false;
						tesseract.FileName = "cmd.exe";
						tesseract.Arguments =
								"/c tesseract.exe " +
								"\"" + Config.inPath + "\\!ocr\\oryginal_files\\png\\" +fileName+ ".png" + "\""+ " " +
								"\"" + Config.inPath + "\\!ocr\\po_ocr\\" +fileName+ "\"" +
								" -l " + "pol " + "pdf" ;
						// Start tesseract.
						Process process = Process.Start(tesseract);
						process.WaitForExit();
			}
			else
			{
				File.Move(Config.inPath + "\\" + scanName, Config.inPath + "\\!ocr\\oryginal_files\\png\\" +scanName);
				string fileName = scanName.Replace(".png", "");
				fileNamePDF = Config.inPath + "\\!ocr\\po_ocr\\" + fileName + ".pdf";
				
						ProcessStartInfo tesseract = new ProcessStartInfo();
						tesseract.WorkingDirectory = ".\\tesseract";
						tesseract.WindowStyle = ProcessWindowStyle.Hidden;
						tesseract.UseShellExecute = false;
						tesseract.FileName = "cmd.exe";
						tesseract.Arguments =
								"/c tesseract.exe " +
								"\"" + Config.inPath + "\\!ocr\\oryginal_files\\png\\" +fileName+ ".png" + "\""+ " " +
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