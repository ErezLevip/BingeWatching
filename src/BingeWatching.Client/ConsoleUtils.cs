using System;
using System.Collections.Generic;
using System.Text;

namespace BingeWatching.Client
{
    public static class ConsoleUtils
    {
        public static T AskUserForData<T>(string text)
        {
            Console.Write(text);
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                return default(T);
            return (T)Convert.ChangeType(input, typeof(T));
        }
        public static T AskUserForDataMultiLine<T>(string[] multiLineText)
        {
            foreach (var txt in multiLineText)
            {
                Console.WriteLine(txt);
            }
            return AskUserForData<T>("selection:");
        }
        public static void PrintObject(object obj, string lineSeperator = "")
        {
            var props = obj.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var p in props)
            {
                if (p.PropertyType.IsArray)
                {
                    foreach (var innerObj in (IList<object>)obj)
                    {
                        PrintObject(innerObj, lineSeperator);
                    }
                }
                else
                {
                    var val = p.GetValue(obj);
                    if (val != null)
                    {
                        if (p.PropertyType.IsClass && p.PropertyType != typeof(string))
                            PrintObject(val, lineSeperator);
                        else
                            Console.WriteLine($"{p.Name}: {val}");
                    }
                }

            }
            Console.WriteLine(lineSeperator);
        }
        public static void PrintMultipleObjects(IEnumerable<object> objects, string lineSeperator = "")
        {
            foreach (var obj in objects)
            {
                PrintObject(obj, lineSeperator);
            }
        }
    }
}
