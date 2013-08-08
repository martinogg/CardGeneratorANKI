/*
JSONSharp, a c# library for generating strings in JSON format
Copyright (C) 2007 Jeff Rodenburg

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

If you have questions about the library, please contact me at jeff.rodenburg@gmail.com.
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using JSONSharp;

namespace JSONSharp.Values
{
    /// <summary>
    /// JSONNumberValue is very much like a C# number, except that octal and hexadecimal formats 
    /// are not used.
    /// </summary>
    public class JSONNumberValue : JSONValue
    {
        private string _value;

        /// <summary>
        /// Number formatting object for handling globalization differences with decimal point separators
        /// </summary>
        protected static NumberFormatInfo JavaScriptNumberFormatInfo;

        static JSONNumberValue()
        {
            JavaScriptNumberFormatInfo = new NumberFormatInfo();
            JavaScriptNumberFormatInfo.NumberDecimalSeparator = ".";
        }

        internal JSONNumberValue(string value)
            : base()
        {
            this._value = value;
        }

        /// <summary>
        /// Public constructor that accepts a value of type int
        /// </summary>
        /// <param name="value">int (System.Int32) value</param>
        public JSONNumberValue(int value)
            : this(value.ToString())
        {
        }

        /// <summary>
        /// Public constructor that accepts a value of type double
        /// </summary>
        /// <param name="value">double (System.Double) value</param>
        public JSONNumberValue(double value)
            : this(value.ToString(JSONNumberValue.JavaScriptNumberFormatInfo))
        {
        }

        /// <summary>
        /// Public constructor that accepts a value of type decimal
        /// </summary>
        /// <param name="value">decimal (System.Decimal) value</param>
        public JSONNumberValue(decimal value)
            : this(value.ToString(JSONNumberValue.JavaScriptNumberFormatInfo))
        {
        }

        /// <summary>
        /// Public constructor that accepts a value of type single
        /// </summary>
        /// <param name="value">single (System.Single) value</param>
        public JSONNumberValue(Single value)
            : this(value.ToString("E", JSONNumberValue.JavaScriptNumberFormatInfo))
        {
        }

        /// <summary>
        /// Public constructor that accepts a value of type byte
        /// </summary>
        /// <param name="value">byte (System.Byte) value</param>
        public JSONNumberValue(byte value)
            : this(value.ToString())
        {
        }

        /// <summary>
        /// Required override of ToString() method.
        /// </summary>
        /// <returns>contained numeric value, rendered as a string</returns>
        public override string ToString()
        {
            return this._value;
        }

        /// <summary>
        /// Required override of the PrettyPrint() method.
        /// </summary>
        /// <returns>this.ToString()</returns>
        public override string PrettyPrint()
        {
            return this.ToString();
        }
    }

}
