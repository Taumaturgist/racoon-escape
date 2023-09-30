using UnityEngine;
using System.Collections.Generic;

namespace Extensions
{
    public static class ListExtension
    {
        public static T RandomItem<T>(this List<T> list)
        {
            var index = Random.Range(0, list.Count);
            return list[index];
        }
        
        public static T RandomItem<T>(this T[] list)
        {
            var index = Random.Range(0, list.Length);
            return list[index];
        }
    }
}

