using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.LoadFile(@"C:\my\ClassLibrary1.dll");

            Type[] types = assembly.GetTypes();

            foreach(var type in types)
            {
                MemberInfo[] members = type.GetMembers();
                foreach(var member in members)
                {
                    Console.WriteLine(member.MemberType + " " + member.Name ); 

                    if(member.MemberType == MemberTypes.Method)
                    {
                        object[] parametres = null;
                        var parametresAsTypes = (member as MethodInfo).GetParameters();
                        if (parametresAsTypes.Count() > 0)
                        {
                            parametres = new object[(member as MethodInfo).GetParameters().Count()];
                            foreach(var par in parametresAsTypes)
                            {
                                if(par.ParameterType == typeof(int))
                                {
                                    parametres[0] = 1;
                                }
                                else if(par.ParameterType == typeof(string))
                                {
                                    parametres[0] = "asdsd";
                                }
                            }
                        }
                        var reuslt = (member as MethodInfo).Invoke(Activator.CreateInstance(type), parametres);

                    }
                }
            }

            Console.ReadLine();
        }
    }
}
