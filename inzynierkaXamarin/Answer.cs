using System;

namespace inzynierkaXamarin
{
    public class Answer
    {
        public string Text { get; set; }

        public Question Question { get; set; }

        public string Path { get; set; }

        public string Diet { get; set; }

        public Answer() { }

        public override string ToString()
        {
            return Text;
        }
    }
}
