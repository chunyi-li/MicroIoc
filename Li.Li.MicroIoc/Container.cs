using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Li.MicroIoc
{
    public class Container
    {
        ConfigModel config;
        public Container()
        {
            config = (ConfigModel)ConfigurationManager.GetSection("ioc");
        }
        public T Resolve<T>(string name) where T : class
        {
            var assemblyConfig = config.GetAssemblyConfig(name);
            return CreateInstance<T>(assemblyConfig);
        }
        private static T CreateInstance<T>(string assemblyConfig) where T : class
        {
            var items = GetAssemblyInfo(assemblyConfig);
            //加载程序集(dll文件地址)，使用Assembly类   
            Assembly assembly = Assembly.LoadFrom($"{AppDomain.CurrentDomain.BaseDirectory}/bin/{items[1].Trim()}.dll");

            //创建该对象的实例，object类型，参数（名称空间+类） 
            var instance = assembly.CreateInstance(items[0].Trim()) as T;
            if (instance == null)
                throw new NotSupportedException($"加载的程序集{items[0]}.dll未找到继承自{typeof(T).Name}的类型");

            return instance;
        }

        /// <summary>
        /// 获取程序集的DLL名称和类型名
        /// </summary>
        /// <param name="assemblyConfig">dll,类型名</param>
        /// <returns></returns>
        private static string[] GetAssemblyInfo(string assemblyConfig)
        {
            var items = assemblyConfig.Split(',');
            if (items.Length != 2)
                throw new Exception("AppSettings中的TrunkLine.AssemblyConfig配置错误");

            return items;
        }
    }
}
