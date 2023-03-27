using System;

namespace HW_23_03_23_inheritance
{
    public enum GroupMenu
    {
        GROUP_NAME = 1,
        GROUP_SPECIALIZATION,
        COURSE_NUMBER,
        GROUP_EXIT
    }

    public enum StudentMenu
    {
        STUDENT_NAME = 1,
        STUDENT_LASTNAME,
        STUDENT_SURNAME,
        STUDENT_PHONE_NUMBER,
        STUDENT_BIRTHDAY,
        STUDENT_ADDRESS,
        STUDENT_EXIT
    }

    public class RandomDataForGroup
    {
        public List<string> GroupNames { get; } = new List<string>() { "P10", "P11", "P12", "P13", "P14", "P15" };
        public List<string> GroupSpecializations { get; } = new List<string>() { "C++", "JavaScript", "C", "C#", "Python", "Java", "Ruby", "PHP" };
        public List<int> CoursesNumber { get; } = new List<int>() { 1, 2, 3, 4, 5 };
    }
}
