using System;
using System.Collections.Generic;
using System.Text;
using JSONSharp;
using JSONSharp.Collections;
using JSONSharp.Values;
using Newtonsoft.Json;

using CardDefDict = System.Collections.Generic.Dictionary<JSONSharp.Values.JSONStringValue, JSONSharp.JSONValue>;

namespace CardGenerator
{

	public class CardDefinitions
	{
		public static String COLUMNSOURCE = "COLUMNSOURCE";
		public static String LANGUAGECODE = "LANGUAGECODE";
		public static String TYPE = "TYPE";

		public class CardDefClass
		{
			public String COLUMNSOURCE { get; set; }
			public String LANGUAGECODE { get; set; }
			public bool TYPE { get; set; }
		}

		public CardDefinitions ()
		{

		}

		public static JSONObjectCollection newCardDef(String sourceColumn, String languageCode, bool bVoice)
		{
			JSONObjectCollection ret = new JSONObjectCollection();
	
			ret.Add(new JSONStringValue(COLUMNSOURCE), new JSONStringValue(sourceColumn));
			ret.Add(new JSONStringValue(LANGUAGECODE), new JSONStringValue(languageCode));
			ret.Add(new JSONStringValue(TYPE), new JSONBoolValue(bVoice));

			return ret;
		}

		public static String JSONStringFromfaceCardStructure(List<CardDefinitions.CardDefClass> cardList)
		{
			JSONArrayCollection ret = new JSONArrayCollection();
			foreach (CardDefClass item in cardList)
			{
				ret.Add(CardDefinitions.newCardDef(item.COLUMNSOURCE, item.LANGUAGECODE, item.TYPE));
			}
			return ret.ToString();
		}
			
		public static List<CardDefClass> audioLanguagesFromJSONString(String faceJSONData)
		{
			List<CardDefClass> ret = new List<CardDefClass>();

			var JSONData = JsonConvert.DeserializeObject(faceJSONData);

			Newtonsoft.Json.Linq.JArray DataArray = (Newtonsoft.Json.Linq.JArray) JSONData;

			List<CardDefClass> ArrayCards = DataArray.ToObject<List<CardDefClass>>();

			foreach (CardDefClass item in ArrayCards)
			{
				if (item.TYPE == true)
				{
					ret.Add(item);
				}
			}
			return ret;
		}

		public static List<CardDefClass> faceCardStructureFromJSONString(String faceJSONData)
		{
			List<CardDefClass> ret = new List<CardDefClass>();

			try
			{
				var JSONData = JsonConvert.DeserializeObject(faceJSONData);

				Newtonsoft.Json.Linq.JArray DataArray = (Newtonsoft.Json.Linq.JArray) JSONData;

				List<CardDefClass> ArrayCards = DataArray.ToObject<List<CardDefClass>>();

				foreach (CardDefClass item in ArrayCards)
				{
					ret.Add(item);	
				}
			}
			catch (Exception _ee)
			{
				Console.WriteLine("ERROR:"+_ee.ToString());
			}

			return ret;
		}
	//	public 
	}
}

