using System;
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

    public struct Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Course { get; set; }
        public string TypeOfCourse { get; set; }
        public int Number { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }

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
