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
using JSONSharp;
using JSONSharp.Values;

namespace JSONSharp.Collections
{
    /// <summary>
    /// JSONObjectCollection is an unordered set of name/value pairs. An object begins 
    /// with "{" (left brace) and ends with "}" (right brace). Each name is followed 
    /// by ":" (colon) and the name/value pairs are separated by "," (comma).
    /// </summary>
    public class JSONObjectCollection : JSONValueCollection
    {
        private Dictionary<JSONStringValue, JSONValue> _namevaluepairs;
        private readonly string NAMEVALUEPAIR_SEPARATOR = ":";

        /// <summary>
        /// Public constructor that accepts a Dictionary of name/value pairs.
        /// </summary>
        /// <param name="namevaluepairs">Dictionary collection of name/value pairs (JSONStringValue=name,JSONValue=pair)</param>
        public JSONObjectCollection(Dictionary<JSONStringValue, JSONValue> namevaluepairs)
            : base()
        {
            this._namevaluepairs = namevaluepairs;
        }

        /// <summary>
        /// Empty public constructor. Use this method in conjunction with
        /// the Add method to populate the internal dictionary of name/value pairs.
        /// </summary>
        public JSONObjectCollection()
            : base()
        {
            this._namevaluepairs = new Dictionary<JSONStringValue, JSONValue>();
        }

        /// <summary>
        /// Adds a JSONStringValue as the "name" and a JSONValue as the "value" to the 
        /// internal Dictionary.  Values are checked to ensure no duplication occurs 
        /// in the internal Dictionary.
        /// </summary>
        /// <param name="name">JSONStringValue "name" to add to the internal dictionary</param>
        /// <param name="value">JSONValue "value" to add to the internal dictionary</param>
        public void Add(JSONStringValue name, JSONValue value)
        {
            if (!this._namevaluepairs.ContainsKey(name))
                this._namevaluepairs.Add(name, value);
        }

        /// <summary>
        /// Required override of the CollectionToPrettyPrint() method.
        /// </summary>
        /// <returns>the entire dictionary as a string in JSON-compliant format, with indentation for readability</returns>
        protected override string CollectionToPrettyPrint()
        {
            JSONValue.CURRENT_INDENT++;
            List<string> output = new List<string>();
            List<string> nvps = new List<string>();
            foreach (KeyValuePair<JSONStringValue, JSONValue> kvp in this._namevaluepairs)
                nvps.Add("".PadLeft(JSONValue.CURRENT_INDENT, Convert.ToChar(base.HORIZONTAL_TAB)) + kvp.Key.PrettyPrint() + this.NAMEVALUEPAIR_SEPARATOR + kvp.Value.PrettyPrint());
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
            foreach (KeyValuePair<JSONStringValue, JSONValue> kvp in this._namevaluepairs)
                nvps.Add(kvp.Key.ToString() + this.NAMEVALUEPAIR_SEPARATOR + kvp.Value.ToString());
            output.Add(string.Join(base.JSONVALUE_SEPARATOR, nvps.ToArray()));
            return string.Join("", output.ToArray());
        }

        /// <summary>
        /// Required override of the BeginMarker property
        /// </summary>
        protected override string BeginMarker
        {
            get { return "{"; }
        }

        /// <summary>
        /// Required override of the EndMarker property
        /// </summary>
        protected override string EndMarker
        {
            get { return "}"; }
        }

    }
}
