using System;
using System.Collections.Generic;

namespace Cw2
{
    [Serializable]
    public class Uczelnia
    {
        public string CreatedAt
        {
            get => DateTime.Now.ToString("dd.MM.yyyy");
            set { }
        }
        public string Author { get; set; }
        public List<Student> Studenci { get; set; } = new List<Student>();
    }
}
