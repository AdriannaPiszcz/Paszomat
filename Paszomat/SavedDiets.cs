using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace inzynierkaXamarin
{
    public class SavedDiets
    {
        public string HorseName { get; set; }

        public string Diet { get; set; }

        public SavedDiets() { }
        public SavedDiets(string horseName, string diet)
        {
            HorseName = horseName;
            Diet = diet;
        }

        public void ShowDiet()
        {
            Read();
            LoadData.WriteToScreen("Koń " + HorseName);
            LoadData.WriteToScreen("Optymalna dieta dla tego konia to: \n" + Diet);
        }

        public void Save()
        {

            List<SavedDiets> temp = new List<SavedDiets>(LoadData.NameDiet.Count);
            foreach (string key in LoadData.NameDiet.Keys)
            {
                temp.Add(new SavedDiets(key, LoadData.NameDiet[key].ToString()));
            }
            var xml = ServiceXml.GenericSerialize(temp);
            LoadData.SaveFile(xml);
        }

        public void Read()
        {
            string file = LoadData.LoadFile();
            if (file == null)
            {
                LoadData.WriteToScreen("Nie ma żadnych zapisanych pozycji!");
                return;
            }
            List<SavedDiets> temp = ServiceXml.GenericDeserialize<List<SavedDiets>>(file);
            foreach (SavedDiets sd in temp)
            {
                LoadData.NameDiet.TryAdd(sd.HorseName, sd.Diet);
            }
        }

        public List<string> GetNames()
        {
            Read();
            List<string> names = new List<string>();
            foreach (var item in LoadData.NameDiet)
            {
                names.Add(item.Key.ToString());
            }

            return names;
        }

        public string Find(string name)
        {
            Read();
            LoadData.NameDiet.TryGetValue(name, out string diet);
            return diet;
        }

        public void Delete(string name)
        {
            try
            {
                Read();
                LoadData.NameDiet.Remove(name);
                Save();

            }
            catch
            {

            }
        }
    }
}
