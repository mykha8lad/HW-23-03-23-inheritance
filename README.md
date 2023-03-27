# Дз от 21/03/23. Properties
## 1. Реализовать индексатор для типа Group
# Для данного задания были определены отдельные исключения для возможных исключительных ситуаций в раннее созданной библиотеке персональных исключений:
## Юсключение для некорректной фамилии
```cs
public class IndexerCountStudentsException : IntException
{
    private readonly string IndexerCountStudentsMessageException = "Please enter a value from the range 0 - {0} (inclusive)\n";
    private string getMessageException() { return this.IndexerCountStudentsMessageException; }

    public IndexerCountStudentsException(int countStudents)
    {
        Console.Write(getMessageException(), countStudents - 1);
    }
}
```
## Юсключение для некорректного диапазона индекса
```cs
public class IndexerLastnameStudentException : StringException
{
    private readonly string strMessageException = "The student with the last name {0} was not found in the group.\n";
    private string getStrMessageException() { return this.strMessageException; }

    public IndexerLastnameStudentException(string lastname)
    {
        Console.Write($"{getStrMessageException()}", lastname);
    }

    public override string ToString() { return $"{StackTrace}\n"; }
}
```
## Индексатор для идентификации элемента по индексу
```cs
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
```
## Пример использования в Main
```cs
Student st = new Student("Vlad", "A", "Ser");
Group gr = new Group();
Console.WriteLine(gr);
gr[0] = st;
Console.WriteLine(gr);
```
[![56.png](https://i.postimg.cc/2SL1YSf0/56.png)](https://postimg.cc/dhvsmv3C)
## Индексатор для идентификации студента по фамилии
```cs
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
```
## Пример использования в Main
```cs
Group gr2 = new Group();
gr2.StudentsInGroup = 5;
Console.WriteLine(gr2);
string index = Console.ReadLine(); // Запрашиваем фамилию
Console.WriteLine($"\nStudent {index}\n{gr2[index]}");
```
## В данном примере производится поиск студента с фамилией Renner. Результат ниже
[![57.png](https://i.postimg.cc/fLRZw89M/57.png)](https://postimg.cc/7Cdj9M9R)
# [Класс Group](https://github.com/mykha8lad/HW-23-03-23-inheritance/blob/main/Group.cs)
## 2  . Реализовать индексатор для типа Group
### Из типа Student выделяем базовый класс Person (переносим все свойства относящиеся к базовому типу из класса Student)
# [Класс Person](https://github.com/mykha8lad/HW-23-03-23-inheritance/blob/main/Person.cs)
### Класс Aspirant, который наследуется от класса Student (наличие дополнительного свойства DissertationTopic)
# [Класс Aspirant](https://github.com/mykha8lad/HW-23-03-23-inheritance/blob/main/Aspirant.cs)
## Вызов в Main
```cs
Person person = new Person("Vitya", "Burka", "Ivanovich");
Console.WriteLine(person);

Student student = new Student();
Console.WriteLine(student);

Aspirant aspirant = new Aspirant("Maks", "Chebanov", "Olegovich", "C#");
Console.WriteLine(aspirant);
```
[![58.png](https://i.postimg.cc/50zwPSsS/58.png)](https://postimg.cc/HcpcnXDr)
