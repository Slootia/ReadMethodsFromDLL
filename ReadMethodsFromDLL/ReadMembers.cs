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

        private static string[] GetFileNames(string path) =>
            Directory.GetFiles(path, "*.dll");

        private static Dictionary<string, Type[]> GetAssemblyTypes(string[] filePaths)
        {
            Dictionary<string, Type[]> fileTypesDictionary = new Dictionary<string, Type[]>();

            foreach (var fileName in filePaths)
                fileTypesDictionary.Add(fileName, Assembly.LoadFrom(fileName).GetTypes());
            fileTypesDictionary.Values.GroupBy(n => n.GroupBy(a => a.Name));
            return fileTypesDictionary;
        }

        public static Dictionary<Type, MethodInfo[]> GetMembers(string path)
        {
            string[] fileNames = GetFileNames(path);
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

        public static void PrintLibraryMembers(Dictionary<Type, MethodInfo[]> classMethodsDictionary)
        {
            Console.OutputEncoding = Encoding.UTF8;
            foreach (var cl in classMethodsDictionary)
            {
                Console.WriteLine($"\u2022 {cl.Key.Name}\n");
                foreach (var method in cl.Value)
                {
                    Console.WriteLine($"\t\u25E6 {method.Name}");
                }
                Console.WriteLine("\n");
            }
        }
    }
}
