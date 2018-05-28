/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-18
 * Godzina: 13:59
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
	public class Gif
	{
		public string fileNamePDF;
		public Gif(string scanName, string fileLogName)
		{
			conf Config = new conf();
			
			if ((File.Exists(Config.inPath + "\\!ocr\\oryginal_files\\gif\\" +scanName)) == true)
			{
				DateTime thisTime = DateTime.Now;
				string filesSurfix = thisTime.ToString().Replace(" ", "_").Replace("-", "").Replace(":","");
				string fileDuble = scanName.Replace(".gif", "") + "_" + filesSurfix + ".gif";
				string fileName = fileDuble.Replace(".gif", "");
				fileNamePDF = Config.inPath + "\\!ocr\\po_ocr\\" + fileName + ".pdf";
				
				File.Move(Config.inPath + "\\" + scanName, Config.inPath + "\\!ocr\\oryginal_files\\gif\\" + fileDuble);
				
						ProcessStartInfo tesseract = new ProcessStartInfo();
						tesseract.WorkingDirectory = ".\\tesseract";
						tesseract.WindowStyle = ProcessWindowStyle.Hidden;
						tesseract.UseShellExecute = false;
						tesseract.FileName = "cmd.exe";
						tesseract.Arguments =
								"/c tesseract.exe " +
								"\"" + Config.inPath + "\\!ocr\\oryginal_files\\gif\\" +fileName+ ".gif" + "\""+ " " +
								"\"" + Config.inPath + "\\!ocr\\po_ocr\\" +fileName+ "\"" +
								" -l " + "pol " + "pdf" ;
						// Start tesseract.
						Process process = Process.Start(tesseract);
						process.WaitForExit();
			}
			else
			{
				File.Move(Config.inPath + "\\" + scanName, Config.inPath + "\\!ocr\\oryginal_files\\gif\\" +scanName);
				string fileName = scanName.Replace(".gif", "");
				fileNamePDF = Config.inPath + "\\!ocr\\po_ocr\\" + fileName + ".pdf";
				
						ProcessStartInfo tesseract = new ProcessStartInfo();
						tesseract.WorkingDirectory = ".\\tesseract";
						tesseract.WindowStyle = ProcessWindowStyle.Hidden;
						tesseract.UseShellExecute = false;
						tesseract.FileName = "cmd.exe";
						tesseract.Arguments =
								"/c tesseract.exe " +
								"\"" + Config.inPath + "\\!ocr\\oryginal_files\\gif\\" +fileName+ ".gif" + "\""+ " " +
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