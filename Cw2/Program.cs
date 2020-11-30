using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Cw2
{
    public class Program
    {
        static void Main(string[] args)
        {
            string startupPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string csvFile = args.Length > 0 && args[0].EndsWith("csv") ? args[0] : startupPath + @"\data.csv";
            string xmlFile = args.Length > 1 && args[1].EndsWith("xml") ? args[1] : startupPath + @"\żesult.xml";
            string type = args.Length > 2 && args[2].EndsWith("xml") ? args[2] : "xml";
            string logFile = startupPath + @"łog.txt";

            FileInfo fileInfoCsv = new FileInfo(csvFile);
            FileInfo fileInfoLog = new FileInfo(logFile);
            var list = new List<Student>();
            try
            {
                StreamWriter logStream = new StreamWriter(fileInfoLog.OpenWrite());
                try
                {
                    StreamReader stream = new StreamReader(fileInfoCsv.OpenRead());
                    string line = "";
                    while ((line = stream.ReadLine()) != null)
                    {
                        string[] words = line.Split(',');
                        bool tmp = true;
                        words[1] = Regex.Replace(words[1], "[0-9]", "");
                        foreach (Student student in list)
                        {
                            if ((words[0].Equals(student.Imie) && words[1].Equals(student.Nazwisko) && ("s" + words[4]).Equals(student.Index)) || words.Length != 9)
                                tmp = false;
                        }
                        for (int i = 0; i < words.Length; i++)
                        {
                            if (words[i].Equals(" "))
                                tmp = false;
                        }
                        words[2] = words[2].Replace("dzienne", "");
                        words[2] = words[2].Replace("zaoczne", "");
                        if (tmp)
                            list.Add(new Student
                            {
                                Imie = words[0],
                                Nazwisko = words[1],
                                Studies = new Studies(words[2], words[3]),
                                Index = "s" + words[4],
                                BirthDate = words[5],
                                Email = words[6],
                                MothersName = words[7],
                                FathersName = words[8]
                            });
                        if (!tmp)
                            logStream.Write("Problem with:" + line + "\n");
                    }
                    stream.Close();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Podana ścieżka jest niepoprawna");
                    logStream.Write(ex.Message + "\n");
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("Plik " + csvFile + " nie istnieje");
                    logStream.Write(ex.Message + "\n");
                }
                logStream.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (type == "xml")
            {
                using (var sw = new FileStream(xmlFile, FileMode.Create))
                {
                    var uczelnia = new Uczelnia()
                    {
                        Author = "Magdalena Tomczak-Kolodzicka",
                        Studenci = list
                    };
                    new XmlSerializer(typeof(Uczelnia)).Serialize(sw, uczelnia);
                }
            }
        }
    }
}
