using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using JSONSharp.Collections;
using JSONSharp.Values;

namespace JSONSharp
{
    /// <summary>
    /// JSONReflector provides a convenient way to convert value and reference type objects
    /// to JSON format through reflection.
    /// 
    /// This implementation build JSON around reflected public properties of type int, float,
    /// double, decimal, byte, string, bool, enum or array.  (Generics and other types may be
    /// supported at a later time.)
    /// </summary>
    public class JSONReflector : JSONValue
    {
        private JSONObjectCollection _jsonObjectCollection;


        private JSONValue GetJSONValue(object objValue)
        {
            Type thisType = objValue.GetType();
            JSONValue jsonValue = null;

            if (thisType == typeof(System.Int32))
            {
                jsonValue = new JSONNumberValue(Convert.ToInt32(objValue));
            }
            else if (thisType == typeof(System.Single))
            {
                jsonValue = new JSONNumberValue(Convert.ToSingle(objValue));
            }
            else if (thisType == typeof(System.Double))
            {
                jsonValue = new JSONNumberValue(Convert.ToDouble(objValue));
            }
            else if (thisType == typeof(System.Decimal))
            {
                jsonValue = new JSONNumberValue(Convert.ToDecimal(objValue));
            }
            else if (thisType == typeof(System.Byte))
            {
                jsonValue = new JSONNumberValue(Convert.ToByte(objValue));
            }
            else if (thisType == typeof(System.String))
            {
                jsonValue = new JSONStringValue(Convert.ToString(objValue));
            }
            else if (thisType == typeof(System.Boolean))
            {
                jsonValue = new JSONBoolValue(Convert.ToBoolean(objValue));
            }
            else if (thisType.BaseType == typeof(System.Enum))
            {
                jsonValue = new JSONStringValue(Enum.GetName(thisType, objValue));
            }
            else if (thisType.IsArray)
            {
                List<JSONValue> jsonValues = new List<JSONValue>();
                Array arrValue = (Array)objValue;
                for (int x = 0; x < arrValue.Length; x++)
                {
                    JSONValue jsValue = this.GetJSONValue(arrValue.GetValue(x));
                    jsonValues.Add(jsValue);
                }
                jsonValue = new JSONArrayCollection(jsonValues);
            }
            return jsonValue;
        }

        /// <summary>
        /// Public constructor that accepts any object
        /// </summary>
        /// <param name="objValue">object to be reflected/evaluated for JSON conversion</param>
        public JSONReflector(object objValue)
        {
            Dictionary<JSONStringValue, JSONValue> jsonNameValuePairs = new Dictionary<JSONStringValue, JSONValue>();

            Type type = objValue.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo pi in properties)
            {
                JSONStringValue jsonParameterName = new JSONStringValue(pi.Name);
                JSONValue jsonParameterValue = this.GetJSONValue(pi.GetValue(objValue, null));
                if (jsonParameterValue != null)
                {
                    jsonNameValuePairs.Add(jsonParameterName, jsonParameterValue);
                }
            }

            this._jsonObjectCollection = new JSONObjectCollection(jsonNameValuePairs);
        }


        /// <summary>
        /// Required override of the ToString() method.
        /// </summary>
        /// <returns>returns the internal JSONObjectCollection ToString() method</returns>
        public override string ToString()
        {
            return this._jsonObjectCollection.ToString();
        }

        /// <summary>
        /// Required override of the PrettyPrint() method.
        /// </summary>
        /// <returns>returns the internal JSONObjectCollection PrettyPrint() method</returns>
        public override string PrettyPrint()
        {
            return this._jsonObjectCollection.PrettyPrint();
        }
    }
}
