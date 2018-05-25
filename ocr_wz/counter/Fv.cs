/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-25
 * Godzina: 11:35
 * 
 */
using System;

namespace ocr_wz.counter
{
	/// <summary>
	/// Description of Fv.
	/// </summary>
	public class Fv
	{
		public string result0;
		public Fv(string result)
		{
			if (result.Length > 10)
			{
				result = result.Replace("/", "_");
				result0 = result.Remove(10);
			}
			else
			{
				result = result.Replace("/", "_");
				result0 = result;
			}
		}
	}
}
