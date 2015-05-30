using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Easyx264CoderGUI
{
    public static class DeepClone
    {
        public static T GetDeepCopy<T>(T obj)
        {
            T DeepCopyObj;
            if (obj.GetType().IsValueType == true)//值类型
            {
                DeepCopyObj = obj;
            }
            else//引用类型
            {
                DeepCopyObj = (T)System.Activator.CreateInstance(typeof(T)); //创建引用对象
                System.Reflection.MemberInfo[] memberCollection = obj.GetType().GetMembers();
                foreach (System.Reflection.MemberInfo member in memberCollection)
                {
                    if (member.MemberType == System.Reflection.MemberTypes.Field)
                    {
                        System.Reflection.FieldInfo field = (System.Reflection.FieldInfo)member;
                        Object fieldValue = field.GetValue(obj);
                        if (fieldValue is ICloneable)
                            field.SetValue(DeepCopyObj, (fieldValue as ICloneable).Clone());
                        else
                            // T fieldValue = (T)field.GetValue(obj);
                            field.SetValue(DeepCopyObj, GetDeepCopy(fieldValue));
                    }
                }
            }
            return DeepCopyObj;
        }

        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

    }
}
