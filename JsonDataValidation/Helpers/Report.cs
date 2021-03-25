using System.Collections.Generic;

namespace JsonDataValidation.Helpers
{
    public class Report
    {
        public static string reportStr { get; set; } = null;

        public static void AddTitle(string title, string style)
        {
            reportStr += $"<h1 {style}>{title}</h1>";
        }
        public static void AddText(string text, string style)
        {
            reportStr += $"<h2 {style}>{text}</h2>";
        }
        public static void AddList(List<string> items, string style)
        {
            string listItems = "";
            foreach (string item in items)
            {
                listItems += $"<li>{item}</li>";
            }
            reportStr += $"<ul {style}>{listItems}</ul>";
        }
    }
}