﻿using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace inzynierkaXamarin
{
        class ServiceXml
        {
            public static T GenericDeserialize<T>(string xml)
            {
                using (TextReader tr = new StringReader(xml))
                {
                    XmlSerializer sr = new XmlSerializer(typeof(T));
                    T res = (T)sr.Deserialize(tr);
                    return res;
                }
                throw new Exception();
            }
            public static string GenericSerialize(object xml, bool smallest = false)
            {
                XmlSerializer serializer = new XmlSerializer(xml.GetType());
                XmlWriterSettings xmlSettings = new XmlWriterSettings()
                {
                    Encoding = Encoding.UTF8
                };
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces(); if (smallest) ns.Add("", ""); //TODO: arkowi zeby obslugiwal xmla bez namespacea - mówi, że się nie da
                using (StringWriter writer = new Utf8StringWriter())
                {
                    serializer.Serialize(writer, xml, ns);
                    return writer.ToString();
                }
            }
            public class Utf8StringWriter : StringWriter
            {// Use UTF8 encoding but write no BOM to the wire
                public override Encoding Encoding
                {
                    get
                    {
                        return new UTF8Encoding(false);
                    } // in real code I'll cache this encoding.
                }
            }
        }
}

