using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

public class Program
{
	public static void Main()
	{

        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var bin = configuration["BIN"];
        var useJulianDate = bool.Parse(configuration["UseJulianDate"]);
        var randomMid = Regex.Replace(Guid.NewGuid().ToString(), "[^\\d]", "").PadRight(10, '0').Substring(0, useJulianDate ? 5 : 10);
        if(useJulianDate)
        {
            var dateToConvert = DateTime.Today; // Or any other date you want!
            var jDate=string.Format("{0}{1}", dateToConvert.ToString("yy"), dateToConvert.DayOfYear.ToString().PadLeft(3, '0'));
            randomMid = jDate + randomMid;
        }
        
		var checkDigit = randomMid.GetLuhnCheckDigit();
		Console.WriteLine($"{bin} - {randomMid} - {checkDigit}");
	}
}
