using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int[] arrayNumbers = { 10, 20, 30 };

        foreach (int n in arrayNumbers)
        {
            Console.WriteLine(n);
        }

        List<int> listNumbers = new List<int>();

        listNumbers.Add(10);
        listNumbers.Add(20);
        listNumbers.Add(30);

        Console.WriteLine(listNumbers[1]);

        listNumbers.Remove(20);

        listNumbers.Add(40);

        foreach (int n in listNumbers)
        {
            Console.WriteLine(n);
        }

        Console.WriteLine(listNumbers.Count);

        Console.WriteLine(listNumbers.Contains(30));

        Dictionary<int, string> students =
            new Dictionary<int, string>();

        students.Add(1, "Nayan");
        students.Add(2, "Rahim");
        students.Add(3, "Karim");

        Console.WriteLine(students[1]);

        foreach (var student in students)
        {
            Console.WriteLine(student.Key + " " + student.Value);
        }

        HashSet<int> uniqueNumbers =
            new HashSet<int>();

        uniqueNumbers.Add(10);
        uniqueNumbers.Add(20);
        uniqueNumbers.Add(10);

        foreach (int n in uniqueNumbers)
        {
            Console.WriteLine(n);
        }

        Console.WriteLine(uniqueNumbers.Contains(20));

        Queue<string> customers =
            new Queue<string>();

        customers.Enqueue("Nayan");
        customers.Enqueue("Rahim");
        customers.Enqueue("Karim");

        Console.WriteLine(customers.Peek());

        customers.Dequeue();

        foreach (string customer in customers)
        {
            Console.WriteLine(customer);
        }

        Stack<int> stackNumbers =
            new Stack<int>();

        stackNumbers.Push(10);
        stackNumbers.Push(20);
        stackNumbers.Push(30);

        Console.WriteLine(stackNumbers.Peek());

        stackNumbers.Pop();

        foreach (int n in stackNumbers)
        {
            Console.WriteLine(n);
        }

        List<int> nums = new List<int>()
        {
            10, 20, 30, 40, 50
        };

        var result = nums.Where(x => x > 20);

        foreach (var n in result)
        {
            Console.WriteLine(n);
        }
    }
}