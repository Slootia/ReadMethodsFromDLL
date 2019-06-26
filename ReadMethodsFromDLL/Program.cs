using System;

namespace ReadMethodsFromDLL
{
    class Program
    {
        static void Main(string[] args)
        {
            //Можно задать ввод с клавиатуры во время работы консольного приложения
            //Указать путь до папки с .dll
            string path = "C:\\Users\\Kuzminykh Iv\\source\\repos\\ReadMethodsFromDLL\\TestLibrary\\bin\\Debug";
            string savePath = "C:\\Users\\Kuzminykh Iv\\Desktop";

            ReadMembers.ShowOnConsole(ReadMembers.GetMembersNames(path));
            ReadMembers.WriteInFile(ReadMembers.GetMembersNames(path), savePath);
            Console.ReadKey();
        }
    }
}
