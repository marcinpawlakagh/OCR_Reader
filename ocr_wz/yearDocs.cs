/*
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
		public string docNameN;
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
		public void yearLP()
		{
			year = documentsName.Replace("LP_", "");
			year = year.Remove(startIndex:2);
			string counter = documentsName.Replace(year,"");
			counter = counter.Replace("LP", "");
			counter = counter.Replace("_", "");
			int yearDoc = int.Parse(year);
			DateTime thisTime = DateTime.Now;
			year = (thisTime.ToString().Replace(" ", "_").Replace("-", "").Replace(":","")).Remove(4).Replace("20", "");
			int yearAct = int.Parse(year);
			if ((yearAct - yearDoc) > 1)
			{
				year = yearAct.ToString();
				docNameN = "LP_" + year + "_" + counter;
			}
			else
			{
				year = yearDoc.ToString();
				docNameN = documentsName;
			}
		}
	}
}
