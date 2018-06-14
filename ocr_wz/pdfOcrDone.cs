/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-25
 * Godzina: 07:43
 * 
 */
using System;
using System.IO;

namespace ocr_wz
{
	/// <summary>
	/// Description of pdfOcrDone.
	/// </summary>
	public class PdfOcrDone
	{
		public PdfOcrDone(string pdfName)
		{
            pdfName = pdfName.Replace(".txt", ".pdf");
			File.Move(pdfName, pdfName.Replace("po_ocr\\", "po_ocr\\przetworzone\\"));
            File.Delete(pdfName.Replace(".pdf", ".txt"));
		}
	}
}
