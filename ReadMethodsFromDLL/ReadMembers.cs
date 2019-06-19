using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;

namespace ReadMethodsFromDLL
{
    public static class ReadMembers
    {


        /// <summary>
        /// Метод возвращающий имя класса и его имена всех public и protected методов
        /// </summary>
        /// <param name="path">Путь до папки с библиотеками (.dll)</param>
        /// <returns>Словарь. Ключ: Имя класса, Значение: Список имен public и protected методов</returns>
        public static Dictionary<string, List<string>> GetMembersNames(string path)
        {
            Dictionary<Type, MethodInfo[]> classMethodsDictionary = GetMembers(path);
            Dictionary<string, List<string>> classMembersDictionary = new Dictionary<string, List<string>>();
            foreach (var item in classMethodsDictionary)
            {
                List<string> tempMembersName = new List<string>();
                foreach (var i in item.Value)
                {
                    tempMembersName.Add(i.Name);
                }
                classMembersDictionary.Add(item.Key.Name, tempMembersName);
            }
            return classMembersDictionary;
        }

        /// <summary>
        /// Метод вывода имени класса и названия его имен всех public и protected методов
        /// </summary>
        /// <param name="classMethodsDictionary">Словарь имен классов и список методов класса</param>
        public static void PrintLibraryMembers(Dictionary<string, List<string>> classMethodsDictionary)
        {
            Console.OutputEncoding = Encoding.UTF8;
            //TODO: Проверить на пустой value
            foreach (var cl in classMethodsDictionary)
            {
                Console.WriteLine($"\u2022 {cl.Key}\n");
                foreach (var method in cl.Value)
                {
                    Console.WriteLine($"\t\u25E6 {method}");
                }
                Console.WriteLine("\n");
            }
        }

        //TODO: Добавить проверку на пустую папку
        private static string[] GetFileNames(string path) =>
            Directory.GetFiles(path, "*.dll");

        private static Dictionary<string, Type[]> GetAssemblyTypes(string[] filePaths)
        {
            Dictionary<string, Type[]> fileTypesDictionary = new Dictionary<string, Type[]>();
            //TODO: Добавить печать ексепшна, если не получилось открыть файл
            foreach (var fileName in filePaths)
                fileTypesDictionary.Add(fileName, Assembly.LoadFrom(fileName).GetTypes());
            fileTypesDictionary.Values.GroupBy(n => n.GroupBy(a => a.Name));
            return fileTypesDictionary;
        }
        private static Dictionary<Type, MethodInfo[]> GetMembers(string path)
        {
            string[] fileNames = GetFileNames(path);
            Dictionary<string, Type[]> fileTypesDictionary = new Dictionary<string, Type[]>();
            Dictionary<Type, MethodInfo[]> classMethodsDictionary = new Dictionary<Type, MethodInfo[]>();
            fileTypesDictionary = GetAssemblyTypes(fileNames);

            //TODO: Проверить на пустую сборку
            foreach (var types in fileTypesDictionary.Values)
            {
                foreach (var type in types)
                {
                    classMethodsDictionary.Add(type, type.GetMethods());
                }
            }
            return classMethodsDictionary;
        }
    }
}
