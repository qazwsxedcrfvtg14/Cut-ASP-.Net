using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cut.Pages
{
    public class VocModel : PageModel
    {
        public List<Tuple<string,string>> Message = new List<Tuple<string,string>>();

        [BindProperty]
        public string Query { get; set; }
        public void OnGet()
        {
            //Message = "";
        }
        public void OnPost()
        {
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.AppendLine($"{activity.Text}");
            //sb.AppendLine(new string('-', 20));
            Message.Clear();
            foreach(var x in Voc.Show(Query))
            {
                //res += x+" ";
                //sb.AppendLine($"{x.Item2}");
                //sb.AppendLine($"    {x.Item1}");
                Message.Add(new Tuple<string,string>($"{x.Item2}",$"{x.Item1}"));
                //await context.PostAsync(x);
            }
            //var s = sb.ToString().Replace("\n", "<br />");
            /*.Replace("(",System.Web.HttpUtility.HtmlEncode("("))*//*.Replace(")","\\)").Replace("[","\\[").Replace("]","\\]")*/
            
            //Message = s;
        }
    }
}
