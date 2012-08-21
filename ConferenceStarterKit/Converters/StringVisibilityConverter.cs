﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 
 
using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace ConferenceStarterKit.Converters
{
    public class StringVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (parameter != null)
                {
                    //i = invert.
                    if (parameter.ToString() == "i")
                    {
                        if ((string)value == string.Empty)
                            return Visibility.Visible;
                        else
                            return Visibility.Collapsed;
                    }
                    else
                    {
                        if ((string)value == string.Empty)
                            return Visibility.Collapsed;
                        else
                            return Visibility.Visible;
                    }
                }
                else
                {
                    if ((string)value == string.Empty)
                        return Visibility.Collapsed;
                    else
                        return Visibility.Visible;
                }
            }
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
