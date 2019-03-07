using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Li.MicroIoc
{
    public class ConfigurationSection : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var model = new ConfigModel();
            foreach (XmlNode item in section.ChildNodes)
            {
                if (item.Name == "Service")
                {
                    var name = item.Attributes["name"];
                    if (name == null || string.IsNullOrEmpty(name.Value))
                    {
                        throw new Exception("Service配置缺少name参数");
                    }

                    var type = item.Attributes["type"];
                    if (type == null || string.IsNullOrEmpty(type.Value))
                    {
                        throw new Exception("Service配置缺少type参数");
                    }

                    if (model.ContainsName(name.Value))
                        throw new Exception($"Service配置已包含名称为{name.Value}节点");

                    model.AddReg(name.Value, type.Value);
                }
            }

            return model;
        }
    }
}
