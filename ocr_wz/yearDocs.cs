﻿/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-24
 * Godzina: 14:06
 * 
 */
using System;

namespace ocr_wz
{
	/// <summary>
	/// Description of yearDocs.
	/// </summary>
	public class yearDocs
	{
		string documentsName;
		public string year;
		public yearDocs(string docName)
		{
			documentsName = docName;
		}
		
		public void yearWZ()
		{
			year = documentsName.Replace("WZ_", "");
			year = year.Remove(startIndex:2);
			
		}
		public void yearZas()
		{
			year = documentsName.Replace("ZAS_", "");
			year = year.Remove(startIndex:2);
		}
		public void yearWW()
		{
			year = documentsName.Replace("WW", "");
			year = year.Remove(startIndex:2);
		}
		public void yearFV()
		{
			year = documentsName.Replace("F_", "");
			year = year.Remove(startIndex:2);
		}
	}
}
