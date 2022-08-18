using ROASApp.Data.IO;
using System.Text.Json;

namespace ROASApp.Domain
{
    public class ROASService
    {
        private static List<ROAS> liste = new List<ROAS>();
        public static ROAS SaveROAS(string reklamKanali, double reklamMaliyeti, double birimFiyat, int satisAdedi)
        {
            ROAS roas = new ROAS();
            roas.reklamKanali = reklamKanali;
            roas.reklamMaliyeti = reklamMaliyeti;
            roas.birimFiyat = birimFiyat;
            roas.satisAdedi = satisAdedi;

            liste.Add(roas);
            //Todo:Dosya sistemine yazma operasyonu gerçekleşecek

            //JSON Convert
            // JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            // serializerOptions.IncludeFields = true;
            //string json= JsonSerializer.Serialize(liste,serializerOptions);


            string json = JsonSerializer.Serialize(liste,
                new JsonSerializerOptions { IncludeFields = true });

            FileOperation.Write(json);

            return roas;
        }
        public static IReadOnlyCollection<ROAS> GetAllROAS()
        {
            LoadListFromFile();
            return liste.AsReadOnly();
        }
        public static IReadOnlyCollection<ROAS> FilterByChannelName(string channelName)
        {
            LoadListFromFile();
            List<ROAS> filteredROAS = new List<ROAS>();
            foreach (ROAS r in liste)
            {
                if (r.reklamKanali.ToLower().Contains(channelName.ToLower()))
                    filteredROAS.Add(r);
            }


            return filteredROAS.AsReadOnly();
        }
        public static void LoadListFromFile()
        {
            string json = FileOperation.Read();
            liste = JsonSerializer.Deserialize<List<ROAS>>(json,
                new JsonSerializerOptions { IncludeFields = true });
        }

        public static List<ROAS> RemoveROAS(string reklamKanali)  
        {
            LoadListFromFile();

            foreach (ROAS a in liste)
            {
                if (a.reklamKanali.ToLower() == reklamKanali.ToLower())
                {
                    liste.Remove(a);
                    break;
                }
                
            }

            string json = JsonSerializer.Serialize(liste,
                new JsonSerializerOptions { IncludeFields = true });

            FileOperation.Write(json);
            return liste;
             
        }

        public static List<ROAS> UpdateROAS(string reklamKanali)
        {
            LoadListFromFile();

            foreach (ROAS  item in liste)
            {
                if (item.reklamKanali.ToLower() == reklamKanali.ToLower())
                {
                    Console.WriteLine("Lütfen yeni kanal adı giriniz: ");
                    item.reklamKanali= Console.ReadLine();
                    Console.WriteLine("Lütfen yeni reklam maliyetini giriniz: ");
                    item.reklamMaliyeti = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Lütfen yeni satış adedini giriniz: ");
                    item.satisAdedi = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Lütfen yeni birim fiyatını giriniz: ");
                    item.birimFiyat = Convert.ToDouble(Console.ReadLine());
                    
                    break;
                }
            }

            string json = JsonSerializer.Serialize(liste,
               new JsonSerializerOptions { IncludeFields = true });

            FileOperation.Write(json);
            return liste;
        }



    }
}
