using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    public class BinarySerialization
    {
        private const string path = @"E:\For_Serialization\Dataaa.txt";

        public static void Serialize(object o)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Directory.CreateDirectory(@"E:\For_Serialization");
            File.Create(path).Dispose();
            using (FileStream sw = new FileStream(path, FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(sw, o);
            }
        }
        public static object Deserialize()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    return binaryFormatter.Deserialize(fs);
                }
            }
            else
            {
                return null;
            }
        }
    }
}
