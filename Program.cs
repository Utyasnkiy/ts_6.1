using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// Интерфейс "Employee"
interface Employee
{
    string Name { get; set; }
    void Work();
    void Rest();
}

// Абстрактный класс "Engineer" реализует интерфейс "Employee"
abstract class Engineer : Employee
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double Salary { get; set; }

    public abstract void Work();

    public virtual void Rest()
    {
        Console.WriteLine("Engineer is resting");
    }

    public void PrintInformation()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"Salary: {Salary}");
    }
}

// Класс "Supervisor" наследуется от "Engineer"
class Supervisor : Engineer
{
    public string Department { get; set; }

    public string Project { get; set; }

    public override void Work()
    {
        Console.WriteLine("Supervisor is working");
    }

    public void ManageTeam()
    {
        Console.WriteLine("Supervisor is managing the team");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем список объектов типа интерфейса "Employee"
        List<Employee> employees = new List<Employee>();
        employees.Add(new Supervisor());

        // Используя рефлексию, находим все классы, реализующие интерфейс "Employee"
        Assembly assembly = Assembly.LoadFrom("C/programm.dll");
        var employeeTypes = assembly.GetTypes().Where(t => typeof(Employee).IsAssignableFrom(t) && t.IsClass);

        // Получаем список названий классов
        List<string> classNames = employeeTypes.Select(t => t.Name).ToList();

        // На форме, используя "дропдаун" или "ComboBox", отображаем список названий классов

        // При выборе класса из "дропдауна" или "ComboBox"
        string selectedClassName = "Выбранное_название_класса";
        Type selectedClassType = employeeTypes.FirstOrDefault(t => t.Name == selectedClassName);

        // Создаем объект выбранного класса
        Employee selectedEmployee = (Employee)Activator.CreateInstance(selectedClassType);

        // Получаем все методы выбранного класса
        MethodInfo[] methods = selectedClassType.GetMethods();

        // Динамически отображаем методы на форме

        // При нажатии кнопки "Выполнить" и выборе метода
        string selectedMethodName = "Выбранное_название_метода";
        MethodInfo selectedMethod = methods.FirstOrDefault(m => m.Name == selectedMethodName);

        // Получаем параметры метода
        ParameterInfo[] parameters = selectedMethod.GetParameters();

        // Запрашиваем у пользователя значения параметров

        // Создаем массив аргументов для вызова метода
        object[] arguments = new object[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
        {
            Console.Write($"Enter value for parameter '{parameters[i].Name}': ");
            string userInput = Console.ReadLine();
            arguments[i] = Convert.ChangeType(userInput, parameters[i].ParameterType);
        }

        // Вызываем метод на объекте
        selectedMethod.Invoke(selectedEmployee, arguments);
    }
}
