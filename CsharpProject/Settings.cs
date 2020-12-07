﻿using APILibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpProject
{
    public static class Settings
    {
        public static UnitType unitType = UnitType.Metric;
        public static string defaultCity= "Vilnius";
        public static string defaultCountry = "LT";
        public static void save()
        {
            try
            {
                string[] lines = { unitType.ToString(), defaultCity , defaultCountry };
                using (StreamWriter writetext = new StreamWriter("config.ini"))
                {
                    writetext.WriteLine(lines[0]);
                    writetext.WriteLine(lines[1]);
                    writetext.WriteLine(lines[2]);
                }
            }
            catch (Exception) { }

        }
        public static void load()
        {
            try
            {
                using (StreamReader readtext = new StreamReader("config.ini"))
                {
                    unitType = readtext.ReadLine().Equals("Metric") ? UnitType.Metric : UnitType.Imperial;
                    defaultCity = readtext.ReadLine();
                    defaultCountry = readtext.ReadLine();
                }
            }
            catch (Exception) { }
        }
    }
}
