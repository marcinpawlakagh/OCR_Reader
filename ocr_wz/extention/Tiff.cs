/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-18
 * Godzina: 13:00
 * 
 */
using System;
using System.IO;
using System.Diagnostics;

namespace ocr_wz
{
	/// <summary>
	/// Description of tiff.
	/// </summary>
	public class Tiff
	{
		public string fileNamePDF;
		public Tiff(string scanName, string fileLogName)
		{
			conf Config = new conf();
			
			if ((File.Exists(Config.inPath + "\\!ocr\\oryginal_files\\tiff\\" +scanName)) == true)
			{
				DateTime thisTime = DateTime.Now;
				string filesSurfix = thisTime.ToString().Replace(" ", "_").Replace("-", "").Replace(":","");
				string fileDuble = scanName.Replace(".tiff", "") + "_" + filesSurfix + ".tiff";
				string fileName = fileDuble.Replace(".tiff", "");
				fileNamePDF = Config.inPath + "\\!ocr\\po_ocr\\" + fileName + ".pdf";
				
				File.Move(Config.inPath + "\\" + scanName, Config.inPath + "\\!ocr\\oryginal_files\\tiff\\" + fileDuble);
				
						ProcessStartInfo tesseract = new ProcessStartInfo();
						tesseract.WorkingDirectory = ".\\tesseract";
						tesseract.WindowStyle = ProcessWindowStyle.Hidden;
						tesseract.UseShellExecute = false;
						tesseract.FileName = "cmd.exe";
						tesseract.Arguments =
								"/c tesseract.exe " +
								"\"" + Config.inPath + "\\!ocr\\oryginal_files\\tiff\\" +fileName+ ".tiff" + "\""+ " " +
								"\"" + Config.inPath + "\\!ocr\\po_ocr\\" +fileName+ "\"" +
								" -l " + "pol " + "pdf" ;
						// Start tesseract.
						Process process = Process.Start(tesseract);
						process.WaitForExit();
			}
			else
			{
				File.Move(Config.inPath + "\\" + scanName, Config.inPath + "\\!ocr\\oryginal_files\\tiff\\" +scanName);
				string fileName = scanName.Replace(".tiff", "");
				fileNamePDF = Config.inPath + "\\!ocr\\po_ocr\\" + fileName + ".pdf";
				
						ProcessStartInfo tesseract = new ProcessStartInfo();
						tesseract.WorkingDirectory = ".\\tesseract";
						tesseract.WindowStyle = ProcessWindowStyle.Hidden;
						tesseract.UseShellExecute = false;
						tesseract.FileName = "cmd.exe";
						tesseract.Arguments =
								"/c tesseract.exe " +
								"\"" + Config.inPath + "\\!ocr\\oryginal_files\\tiff\\" +fileName+ ".tiff" + "\""+ " " +
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