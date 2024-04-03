﻿using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CompanyProjectManagement.Data
{
    public class XmlFileManager<T>
    {
        private readonly string _filePath;
        public XmlFileManager(string filePath)
        {
            _filePath = filePath;
        }

        public T ReadData()
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException("XML file not found.", _filePath);
            }

            T data;

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (var stream = new FileStream(_filePath, FileMode.Open)) 
            {
                string xmlString = GetFileStreamString(stream);

                using (var reader = new StringReader(xmlString))
                {
                    data = (T)serializer.Deserialize(reader);
                }
            }

            return data;
        }

        private string GetFileStreamString(Stream fileStream)
        {
            StreamReader streamReader;
            EncodingProvider ppp = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);

            streamReader = new StreamReader(fileStream, Encoding.GetEncoding(1250), true);
            return streamReader.ReadToEnd();
        }
    }
}
