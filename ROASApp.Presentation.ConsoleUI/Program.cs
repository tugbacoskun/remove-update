﻿using ROASApp.Domain;

namespace ROASApp.Presentaion.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {
            Menu();
        }

        private static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1. Yeni ROAS Kaydı\n2. Roas Listesi\n3. ROAS Filtrele\n4. Sil\n5. Güncelle\n6. Çıkış");
            MenuSelection();
        }
        private static void MenuSelection()
        {
            Console.Write("Seçiminiz : ");
            string choose = Console.ReadLine();
            switch (choose)
            {
                case "1":
                    NewROAS();
                    break;
                case "2":
                    ListOfROAS();
                    break;
                case "3":
                    FilterROAS();
                    break;
                case "4":

                    RemoveROAS();
                    break;
                case "5":
                    UpdateROAS();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    MenuSelection();
                    break;
            }
        }



        static void Again()
        {
            Console.WriteLine("Menüye dönmek için Enter");
            Console.ReadLine();
            Menu();
        }
        private static void FilterROAS()
        {
            Console.WriteLine("Kanal adı içinde geçen kelimeyi yazın : ");
            string filterKeyword = Console.ReadLine();
            var data = ROASService
                .FilterByChannelName(filterKeyword);
            PrintList(data);
        }
        private static void ListOfROAS()
        {
            var list = ROASService.GetAllROAS();
            PrintList(list);
        }
        static void PrintList(IReadOnlyCollection<ROAS> list)
        {

            Console.WriteLine("----------- Liste Başlangıcı ----------");
            foreach (ROAS r in list)
            {
                Console.WriteLine(r.ROASInfo());
                Console.WriteLine("--------------------------------------");
            }
            Console.WriteLine("----------- Liste Sonu ----------");
            Again();
        }
        private static void NewROAS()
        {
            Console.WriteLine("Reklam Kanalı Adı : ");
            string kanalAdi = Console.ReadLine();
            Console.WriteLine("Reklam Maliyeti : ");
            double maliyet = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Satılan Ürünlerin Birim Fiyatı : ");
            double birimFiyat = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Satılan Ürün Adedi : ");
            int adet = Convert.ToInt32(Console.ReadLine());


            var data = ROASService.SaveROAS(kanalAdi, maliyet, birimFiyat, adet);

            Console.WriteLine($"Hesaplanan ROAS Değeri : %{data.ROASGetirisi()}");
            Again();
        }
        private static void RemoveROAS()
        {

            Console.WriteLine("Listeden silmek istediğiniz kaydın kanal adını giriniz: ");
            string removeKeyWord = Console.ReadLine();
            var data = ROASService.RemoveROAS(removeKeyWord);
            Console.WriteLine("Silme tamamlandı. Liste için bir tuşa basınız.");
            Console.ReadLine();
            PrintList(data);

        }
        private static void UpdateROAS()
        {
            Console.WriteLine("Listeden güncellemek istediğiniz kaydın kanal adını giriniz: ");
            string updateKanalAdi = Console.ReadLine();
            var data = ROASService.UpdateROAS(updateKanalAdi);
            Console.WriteLine("Güncelleme tamamlandı. Liste için bir tuşa basınız.");
            Console.ReadLine();
            PrintList(data);
        }


    }
}