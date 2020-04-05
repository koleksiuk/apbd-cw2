using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace cw2
{
    [Serializable]
    [XmlType(TypeName = "uczelnia")]
    public class University
    {
        public University() { }
        public University(List<Student> students)
        {
            Students = students;
        }

        [XmlArray("studenci")]
        public List<Student> Students { get; set; }

        [XmlAttribute("author")]
        public string Author { get; set; }

        [XmlAttribute("createdAt")]
        public String CreatedAt {
            get; set;
        }

        [XmlArray("activeStudies")]
        public List<UniversityCourse> Courses
        {
            get
            {
                var allCourses = new Dictionary<string, UniversityCourse>();

                Students.ForEach(delegate (Student s)
                {
                    if (allCourses.ContainsKey(s.Studies.Course))
                    {
                        allCourses[s.Studies.Course].NumberOfStudents += 1;
                    }
                    else
                    {
                        allCourses.Add(s.Studies.Course, new UniversityCourse(s.Studies.Course));
                    }
                });

                return new List<UniversityCourse>(allCourses.Values);
            }
            set
            {
                Courses = value;
            }
        }
    }
}
