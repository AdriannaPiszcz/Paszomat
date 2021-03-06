﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace inzynierkaXamarin
{
    [Activity(Label = "GeneratedDiet", Theme = "@style/AppTheme", MainLauncher = false)]
    class DietShowActivity : Activity
    {
        private TextView diet;

        private Button save;

        private EditText name;

        private bool firstClick = true;

        public override void OnBackPressed()
        {
            if(firstClick)
            {
                Toast.MakeText(this, "Nie zapisano diety, kliknij ponownie aby wyjść", ToastLength.Long).Show();
                firstClick = false;
            }
            else
            {
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_dietShow);
            { }

            save = (Button)FindViewById(Resource.Id.save);
            diet = (TextView)FindViewById(Resource.Id.diet);
            name = (EditText)FindViewById(Resource.Id.name);

            save.Click += Save_Click;

            ShowDiet();
        }

        private bool NameValid()
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(name.Text, "^[a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]{1,50}$"))
            {
                Toast.MakeText(this, "Błędne imię", ToastLength.Short).Show();
                name.Text = "";
                return false;
            }
            return true;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            SaveDiet(name.Text, Intent.GetStringExtra(null));
        }

        private void ShowDiet()
        {
            diet.Text = Intent.GetStringExtra(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public void SaveDiet(string name, string diet)
        {
            SavedDiets save = new SavedDiets();
            if (!NameValid())
                return;

            save.HorseName = name.ToUpper();
            save.Diet = diet;
            try
            {
                if (!LoadData.NameDiet.TryAdd(name.ToUpper(), diet))
                {
                    Toast.MakeText(this, "Dieta dla konia o tym imieniu została już zapisana!", ToastLength.Long).Show();
                    return;
                }
                save.Read();
                save.Save();
                Toast.MakeText(this, "Zapisano pomyślnie!", ToastLength.Short).Show();
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
            }
            catch (ArgumentException)
            {
                Toast.MakeText(this, "Dieta dla konia o tym imieniu została już zapisana!", ToastLength.Long).Show();
                LoadData.WriteToScreen("Dieta dla konia o tym imieniu została już zapisana!");
            }
        }
    }
}
