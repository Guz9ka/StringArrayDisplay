using Microsoft.AspNetCore.Mvc;
using StringArrayDisplay.Data;
using System.Collections.Generic;
using static StringArrayDisplay.Data.Globals;
using static StringArrayDisplay.Auxiliary.ListHelper;

namespace StringArrayDisplay.Controllers
{
    public class TextController : Controller
    {
        [HttpGet]
        public ActionResult Display()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Display(int pageNumber, int stringsPerPage)
        {
            var availableStrings = DataBaseMock.GetAllStringsList();

            if (CheckIfOperationCanBeHandled(pageNumber, stringsPerPage))
            {
                return null;
            }

            var displayedStrings = TryGetDisplayedStrings(pageNumber, stringsPerPage, availableStrings);
            return View(displayedStrings);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string stringToAdd)
        {
            DataBaseMock.TryAddNewString(stringToAdd);
            return View();
        }

        #region Main Actions

        private List<string> TryGetDisplayedStrings(int pageNumber, int stringsPerPage, List<string> availableStrings)
        {
            if (availableStrings == null)
            {
                return null;
            }

            List<string> displayedStrings = null;

            var firstElementID = GetFirstPageElementID(pageNumber, stringsPerPage);

            displayedStrings = TryGetRangeWithOffset<string>(availableStrings, firstElementID, stringsPerPage);

            if(displayedStrings == null)
            {
                displayedStrings = TryGetDecreasedRangeWithOffset<string>(availableStrings, firstElementID, stringsPerPage);
            }

            return displayedStrings;
        }

        #endregion

        #region Auxiliary Actions

        private int GetFirstPageElementID(int pageNumber, int stringsPerPage)
        {
            if (pageNumber == 1)
            {
                return 0;
            }

            return (pageNumber - 1) * stringsPerPage;
        }

        private bool CheckIfOperationCanBeHandled(int pageNumber, int stringsPerPage)
        {
            return pageNumber == 0;
        }

        #endregion
    }
}
