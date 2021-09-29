using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringArrayDisplay.Auxiliary
{
    public static class ListHelper
    {
        public static List<T> TryGetRangeWithOffsetOrLess<T>(List<T> strings, int firstElementId, int pickAmount)
        {
            for (int requestedStringsAmount = pickAmount; requestedStringsAmount > 0; requestedStringsAmount--)
            {
                const int rangeMinValue = 0;
                var rangeMaxValue = strings.Count;
                
                var firstValueInRange = CheckIfIndexInRange(firstElementId, rangeMinValue, rangeMaxValue);
                var pickAmountInRange = CheckIfIndexInRange(firstElementId + pickAmount, 
                    rangeMinValue, rangeMaxValue);

                if (!firstValueInRange || !pickAmountInRange) continue;

                return strings.GetRange(firstElementId, pickAmount);
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