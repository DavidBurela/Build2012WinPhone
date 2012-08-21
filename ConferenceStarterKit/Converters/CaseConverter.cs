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

namespace ConferenceStarterKit.Converters
{
    public class CaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string convertedValue = string.Empty;

            if (value != null)
            {
                convertedValue = (string)value;

                if (parameter.ToString().ToLower() == "u")
                    convertedValue = convertedValue.ToUpper();
                else
                    convertedValue = convertedValue.ToLower();

                return convertedValue;
            }
            else
                return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

