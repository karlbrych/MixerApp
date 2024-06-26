﻿using System.Diagnostics.Metrics;
using System.Drawing;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net.Http.Json;
using System;
using System.Diagnostics;
using NAudio.Wave;
using System.Text;
using System.Net.Http.Headers;

namespace MixerApp
{

    internal class Program
    {
        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorVisible = false;
            MixerModel model = new MixerModel();

            MenuItem menu = new MenuItem();
            MenuItem item1 = new MenuItem() { Title = "Řazení" };
            item1.Items.Add(new MenuItem() { Title = "Podle jména" });
            item1.Items.Add(new MenuItem() { Title = "Podle výrobce" });
            menu.Items.Add(item1);

            MenuItem item2 = new MenuItem() { Title = "Funkce" };
            item2.Items.Add(new MenuItem() { Title = "Tyčový" });
            item2.Items.Add(new MenuItem() { Title = "Stolní" });
            item2.Items.Add(new MenuItem() { Title = "Ruční" });
            menu.Items.Add(item2);

            MenuItem item3 = new MenuItem() { Title = "Přidat mixér,Uložit,Smazat,Načíst" };
            item3.Items.Add(new MenuItem() { Title = "Uložit" });
            item3.Items.Add(new MenuItem() { Title = "Smazat" });
            item3.Items.Add(new MenuItem() { Title = "Načíst" });
            item3.Items.Add(new MenuItem() { Title = "Přidat" });
            menu.Items.Add(item3);
            MenuItem item4 = new MenuItem() { Title = "Najít Mixéry" };
            menu.Items.Add(item4);

            MenuItem item5 = new MenuItem() { Title = "Editovat" };
            menu.Items.Add(item5);

            MenuItem item6 = new MenuItem() { Title = "Pustit si písničku" };
            menu.Items.Add(item6);
            MenuItem item8 = new MenuItem() { Title = "Ukončit" };
            menu.Items.Add(item8);
            MenuItem item9 = new MenuItem() { Title = "Můj GitHub" };
            menu.Items.Add(item9);

            while (true)
            {
                var selected = menu.Select();
                if (selected == null)
                    continue;
                switch (selected.Title)
                {
                    case "Můj GitHub":
                        model.OpenURL();
                        break;
                    case "Načíst":
                        List<Mixer> mixers = model.LoadJson();
                        break;
                    case "Uložit":
                        model.SaveJson(); break;
                    case "Smazat":
                        model.DeleteJson(); break;
                    case "Pustit si písničku":
                        model.PlayMusic();
                        break;
                    case "Ukončit":
                        model.Exit();
                        break;
                    case "Editovat":
                        Console.WriteLine("");
                        model.EditMixers();
                        break;
                    case "Najít Mixér":
                        Console.WriteLine("");
                        model.FindMixer();
                        break;
                    case "Přidat":
                        Console.WriteLine("");
                        model.AddMixer();
                        break;
                    case "Podle jména":
                        Console.WriteLine("");
                        model.OrderByModelName();
                        break;
                    case "Podle výrobce":
                        Console.WriteLine("");
                        model.OrderByBrand();
                        break;
                    case "Tyčový":
                        Console.WriteLine("");
                        model.OrderByStick();
                        break;
                    case "Ruční":
                        Console.WriteLine("");
                        model.OrderByHandheld();
                        break;
                    case "Stolní":
                        Console.WriteLine("");
                        model.OrderByTable();
                        break;



                }
                Console.WriteLine("Pokračuj klávesou Enter");
                Console.ReadLine();
            }

        }
    }
    public class Mixer
    {
        public string ModelName { get; set; }
        public string Brand { get; set; }
        public Type Type { get; set; }
        public double Speed { get; set; }

    }

    public class Type
    {
        public bool Stick { get; set; }
        public bool Table { get; set; }
        public bool Handheld { get; set; }

    }
}





