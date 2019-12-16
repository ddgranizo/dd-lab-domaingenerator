using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using IO = System.IO;

namespace DD.Lab.Services.System.Utilities
{
    public static class StoredDataManager
    {

        public static T GetStoredData<T>(string path) where T: class
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("message", nameof(path));
            }
            var content = File.ReadAllText(path);
            if (!string.IsNullOrEmpty(content))
            {
                T data = default(T);
                using (var stream = IO.File.OpenRead(path))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    data = serializer.Deserialize(stream) as T;
                }
                return data;
            }
            throw new Exception("Path file is empty");
        }



        public static void SaveStoredData<T>(string path, T data) where T : class
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("message", nameof(path));
            }
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            using (var writer = new IO.StreamWriter(path))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, data);
                writer.Flush();
            }
        }
    }
}
