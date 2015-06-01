using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SklepWPF
{
    public static class XMLDeserializer
    {
        public static Sklep DeserializeFromXML()
        {
            Type[] typy = { typeof(ProduktSpozywczy), typeof(ProduktChemiczny), typeof(Rtv_agd) };
            Sklep XmlData = null;
            XmlSerializer deserializer = new XmlSerializer(typeof(Sklep), typy);
            string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "sklep.xml");
            using (TextReader textReader = new StreamReader(startupPath))
            {
                XmlData = deserializer.Deserialize(textReader) as Sklep;
            }
            return XmlData;
        }

        public static List<Produkt> DesrializeProducts()
        {
            string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "sklep.xml");
            string xmlstr = File.ReadAllText(startupPath);

            Assembly asm = Assembly.GetExecutingAssembly();
            XDocument xDoc = XDocument.Load(new StringReader(xmlstr));
            Produkt[] produkty = xDoc.Descendants("Produkt")
                .Select(produkt =>
                {
                    string typeName = produkt.Attribute("Typ").Value;
                    var type = asm.GetTypes().Where(t => t.Name == typeName).First();
                    Produkt p = Activator.CreateInstance(type) as Produkt;
                    p.Typ = typeName;
                    foreach (var prop in produkt.Descendants())
                        type.GetProperty(prop.Name.LocalName).SetValue(p, prop.Value, null);
                    return p;
                }).ToArray();

            List<Produkt> list = new List<Produkt>();
            foreach (var p in produkty)
                list.Add(p);
            return list;
        }
    }
}
