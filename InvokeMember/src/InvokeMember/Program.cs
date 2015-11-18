using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime;

namespace InvokeMember
{
    public class Program
    {
        public void Main(string[] args)
        {
            InvokeMemberOnType(typeof (Program), new Program(),  "DoStuff", null);
            Console.Read();
        }

        public void DoStuff()
        {
            Console.WriteLine("Doing stuff");
        }

        private static object InvokeMemberOnType(Type type, object target, string name, object[] args)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            try
            {
                // Try to invokethe method
                return type.InvokeMember(
                    name,
                    BindingFlags.InvokeMethod | bindingFlags,
                    null,
                    target,
                    args);
            }
            catch (MissingMethodException)
            {
                // If we couldn't find the method, try on the base class
                if (type.GetTypeInfo().BaseType != null)
                {
                    return InvokeMemberOnType(type.GetTypeInfo().BaseType, target, name, args);
                }
                //Don't care if the method don't exist.
                return null;
            }
        }
    }

}