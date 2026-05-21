using System;
using System.Collections.Generic;

class Box<T>
{
    public T Value;

    public Box(T value)
    {
        Value = value;
    }

    public void Show()
    {
        Console.WriteLine(Value);
    }
}

class Program
{
    static void Print<T>(T data)
    {
        Console.WriteLine(data);
    }

    static void Main()
    {
        Box<int> number = new Box<int>(100);
        number.Show();

        Box<string> text = new Box<string>("Hello");
        text.Show();

        Print<int>(50);
        Print<string>("C# Generic");

        List<string> names = new List<string>();

        names.Add("Nayan");
        names.Add("Rahim");

        foreach (string name in names)
        {
            Console.WriteLine(name);
        }
    }
}