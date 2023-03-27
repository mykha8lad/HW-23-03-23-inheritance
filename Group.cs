using Faker;
using System.Text.RegularExpressions;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using Faker.Resources;
using System.Xml.Linq;
using ExceptionLibrary;

namespace HW_23_03_23_inheritance
{
    public class Group
    {
        RandomDataForGroup randomData = new RandomDataForGroup();

        private List<Student> students = new List<Student>();
        public List<Student> Students
        {
            set
            {
                if (value == null)
                    throw new ArgumentException();
                students = value;
            }
            get
            {
                return this.students;
            }
        }

        private int studentsInGroup = 10;
        private string groupName;
        private string groupSpecialization;
        private int courseNumber;

        public int StudentsInGroup
        {
            set
            {
                if (value < 5 || value > 15) throw new CountStudentsException();

                if (value > this.StudentsInGroup)
                {
                    for (int student = value; student != StudentsInGroup; --student)
                    {
                        string phoneRegexp = @"^\(\d{3}\)\d{3}\-\d{4}$";
                        string phoneNumber;
                        do
                        {
                            phoneNumber = Faker.Phone.Number();
                        } while (!Regex.IsMatch(phoneNumber, phoneRegexp));
                        Random random = new Random();
                        DateTime birthday = new DateTime(random.Next(2003, 2007), random.Next(1, 13), random.Next(1, 29));
                        Students.Add(new Student(Faker.Name.First(), Faker.Name.Last(), Faker.Name.Middle(), birthday, phoneNumber, Faker.Address.City(), Faker.Address.StreetName(), Faker.Address.ZipCode()));
                    }
                }
                else if (value < this.StudentsInGroup)
                {
                    for (int student = StudentsInGroup; student != value; --student)
                    {
                        Students.RemoveAt(student - 1);
                    }
                }

                this.studentsInGroup = value;
            }
            get
            {
                return this.studentsInGroup;
            }
        }
        public string GroupName
        {
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value)) throw new StringException();
                this.groupName = value;
            }
            get
            {
                return this.groupName;
            }
        }
        public string GroupSpecialization
        {
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value)) throw new StringException();
                this.groupSpecialization = value;
            }
            get
            {
                return this.groupSpecialization;
            }
        }
        public int CourseNumber
        {
            set
            {
                if (value < 1 || value > 5) throw new CourseNumberException();
                this.courseNumber = value;
            }
            get
            {
                return this.courseNumber;
            }
        }

        public Group()
        {
            createGroup(randomData.GroupNames[new Random().Next(randomData.GroupNames.Count)], randomData.GroupSpecializations[new Random().Next(randomData.GroupSpecializations.Count)], randomData.CoursesNumber[new Random().Next(randomData.CoursesNumber.Count)]);
        }
        public Group(Group group)
        {
            GroupName = group.GroupName;
            GroupSpecialization = group.GroupSpecialization;
            CourseNumber = group.CourseNumber;
            copyToThisListStudents(group.Students);
            group.clearGroup();
            group = null;
        }
        public Group(List<Student> oldListStudents)
        {
            GroupName = randomData.GroupNames[new Random().Next(randomData.GroupNames.Count)];
            GroupSpecialization = randomData.GroupSpecializations[new Random().Next(randomData.GroupSpecializations.Count)];
            CourseNumber = randomData.CoursesNumber[new Random().Next(randomData.CoursesNumber.Count)];
            copyToThisListStudents(oldListStudents);
            oldListStudents.Clear();
        }
        public Group(string groupName, string groupSpecialization, int courseNumber, int countStudents)
        {
            StudentsInGroup = countStudents;
            createGroup(groupName, groupSpecialization, courseNumber);
        }
        public Group(List<Student> oldListStudents, string groupName, string groupSpecialization, int courseNumber)
        {
            GroupName = groupName;
            GroupSpecialization = groupSpecialization;
            CourseNumber = courseNumber;
            copyToThisListStudents(oldListStudents);
            oldListStudents.Clear();
        }

        private void deleteStudent(Student student) { Students.Remove(student); }
        private void copyToThisListStudents(List<Student> oldListStudents)
        {
            foreach (Student student in oldListStudents)
            {
                this.Students.Add(student);
            }
        }
        private void clearGroup()
        {
            students.Clear();
            groupName = null;
            groupSpecialization = null;
            courseNumber = 0;
        }

        // operator overloading
        public override int GetHashCode() { return StudentsInGroup.GetHashCode(); }
        public override bool Equals(object obj)
        {
            Group group = obj as Group;
            if (group == null || GetType() != group.GetType()) { throw new ArgumentException(); }

            return this.StudentsInGroup == group.StudentsInGroup;
        }

        public static bool operator ==(Group firstGroup, Group secondGroup)
        {
            if (object.ReferenceEquals(firstGroup, secondGroup)) { return true; }

            if (object.ReferenceEquals(firstGroup, null) || object.ReferenceEquals(secondGroup, null)) { return false; }

            return firstGroup.StudentsInGroup == secondGroup.StudentsInGroup;
        }
        public static bool operator !=(Group firstGroup, Group secondGroup) { return !(firstGroup == secondGroup); }

        public static bool operator >(Group firstGroup, Group secondGroup)
        {
            return firstGroup.StudentsInGroup > secondGroup.StudentsInGroup;
        }
        public static bool operator <(Group firstGroup, Group secondGroup)
        {
            return firstGroup.StudentsInGroup > secondGroup.StudentsInGroup;
        }

        public static bool operator >=(Group firstGroup, Group secondGroup)
        {
            return firstGroup.StudentsInGroup > secondGroup.StudentsInGroup;
        }
        public static bool operator <=(Group firstGroup, Group secondGroup)
        {
            return firstGroup.StudentsInGroup > secondGroup.StudentsInGroup;
        }

        public Student this[int index]
        {
            get
            {
                if (index < 0 || index > StudentsInGroup)
                    throw new IndexerCountStudentsException(StudentsInGroup - 1);
                return students[index];
            }
            set
            {
                if (index < 0 || index > StudentsInGroup)
                    throw new IndexerCountStudentsException(StudentsInGroup - 1);
                students[index] = value;
            }
        }
        public Student this[string lastname]
        {
            get
            {
                Student student = students.Find(s => s.Lastname == lastname);
                if (student == null)
                    throw new IndexerLastnameStudentException(lastname);
                return student;
            }
            set
            {
                Student student = students.Find(s => s.Lastname == lastname);
                if (student == null)
                    throw new IndexerLastnameStudentException(lastname);

                int index = students.FindIndex(s => s.Lastname == lastname);
                students[index] = value;
            }
        }
        // operator overloading

        public void createGroup(string groupName, string groupSpecialization, int courseNumber)
        {
            GroupName = groupName;
            GroupSpecialization = groupSpecialization;
            CourseNumber = courseNumber;

            for (int student = 1; student <= StudentsInGroup; ++student)
            {
                string phoneRegexp = @"^\(\d{3}\)\d{3}\-\d{4}$";
                string phoneNumber;
                do
                {
                    phoneNumber = Faker.Phone.Number();
                } while (!Regex.IsMatch(phoneNumber, phoneRegexp));
                Random random = new Random();
                DateTime birthday = new DateTime(random.Next(2003, 2007), random.Next(1, 13), random.Next(1, 29));
                Students.Add(new Student(Faker.Name.First(), Faker.Name.Last(), Faker.Name.Middle(), birthday, phoneNumber, Faker.Address.City(), Faker.Address.StreetName(), Faker.Address.ZipCode()));
            }
        }

        public void addStudentInGroup(Student student) { students.Add(student); }

        private void ShowStudentMenu()
        {
            int userAnswer;
            bool flag = true;
            Student st = null;

            Console.WriteLine($"Enter id student this group ({GroupName})");
            int id = int.Parse(Console.ReadLine());

            foreach (Student student in students)
            {
                if (student.Id == id)
                {
                    st = student;
                    break;
                }
            }

            while (flag)
            {
                Console.WriteLine("Enter menu item\n1 - Name\n2 - Lastname\n3 - Surname\n4 - Phone number < (xxx)xxx-xxxx >\n5 - Birthday < DD.MM.YYYY >\n6 - Address\n7 - EXIT");
                do
                {
                    Console.Write("> ");
                    userAnswer = int.Parse(Console.ReadLine());
                } while (userAnswer < 1 || userAnswer > 7);

                switch (userAnswer)
                {
                    case (int)StudentMenu.STUDENT_NAME:
                        Console.WriteLine("Enter name student");
                        string stName;
                        do
                        {
                            Console.Write("> ");
                            stName = Console.ReadLine();
                        } while (String.IsNullOrEmpty(stName));
                        st.Name = stName;
                        break;
                    case (int)StudentMenu.STUDENT_LASTNAME:
                        Console.WriteLine("Enter lastname student");
                        string stLastName;
                        do
                        {
                            Console.Write("> ");
                            stLastName = Console.ReadLine();
                        } while (String.IsNullOrEmpty(stLastName));
                        st.Lastname = stLastName;
                        break;
                    case (int)StudentMenu.STUDENT_SURNAME:
                        Console.WriteLine("Enter surname student");
                        string stSurname;
                        do
                        {
                            Console.Write("> ");
                            stSurname = Console.ReadLine();
                        } while (String.IsNullOrEmpty(stSurname));
                        st.Surname = stSurname;
                        break;
                    case (int)StudentMenu.STUDENT_PHONE_NUMBER:
                        Console.WriteLine("Enter phone student (xxx)xxx-xxxx");
                        string phoneNumber;
                        do
                        {
                            Console.Write("> ");
                            phoneNumber = Console.ReadLine();
                        } while (String.IsNullOrEmpty(phoneNumber));
                        st.PhoneNumber = phoneNumber;
                        break;
                    case (int)StudentMenu.STUDENT_BIRTHDAY:
                        Console.WriteLine("Enter birthday xx.xx.xxxx");
                        DateTime birthday;
                        do
                        {
                            Console.Write("> ");
                            birthday = DateTime.Parse(Console.ReadLine());
                        } while (String.IsNullOrEmpty(birthday.ToString("d")));
                        st.Birthday = birthday;
                        break;
                    case (int)StudentMenu.STUDENT_ADDRESS:
                        string city;
                        string street;
                        string homeNumber;

                        Console.WriteLine("Enter address");
                        do
                        {
                            Console.Write("City > ");
                            city = Console.ReadLine();
                            Console.Write("Street > ");
                            street = Console.ReadLine();
                            Console.Write("Home Number > ");
                            homeNumber = Console.ReadLine();
                        } while (String.IsNullOrEmpty(city) & String.IsNullOrEmpty(street) & String.IsNullOrEmpty(homeNumber));
                        st.Address = new Address(city, street, homeNumber);
                        break;
                    case (int)StudentMenu.STUDENT_EXIT:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Wrong item");
                        break;
                }
            }
        }
        private void ShowGroupMenu()
        {
            int userAnswer;
            bool flag = true;

            while (flag)
            {
                Console.WriteLine("Enter menu item\n1 - Name group\n2 - Group Specialization\n3 - Course number\n4 - EXIT");
                do
                {
                    Console.Write("> ");
                    userAnswer = int.Parse(Console.ReadLine());
                } while (userAnswer < 1 || userAnswer > 4);

                switch (userAnswer)
                {
                    case (int)GroupMenu.GROUP_NAME:
                        Console.WriteLine("Enter group name");
                        string gName;
                        do
                        {
                            Console.Write("> ");
                            gName = Console.ReadLine();
                        } while (!randomData.GroupNames.Contains(gName));
                        GroupName = gName;
                        break;
                    case (int)GroupMenu.GROUP_SPECIALIZATION:
                        Console.WriteLine("Enter group specialization");
                        string gSpec;
                        do
                        {
                            Console.Write("> ");
                            gSpec = Console.ReadLine();
                        } while (!randomData.GroupSpecializations.Contains(gSpec));
                        GroupSpecialization = gSpec;
                        break;
                    case (int)GroupMenu.COURSE_NUMBER:
                        Console.WriteLine("Enter course number");
                        int gCourse;
                        do
                        {
                            Console.Write("> ");
                            gCourse = int.Parse(Console.ReadLine());
                        } while (!randomData.CoursesNumber.Contains(gCourse));
                        CourseNumber = gCourse;
                        break;
                    case (int)GroupMenu.GROUP_EXIT:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Wrong item");
                        break;
                }
            }
        }
        public void editData()
        {
            int userAnswer;
            bool flag = true;

            while (flag)
            {
                Console.WriteLine("Enter item menu\n1 - Edit group\n2 - Edit student info\n3 - EXIT");
                do
                {
                    Console.Write("> ");
                    userAnswer = int.Parse(Console.ReadLine());
                } while (userAnswer < 1 || userAnswer > 3);

                switch (userAnswer)
                {
                    case 1:
                        ShowGroupMenu();
                        break;
                    case 2:
                        ShowStudentMenu();
                        break;
                    case 3:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Wrong item");
                        break;
                }
            }
        }

        public void studentTransfer(Group group)
        {
            Console.WriteLine($"Enter id student this group ({GroupName}), for transfer in group {group.GroupName}");
            int id = int.Parse(Console.ReadLine());

            foreach (Student student in students)
            {
                if (student.Id == id)
                {
                    group.students.Add(student);
                    deleteStudent(student);
                    break;
                }
            }
        }

        public void deletingAllStudentPassSession()
        {
            students.RemoveAll(s => s.Offsets.Any(score => score < 7));
        }
        public void deleteFailedStudent()
        {
            double minAvg = double.MaxValue;
            Student failedStudent = null;
            foreach (Student student in students)
            {
                double avg = 0;
                avg += student.Offsets.Average() + student.Hometasks.Average() + student.Exams.Average();

                if (avg < minAvg)
                {
                    minAvg = avg;
                    failedStudent = student;
                }
            }
            Console.WriteLine($"Student {failedStudent.Name} ({failedStudent.Id}) remove");
            deleteStudent(failedStudent);
        }

        private string getAllStudentsInfo()
        {
            students.Sort((firstStudent, secondStudent) => firstStudent.Lastname.CompareTo(secondStudent.Lastname));
            return string.Join("\n\n", Students);
        }
        public override string ToString()
        {
            return $"Group: {GroupName}. Specialization: {GroupSpecialization}. Course: {CourseNumber}. Count students: {StudentsInGroup}\n" +
                $"--------------------------------------------------------------------------------\n" +
                $"{getAllStudentsInfo()}\n";
        }
    }
}
