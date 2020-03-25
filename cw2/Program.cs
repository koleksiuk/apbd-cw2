using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            // default arguments using Linq ElementAtOrDefault
            var maybeSourceFile = args.ElementAtOrDefault(0);
            var sourceFile = String.IsNullOrEmpty(maybeSourceFile) ? "dane.csv" : maybeSourceFile.ToString();

            var maybeOutputFile = args.ElementAtOrDefault(1);
            var outputFile = String.IsNullOrEmpty(maybeOutputFile) ? "result.xml" : maybeOutputFile.ToString();

            var maybeFormat = args.ElementAtOrDefault(2);
            var format = String.IsNullOrEmpty(maybeFormat) ? "xml" : maybeFormat.ToString();

            Console.WriteLine($"Source File: {sourceFile}, output file: {outputFile}, format: {format}");

            if (!File.Exists(sourceFile))
            {
                throw new System.IO.FileNotFoundException($"File {sourceFile} was not found");
            }

            StreamWriter errorLog = new StreamWriter("/Users/konole/Downloads/log.csv");

            Dictionary<int, Student> students = new Dictionary<int, Student>();

            using (var stream = new StreamReader(File.OpenRead(sourceFile)))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null && line != "")
                {
                    string[] studentData = line.Split(',');
                    var st = new Student {
                        FirstName = studentData[0],
                        LastName = studentData[1],
                        Course = studentData[2],
                        TypeOfCourse = studentData[3],
                        Number = int.Parse(studentData[4]),
                        BirthdateString = studentData[5],
                        Email = studentData[6],
                        MotherName = studentData[7],
                        FatherName = studentData[8]                      
                    };

                    if (!students.ContainsKey(st.Number))
                    {
                        if (st.IsValid())
                        {
                            students.Add(st.Number, st);
                        }
                        else
                        {
                            WriteToErrorFile(errorLog, line);
                        }

                    }
                    else
                    {
                        WriteToErrorFile(errorLog, line);
                    }
                    
                }
            }

            errorLog.Close();


            University uni = new University(students.Values.ToList());
            uni.Author = "Konrad Oleksiuk";
            uni.CreatedAt = DateTime.Now.ToString("dd.MM.yyyy");

            FileStream writer = new FileStream(outputFile, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(University));
            serializer.Serialize(writer, uni);
            writer.Close();
        }

        static void WriteToErrorFile(StreamWriter sw, string line)
        {
            sw.WriteLine(line);           
        }
    }
}
