using NAudio.Mixer;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MixerApp
{
    public class MenuItem
    {
        public string Title { get; set; }
        public List<MenuItem> Items { get; } = new List<MenuItem>();
        public MenuItem Select()
        {
            var bg = Console.BackgroundColor;
            var fg = Console.ForegroundColor;
            int index = 0;
            while (true)
            {
                Console.Clear();
                for (int i = 0; i < Items.Count; i++)
                {
                    if (i == index)
                    {
                        Console.BackgroundColor = fg;
                        Console.ForegroundColor = bg;
                    }
                    Console.WriteLine($"{i + 1}) {Items[i].Title}");
                    if (i == index)
                    {
                        Console.BackgroundColor = bg;
                        Console.ForegroundColor = fg;
                    }

                }
                var cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.UpArrow)
                {
                    index = Math.Max(0, index - 1);
                }
                if (cki.Key == ConsoleKey.DownArrow)
                {
                    index = Math.Min(Items.Count - 1, index + 1);
                }
                if (cki.Key == ConsoleKey.Enter)
                {
                    if (Items[index].Items.Count > 0)
                        return Items[index].Select();
                    return Items[index];
                }
                if (cki.Key == ConsoleKey.RightArrow)
                {
                    if (Items[index].Items.Count > 0)
                    {
                        var selectedItem = Items[index].Select();
                        if (selectedItem != null)
                            return selectedItem;
                    }
                    else
                    {
                        return Items[index];
                    }
                }
                if (cki.Key == ConsoleKey.LeftArrow)
                {
                    return null;
                }
            }
        }
    }
    public class MixerModel()
    {

       public List<Mixer> mixers = new List<Mixer>();
       
      public  void AddMixer()
        {
            Console.CursorVisible = true;
            Console.WriteLine("Zadejte jmeno modelu");
            string model = Console.ReadLine();
            Console.WriteLine("Zadejte výrobce");
            string brand = Console.ReadLine();
            Console.WriteLine("Zadejte rychlost otáček za minutu");
          
                bool isnumber = double.TryParse(Console.ReadLine(), out double speed);
          
                if (!isnumber || speed == 0)
                {
                    Console.WriteLine("Špatně zadané číslo");
                    return;
                }

            
            Console.WriteLine("Ruční? true or false");
            bool isbool = bool.TryParse(Console.ReadLine(), out bool handheld);
            if (!isbool)
            {
                Console.WriteLine("Špatně zadaná hodnota");
                return;
            }

            Console.WriteLine("Tyčový? true or false");
            bool iceMaker = bool.TryParse(Console.ReadLine(), out bool stick);
            if (!iceMaker)
            {
                Console.WriteLine("Špatně zadaná hodnota");
            }

            Console.WriteLine("Stolní? true or false");
            bool isdisplay = bool.TryParse(Console.ReadLine(), out bool table);
            if (!isdisplay)
            {
                Console.WriteLine("Špatně zadaná hodnota");
            }
            mixers.Add(new Mixer
            {
                ModelName = model,
                Brand = brand,
                Speed = speed,
                Type = new Type
                {
                    Handheld = handheld,
                }
            }); ;
        }
      public  void OrderByTable()
        {
            var orderedMixers = mixers.OrderBy(x => x.Type.Table).ToList();
            
        }
       public void PlayMusic()
        {
            var player = new WaveOutEvent();
            var reader = new Mp3FileReader("TheFatRat_Timelapse.mp3");
            player.Init(reader);
            player.Play();
        }
      public  void OrderByStick()
        {
            var orderedMixers = mixers.OrderBy(x => x.Type.Stick).ToList();
            DisplayMixers(orderedMixers);
        }
        public void OrderByHandheld()
        {
            var orderedMixers = mixers.OrderBy(x => x.Type.Handheld).ToList();
            DisplayMixers(orderedMixers);
        }
       
       public  void OrderByBrand()
        {
            var orderedMixers = mixers.OrderBy(x => x.Brand).ToList();
            DisplayMixers(orderedMixers);
        }
         public void OrderByModelName()
        {
            var orderedMixers = mixers.OrderBy(x => x.ModelName).ToList();
            DisplayMixers(orderedMixers);
        }
        public void EditMixers()

        {
            Console.CursorVisible = true;
            Console.WriteLine("vsechny mixery:");
            Console.WriteLine("");
            mixers.ForEach(x =>
            {
                Console.WriteLine("------------------");
                Console.WriteLine("|" + x.ModelName + "|");

            });
            Console.WriteLine("");
            Console.WriteLine("Zadejte jméno mixéru kteru chcete editovat");
            Console.WriteLine("Pokud mají stejná jména tak se ukáží postupně,pro přeskočení zmáčkněte backspace a pro editovani enter");


            string jmeno = Console.ReadLine();

            var pocet = mixers.Where(x => x.ModelName == jmeno).ToList();

            if (pocet.Count == 0)
            {
                Console.WriteLine("Nic se nenašlo");
                return;
            }

            pocet.ForEach(x =>
            {


                Console.WriteLine("------------------");
                Console.WriteLine($"Model: {x.ModelName}");
                Console.WriteLine($"Výrobce: {x.Brand}");
                Console.WriteLine($"Rychlost(rpm): {x.Speed}/m");
                Console.WriteLine($"Stolní: {x.Type.Table}");
                Console.WriteLine($"Tyčový: {x.Type.Stick}");
                Console.WriteLine($"Ruční: {x.Type.Handheld}");
                Console.WriteLine("------------------");
                var backspace = Console.ReadKey();
                if (backspace.Key == ConsoleKey.Backspace)
                {
                    return;
                }

                Console.WriteLine("Zadejte nové jméno");
                string newmodel = Console.ReadLine();
                Console.WriteLine("Zadejte nového výrobce");
                string newbrand = Console.ReadLine();
                Console.WriteLine("Tyčový:true or false");
                bool isbool = bool.TryParse(Console.ReadLine(), out bool newStick);
                if (!isbool)
                {
                    Console.WriteLine("Špatně zadaná hodnota");
                    return;
                }
                Console.WriteLine("Stolní:true or false");
                bool isTable = bool.TryParse(Console.ReadLine(), out bool newTable);
                if (!isTable)
                {
                    Console.WriteLine("Špatně zadaná hodnota");
                }
                Console.WriteLine("Ruční:true or false");
                bool isHandheld = bool.TryParse(Console.ReadLine(), out bool newHandheld);
                if (!isHandheld)
                {
                    Console.WriteLine("Špatně zadaná hodnota");
                }
                x.ModelName = newmodel;
                x.Brand = newbrand;

                x.Type = new Type
                {
                    Stick = newStick,
                    Table = newTable,
                    Handheld = newHandheld
                };
                if (x.ModelName == null)
                {
                    Console.WriteLine("nic se nenašlo");
                }
            });
        }
        public void FindMixer()
        {
            Console.CursorVisible = true;
            Console.WriteLine("mixér ktere tu sou:");
            Console.WriteLine("");
            mixers.ForEach(x =>
            {
                Console.WriteLine("------------------");
                Console.WriteLine("|" + x.ModelName + "|");

            });
            Console.WriteLine("");
            Console.WriteLine("Zadejte jméno mixéru, kterou chcete najit");
            string jmeno = Console.ReadLine();

            var pocet = mixers.Where(x => x.ModelName == jmeno).ToList();

            if (pocet.Count == 0)
            {
                Console.WriteLine("Nic se nenašlo");
                return;
            }

            pocet.ForEach(x =>
            {
                Console.WriteLine("------------------");
                Console.WriteLine($"Model: {x.ModelName}");
                Console.WriteLine($"Výrobce: {x.Brand}");
                Console.WriteLine($"Tyčový: {x.Type.Stick}");
                Console.WriteLine($"Ruční: {x.Type.Handheld}");
                Console.WriteLine($"Stolní: {x.Type.Table}");
                Console.WriteLine("------------------");
            });

        }
        public void DisplayMixers(List<Mixer> orderedMixers)
        {
            int count = 0;
            int pageSize = 4;

            while (count < orderedMixers.Count)
            {
                var page = orderedMixers.Skip(count).Take(pageSize);

                foreach (var mixer in page)
                {
                    Console.WriteLine("------------------");
                    Console.WriteLine($"Model: {mixer.ModelName}");
                    Console.WriteLine($"Výrobce: {mixer.Brand}");
                    Console.WriteLine($"Stolní: {mixer.Type.Table}");
                    Console.WriteLine($"Ruční: {mixer.Type.Handheld}");
                    Console.WriteLine($"Tyčový: {mixer.Type.Stick}");
                    Console.WriteLine("------------------");
                }

                count += pageSize;

                if (count < orderedMixers.Count)
                {
                    Console.WriteLine("pravou šipku pro další stranu, levou pro předešlou stranu. Enter pro ukončení ");
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.RightArrow)
                    {
                        Console.Clear();
                    }
                    else if (key.Key == ConsoleKey.LeftArrow)
                    {
                        count -= 2 * pageSize;
                        if (count < 0) count = 0;
                        Console.Clear();
                    }
                    else
                    {
                        break;
                    }
                    Console.WriteLine();
                }
            }
        }
        public void SaveJson()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(mixers);
                File.WriteAllText("databaze.json", jsonString);
                Console.WriteLine("Data byla úspěšně uložena do databaze.json");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při ukládání dat: {ex.Message}");
            }
        }
        public void DeleteJson()
        {
            try
            {
                if (File.Exists("databaze.json"))
                {
                    File.Delete("databaze.json");
                    Console.WriteLine("databaze.json byla úspěšně smazána");
                }
                else
                {
                    Console.WriteLine("Soubor databaze.json neexistuje");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při mazání souboru: {ex.Message}");
            }
        }
        public void LoadJson()
        {
            try
            {
                if (File.Exists("databaze.json"))
                {
                    string jsonString = File.ReadAllText("databaze.json");
                    mixers = JsonSerializer.Deserialize<List<Mixer>>(jsonString);
                    Console.WriteLine("Data byla úspěšně načtena z databaze.json");
                }
                else
                {
                    Console.WriteLine("Soubor databaze.json neexistuje");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při načítání dat: {ex.Message}");
            }
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}

