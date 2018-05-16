/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-09
 * Godzina: 12:31
 * 
 */
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ocr_wz
{ 

	public class Scan
	{
		public Scan(string scanName, string fileLogName)
		{
			StreamWriter SW;
			SW = File.AppendText(fileLogName);
			SW.WriteLine("");
			SW.WriteLine("..............................................................................");
			SW.WriteLine(scanName);
			SW.Close();
			
			if (scanName.Contains(".pdf"))
				{
				Pdf scanPDF = new Pdf(scanName, fileLogName);
				PdfToText PdfToText = new PdfToText(scanPDF.fileNamePDF);
				ReadTxt reading = new ReadTxt(PdfToText.fileNameTXT, fileLogName);
				}
			else if (scanName.Contains(".png"))
					{
					Png scanPNG = new Png(scanName, fileLogName);
					PdfToText PdfToText = new PdfToText(scanPNG.fileNamePDF);
					ReadTxt reading = new ReadTxt(PdfToText.fileNameTXT, fileLogName);
					}
			else if (scanName.Contains(".tiff"))
					{
					Tiff scanTIFF = new Tiff(scanName, fileLogName);
					PdfToText PdfToText = new PdfToText(scanTIFF.fileNamePDF);
					ReadTxt reading = new ReadTxt(PdfToText.fileNameTXT, fileLogName);
					}
			else if (scanName.Contains(".tif"))
					{
					Tif scanTIF = new Tif(scanName, fileLogName);
					PdfToText PdfToText = new PdfToText(scanTIF.fileNamePDF);
					ReadTxt reading = new ReadTxt(PdfToText.fileNameTXT, fileLogName);
					}
			else if (scanName.Contains(".jpg"))
					{
					Jpg scanJPG = new Jpg(scanName, fileLogName);
					PdfToText PdfToText = new PdfToText(scanJPG.fileNamePDF);
					ReadTxt reading = new ReadTxt(PdfToText.fileNameTXT, fileLogName);
					}
			else if (scanName.Contains(".gif"))
					{
					Gif scanGIF = new Gif(scanName, fileLogName);
					PdfToText PdfToText = new PdfToText(scanGIF.fileNamePDF);
					ReadTxt reading = new ReadTxt(PdfToText.fileNameTXT, fileLogName);
					}
			else if (scanName.Contains(".bmp"))
					{
					Bmp scanBMP = new Bmp(scanName, fileLogName);
					PdfToText PdfToText = new PdfToText(scanBMP.fileNamePDF);
					ReadTxt reading = new ReadTxt(PdfToText.fileNameTXT, fileLogName);
					}
		}
		
	}
}
