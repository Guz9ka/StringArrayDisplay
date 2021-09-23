using Microsoft.AspNetCore.Mvc;
using StringArrayDisplay.Data;
using System.Collections.Generic;
using static StringArrayDisplay.Data.Globals;

namespace StringArrayDisplay.Controllers
{
    public class TextController : Controller
    {
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

        public void Add(string stringToAdd)
        {
            DataBaseMock.TryAddNewString(stringToAdd);
        }

        #region Main Actions

        private List<string> TryGetDisplayedStrings(int pageNumber, int stringsPerPage, List<string> availableStrings)
        {
            if (availableStrings == null)
            {
                return null;
            }

            List<string> displayedStrings = null;

            var firstElementID = GetFirstElementID(pageNumber, stringsPerPage);

            displayedStrings = TryGetRangeWithOffset(availableStrings, firstElementID, stringsPerPage);

            if(displayedStrings == null)
            {
                displayedStrings = TryGetDecreasedRangeWithOffset(availableStrings, firstElementID, stringsPerPage);
            }

            return displayedStrings;
        }

        #endregion

        #region Auxiliary Actions

        private List<string> TryGetRangeWithOffset(List<string> strings, int firstElementID, int pickAmount)
        {
            var rangeMinValue = 0;
            var rangeMaxValue = strings.Count;

            var firstValueInRange = CheckIfIndexInRange(firstElementID, rangeMinValue, rangeMaxValue);
            var pickAmountInRange = CheckIfIndexInRange(firstElementID + pickAmount, rangeMinValue, rangeMaxValue);

            if (!firstValueInRange || !pickAmountInRange) return null;

            return strings.GetRange(firstElementID, pickAmount);
        }

        private List<string> TryGetDecreasedRangeWithOffset(List<string> availableStrings, int firstElementID, int stringsPerPage)
        {
            if (availableStrings == null) return null;

            for (int requestedStringsAmount = stringsPerPage; requestedStringsAmount >= 1; requestedStringsAmount--)
            {
                var result = TryGetRangeWithOffset(availableStrings, firstElementID, requestedStringsAmount);

                if (result == null) continue;
                return result;
            }

            return null;
        }

        private int GetFirstElementID(int pageNumber, int stringsPerPage)
        {
            if (pageNumber == 1)
            {
                return 0;
            }

            return (pageNumber - 1) * stringsPerPage;
        }

        private bool CheckIfIndexInRange(int id, int rangeMinValue, int rangeMaxValue)
        {
            return rangeMinValue <= id && id <= rangeMaxValue;
        }

        private bool CheckIfOperationCanBeHandled(int pageNumber, int stringsPerPage)
        {
            return pageNumber == 0;
        }

        #endregion
    }
}
