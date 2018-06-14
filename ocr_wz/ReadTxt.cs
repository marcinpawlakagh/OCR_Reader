/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-25
 * Godzina: 14:42
 * 
 */
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data;

namespace ocr_wz
{
	/// <summary>
	/// Description of ReadTxt.
	/// </summary>
	public class ReadTxt
	{
		public ReadTxt(string fileNameTXT, string fileLogName)
		{ 
			try
			{
				if (File.Exists(fileNameTXT))
				{
					FileStream fs = new FileStream(fileNameTXT,
					FileMode.Open, FileAccess.ReadWrite);
			
					StreamReader sr = new StreamReader(fs);
					int wz = 0;
					int ww = 0;
					int fv = 0;
					int rw = 0;
					int raben = 0;
					int schenker = 0;
					while (!sr.EndOfStream)
					{
						string text1 = sr.ReadLine().Replace(" ", "");
						compilerDocName.All allCompiler = new ocr_wz.compilerDocName.All(text1);
						string text = allCompiler.resultText;
						if(text.Contains("FAKTURA") && !text.Contains("KORYGUJ") && text.Contains("F/"))
						{
							fv++;
						}
                        else if (((text.Contains("yda") || text.Contains("ani")) && text.Contains("mer")) && (text.Contains("WZ") || text.Contains("WŻ") || text.Contains("wz") || text.Contains("Wz") || text.Contains("W2/") || text.Contains("WZ/")))
						{
							wz++;
						}
						else if (text.Contains("yda") && text.Contains("mer") && text.Contains("WW"))
						{
							ww++;
						}
						else if (text.Contains("rku") && text.Contains("LP/"))
						{
							rw++;
						}
						else if ((text.Contains("ZA") && text.Contains("CZO") && text.Contains("DOKU")) || text.Contains("Raben"))
						{
							raben++;
						}
                        else if ((text.Contains("oku") && text.Contains("nt") && text.Contains("WZ")))
						{
							schenker++;
						}
					
					}
						fs.Close();
					if (wz > 0)
					{
						if (ww > 0)
						{
                            try
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("W pliku znajdują się dokumenty WZ i dokument WW");
                                SW.Close();
                                documents.Wz Wz = new documents.Wz(fileNameTXT, fileLogName);
                                documents.Ww Ww = new documents.Ww(fileNameTXT, fileLogName);
                                PdfOcrDone pdfDone = new PdfOcrDone(fileNameTXT);
                            }
                            catch
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("!!Przetwarzanie zakończono błędem!!");
                                SW.Close();
                            }
						}
						else if (fv > 0)
						{
                            try
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("W pliku znajdują się dokumenty WZ i FV");
                                SW.Close();
                                documents.Wz Wz = new documents.Wz(fileNameTXT, fileLogName);
                                documents.Fv Fv = new documents.Fv(fileNameTXT, fileLogName);
                                PdfOcrDone pdfDone = new PdfOcrDone(fileNameTXT);
                            }
                            catch
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("!!Przetwarzanie zakończono błędem!!");
                                SW.Close();
                            }
						}
						else if (rw > 0)
						{
                            try
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("W pliku znajdują się dokumenty WZ i RW");
                                documents.Wz Wz = new documents.Wz(fileNameTXT, fileLogName);
                                documents.Rw Rw = new documents.Rw(fileNameTXT, fileLogName);
                                PdfOcrDone pdfDone = new PdfOcrDone(fileNameTXT);
                            }
                            catch
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("!!Przetwarzanie zakończono błędem!!");
                                SW.Close();
                            }
						}
						else if (raben > 0)
						{
                            try
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("W pliku znajdują się dokumenty WZ i dokument dostawy Raben");
                                SW.Close();
                                documents.Wz Wz = new documents.Wz(fileNameTXT, fileLogName);
                                documents.Raben Raben = new documents.Raben(fileNameTXT, fileLogName);
                                PdfOcrDone pdfDone = new PdfOcrDone(fileNameTXT);
                            }
                            catch
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("!!Przetwarzanie zakończono błędem!!");
                                SW.Close();
                            }
						}
						else if (schenker > 0)
						{
                            try
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("W pliku znajdują się dokumenty WZ i dokument dostawy Schenker");
                                SW.Close();
                                documents.Wz Wz = new documents.Wz(fileNameTXT, fileLogName);
                                documents.Schenker Schenker = new documents.Schenker(fileNameTXT, fileLogName);
                                PdfOcrDone pdfDone = new PdfOcrDone(fileNameTXT);
                            }
                            catch
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("!!Przetwarzanie zakończono błędem!!");
                                SW.Close();
                            }
						}
						else
                        {
                            try
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("W pliku znajdują się tylko dokumenty WZ");
                                SW.Close();
                                documents.Wz Wz = new ocr_wz.documents.Wz(fileNameTXT, fileLogName);
                                PdfOcrDone pdfDone = new PdfOcrDone(fileNameTXT);
                            }
                            catch
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("!!Przetwarzanie zakończono błędem!!");
                                SW.Close();
                            }
							
						}
						
					}
					else if (fv <1 && wz <1 && ww <1 && rw <1 && raben <1 && schenker <1)
					{
                        try
                        {
                            StreamWriter SW;
                            SW = File.AppendText(fileLogName);
                            SW.WriteLine("!!! Nie mogę odczytać dokumentu !!! dokument nazwany ZAS");
                            SW.Close();
                            documents.Zas Zas = new ocr_wz.documents.Zas(fileNameTXT, fileLogName);
                            PdfOcrDone pdfDone = new PdfOcrDone(fileNameTXT);
                        }
                        catch
                        {
                            StreamWriter SW;
                            SW = File.AppendText(fileLogName);
                            SW.WriteLine("!!Przetwarzanie zakończono błędem!!");
                            SW.Close();
                        }
					}
					else if(wz < 1)
					{
						if (ww > 0)
						{
                            try
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("W pliku znajdują się tylko dokumenty WW");
                                SW.Close();
                                documents.Ww Ww = new ocr_wz.documents.Ww(fileNameTXT, fileLogName);
                                PdfOcrDone pdfDone = new PdfOcrDone(fileNameTXT);
                            }
                            catch
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("!!Przetwarzanie zakończono błędem!!");
                                SW.Close();
                            }
						}
						else if (fv > 0)
						{
                            try
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("W pliku znajdują się tylko dokumenty FV");
                                SW.Close();
                                documents.Fv Fv = new ocr_wz.documents.Fv(fileNameTXT, fileLogName);
                                PdfOcrDone pdfDone = new PdfOcrDone(fileNameTXT);
                            }
                            catch
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("!!Przetwarzanie zakończono błędem!!");
                                SW.Close();
                            }
						}
						else if (raben > 0)
						{
                            try
                            {                        
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("W pliku znajdują się tylko dokumenty dostawy Raben");
                                SW.Close();
                                documents.Raben Raben = new ocr_wz.documents.Raben(fileNameTXT, fileLogName);
                                PdfOcrDone pdfDone = new PdfOcrDone(fileNameTXT);
                            }
                            catch
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("!!Przetwarzanie zakończono błędem!!");
                                SW.Close();
                            }
						}
						else if (schenker > 0)
						{
                            try
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("W pliku znajdują się tylko dokumenty dostawy Schenker");
                                SW.Close();
                                documents.Schenker Schenker = new ocr_wz.documents.Schenker(fileNameTXT, fileLogName);
                                PdfOcrDone pdfDone = new PdfOcrDone(fileNameTXT);
                            }
                            catch
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("!!Przetwarzanie zakończono błędem!!");
                                SW.Close();
                            }
						}
						else if (rw > 0)
						{
                            try
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("W pliku znajdują się tylko dokumenty RW");
                                SW.Close();
                                documents.Rw RW = new ocr_wz.documents.Rw(fileNameTXT, fileLogName);
                                PdfOcrDone pdfDone = new PdfOcrDone(fileNameTXT);
                            }
                            catch
                            {
                                StreamWriter SW;
                                SW = File.AppendText(fileLogName);
                                SW.WriteLine("!!Przetwarzanie zakończono błędem!!");
                                SW.Close();
                            }
						}
					}
				}
			}
			catch (Exception ex)
			{
					Console.WriteLine("Nie odnaleziono pliku txt!" + ex);
			}
		}
	}
}
