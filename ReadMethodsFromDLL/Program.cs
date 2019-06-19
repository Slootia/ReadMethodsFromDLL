using System;

namespace ReadMethodsFromDLL
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\Иван\\source\\repos\\ReadMethodsFromDLL\\TestLibrary\\bin\\Debug";

            ReadMembers.PrintLibraryMembers(ReadMembers.GetMembers(path));

            Console.ReadKey();
        }
    }
}
