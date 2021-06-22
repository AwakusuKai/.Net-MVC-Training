using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Mappers
{
    public static class Mapper
    {
        public static I Convert<T, I>(T obj) where I : new()
        {
            I result = new I();
            foreach (PropertyInfo property in typeof(I).GetProperties())
            {
                PropertyInfo propertyInfo = typeof(T).GetProperty(property.Name);
                if (property.CanWrite)
                {
                    property.SetValue(result, System.Convert.ChangeType(propertyInfo.GetValue(obj), property.PropertyType), null);
                    continue;
                }
            }
            return result;
        }

    }
}
