using System;
using System.Net;
using System.Collections.Generic;

using System.Text;

class CSharpLogParse
{

    static void Main()
    {

        WebClient client = new WebClient();
        string s = client.DownloadString("https://coderbyte.com/api/challenges/logs/web-logs-raw");
        List<string> list = new List<string>();
        Dictionary<string, int> resultset = new Dictionary<string, int>();
        var shareLinkId = string.Empty;
        //Split data on the base Apr 10
        var data = s.Split(new string[] { "Apr" }, StringSplitOptions.None);
        foreach (var item in data)
        {
            // Check shareLinkId exist in the item
            if (item.Contains("?shareLinkId="))
            {
                // splitng data on the base of " 
                var queryValues = item.Trim().Split('"');
                // foreach on query item 
                foreach (var queryItem in queryValues)
                {
                    shareLinkId = string.Empty;
                   // Check shareLinkId exist in the item
                    if (queryItem.Contains("GET /backend/requests/editor/placeholder?shareLinkId="))
                    {
                        // Replace string and get shareLinkId value only
                        shareLinkId = queryItem.Replace(@"GET /backend/requests/editor/placeholder?shareLinkId=", " ");
                        if (shareLinkId.Contains("HTTP/1.1"))
                        {
                            shareLinkId = shareLinkId.Replace("HTTP/1.1", "");
                        }
                     
                    }
                    // Replace string and get shareLinkId value only
                    else if (queryItem.Contains("/backend/requests/editor/placeholder?shareLinkId="))
                    {
                        shareLinkId = queryItem.Replace(@"/backend/requests/editor/placeholder?shareLinkId=", " ");
                        if (shareLinkId.Contains("HTTP/1.1"))
                        {
                            shareLinkId = shareLinkId.Replace("HTTP/1.1", "");
                        }
                       
                    }
                    shareLinkId = shareLinkId.Trim();
                    // Adding value in resultset
                    if (shareLinkId.Length > 0)
                    {
                        // If key already in resultset then increment the value
                        if (resultset.ContainsKey(shareLinkId))
                        {
                            int keyValue = resultset[shareLinkId];
                            resultset[shareLinkId] = keyValue + 1; ;
                        }
                        else
                            // add value resultset
                            resultset.Add(shareLinkId, 1);
                    }
                }

            }
            
        }
       // For print 
        StringBuilder finalResult = new StringBuilder();
        foreach (var item in resultset)
        {
            finalResult.Append(item.Key.ToString());
            if (item.Value > 1)
            {
                finalResult.Append(":").Append(item.Value);
            }
            finalResult.Append("\n");
        }
        Console.WriteLine(finalResult.ToString());

        Console.ReadLine();

    }



}