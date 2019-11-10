using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Configuration;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Xml;
using System.Xml.Linq;

namespace Lab14
{

    class Program
    {
        static void Main(string[] args)
        {
            // объект для сериализации
            Button button1 = new Button("Name1", "Form1", "Color1", "Border1", 1);
            Button button2 = new Button("Name2", "Form2", "Color2", "Border2", 2);
            Button button3 = new Button("Name3", "Form3", "Color3", "Border3", 3);
            Button button4 = new Button("Name4", "Form4", "Color4", "Border4", 4);

            Button[] buttons = new Button[] { button1, button2, button3, button4 };

            Console.WriteLine("Objects was created");

            BinaryFormatter binformatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("buttons.bin", FileMode.Create))
            {
                binformatter.Serialize(fs, buttons);
                Console.WriteLine("Buttons was serialized(BinaryFormatter)");
            }
            using (FileStream fs = new FileStream("buttons.bin", FileMode.Open))
            {
                Console.WriteLine("Buttons was deserialized(BinaryFormatter)");
                Button[] newButs = (Button[])binformatter.Deserialize(fs);
                foreach (Button but in newButs)
                    Console.WriteLine(but.ToString());
            }

            SoapFormatter soapformatter = new SoapFormatter();
            using (FileStream fs = new FileStream("buttons.soap", FileMode.Create))
            {
                soapformatter.Serialize(fs, buttons);
                Console.WriteLine("Buttons was serialized(SoapFormatter)");
            }
            using (FileStream fs = new FileStream("buttons.soap", FileMode.Open))
            {
                Console.WriteLine("Buttons was deserialized(SoapFormatter)");
                Button[] newButs = (Button[])soapformatter.Deserialize(fs);
                foreach (Button but in newButs)
                    Console.WriteLine(but.ToString());
            }

            DataContractJsonSerializer jsonformatter = new DataContractJsonSerializer(typeof(Button[]));
            using (FileStream fs = new FileStream("buttons.json", FileMode.Create))
            {
                jsonformatter.WriteObject(fs, buttons);
                Console.WriteLine("Buttons was serialized(JsonSerializer)");
            }
            using (FileStream fs = new FileStream("buttons.json", FileMode.Open))
            {
                Console.WriteLine("Buttons was deserialized(JsonFormatted)");
                Button[] newButs = (Button[])jsonformatter.ReadObject(fs);
                foreach (Button but in newButs)
                    Console.WriteLine(but.ToString());
            }

            XmlSerializer xmlformatter = new XmlSerializer(typeof(Button[]));
            using (FileStream fs = new FileStream("buttons.xml", FileMode.Create))
            {
                xmlformatter.Serialize(fs, buttons);
                Console.WriteLine("Buttons was serialized(XMLFormatted)");
            }
            using (FileStream fs = new FileStream("buttons.xml", FileMode.Open))
            {
                Console.WriteLine("Buttons was deserialized(XMLFormatted)");
                Button[] newButs = (Button[])xmlformatter.Deserialize(fs);
                foreach (Button but in newButs)
                    Console.WriteLine(but.ToString());
            }

            Console.WriteLine("\n3 Task");
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("buttons.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            Console.WriteLine("All nodes");
            XmlNodeList childnodes = xRoot.SelectNodes("*");
            foreach (XmlNode n in childnodes)
                Console.WriteLine(n.OuterXml);

            Console.WriteLine("\nNode with name Name1:");
            XmlNode childnode = xRoot.SelectSingleNode("Button[_name='Name1']");
            if (childnode != null)
                Console.WriteLine(childnode.OuterXml);



            Console.WriteLine("\nLINQ to XML");
            XDocument xdoc = new XDocument();
            // создаем 1 элемент
            XElement subject1 = new XElement("subject");
            XAttribute subject1_name_attr = new XAttribute("name", "OOP");
            XElement subject1_teacher_elem = new XElement("teacher", "good");
            XElement subject1_marks_elem = new XElement("marks", "good");
            subject1.Add(subject1_name_attr);
            subject1.Add(subject1_teacher_elem);
            subject1.Add(subject1_marks_elem);

            // создаем 2 элемент
            XElement subject2 = new XElement("subject");
            XAttribute subject2_name_attr = new XAttribute("name", "PL");
            XElement subject2_teacher_elem = new XElement("teacher", "normal");
            XElement subject2_marks_elem = new XElement("marks", "bad");
            subject2.Add(subject2_name_attr);
            subject2.Add(subject2_teacher_elem);
            subject2.Add(subject2_marks_elem);

            // создаем корневой элемент
            XElement teams = new XElement("subjects");
            teams.Add(subject1);
            teams.Add(subject2);

            xdoc.Add(teams);
            //сохраняем документ
            xdoc.Save("subjects.xml");

            var items = from xe in xdoc.Elements("subjects").Elements("subject")
                        where xe.Element("teacher").Value == "good"
                        select new Subject
                        {
                            Name = xe.Attribute("name").Value,
                            Marks = xe.Element("marks").Value
                        };
            foreach (var item in items)
            {
                Console.WriteLine("\t{0} - {1}", item.Name, item.Marks);
            }

            Console.ReadLine();
        }
    }
}