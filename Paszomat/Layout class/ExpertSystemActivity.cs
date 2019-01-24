using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace inzynierkaXamarin
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    class ExpertSystemActivity : Activity
    {
        private Button ans1;

        private Button ans2;

        private Button ans3;

        private Button ans4;

        private TextView question;

        private Question currentQuestion = LoadData.core;

        public override void OnBackPressed()
        {
            if(!currentQuestion.GetPreviousQuestion())
            base.OnBackPressed();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_expertSystem); {}
            LoadData.IdButton.Clear();
            
            ans1 = (Button)FindViewById(Resource.Id.ans1);
            ans2 = (Button)FindViewById(Resource.Id.ans2);
            ans3 = (Button)FindViewById(Resource.Id.ans3);
            ans4 = (Button)FindViewById(Resource.Id.ans4);
            question = (TextView)FindViewById(Resource.Id.question);

            LoadData.IdButton.TryAdd(1, ans1);
            LoadData.IdButton.TryAdd(2, ans2);
            LoadData.IdButton.TryAdd(3, ans3);
            LoadData.IdButton.TryAdd(4, ans4);

            ans1.Click += Ans1_Click;
            ans2.Click += Ans2_Click;
            ans3.Click += Ans3_Click;
            ans4.Click += Ans4_Click;

            Question.OnNewAnswer += Q_OnChangeAnswer;
            Question.OnNewQuestion += Q_OnShowQuestion;
            Question.OnInactiveButton += Q_OnInactiveButton;
            Question.OnDietGet += Question_OnDietGet;

            LoadData.core.SetTree();
            LoadData.core.GetQuestionAndAnswers();
            LoadData.WriteToScreen("joł joł");
        }

        private void Question_OnDietGet(Question sender, string text)
        {
            //Toast.MakeText(this, text, ToastLength.Long).Show();
            Intent i = new Intent(this, typeof(DietShowActivity));
            Bundle b = new Bundle();
            b.PutString(null, text);
            i.PutExtras(b);
            StartActivity(i);
        }

        private void Q_OnInactiveButton(Question sender, Button button)
        {
            SetVisibility(ViewStates.Invisible, button);
            currentQuestion = sender;
        }

        private void Q_OnShowQuestion(Question sender, string text)
        {
            SetQuestion(text);
            currentQuestion = sender;
        }

        private void Q_OnChangeAnswer(Question sender, Button button, string text)
        {
            SetAnswer(text, button);
            SetVisibility(ViewStates.Visible, button);
            currentQuestion = sender;
        }

        private void Ans4_Click(object sender, EventArgs e)
        {
            currentQuestion.AnswerChosen(SearchClicked(sender));
        }

        private void Ans3_Click(object sender, EventArgs e)
        {
            currentQuestion.AnswerChosen(SearchClicked(sender));
        }

        private void Ans2_Click(object sender, EventArgs e)
        {
            currentQuestion.AnswerChosen(SearchClicked(sender));
        }

        private void Ans1_Click(object sender, EventArgs e)
        {
            currentQuestion.AnswerChosen(SearchClicked(sender));
        }

        private void SetQuestion(string text)
        {
            question.Text = text;
        }

        private void SetAnswer(string text, Button button)
        {
            button.Text = text;
        }

        private void SetVisibility(ViewStates visibility, Button button)
        {
            button.Visibility = visibility;
        }

        private int SearchClicked(object sender)
        {
            foreach (var button in LoadData.IdButton)
            {
                if (button.Value == sender)
                {
                    return button.Key;
                }
            }
            return 0;
        }
    }
}