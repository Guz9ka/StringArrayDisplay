﻿using Microsoft.AspNetCore.Mvc;
using StringArrayDisplay.Data;
using System.Collections.Generic;
using StringArrayDisplay.ViewModels;
using static StringArrayDisplay.Data.Globals;
using static StringArrayDisplay.Auxiliary.ListHelper;

namespace StringArrayDisplay.Controllers
{
    public class TextController : Controller
    {
        private const int DefaultPageNumber = 1;
        private const int DefaultStringsPerPage = 3;

        public ActionResult Display(int? pageNumber, int? stringsPerPage)
        {
            TrySetDefaultValues(ref pageNumber, ref stringsPerPage);
            var castedStringsPerPage = (int) stringsPerPage;
            var castedPageNumber = (int) pageNumber;
            
            var availableStrings = DataBaseMock.GetAllStringsList();
            var displayedStrings = TryGetRangeWithOffsetOrLess(availableStrings, 
                GetFirstPageElementID(castedPageNumber, castedStringsPerPage), castedStringsPerPage);

            var viewModel = new PageInfoViewModel
            {
                strings = displayedStrings,
                availableStringsCount = availableStrings.Count,
                stringsPerPage = castedStringsPerPage
            };
            
            return View(viewModel);
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

        #region Auxiliary Actions

        private int GetFirstPageElementID(int pageNumber, int stringsPerPage)
        {
            if (pageNumber == 1)
            {
                return 0;
            }

            return (pageNumber - 1) * stringsPerPage;
        }

        private void TrySetDefaultValues(ref int? pageNumber, ref int? stringsPerPage)
        {
            if (pageNumber == null)
            {
                pageNumber = DefaultPageNumber;
            }

            if (stringsPerPage == null)
            {
                stringsPerPage = DefaultStringsPerPage;
            }
        }

        #endregion
    }
}