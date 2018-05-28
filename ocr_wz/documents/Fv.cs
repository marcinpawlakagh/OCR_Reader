/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-24
 * Godzina: 13:48
 * 
 */
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data;

namespace ocr_wz.documents
{
	/// <summary>
	/// Description of fv.
	/// </summary>
	public class Fv
	{
		public Fv(string fileNameTXT, string fileLogName)
		{
			string pdfName = fileNameTXT.Replace(".txt", ".pdf");
			conf Config = new conf();
			FileStream fs = new FileStream(fileNameTXT,
			                               FileMode.Open, FileAccess.ReadWrite);
			DataTable docNames = new DataTable();
			docNames.Columns.Add("WZ", typeof(string));
			
			try
			{
				StreamReader sr = new StreamReader(fs);
				
				while (!sr.EndOfStream)
				{
					string text = sr.ReadLine().Replace(" ", "");
					if (
						text.Contains("F/")
						|| (text.Contains("Nu") && text.Contains("dow") && text.Contains("du"))
					)
					{
						if (text.Contains("F/"))
						{
							compilerDocName.Fv FvName = new ocr_wz.compilerDocName.Fv(text);
							counter.Fv licznikFv = new ocr_wz.counter.Fv(FvName.resultFV);
							if (licznikFv.result0 != null)
							{
								docNames.Rows.Add(licznikFv.result0);
							}
						}
						else if (text.Contains("Nu") && text.Contains("dow") && text.Contains("du"))
						
						{
							compilerDocName.Wz WzName = new ocr_wz.compilerDocName.Wz(text);
							counter.Wz licznikWz = new ocr_wz.counter.Wz(WzName.resultWZ);
							if (licznikWz.result0 != null)
							{
								docNames.Rows.Add(licznikWz.result0);
							}
						}
						
					}
				}
				var UniqueRows = docNames.AsEnumerable().Distinct(DataRowComparer.Default);
				DataTable uniqDocNames = UniqueRows.CopyToDataTable();
				StreamWriter SW;
				SW = File.AppendText(fileLogName);
				SW.WriteLine("Tablica dokumentów:");
				SW.Close();
				int ileFV = 0;
				int ileWZ = 0;
				foreach (DataRow row in uniqDocNames.Rows)
				{
					StreamWriter SW2;
					SW2 = File.AppendText(fileLogName);
					SW2.WriteLine(row.Field<string>(0));
					SW2.Close();
					string docName = row.Field<string>(0);
					if (docName.Contains("F_"))
					{
						ileFV++;
					}
					else if (docName.Contains("WZ_"))
					{
						ileWZ++;
					}
				}
				if (ileFV == ileWZ || ileWZ > ileFV)
				{
					foreach (DataRow row in uniqDocNames.Rows)
					{
						if (row.Field<string>(0).Contains("WZ_"))
						{
							yearDocs WZ = new yearDocs(row.Field<string>(0));
							WZ.yearWZ();
							Copy CopyNewName = new Copy(pdfName, WZ.year, row.Field<string>(0), fileLogName);
							CopyNewName.CopyWZFV();
						}
					}
					PdfOcrDone pdfDone = new PdfOcrDone(pdfName);
				}
				else
				{
					foreach (DataRow row in uniqDocNames.Rows)
					{
						string docName = row.Field<string>(0);
						if (docName.Contains("F_"))
						{
							yearDocs readYear = new yearDocs(docName);
							readYear.yearFV();
							Copy CopyNewName = new Copy(pdfName, readYear.year, docName, fileLogName);
							CopyNewName.CopyFV();
						}
					}
					PdfOcrDone pdfDone = new PdfOcrDone(pdfName);
				}
				fs.Close();
				File.Delete(fileNameTXT);
			}
			catch(Exception ex)
			{
				string docName = pdfName.Replace(Config.inPath + "\\!ocr\\po_ocr\\","");
				Copy CopyNewName = new Copy(pdfName, "0", docName, fileLogName);
				CopyNewName.CopyOther();
				Console.WriteLine("Nie odnaleziono pliku txt!" + ex);
				PdfOcrDone pdfDone = new PdfOcrDone(pdfName);
				fs.Close();
				File.Delete(fileNameTXT);
			}
		}
	}
}
