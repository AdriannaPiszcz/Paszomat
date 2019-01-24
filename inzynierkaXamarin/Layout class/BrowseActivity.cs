using System;
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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    class BrowseActivity : Activity
    {
        private Spinner horseName;

        private TextView horseDiet;

        private ArrayAdapter adapter;

        private ImageButton delete;

        private SavedDiets sd = new SavedDiets();

        private int selected = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_browse);
            { }

            horseName = (Spinner)FindViewById(Resource.Id.horseName);
            horseDiet = (TextView)FindViewById(Resource.Id.horseDiet);
            delete = (ImageButton)FindViewById(Resource.Id.trash);

            delete.Click += Delete_Click;

            horseName.ItemSelected += HorseName_ItemSelected;

            adapter = new ArrayAdapter(this, Resource.Layout.spinner_item, sd.GetNames());
            horseName.Adapter = adapter;
            NullSpinnerChecker();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (horseName.Count == 0)
            {
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
                Toast.MakeText(this, "Nie ma zapisanych diet!", ToastLength.Long).Show();
            }
            NullSpinnerChecker();
            sd.Delete(horseName.GetItemAtPosition(selected).ToString());
            this.Recreate();
            Toast.MakeText(this, "Usunięto pomyślnie!", ToastLength.Long).Show();
        }

        private void HorseName_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            selected = e.Position;
            horseDiet.Text = sd.Find(horseName.GetItemAtPosition(e.Position).ToString());
        }

        private void NullSpinnerChecker()
        {
            if (horseName.Count < 1)
            {
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
                Toast.MakeText(this, "Brak zapisanych diet", ToastLength.Short).Show();
                return;
            }
        }
    }

}