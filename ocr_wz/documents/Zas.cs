/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-02
 * Godzina: 11:10
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
	/// Description of Zas.
	/// </summary>
	public class Zas
	{
		string pdfName;
		public Zas(string fileNameTXT, string fileLogName)
		{
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
						text.Contains("mówienie")
						|| (text.Contains("za") && text.Contains("wie") && text.Contains("mer"))
					)
					{
						if (text.Contains("ZAS"))
						{
							compilerDocName.Zas ZasName = new ocr_wz.compilerDocName.Zas(text);
							counter.Zas licznikZas = new ocr_wz.counter.Zas(ZasName.resultZas);
							if (licznikZas.result0 != null)
							{
								docNames.Rows.Add(licznikZas.result0);
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
				int ileZAS = 0;
				foreach (DataRow row in uniqDocNames.Rows)
				{
					StreamWriter SW1;
					SW1 = File.AppendText(fileLogName);
					SW1.WriteLine(row.Field<string>(0));
					SW1.Close();
					if (row.Field<string>(0).Contains("ZAS_"))
					{
						ileZAS++;
					}
				}
				pdfName = fileNameTXT.Replace(".txt", ".pdf");
				if (ileZAS > 0)
				{
					foreach (DataRow row in uniqDocNames.Rows)
					{
						if (row.Field<string>(0).Contains("ZAS_"))
						{
							yearDocs readYear = new yearDocs(Convert.ToString(row.Field<string>(0)));
							readYear.yearZas();
							Copy CopyNewName = new Copy(pdfName, readYear.year, row.Field<string>(0), fileLogName);
							CopyNewName.CopyZAS();
						}
					}
					PdfOcrDone pdfDone = new PdfOcrDone(pdfName);
				}
				else
				{
					int tableElements = uniqDocNames.Rows.Count;
					string tableRow = Convert.ToString(uniqDocNames.Rows[0]["WZ"]);
					for (int i = 0; i < tableElements; i++)
					{
						yearDocs readYear = new yearDocs(Convert.ToString(uniqDocNames.Rows[i]["WZ"]));
						readYear.yearZas();
						Copy CopyNewName = new Copy(pdfName, readYear.year, Convert.ToString(uniqDocNames.Rows[i]["WZ"]), fileLogName);
						CopyNewName.CopyZAS();
						
					}
					PdfOcrDone pdfDone = new PdfOcrDone(pdfName);
				}
				
				fs.Close();
				File.Delete(fileNameTXT);
			}
			catch
			{
				fs.Close();
				string year = "0";
				pdfName = fileNameTXT.Replace(".txt", ".pdf");
				string docName = pdfName.Replace(Config.inPath + "\\!ocr\\po_ocr\\", "");
				Copy CopyNewName = new Copy(pdfName, year, docName, fileLogName);
				CopyNewName.CopyOther();
				PdfOcrDone pdfDone = new PdfOcrDone(pdfName);
				File.Delete(fileNameTXT);
			}
		}
	}
}
