using System.Collections.Generic;

namespace StringArrayDisplay.Auxiliary
{
    public static class ListHelper
    {
        public static List<T> TryGetRangeWithOffsetOrLess<T>(List<T> list, int firstElementId, int desiredPickAmount)
        {
            for (int currentPickAmount = desiredPickAmount; currentPickAmount > 0; currentPickAmount--)
            {
                const int rangeMinValue = 0;
                var rangeMaxValue = list.Count;
                
                var firstValueInRange = CheckIfIndexInRange(firstElementId, rangeMinValue, rangeMaxValue);
                var pickAmountInRange = CheckIfIndexInRange(firstElementId + currentPickAmount, 
                    rangeMinValue, rangeMaxValue);
                
                if (!firstValueInRange || !pickAmountInRange) continue;

                return list.GetRange(firstElementId, currentPickAmount);
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