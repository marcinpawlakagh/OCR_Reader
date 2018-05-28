/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-09
 * Godzina: 13:34
 * 
 */
using System;
using System.IO;
using ImageMagick;
using System.Diagnostics;


namespace ocr_wz
{
	/// <summary>
	/// Description of pdf.
	/// </summary>
	public class Pdf
	{
		public string fileNamePDF;
		public Pdf(string scanName, string fileLogName)
		{
			conf Config = new conf();
			string fileName;
			if ((File.Exists(Config.inPath + "\\!ocr\\oryginal_files\\pdf\\" +scanName)) == true)
			{
				DateTime thisTime = DateTime.Now;
				string filesSurfix = thisTime.ToString().Replace(" ", "_").Replace("-", "").Replace(":","");
				string fileDuble = scanName.Replace(".pdf", "") + "_" + filesSurfix + ".pdf";
				
				File.Move(Config.inPath + "\\" + scanName, Config.inPath + "\\!ocr\\oryginal_files\\pdf\\" + fileDuble);
				
				MagickReadSettings settings = new MagickReadSettings();
				settings.Density = new Density(300,300);
				settings.Compression = Compression.Fax;
				using (MagickImageCollection filePDF = new MagickImageCollection())
				{
					filePDF.Read(Config.inPath + "\\!ocr\\oryginal_files\\pdf\\" + fileDuble, settings);
					fileName = fileDuble.Replace(".pdf", "");
					fileNamePDF = Config.inPath + "\\!ocr\\po_ocr\\" + fileName + ".pdf";
					filePDF.Write(Config.inPath + "\\!ocr\\tmp\\" +fileName+ ".tif");
				}
						ProcessStartInfo tesseract = new ProcessStartInfo();
						tesseract.WorkingDirectory = ".\\tesseract";
						tesseract.WindowStyle = ProcessWindowStyle.Hidden;
						tesseract.UseShellExecute = false;
						tesseract.FileName = "cmd.exe";
						tesseract.Arguments =
								"/c tesseract.exe " +
								"\"" + Config.inPath + "\\!ocr\\tmp\\" +fileName+ ".tif" + "\""+ " " +
								"\"" + Config.inPath + "\\!ocr\\po_ocr\\" +fileName+ "\"" +
								" -l " + "pol " + "pdf" ;
						// Start tesseract.
						Process process = Process.Start(tesseract);
						process.WaitForExit();
							if(System.IO.File.Exists(Config.inPath + "\\!ocr\\tmp\\" +fileName+ ".tif"))
							{
								try
								{
									System.IO.File.Delete(Config.inPath + "\\!ocr\\tmp\\" +fileName+ ".tif");
								}
								catch (System.IO.IOException)
								{
									return;
								}
							}
			}
			else
			{
				File.Move(Config.inPath + "\\" + scanName, Config.inPath + "\\!ocr\\oryginal_files\\pdf\\" +scanName);
				
				MagickReadSettings settings = new MagickReadSettings();
				settings.Density = new Density(300,300);
				settings.Compression = Compression.Fax;

				using (MagickImageCollection filePDF = new MagickImageCollection())
				{
					filePDF.Read(Config.inPath + "\\!ocr\\oryginal_files\\pdf\\" +scanName, settings);
					fileName = scanName.Replace(".pdf", "");
					fileNamePDF = Config.inPath + "\\!ocr\\po_ocr\\" + fileName + ".pdf";
					filePDF.Write(Config.inPath + "\\!ocr\\tmp\\" +fileName+ ".tif");
				}
				
						ProcessStartInfo tesseract = new ProcessStartInfo();
						tesseract.WorkingDirectory = ".\\tesseract";
						tesseract.WindowStyle = ProcessWindowStyle.Hidden;
						tesseract.UseShellExecute = false;
						tesseract.FileName = "cmd.exe";
						tesseract.Arguments =
								"/c tesseract.exe " +
								"\"" + Config.inPath + "\\!ocr\\tmp\\" +fileName+ ".tif" + "\""+ " " +
								"\"" + Config.inPath + "\\!ocr\\po_ocr\\" +fileName+ "\"" +
								" -l " + "pol " + "pdf" ;
						// Start tesseract.
						Process process = Process.Start(tesseract);
						process.WaitForExit();
							if(System.IO.File.Exists(Config.inPath + "\\!ocr\\tmp\\" +fileName+ ".tif"))
							{
								try
								{
									System.IO.File.Delete(Config.inPath + "\\!ocr\\tmp\\" +fileName+ ".tif");
								}
								catch (System.IO.IOException)
								{
									return;
								}
							}
			}
			StreamWriter SW;
			SW = File.AppendText(fileLogName);
			SW.WriteLine("Przetworzenie do przeszukiwalnego pliku PDF ...   OK");
			SW.Close();
			foreach (var file in Directory.GetFiles(Path.GetTempPath(), "*.*"))
			{
				try
				{
					File.Delete(file);
				}
				catch
				{
					
				}
			}
		}
	}
}