using System.Collections.Generic;

namespace StringArrayDisplay.Data
{
    public static class DataBaseMock
    {
        private static List<string> _availableStrings = new List<string>()
        {
                "Somebody", //0
                "Once", //1
                "Told", //2
                "Me", //3
                "The", //4
                "World", //5
                "Is", //6
                "Gonna", //7
                "Roll", //8
                "Me", //9
        };

        public static List<string> GetAllStringsList()
        {
            return _availableStrings;
        }

        public static void TryAddNewString(string stringToAdd)
        {
            _availableStrings?.Add(stringToAdd);
        }
    }
}
