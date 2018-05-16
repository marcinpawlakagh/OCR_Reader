/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-23
 * Godzina: 11:01
 * 
 */
using System;
using System.Diagnostics;

namespace ocr_wz
{
	/// <summary>
	/// Description of pdftotext.
	/// </summary>
	public class PdfToText
	{	public string fileNameTXT;
		public PdfToText(string fileNamePDF)
		{
						ProcessStartInfo pdf2txt = new ProcessStartInfo();
						pdf2txt.WorkingDirectory = ".\\tesseract";
						pdf2txt.WindowStyle = ProcessWindowStyle.Hidden;
						pdf2txt.UseShellExecute = false;
						pdf2txt.FileName = "cmd.exe";
						pdf2txt.Arguments =
								"/c pdftotext.exe " + "-table -enc UTF-8 " +
								"\"" + fileNamePDF;
						// Start tesseract.
						Process process = Process.Start(pdf2txt);
						process.WaitForExit();
						fileNameTXT = fileNamePDF.Replace(".pdf", ".txt");
		}
	}
}
