using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Submission
    {
        public static string xmlURL = "https://github.com/ashervh1/cse445_a4/blob/main/NationalParks.xml";
        public static string xmlErrorURL = "https://github.com/ashervh1/cse445_a4/blob/main/NationalParksErrors.xml";
        public static string xsdURL = "https://github.com/ashervh1/cse445_a4/blob/main/NationalParks.xsd";

        public static void Main(string[] args)
        {    //verify xml from xsd
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);

            //validation of error from xsd
            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);

            //json convertion
            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            //return "No Error" if XML is valid. Otherwise, return the desired exception message.
            StringBuilder err = new StringBuilder();

            
        }

        public static string Xml2Json(string xmlUrl)
        {
            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
        }

        // Helper method to download content from URL
        private static string DownloadContent(string url)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }

}
