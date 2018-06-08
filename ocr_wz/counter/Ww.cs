/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-25
 * Godzina: 08:57
 * 
 */
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ocr_wz.counter
{
	/// <summary>
	/// Description of Ww.
	/// </summary>
	public class Ww
	{
		string sCounterWz;
		public string result0;
		public Ww(string result)
		{
			Regex regex = new Regex(@"Wyd");
			sCounterWz = Regex.Replace(result, @"WW[0-9][0-9]/", "");
			sCounterWz = Regex.Replace(sCounterWz, @"[AĄ]", "4");
			sCounterWz = Regex.Replace(sCounterWz, @"[A-Za-z!-/:-~«„]", "");
            var checkVar = @"^[W][W][0-9][0-9]/[0-9][0-9][0-9][0-9][0-9][0-9]";
			
			if (result.Count() > 11)
			{
				if (sCounterWz.Length >= 6)
				{
					if (sCounterWz.Length > 6)
					{
						sCounterWz = sCounterWz.Remove(6);
					}
					try
					{
						int counter = int.Parse(sCounterWz);
						int licznikWZCount = sCounterWz.Count();
						if (counter > 999999)
						{
							sCounterWz = Regex.Replace(sCounterWz, @"^1" , "");
							result = result.Remove(startIndex:5) + sCounterWz;
                            var checkIn = Regex.Match(result, checkVar, RegexOptions.IgnoreCase);
                            if (checkIn.Success)
                            {
                                result0 = Regex.Replace(result, "/", "_");
                            }
							
						}
						else
						{
							result = result.Remove(startIndex:5) + sCounterWz;
                            var checkIn = Regex.Match(result, checkVar, RegexOptions.IgnoreCase);
                            if (checkIn.Success)
                            {
                                result0 = Regex.Replace(result, "/", "_");
                            }
						}
						
					}
					catch
					{
						
					}
				}
				else
				{
					result = result.Remove(startIndex:6) + sCounterWz;
                    var checkIn = Regex.Match(result, checkVar, RegexOptions.IgnoreCase);
                    if (checkIn.Success)
                    {
                        result0 = Regex.Replace(result, "/", "_");
                    }
				}
			}
			else if(result.Count() == 11)
			{
                var checkIn = Regex.Match(result, checkVar, RegexOptions.IgnoreCase);
                if (checkIn.Success)
                {
                    result0 = Regex.Replace(result, "/", "_");
                }
			}
			else
			{
				
			}
		}
	}
}
