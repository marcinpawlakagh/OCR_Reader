/*
 * Wykonał Marcin Pawlak
 * Data: 2018-05-24
 * Godzina: 13:05
 * 
 */
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ocr_wz.counter
{
	/// <summary>
	/// Description of Wz.
	/// </summary>
	public class Wz
	{
		string sCounterWz;
		public string result0;
		public Wz(string result)
		{
			Regex regex = new Regex(@"Wyd");
			sCounterWz = Regex.Replace(result, @"WZ/[0-9][0-9]/", "");
            sCounterWz = Regex.Replace(sCounterWz, @"/", "7");
			sCounterWz = Regex.Replace(sCounterWz, @"[AĄ]", "4");
			sCounterWz = Regex.Replace(sCounterWz, @"[A-Za-z!-/:-~«„]", "");
            
			
			if (result.Count() > 11)
			{
				if (sCounterWz.Length >= 6)
				{
					for (int i = 0; i < 6; i++)
					{
                        sCounterWz = Regex.Replace(sCounterWz, @"[A-Za-zĘęÓóĄąŚśŁłŻżŹźĆćŃń”—„|]", "");
					}
					if (sCounterWz.Length > 6)
					{
						sCounterWz = sCounterWz.Remove(6);
					}
					try
					{
						int counter = int.Parse(sCounterWz);
						int licznikWZCount = sCounterWz.Count();
						
						if (counter > 99999)
						{
							sCounterWz = Regex.Replace(sCounterWz, @"^1" , "");
							result = result.Remove(startIndex:6) + sCounterWz;
							result0 = Regex.Replace(result, "/", "_");
						}
						else
						{
							if (sCounterWz.Length > 5)
							{
								sCounterWz = sCounterWz.Remove(5);
							}
							result = result.Remove(startIndex:6) + sCounterWz;
							result0 = Regex.Replace(result, "/", "_");
						}
					}
					catch
					{
						
					}
				}
                else if (sCounterWz.Length == 5)
                {
                    result = result.Remove(startIndex:6) + sCounterWz;
                    result0 = Regex.Replace(result, "/", "_");
                }
				
			}
			else if(result.Count() == 11)
			{
                result = result.Remove(6);
                result = Regex.Replace(result, @"WZ/181", @"WZ/18/");
                result = Regex.Replace(result, @"WZ/191", @"WZ/19/");
                if (sCounterWz.Length == 5)
                {
                    try
                    {
                        int intsCounter = Int32.Parse(sCounterWz);
                        result = result + sCounterWz;
                        var checkVar = @"^[W-Z][W-Z]/[0-9][0-9]/[0-9][0-9][0-9][0-9][0-9]";
                        var checkIn = Regex.Match(result, checkVar, RegexOptions.IgnoreCase);
                        if (!checkIn.Success)
                        {
                            result0 = Regex.Replace(result, "/", "_");
                        }
                    }
                    catch
                    {

                    }  
                }
			}
			else
			{
				
			}
		}
	}
}
