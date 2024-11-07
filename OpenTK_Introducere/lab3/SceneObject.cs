
using System.IO;
using System.Xml.Serialization;

namespace lab3
{
    /// <summary>
    /// Aceasta clasa abstracta trebuie sa o mosteneasca toate obiectele scenei 3D.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SceneObject<T>
    {
        public abstract void SetDefault();

        public void SerializeXml(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (FileStream file = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                serializer.Serialize(file, this);
            }
        }

        public T DeserializeXml(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (FileStream file = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                return (T)serializer.Deserialize(file);
            }
        }
    }
}
