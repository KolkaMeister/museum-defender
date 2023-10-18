using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Infrastructure
{
    public static class Test
    {
        [MenuItem("Test/GetOrderedList")]
        private static void BuildTest()
        {
            var a = new List<int> { 1, 2, 3, 4, 5, 6 };
            var b = new List<int> { 7, 8, 9, 10, 6 };

            string result = GetOrderedList(a, b).Aggregate("", (current, i) => current + (i + " "));
            
            result = $"{{ {result.Trim().Replace(" ", ", ")} }}";
            
            Debug.Log(result);
        }
        
        private static List<int> GetOrderedList(List<int> aList, List<int> bList)
        {
            int repeatedIndex = 0;
            for (int i = 1; i <= aList.Count && i <= bList.Count; i++)
            {
                if (aList[^i] != bList[^i])
                    break;
                repeatedIndex = i;
            }

            int aIndex = aList.Count - repeatedIndex;
            int bIndex = bList.Count - repeatedIndex;

            var ordered = new List<int>();
            ordered.AddRange(aList.GetRange(0, aIndex));
            ordered.AddRange(bList.GetRange(0, bIndex));
            ordered.AddRange(aList.GetRange(aIndex, repeatedIndex));

            return ordered;
        }
        
    }
}