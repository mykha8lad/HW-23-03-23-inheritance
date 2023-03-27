namespace HW_23_03_23_inheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Идентификация студента по индексу элемента
            Student st = new Student("Vlad", "A", "Ser");
            Group gr = new Group();
            Console.WriteLine(gr);
            gr[0] = st;
            Console.WriteLine(gr);
            #endregion

            #region Идентификация студента по фамилии студента
            Group gr2 = new Group();
            gr2.StudentsInGroup = 5;
            Console.WriteLine(gr2);
            string index = Console.ReadLine();
            Console.WriteLine($"\nStudent {index}\n{gr2[index]}");
            #endregion

            #region 
            Person person = new Person("Vitya", "Burka", "Ivanovich");
            Console.WriteLine(person);

            Student student = new Student();
            Console.WriteLine(student);

            Aspirant aspirant = new Aspirant("Maks", "Chebanov", "Olegovich", "C#");
            Console.WriteLine(aspirant);
            #endregion
        }
    }
}