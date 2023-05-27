using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

interface Employee
{
    string Name { get; set; }
    void Work();
    void Rest();
}

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
        List<Employee> employees = new List<Employee>();
        employees.Add(new Supervisor());

        Assembly assembly = Assembly.LoadFrom("C/programm.dll");
        var employeeTypes = assembly.GetTypes().Where(t => typeof(Employee).IsAssignableFrom(t) && t.IsClass);

        List<string> classNames = employeeTypes.Select(t => t.Name).ToList();

        string selectedClassName = "Выбранное_название_класса";
        Type selectedClassType = employeeTypes.FirstOrDefault(t => t.Name == selectedClassName);

        Employee selectedEmployee = (Employee)Activator.CreateInstance(selectedClassType);

        MethodInfo[] methods = selectedClassType.GetMethods();

        string selectedMethodName = "Выбранное_название_метода";
        MethodInfo selectedMethod = methods.FirstOrDefault(m => m.Name == selectedMethodName);

        ParameterInfo[] parameters = selectedMethod.GetParameters();


        object[] arguments = new object[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
        {
            Console.Write($"Enter value for parameter '{parameters[i].Name}': ");
            string userInput = Console.ReadLine();
            arguments[i] = Convert.ChangeType(userInput, parameters[i].ParameterType);
        }

        selectedMethod.Invoke(selectedEmployee, arguments);
    }
}
