using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace inzynierkaXamarin
{
    [Activity(Label = "Paszomat", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Button generate;

        private Button browse;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            generate = (Button)FindViewById(Resource.Id.generate);
            browse = (Button)FindViewById(Resource.Id.view);

            generate.Click += Generate_Click;
            browse.Click += Browse_Click;
        }

        private void Browse_Click(object sender, System.EventArgs e)
        {
            Intent i = new Intent(this, typeof(BrowseActivity));
            StartActivity(i);
        }

        private void Generate_Click(object sender, System.EventArgs e)
        {
            Intent i = new Intent(this, typeof(ExpertSystemActivity));
            StartActivity(i);
        }

    }
}