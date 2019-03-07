using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Li.MicroIoc
{
    public class ConfigModel
    {
        private Dictionary<string, string> dic = new Dictionary<string, string>();

        public void AddReg(string name, string assemblyConfig)
        {
            this.dic.Add(name, assemblyConfig);
        }

        public bool ContainsName(string name)
        {
            return dic.ContainsKey(name);
        }

        public string GetAssemblyConfig(string name)
        {
            return this.dic[name];
        }
    }
}
