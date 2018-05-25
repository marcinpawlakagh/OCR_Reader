/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-30
 * Godzina: 13:58
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
	/// Description of Copy.
	/// </summary>
	public class Copy
	{
		string pdfName, year, docName, fileLog;
		public Copy(string pdfNameWZ, string yearWZ, string docNameWZ, string fileLogName)
		{
			pdfName = pdfNameWZ;
			year = yearWZ;
			docName = docNameWZ;
			fileLog = fileLogName;
		}
		
		public void CopyWZ()
		{
			conf Config = new conf();
			try
			{
				File.Copy(pdfName, Config.outPath +"Rok_20"+ year + "\\" + docName + ".pdf" );
				StreamWriter WZ;
				WZ = File.AppendText(fileLog);
				WZ.WriteLine(Config.outPath +"Rok_20"+ year + "\\" + docName + ".pdf");
				WZ.Close();
			}
			catch (IOException)
			{
				FileInfo infoFirst = new FileInfo(Config.outPath +"Rok_20"+ year + "\\" + docName + ".pdf");
				long byteFirst = infoFirst.Length;
				FileInfo infoSecond = new FileInfo(pdfName);
				long byteSecond = infoSecond.Length;
				if (byteFirst != byteSecond)
				{
					File.Copy(pdfName, Config.outPath +"Rok_20"+ year + "\\duplikaty\\" + docName + ".pdf" );
					StreamWriter WZ2;
					WZ2 = File.AppendText(fileLog);
					WZ2.WriteLine("!! DUPLIKAT !!    " + Config.outPath +"Rok_20"+ year + "\\duplikaty\\" + docName + ".pdf" + "    !! DUPLIKAT !!");
					WZ2.Close();
				}
				else
				{
					StreamWriter WZ3;
					WZ3 = File.AppendText(fileLog);
					WZ3.WriteLine("!! Identyczny plik istnieje !!    " + Config.outPath +"Rok_20"+ year + "\\" + docName + ".pdf" + "    !! Identyczny plik istnieje !!");
					WZ3.Close();
				}
			}
		}
		public void CopyZAS()
		{
			conf Config = new conf();
			try
			{
				File.Copy(pdfName, Config.inPath + "\\!do_przetworzenia_recznego\\" + docName + ".pdf" );
				StreamWriter ZAS;
				ZAS = File.AppendText(fileLog);
				ZAS.WriteLine(Config.inPath + "\\!do_przetworzenia_recznego\\" + docName + ".pdf");
				ZAS.Close();
			}
			catch (IOException)
			{
				FileInfo infoFirst = new FileInfo(Config.inPath + "\\!do_przetworzenia_recznego\\" + docName + ".pdf");
				long byteFirst = infoFirst.Length;
				FileInfo infoSecond = new FileInfo(pdfName);
				long byteSecond = infoSecond.Length;
				if (byteFirst != byteSecond)
				{
					File.Copy(pdfName, Config.inPath + "\\!do_przetworzenia_recznego\\duplikaty\\" + docName + ".pdf");
					StreamWriter ZAS2;
					ZAS2 = File.AppendText(fileLog);
					ZAS2.WriteLine("!! DUPLIKAT !!    " + Config.inPath + "\\!do_przetworzenia_recznego\\duplikaty\\" + docName + ".pdf" + "    !! DUPLIKAT !!");
					ZAS2.Close();
				}
				else
				{
					StreamWriter ZAS3;
					ZAS3 = File.AppendText(fileLog);
					ZAS3.WriteLine("!! Identyczny plik istnieje !!    " + Config.inPath + "\\!do_przetworzenia_recznego\\" + docName + ".pdf" + "    !! Identyczny plik istnieje !!");
					ZAS3.Close();
				}
			}
		}
		public void CopyWW()
		{
			conf Config = new conf();
			try
			{
				File.Copy(pdfName, Config.outPath +"Rok_20"+ year + "\\WW\\" + docName + ".pdf" );
				StreamWriter WW;
				WW = File.AppendText(fileLog);
				WW.WriteLine(Config.outPath +"Rok_20"+ year + "\\WW\\" + docName + ".pdf" );
				WW.Close();
			}
			catch (IOException)
			{
				FileInfo infoFirst = new FileInfo(Config.outPath +"Rok_20"+ year + "\\WW\\" + docName + ".pdf");
				long byteFirst = infoFirst.Length;
				FileInfo infoSecond = new FileInfo(pdfName);
				long byteSecond = infoSecond.Length;
				if (byteFirst != byteSecond)
				{
					File.Copy(pdfName, Config.outPath +"Rok_20"+ year + "\\duplikaty\\" + docName + ".pdf" );
					StreamWriter WW2;
					WW2 = File.AppendText(fileLog);
					WW2.WriteLine("!! DUPLIKAT !!    " + Config.outPath +"Rok_20"+ year + "\\duplikaty\\" + docName + ".pdf" + "    !! DUPLIKAT !!");
					WW2.Close();
				}
				else
				{
					StreamWriter WW3;
					WW3 = File.AppendText(fileLog);
					WW3.WriteLine("!! Identyczny plik istnieje !!    " + Config.outPath +"Rok_20"+ year + "\\" + docName + ".pdf" + "    !! Identyczny plik istnieje !!");
					WW3.Close();
				}
			}
		}
		public void CopyWZFV()
		{
			conf Config = new conf();
			try
			{
				File.Copy(pdfName, Config.outPath +"Rok_20"+ year + "\\" + docName + "_FV.pdf" );
				StreamWriter FV;
				FV = File.AppendText(fileLog);
				FV.WriteLine(Config.outPath +"Rok_20"+ year + "\\" + docName + "_FV.pdf");
				FV.Close();
			}
			catch (IOException)
			{
				FileInfo infoFirst = new FileInfo(Config.outPath +"Rok_20"+ year + "\\" + docName + "_FV.pdf");
				long byteFirst = infoFirst.Length;
				FileInfo infoSecond = new FileInfo(pdfName);
				long byteSecond = infoSecond.Length;
				if (byteFirst != byteSecond)
				{
					File.Copy(pdfName, Config.outPath +"Rok_20"+ year + "\\duplikaty\\" + docName + "_FV.pdf" );
					StreamWriter FV2;
					FV2 = File.AppendText(fileLog);
					FV2.WriteLine("!! DUPLIKAT !!    " + Config.outPath +"Rok_20"+ year + "\\duplikaty\\" + docName + "_FV.pdf" + "    !! DUPLIKAT !!");
					FV2.Close();
				}
				else
				{
					StreamWriter FV3;
					FV3 = File.AppendText(fileLog);
					FV3.WriteLine("!! Identyczny plik istnieje !!    " + Config.outPath +"Rok_20"+ year + "\\" + docName + "_FV.pdf" + "    !! Identyczny plik istnieje !!");
					FV3.Close();
				}
			}
		}
		public void CopyFV()
		{
			conf Config = new conf();
			try
			{
				File.Copy(pdfName, Config.outPath +"Rok_20"+ year + "\\FV\\" + docName + ".pdf" );
				StreamWriter FV;
				FV = File.AppendText(fileLog);
				FV.WriteLine(Config.outPath +"Rok_20"+ year + "\\FV\\" + docName + ".pdf");
				FV.Close();
			}
			catch (IOException)
			{
				FileInfo infoFirst = new FileInfo(Config.outPath +"Rok_20"+ year + "\\FV\\" + docName + ".pdf");
				long byteFirst = infoFirst.Length;
				FileInfo infoSecond = new FileInfo(pdfName);
				long byteSecond = infoSecond.Length;
				if (byteFirst != byteSecond)
				{
					File.Copy(pdfName, Config.outPath +"Rok_20"+ year + "\\duplikaty\\" + docName + ".pdf" );
					StreamWriter FV2;
					FV2 = File.AppendText(fileLog);
					FV2.WriteLine("!! DUPLIKAT !!    " + Config.outPath +"Rok_20"+ year + "\\duplikaty\\" + docName + ".pdf" + "    !! DUPLIKAT !!");
					FV2.Close();
				}
				else
				{
					StreamWriter FV3;
					FV3 = File.AppendText(fileLog);
					FV3.WriteLine("!! Identyczny plik istnieje !!    " + Config.outPath +"Rok_20"+ year + "\\FV\\" + docName + "_.pdf" + "    !! Identyczny plik istnieje !!");
					FV3.Close();
				}
			}
		}
		public void CopyRW()
		{
			conf Config = new conf();
			try
			{
				File.Copy(pdfName, Config.outPath +"Rok_20"+ year + "\\RW\\" + docName + ".pdf" );
				StreamWriter RW;
				RW = File.AppendText(fileLog);
				RW.WriteLine(Config.outPath +"Rok_20"+ year + "\\RW\\" + docName + ".pdf");
				RW.Close();
		}
			catch (IOException)
			{
				FileInfo infoFirst = new FileInfo(Config.outPath +"Rok_20"+ year + "\\RW\\" + docName + ".pdf");
				long byteFirst = infoFirst.Length;
				FileInfo infoSecond = new FileInfo(pdfName);
				long byteSecond = infoSecond.Length;
				if (byteFirst != byteSecond)
				{
					File.Copy(pdfName, Config.outPath +"Rok_20"+ year + "\\duplikaty\\" + docName + ".pdf" );
					StreamWriter RW2;
					RW2 = File.AppendText(fileLog);
					RW2.WriteLine("!! DUPLIKAT !!    " + Config.outPath +"Rok_20"+ year + "\\duplikaty\\" + docName + ".pdf" + "    !! DUPLIKAT !!");
					RW2.Close();
				}
				else
				{
					StreamWriter RW3;
					RW3 = File.AppendText(fileLog);
					RW3.WriteLine("!! Identyczny plik istnieje !!    " + Config.outPath +"Rok_20"+ year + "\\" + docName + ".pdf" + "    !! Identyczny plik istnieje !!");
					RW3.Close();
				}
			}
		}
		public void CopyOther()
		{
			conf Config = new conf();
			File.Copy(pdfName, Config.inPath +"\\!do_przetworzenia_recznego\\"+ docName );
			StreamWriter RW;
			RW = File.AppendText(fileLog);
			RW.WriteLine(Config.inPath +"\\!do_przetworzenia_recznego\\"+ docName);
			RW.Close();
		}
	}
}