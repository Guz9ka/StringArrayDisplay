using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StringArrayDisplay.ViewModels
{
    public class StringsDisplayInfoViewModel : PageModel
    {
        public List<string> Strings;
        
        public int AvailableStringsCount;
        public int StringsPerPage;
    }
}