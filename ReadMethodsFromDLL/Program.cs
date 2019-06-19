using System;

namespace ReadMethodsFromDLL
{
    class Program
    {
        static void Main(string[] args)
        {
            //Можно задать ввод с клавиатуры во время работы консольного приложения
            //Указать путь до папки с .dll
            string path = "C:\\Users\\Иван\\source\\repos\\ReadMethodsFromDLL\\TestLibrary\\bin\\Debug"; 

            //реализация через прописанное в ТЗ
            ReadMembers.PrintLibraryMembers(ReadMembers.GetMembersNames(path));

            Console.ReadKey();
        }
    }
}
