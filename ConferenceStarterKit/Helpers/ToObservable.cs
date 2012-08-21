using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;


namespace ConferenceStarterKit
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source, ObservableCollection<T> collection)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            foreach (T item in source)
                collection.Add(item);
           
            return collection;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }          

            return new ObservableCollection<T>(source);
        }
    }
}

