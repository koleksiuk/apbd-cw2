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
    }
}
