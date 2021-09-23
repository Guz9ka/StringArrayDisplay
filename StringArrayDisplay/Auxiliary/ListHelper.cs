using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringArrayDisplay.Auxiliary
{
    public static class ListHelper
    {
        public static List<T> TryGetRangeWithOffset<T>(List<T> strings, int firstElementID, int pickAmount)
        {
            var rangeMinValue = 0;
            var rangeMaxValue = strings.Count;

            var firstValueInRange = CheckIfIndexInRange(firstElementID, rangeMinValue, rangeMaxValue);
            var pickAmountInRange = CheckIfIndexInRange(firstElementID + pickAmount, rangeMinValue, rangeMaxValue);

            if (!firstValueInRange || !pickAmountInRange) return null;

            return strings.GetRange(firstElementID, pickAmount);
        }

        public static List<T> TryGetDecreasedRangeWithOffset<T>(List<T> availableStrings, int firstElementID, int stringsPerPage)
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

        #region Auxiliary Actions

        private static bool CheckIfIndexInRange(int id, int rangeMinValue, int rangeMaxValue)
        {
            return rangeMinValue <= id && id <= rangeMaxValue;
        }

        #endregion
    }
}
