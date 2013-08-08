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
    /// JSONValueCollection represents any collection in JSONSharp.  It is used to 
    /// represent arrays of values to be contained within a JSON-compliant string of characters.
    /// 
    /// A JSONValueCollection is itself a JSONValue object.
    /// </summary>
    public abstract class JSONValueCollection : JSONValue
    {
        /// <summary>
        /// Named element for the separation character for this JSONValue object.
        /// </summary>
        protected readonly string JSONVALUE_SEPARATOR = ",";

        internal JSONValueCollection()
        {
        }

        /// <summary>
        /// Any implementation must override CollectionToPrettyPrint(), used for rendering the
        /// contained object data in JSON-compliant form but with indentation for readability.
        /// </summary>
        /// <returns>The value as a string, indented for readability.</returns>
        protected abstract string CollectionToPrettyPrint();

        /// <summary>
        /// Any implementation must override the base ToString() method, used for
        /// producing the contained object data in JSON-compliant form.
        /// </summary>
        /// <returns>The value as a string, formatted in compliance with RFC 4627.</returns>
        protected abstract string CollectionToString();

        /// <summary>
        /// Required override the base ToString() method. Writes contained data using
        /// the abstract CollectionToString() method, bounded by the BeginMarker and EndMarker
        /// properties.
        /// </summary>
        /// <returns>The value as a string, formatted in compliance with RFC 4627.</returns>
        public override string ToString()
        {
            return this.BeginMarker + this.CollectionToString() + this.EndMarker;
        }

        /// <summary>
        /// Required override of PrettyPrint(). Utilizes the CollectionToPrettyPrint()
        /// method, required by implementors of this class.
        /// </summary>
        /// <returns>The value as a string, indented for readability.</returns>
        public override string PrettyPrint()
        {
            return Environment.NewLine +
                "".PadLeft(JSONValue.CURRENT_INDENT, Convert.ToChar(base.HORIZONTAL_TAB)) +
                this.BeginMarker +
                Environment.NewLine +
                this.CollectionToPrettyPrint() +
                Environment.NewLine +
                "".PadLeft(JSONValue.CURRENT_INDENT, Convert.ToChar(base.HORIZONTAL_TAB)) +
                this.EndMarker;
        }

        /// <summary>
        /// Any implementation must override the BeginMarker property, used for
        /// denoting the lead wrapping character for the collection type.
        /// </summary>
        protected abstract string BeginMarker { get; }

        /// <summary>
        /// Any implementation must override the EndMarker property, used for
        /// denoting the trailing wrapping character for the collection type.
        /// </summary>
        protected abstract string EndMarker { get; }

    }
}
