using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Utils
{
    public static class ObjectUtils
    {
        public static byte[] ConvertAnyObjectToByteArray(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            BinaryFormatter bf = new BinaryFormatter();
            using MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        public static T ConvertByteArrayToObject<T>(byte[] arr)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using MemoryStream ms = new MemoryStream(arr);
            object obj = bf.Deserialize(ms);
            return (T) obj;
        }
    }
}