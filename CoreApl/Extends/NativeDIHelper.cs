using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CoreApl.Extends
{

    /// <summary>
    /// 原生DI
    /// </summary>
    public static class NativeDIHelper
    {

        /// <summary>
        /// 批量注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName"></param>
        /// <param name="postfix"></param>
        public static void BatchInjection(this IServiceCollection services, string assemblyName, string postfix)
        {
            var assemblies = GetAssemblies(assemblyName);
            if (assemblies != null && assemblies.Count > 0)
            {
                foreach (var assembly in assemblies)
                {
                    var types = assembly.GetTypes().Where(c => c.IsClass && !c.IsGenericType && c.Name.EndsWith(postfix, StringComparison.OrdinalIgnoreCase));
                    foreach (var implement in types)
                    {
                        var inter = implement.GetInterfaces().Where(c => c.IsInterface && c.Name.EndsWith(postfix, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        services.AddScoped(inter, implement);
                    }

                }
            } 
        }


        /// <summary>
        /// 根据程序集名称获取反射集合
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns></returns>
        public static List<Assembly> GetAssemblies(string assemblyName)
        {
            var assemblies = new List<Assembly>();
            if (!assemblyName.EndsWith("dll", StringComparison.OrdinalIgnoreCase))
                assemblyName = $"{assemblyName}.dll";
            var root = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            foreach (var item in root.GetFiles(assemblyName))
            {
                assemblies.Add(Assembly.LoadFrom(item.FullName));
            }
            return assemblies;
        }

    }
}
