using Android.Content;
using Android.Widget;
using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace inzynierkaXamarin
{
    public class Question
    {
        public string Text { get; set; }

        public Answer[] Answers { get; set; }

        public Question Parent { get; set; }

        private bool ifDiet = false;

        public delegate void OnNewAnswerDelegate(Question sender, Button button, string text);
        public static event OnNewAnswerDelegate OnNewAnswer;

        public delegate void OnInactiveButtonDelegate(Question sender, Button button);
        public static event OnInactiveButtonDelegate OnInactiveButton;

        public delegate void OnNewQuestionDelegate(Question sender, string text);
        public static event OnNewQuestionDelegate OnNewQuestion;

        public delegate void OnDietGetDelegate(Question sender, string text);
        public static event OnDietGetDelegate OnDietGet;

        public Question() { }
        /// <summary>
        ///    Printing question and answers to the screen
        /// </summary>
        public void GetQuestionAndAnswers()
        {
            OnNewQuestion?.Invoke(this, Text);
            LoadData.WriteToScreen(Text);
            if (Answers == null)
                return;
            /// ze słownika wyciągam nazwę po numerze żeby mieć dostęp do parametrów buttona
            foreach (var button in LoadData.IdButton)
            {
                if (button.Key <= Answers.Length)
                {
                    OnNewAnswer?.Invoke(this, button.Value, Answers[button.Key - 1].Text);
                    LoadData.WriteToScreen(Answers[button.Key - 1].Text);
                }
                else
                {
                    OnInactiveButton?.Invoke(this, button.Value);
                }
            }

        }

        public bool GetPreviousQuestion()
        {
            if (Parent != null && ifDiet == false)
            {
                Parent.GetQuestionAndAnswers();
                return true;
            }
            return false;

        }

        public void GetDiet(Answer next)
        {
            if (next.Diet != null)
            {
                string diet = LoadData.ReadFile(next.Diet, "Windows-1250");
                ifDiet = true;
                Parent = null;
                OnDietGet?.Invoke(this, diet);
            }
        }

        public void AnswerChosen(int res)
        {
            var next = Answers[res - 1];
            if (next.Question != null)
                next.Question.GetQuestionAndAnswers();
            else
                GetDiet(next);
        }

        /// <summary>
        /// Setting parent for actual object - you can return to the previous question this way
        /// </summary>
        public void SetTree()
        {
            if (Answers != null)
                foreach (var item in Answers)
                {
                    //setting path after main tree
                    if (item.Path != " " && item.Path != null)
                    {
                        item.Question = LoadData.FilePath[item.Path];
                    }
                    if (item.Question != null)
                    {
                        item.Question.Parent = this;
                        item.Question.SetTree();
                    }
                }
        }

    }
}
