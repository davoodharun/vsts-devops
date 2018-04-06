using System;
using System.Collections.Generic;
using System.Dynamic;

namespace SeleniumExample
{
    public class ReadCSV
    {
        public static List<dynamic> GetSites(string path)
        {
            List<dynamic> itemList = new List<dynamic>();
            string[] lines = System.IO.File.ReadAllLines(path);
            for (int j = 1; j < lines.Length; j++)
            {
                var values = lines[j].Split(',');
                dynamic newSite = new ExpandoObject();
                string[] headers = lines[0].Split(',');
                for (int i = 0; i < headers.Length; i++)
                {
                    var header = headers[i];
                    AddProperty(newSite, header, values[i]);
                }
                itemList.Add(newSite);
            }

            return itemList;
        }

        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

    }
}
