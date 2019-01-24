using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace inzynierkaXamarin
{
    public class LoadData
    {
        public static Question core;

        public static Question path1;

        public static Question path2;

        public static Question path3;

        public static Dictionary<string, string> NameDiet;

        public static Dictionary<int, Button> IdButton;

        public static readonly Dictionary<string, Question> FilePath;

        static LoadData()
        {

            core = ServiceXml.GenericDeserialize<Question>(ReadFile("db.xml", "UTF-8"));

            path1 = ServiceXml.GenericDeserialize<Question>(ReadFile("ogiery.xml", "UTF-8"));

            path2 = ServiceXml.GenericDeserialize<Question>(ReadFile("klacze zrebne i karmiace.xml", "UTF-8"));

            path3 = ServiceXml.GenericDeserialize<Question>(ReadFile("robocze.xml", "UTF-8"));

            NameDiet = new Dictionary<string, string>();

            IdButton = new Dictionary<int, Button>();

            FilePath = new Dictionary<string, Question>()
        {
            { "path1", path1 },
            { "path2", path2 },
            { "path3", path3 }
        };
        }

        public static string ReadFile(string fileName, string coding)
        {
            string text;
            using (StreamReader sr = new StreamReader(Android.App.Application.Context.Assets.Open(fileName), Encoding.GetEncoding(coding)))
            {
                text = sr.ReadToEnd();
            }
            return text;
        }

        public static void SaveFile(string text)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "saved.xml");
            
            using (StreamWriter write = new StreamWriter(filePath, false))
            {
                write.Write(text.ToString());
            }

        }

        public static string LoadFile()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "saved.xml");
            if (!File.Exists(filePath))
                return null;
            using (StreamReader reader = new StreamReader(filePath, true))
            {
                return reader.ReadToEnd();
            }
        }

        public static void WriteToScreen(string str)
        {
            System.Diagnostics.Debug.WriteLine(str);
        }

    }
}

