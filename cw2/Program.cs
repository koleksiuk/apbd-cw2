using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
                            // log to error log
                            Console.WriteLine($"Invalid record: {st}");

                            WriteToErrorFile(errorLog, line);
                        }

                    }
                    else
                    {
                        // log to error log
                        Console.WriteLine($"Student already exists: {st}");

                        WriteToErrorFile(errorLog, line);
                    }
                    
                }
            }

            errorLog.Close();

            foreach(KeyValuePair<int, Student> entry in students)
            {
                var number = entry.Key;
                var student = entry.Value;
                Console.WriteLine($"{number}: {student.FirstName}");
            }
        }

        static void WriteToErrorFile(StreamWriter sw, string line)
        {
            sw.WriteLine(line);           
        }
    }
}
