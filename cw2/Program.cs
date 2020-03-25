using System;
using System.Collections.Generic;
using System.IO;

namespace cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceFile = args[0];
            var outputFile = args[1];
            var format = args[2];

            Console.WriteLine($"Source File: {sourceFile}, output file: {outputFile}, format: {format}");

            if (!File.Exists(sourceFile))
            {
                Console.WriteLine("File does not exist");
                return;
            }

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
                        } else
                        {
                            // log to error log
                            Console.WriteLine($"Invalid record: {st}");
                        }
                        
                    }
                    else
                    {
                        // log to error log
                        Console.WriteLine($"Student already exists: {st}");
                    }
                    
                }
            }

            foreach(KeyValuePair<int, Student> entry in students)
            {
                var number = entry.Key;
                var student = entry.Value;
                Console.WriteLine($"{number}: {student.FirstName}");
            }
        }
    }
}
