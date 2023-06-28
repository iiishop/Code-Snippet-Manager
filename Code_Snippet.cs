using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Code_Snippet_Manager
{
    public class Code_Snippet
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Language Language { get; set; }
        public Mode Mode { get; set; }
        public string Code { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public Code_Snippet(string title, Language language, Mode mode, string code, string notes)
        {
            Id = Program.MaxId++;
            Title = title;
            Language = language;
            Mode = mode;
            Code = code;
            Notes = notes;
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }
        public override string ToString()
        {
            return $"Id: {Id}\nTitle: {Title}\nLanguage: {Language}\nMode: {Mode}\nCode: {Code}\nNotes: {Notes}\nCreated: {Created}\nModified: {Modified}\n\n";
        }
        public void Analyze()
        {

        }
    }
}