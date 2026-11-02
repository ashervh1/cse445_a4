using System;
using System.Text;
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
        // connects hosted branch URLs for loading content
        public static string xmlURL = "https://raw.githubusercontent.com/ashervh1/cse445_a4/main/NationalParks.xml";
        public static string xmlErrorURL = "https://raw.githubusercontent.com/ashervh1/cse445_a4/main/NationalParksErrors.xml";
        public static string xsdURL = "https://raw.githubusercontent.com/ashervh1/cse445_a4/main/NationalParks.xsd";

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
        //return "No errors are found" if XML is valid. Otherwise, return the desired exception message.
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            //gets error messages
            StringBuilder err = new StringBuilder();

            try
            {
                //downloads content from URLs
                string xsdLoad = DownloadContent(xsdUrl);
                string xmlLoad = DownloadContent(xmlUrl);
                //schyema validation with xsdLoad
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas.Add(null, XmlReader.Create(new StringReader(xsdLoad)));
                //saving errors
                settings.ValidationEventHandler += (sender, e) => err.AppendLine(e.Message);
                //validate each park entry node
                using (XmlReader reader = XmlReader.Create(new StringReader(xmlLoad), settings))
                {
                    while (reader.Read()) { }
                }
            }
            //syntax error 
            catch (XmlException ex)
            {
                err.AppendLine(ex.Message);
            }
            //informartion error
            catch (Exception ex)
            {
                err.AppendLine(ex.Message);
            }
            //else no errors!!
            return err.Length == 0 ? "No errors are found" : err.ToString().TrimEnd();
        }
        //converts XML to JSON txt
        public static string Xml2Json(string xmlUrl)
        {
            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            string xmlLoad = DownloadContent(xmlUrl);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlLoad);
            //checking phone attribute requirement for every node in array
            foreach (XmlNode phone in doc.SelectNodes("//Phone"))
            {
                XmlAttribute array = doc.CreateAttribute("json", "Array", "http://james.newtonking.com/projects/json");
                array.Value = "true";
                phone.Attributes.Append(array);
            }
            //sterilize root 
            return JsonConvert.SerializeXmlNode(doc.DocumentElement);
        }

        // Helper method to download content from URL
        private static string DownloadContent(string url)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
            
                client.Encoding = Encoding.UTF8;
                return client.DownloadString(url).TrimStart('\uFEFF');
            }
        }
    }

}
