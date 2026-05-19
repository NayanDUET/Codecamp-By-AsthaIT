using System;

class Program3
{
    public void Exception() 
    {
        
        try
        {
            int[] myNumbers = {1, 2, 3};
            Console.WriteLine(myNumbers[10]);
        }
        catch (Exception e)
        {
             Console.WriteLine(e.Message);
        }
    }
}
