/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-24
 * Godzina: 14:02
 * 
 */
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data;

namespace ocr_wz.compilerDocName
{
	/// <summary>
	/// Description of Zas.
	/// </summary>
	public class Zas
	{
		public string resultZas;
		public Zas(string result)
		{
			result = Regex.Replace(result, "2AS", "ZAS");
			result = Regex.Replace(result, "ŻAS", "ZAS");
			result = Regex.Replace(result, "2A", "ZA");
			result = Regex.Replace(result, "ZA[0-9A-Za-z]/", "ZAS/");
			for (int i = 0; i < result.Length + 10; i++ )
			{
				result = Regex.Replace(result, @"[:punct:]ZAS/", "ZAS/");
				result = Regex.Replace(result, @"[:alpha:]ZAS/", "ZAS/");
				result = Regex.Replace(result, @"[:numeric:]ZAS/", "ZAS/");
				result = Regex.Replace(result, @"[-|0-9A-Za-ząęółśżźćń\=„*+',;\._<>""()«%]ZAS/", "ZAS/");
			}
			resultZas = Regex.Replace(result, @"[^a-z0-9A-Z]ZAS/", "ZAS/");
		}
	}
}
