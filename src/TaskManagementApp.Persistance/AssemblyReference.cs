

using System.Reflection;

namespace TaskManagementApp.Persistance
{
    public class AssemblyReference
    {
         public static readonly Assembly Assembly = typeof(Assembly).Assembly;
    }
}