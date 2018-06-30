using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspnetcoreapp.Pages
{
    public class VocModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "";
        }
        public void OnPost()
        {
            Message = "Hello Voc.";
        }
    }
}
