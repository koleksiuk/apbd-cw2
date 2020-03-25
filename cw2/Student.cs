using System;
using System.Xml.Serialization;

namespace cw2
{
    public class StudentValidator
    {
        public StudentValidator(Student student)
        {
            this.Student = student;
        }

        private Student Student { set; get; }

        public Boolean IsValid()
        {
            return Student.FirstName != "" &&
                Student.LastName != "" &&
                Student.Course != "" &&
                Student.TypeOfCourse != "" &&
                Student.Email != "" &&
                Student.FatherName != "" &&
                Student.MotherName != "";
        }
    }

    [Serializable]
    public struct Studies
    {
        [XmlElement("name")]
        public string Course { get; set; }

        [XmlElement("mode")]
        public string TypeOfCourse { get; set; }
    }

    [Serializable]
    [XmlType(TypeName = "student")]
    public struct Student
    {
        [XmlElement("fname")]
        public string FirstName { get; set; }

        [XmlElement("lname")]
        public string LastName { get; set; }

        [XmlElement("studies")]
        public Studies Studies;

        [XmlAttribute("indexNumber")]
        public int Number { get; set; }

        [XmlIgnore]
        public String Course
        {
            set
            {
                Studies.Course = value;
            }
            get
            {
                return Studies.Course;
            }
        }

        [XmlIgnore]
        public String TypeOfCourse
        {
            set
            {
                Studies.TypeOfCourse = value;
            }
            get
            {
                return Studies.TypeOfCourse;
            }
        }

        [XmlIgnore]
        public DateTime Birthdate { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("fathersName")]
        public string FatherName { get; set; }

        [XmlElement("mothersName")]
        public string MotherName { get; set; }

        [XmlElement("birthdate")]
        public String BirthdateString
        {
            set
            {
                try
                {
                    this.Birthdate = Convert.ToDateTime(value);
                }
                catch (FormatException)
                {
                }
            }
            get
            {
                return this.Birthdate.ToString("dd.MM.yyyy");
            }
        }

        public override String ToString()
        {
            return $"Student [{Number}] FirstName: {FirstName}, LastName: {LastName}, Course: {Course}, TypeOfCourse: {TypeOfCourse}, Birthdate: {Birthdate}, Email: {Email}, FatherName: {FatherName}, MotherName: {MotherName}";
        }

        public Boolean IsValid()
        {
            return new StudentValidator(this).IsValid();
        }
    }
}
