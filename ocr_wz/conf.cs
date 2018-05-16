/*
 * Wykonał Marcin Pawlak
 * Data: 2018-04-06
 * Godzina: 13:12
 * W tej klasie pobieram dane z pliku conf.cfg
 * 1 linia: Ścieżka do folderu z zeskanowanymi dokumentami
 * 2 linia: Ścieżka wyjściowa z folderem do którego poprawnie nazwane dokumenty będą zapisane (musi zawierać podfoldery Rok_XXXX)
 */
using System;
using System.IO;
using ocr_wz;

namespace ocr_wz
{
	/// <summary>
	/// Description of conf.
	/// </summary>
	public class conf
	{
		public string inPath, outPath;
		const string path = @"conf.cfg";
		
		public conf()
		{
				if (File.Exists(path))
			    {
				StreamReader readingConf = new StreamReader(path);
				inPath = readingConf.ReadLine();
				outPath = readingConf.ReadLine();
			    }
		}

	}
}
