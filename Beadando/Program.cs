using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Xml;

namespace Beadando
{
    class Program
    {
        static void Main(string[] args)
        {
            CountdownEvent cde = new CountdownEvent(10);

            Console.WriteLine("There is a library. The librarian has a bag which contains the titles of the library's books.");
            ConcurrentBag<string> titles = new ConcurrentBag<string>();

            ReadXML(@"BookTitle.xml", titles);

            Titles bookTitles = new Titles(titles);

            List<string> sortedlist = new List<string>();

            Console.WriteLine("The elements of the bag:");

            while (!bookTitles.TitleBag.IsEmpty)
            {
                bookTitles.TitleBag.TryTake(out string result);

                Console.WriteLine($"{result}  ");
                cde.Wait(new TimeSpan(0, 0, 0, 0, 500));
                sortedlist.Add(result);
            }
            

            sortedlist.Sort();

            foreach (string title in sortedlist)
            {
                bookTitles.TitleBag.Add(title);
            }

            bookTitles.TitleBag.TryPeek(out string last);

            Console.WriteLine($"Now we have alphabetically sorted the titles at the bag. The last title is:{last}");

            Console.WriteLine($"The number of titles in the bag:{bookTitles.TitleBag.Count}");

            bookTitles.TitleBag.Clear();

            Console.WriteLine($"The library burnt down so there are no book left. The bag has {bookTitles.TitleBag.Count} title(s) in it.");

            cde.Dispose();

        }

        public static void ReadXML(string xmlPath, ConcurrentBag<string> bag)
        {
            //Reading infos form the XML...

            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlPath);

            foreach (XmlNode node in xmlDocument.DocumentElement.SelectNodes("/list/title"))
            {
                bag.Add(node.InnerText);
            }

            //"Done reading.
            return;
        }
    }
}
