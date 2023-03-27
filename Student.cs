using System;
using System.Text.RegularExpressions;
using ExceptionLibrary;

namespace HW_23_03_23_inheritance
{
    public class Student
    {
        private int id;
        private string name;
        private string lastname;
        private string surname;
        private string phoneNumber;
        private DateTime birthday;
        private Address address;

        private List<int> offsets = new List<int>();
        private List<int> hometasks = new List<int>();
        private List<int> exams = new List<int>();

        public int Id
        {
            set
            {
                this.id = value;
            }
            get
            {
                return this.id;
            }
        }
        public string Name
        {
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value)) throw new StringException();
                this.name = value;
            }
            get
            {
                return this.name;
            }
        }
        public string Lastname
        {
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value)) throw new StringException();
                this.lastname = value;
            }
            get
            {
                return this.lastname;
            }
        }
        public string Surname
        {
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value)) throw new StringException();
                this.surname = value;
            }
            get
            {
                return this.surname;
            }
        }
        public string PhoneNumber
        {
            set
            {
                string phoneRegexp = @"^\(\d{3}\)\d{3}\-\d{4}$";
                if (Regex.IsMatch(value, phoneRegexp)) this.phoneNumber = value;
                else this.phoneNumber = "(000)000-0000";
            }
            get
            {
                return this.phoneNumber;
            }
        }
        public DateTime Birthday
        {
            set
            {
                this.birthday = value;
            }
            get
            {
                return this.birthday;
            }
        }
        public Address Address
        {
            set
            {
                this.address = value;
            }
            get
            {
                return this.address;
            }
        }

        public List<int> Offsets
        {
            set
            {
                this.offsets = new List<int>();
            }
            get
            {
                return this.offsets;
            }
        }
        public List<int> Hometasks
        {
            set
            {
                this.hometasks = new List<int>();
            }
            get
            {
                return this.hometasks;
            }
        }
        public List<int> Exams
        {
            set
            {
                this.exams = new List<int>();
            }
            get
            {
                return this.exams;
            }
        }

        public Student(string name, string lastname, string surname, DateTime birthday, string phoneNumber, string city, string street, string homeNumber)
        {
            Name = name;
            Lastname = lastname;
            Surname = surname;
            PhoneNumber = phoneNumber;
            Birthday = birthday;
            Address = new Address(city, street, homeNumber);            
            Id = new Random().Next(357943, 8357235);
            fillingLists();
        }
        public Student(string name, string lastname, string surname, DateTime birthday, string phoneNumber) :
            this(name, lastname, surname, birthday, phoneNumber, "None", "None", "None")
        { }
        public Student(string name, string lastname, string surname) :
            this(name, lastname, surname, new DateTime(1, 1, 1), "(000)000-0000", "None", "None", "None")
        { }
        public Student() :
            this("None", "None", "None", new DateTime(1, 1, 1), "(000)000-0000", "None", "None", "None")
        { }
                               
        public double getAverageMark()
        {
            double avgMark = 0;
            avgMark += Offsets.Average() + Hometasks.Average() + Exams.Average();

            return Math.Truncate(avgMark);
        }
        public void fillingLists()
        {
            for (int i = 0; i < 7; ++i)
            {
                this.offsets.Add(new Random().Next(1, 13));
                this.hometasks.Add(new Random().Next(1, 13));
                this.exams.Add(new Random().Next(1, 13));
            }
        }        

        // operator overloading
        public override int GetHashCode() { return getAverageMark().GetHashCode(); }
        public override bool Equals(object obj)
        {
            Student student = obj as Student;
            if (student == null || GetType() != student.GetType()) { throw new ArgumentException(); }

            return this.getAverageMark() == student.getAverageMark();
        }

        public static bool operator ==(Student firstStudent, Student secondStudent)
        {
            if (object.ReferenceEquals(firstStudent, secondStudent)) { return true; }

            if (object.ReferenceEquals(firstStudent, null) || object.ReferenceEquals(secondStudent, null)) { return false; }

            return firstStudent.getAverageMark() == secondStudent.getAverageMark();
        }
        public static bool operator !=(Student firstStudent, Student secondStudent) { return !(firstStudent == secondStudent); }

        public static bool operator >(Student firstStudent, Student secondStudent)
        {
            return firstStudent.getAverageMark() > secondStudent.getAverageMark();
        }
        public static bool operator <(Student firstStudent, Student secondStudent)
        {
            return firstStudent.getAverageMark() > secondStudent.getAverageMark();
        }

        public static bool operator >=(Student firstStudent, Student secondStudent)
        {
            return firstStudent.getAverageMark() > secondStudent.getAverageMark();
        }
        public static bool operator <=(Student firstStudent, Student secondStudent)
        {
            return firstStudent.getAverageMark() > secondStudent.getAverageMark();
        }
        // operator overloading

        public string getListOffsetsForToString() { return string.Join(" ", Offsets); }
        public string getListHometasksForToString() { return string.Join(" ", Hometasks); }
        public string getListExamsForToString() { return string.Join(" ", Exams); }

        public override string ToString()
        {
            return ($"ID: {Id}\n" +
                $"Student: {Lastname} {Name} {Surname}\n" +
                $"Birthday: {Birthday.Date.ToString("d")}\n" +
                $"Address: {Address}\n" +
                $"Phone number: {PhoneNumber}\n" +
                $"Rating\n" +
                $"Scores offsets - {getListOffsetsForToString()}\n" +
                $"Scores hometasks - {getListHometasksForToString()}\n" +
                $"Scores exams - {getListExamsForToString()}\n" +
                $"Average score: {getAverageMark()}\n");
        }
    }
}