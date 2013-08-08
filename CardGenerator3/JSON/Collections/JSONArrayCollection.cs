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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JSONSharp;

namespace JSONSharp.Collections
{
    /// <summary>
    /// JSONArrayCollection is an ordered collection of values. An array begins with 
    /// "[" (left bracket) and ends with "]" (right bracket). Array elements are 
    /// separated by "," (comma).
    /// </summary>
    public class JSONArrayCollection : JSONValueCollection
    {
        /// <summary>
        /// Internal generic list of JSONValue objects that comprise the elements
        /// of the JSONArrayCollection.
        /// </summary>
        protected List<JSONValue> _values;

        /// <summary>
        /// Public constructor that accepts a generic list of JSONValue objects.
        /// </summary>
        /// <param name="values">Generic list of JSONValue objects.</param>
        public JSONArrayCollection(List<JSONValue> values)
            : base()
        {
            this._values = values;
        }

        /// <summary>
        /// Empty public constructor. Use this method in conjunction with
        /// the Add method to populate the internal array of elements.
        /// </summary>
        public JSONArrayCollection()
            : base()
        {
            this._values = new List<JSONValue>();
        }

        /// <summary>
        /// Adds a JSONValue to the internal object array.  Values are checked to 
        /// ensure no duplication occurs in the internal array.
        /// </summary>
        /// <param name="value">JSONValue to add to the internal array</param>
        public void Add(JSONValue value)
        {
            if (!this._values.Contains(value))
                this._values.Add(value);
        }

        /// <summary>
        /// Required override of the CollectionToPrettyPrint() method.
        /// </summary>
        /// <returns>the entire collection as a string in JSON-compliant format, with indentation for readability</returns>
        protected override string CollectionToPrettyPrint()
        {
            JSONValue.CURRENT_INDENT++;
            List<string> output = new List<string>();
            List<string> nvps = new List<string>();
            foreach (JSONValue jv in this._values)
                nvps.Add("".PadLeft(JSONValue.CURRENT_INDENT, Convert.ToChar(base.HORIZONTAL_TAB)) + jv.PrettyPrint());
            output.Add(string.Join(base.JSONVALUE_SEPARATOR + Environment.NewLine, nvps.ToArray()));
            JSONValue.CURRENT_INDENT--;
            return string.Join("", output.ToArray());
        }

        /// <summary>
        /// Required override of the CollectionToString() method.
        /// </summary>
        /// <returns>the entire collection as a string in JSON-compliant format</returns>
        protected override string CollectionToString()
        {
            List<string> output = new List<string>();
            List<string> nvps = new List<string>();
            foreach (JSONValue jv in this._values)
                nvps.Add(jv.ToString());

            output.Add(string.Join(base.JSONVALUE_SEPARATOR, nvps.ToArray()));
            return string.Join("", output.ToArray());
        }

        /// <summary>
        /// Required override of the BeginMarker property
        /// </summary>
        protected override string BeginMarker
        {
            get { return "["; }
        }

        /// <summary>
        /// Required override of the EndMarker property
        /// </summary>
        protected override string EndMarker
        {
            get { return "]"; }
        }


    }
}
