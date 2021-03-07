using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AiSoft.Tools.Extensions
{
    /// <summary>
    /// Obs排序
    /// </summary>
    public static class ObsExtension
    {
        public static void Sort<T>(this ObservableCollection<T> collection, Comparison<T> comparison)
        {
            var comparer = new Comparer<T>(comparison);
            var sorted = collection.OrderBy(x => x, comparer).ToList();
            for (var i = 0; i < sorted.Count(); i++)
            {
                collection.Move(collection.IndexOf(sorted[i]), i);
            }
        }

        private class Comparer<T> : IComparer<T>
        {
            private readonly Comparison<T> comparison;

            public Comparer(Comparison<T> comparison)
            {
                this.comparison = comparison;
            }

            public int Compare(T x, T y)
            {
                return comparison.Invoke(x, y);
            }
        }
    }
}