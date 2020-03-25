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
            }

            List<Student> students = new List<Student>();

            var file = File.Open(sourceFile, FileMode.Open);

            using (var stream = new StreamReader(File.OpenRead(sourceFile)))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] student = line.Split(','); var st = new Student
                    {
                        firstName = student[0],
                        lastName = student[1]
                    };
                }
            }

            Console.WriteLine($"{students}");
        }

        private static Exception FileNotFoundException(string v)
        {
            throw new NotImplementedException();
        }
    }
}
