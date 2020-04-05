using System;
using System.Xml.Serialization;

namespace cw2
{
    [Serializable]
    [XmlType(TypeName = "studies")]
    public class UniversityCourse
    {
        public UniversityCourse() { }
        public UniversityCourse(string name)
        {
            Name = name;
            NumberOfStudents = 0;
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("numberOfStudents")]
        public int NumberOfStudents { get; set; }
    }
}
