using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Wpf_CooperativeFarmers.Classes
{
    public class MyStorage
    {
        public static void WriteXml<T>(T data, string fileName)
        {
            XmlSerializer sr = new XmlSerializer(typeof(T));

            FileStream stream;

            stream = new FileStream(fileName, FileMode.Create);
            sr.Serialize(stream, data);
            stream.Close();

        }

        public static T ReadXML<T>(string fileName)
        {

            try
            {
                using (StreamReader stream = new StreamReader(fileName))
                {
                    XmlSerializer sr = new XmlSerializer(typeof(T));
                    return (T)sr.Deserialize(stream);
                }
            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message, "ERROR");
                return default(T);

            }


        }
    }
}
