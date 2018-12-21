using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace UpdateXml
{
    class Program
    {
        static int Main(string[] args)
        {
            String pipedText = "";
            bool isKeyAvailable;

            try
            {
                isKeyAvailable = System.Console.KeyAvailable;
            }
            catch (InvalidOperationException expected)
            {
                pipedText = System.Console.In.ReadToEnd();
            }

            string xpath, newValue;

            XmlDocument document = new XmlDocument();
            if (string.IsNullOrWhiteSpace(pipedText))
            {

                if (args.Length != 3)
                {
                    HelpText();
                    return 0;
                }
                if (!File.Exists(args[0]))
                {
                    Console.WriteLine($"Datei {args[0]} existiert nicht");
                    return 1;
                }
                document.Load(args[0]);
                xpath = args[1];
                newValue = args[2];
            }
            else
            {
                document.LoadXml(pipedText);
                xpath = args[0];
                newValue = args[1];
            }

            XPathNavigator navigator = document.CreateNavigator();
            XmlNamespaceManager manager = new XmlNamespaceManager(navigator.NameTable);

            foreach (XPathNavigator nav in navigator.Select(xpath, manager))
            {
                nav.SetValue(newValue);
            }

            Console.WriteLine(navigator.OuterXml);

            return 0;
        }

        private static void HelpText()
        {
            Console.WriteLine(@"Update-XML Tool
Update a value in a XML-Document by XPath
There are two operating modes:

First you´ll send the XML Doc via Standard Input   e.g.  updateXML <parameter> < inputFile.xml
or second you give the filename as first parameter.

in first case you could start with:
 UpdateXML /package/metadata/version 2.0.0 < myPackage.nuspec
to update VersionInfo in a NuGet Spec file.

in second case you would call:
 UpdateXML myPackage.nuspec /package/metadata/version 2.0.0

The Result is in both cases the output of the manipuled File
");
        }
    }
}
