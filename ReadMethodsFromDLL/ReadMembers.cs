using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Diagnostics;

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

        private static string[] GetFileNames(string path)
        {
            string[] files = null;
            try
            {
                files = Directory.GetFiles(path, "*.dll");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("ERROR! Не найдена директория: " + path);
                Debug.WriteLine("ERROR! Не найдена директория: " + path);
                throw;
            }
            return files;
        }

        private static Dictionary<string, Type[]> GetAssemblyTypes(string[] filePaths)
        {
            Dictionary<string, Type[]> fileTypesDictionary = new Dictionary<string, Type[]>();
            try
            {
                foreach (var fileName in filePaths)
                    fileTypesDictionary.Add(fileName, Assembly.LoadFrom(fileName).GetTypes());
            }
            catch (BadImageFormatException e)
            {
                Console.WriteLine("ERROR! Ошибка чтения файла: " + e.FileName);
                Debug.WriteLine("ERROR! Ошибка чтения файла: " + e.FileName);
            }
            fileTypesDictionary.Values.GroupBy(n => n.GroupBy(a => a.Name));
            return fileTypesDictionary;
        }
        private static Dictionary<Type, MethodInfo[]> GetMembers(string path)
        {
            string[] fileNames = GetFileNames(path);
            if (fileNames.Count() == 0)
                Console.WriteLine("WARNING! Папка не содержит библиотек(.dll)!");
            Dictionary<string, Type[]> fileTypesDictionary = new Dictionary<string, Type[]>();
            Dictionary<Type, MethodInfo[]> classMethodsDictionary = new Dictionary<Type, MethodInfo[]>();
            fileTypesDictionary = GetAssemblyTypes(fileNames);

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
