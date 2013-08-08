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
using System.Text;

namespace JSONSharp
{
    /// <summary>
    /// JSONValue represents the core object in JSONSharp.  It is used to represent values
    /// to be contained within a JSON-compliant string of characters.
    /// 
    /// A JSON value can be a string in double quotes, a number, true or false, null, an 
    /// object or an array. These structures can be nested.
    /// </summary>
    public abstract class JSONValue
    {
        /// <summary>
        /// Named element to represent a horizontal tab character. Used for PrettyPrint().
        /// </summary>
        protected readonly string HORIZONTAL_TAB = "\t";

        /// <summary>
        /// Static counter used for PrettyPrint().
        /// </summary>
        public static int CURRENT_INDENT = 0;

        internal JSONValue()
        {
        }

        /// <summary>
        /// Any implementation must override the base ToString() method, used for
        /// producing the contained object data in JSON-compliant form.
        /// </summary>
        /// <returns>The value as a string, formatted in compliance with RFC 4627.</returns>
        public abstract override string ToString();

        /// <summary>
        /// Any implementation must override PrettyPrint(), used for rendering the
        /// contained object data in JSON-compliant form but with indentation for readability.
        /// </summary>
        /// <returns>The value as a string, indented for readability.</returns>
        public abstract string PrettyPrint();
    }
}
