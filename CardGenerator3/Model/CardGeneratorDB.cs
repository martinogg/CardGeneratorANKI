using System;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Text;
using JSONSharp;
using JSONSharp.Collections;
using JSONSharp.Values;

namespace CardGenerator
{
	public enum CGError
	{
		OK = 0,
		NOFILE,
		FILEERROR,
		FILEEXISTS,
	};

	public class CardGeneratorDB
	{
		public CardGeneratorDB ()
		{
		}

		private static volatile CardGeneratorDB instance;
		private static object syncRoot = new Object ();
		public const String TABLECardData = "CardData";
		public const String TABLECardDefinitions = "CardDefinitions";
		public const String TABLECardOutput = "CardOutput";
		public const String COLUMNCardDataID = "CardDataID";
		public const String COLUMNCardDefinitionID = "CardDefinitionID";
		public const String COLUMNCardOutputID = "CardOutputID";
		public const String COLUMNCardDefinitionName = "FaceCardName";
		public const String COLUMNFace = "Face";
		private SQLiteConnection _DB = null;
		private String _DBPath = "";

		public String DBPath {
			get {
				return _DBPath;
			}
		}

		public static CardGeneratorDB Instance {
			get {
				if (instance == null) {
					lock (syncRoot) {
						if (instance == null) 
							instance = new CardGeneratorDB ();
					}
				}

				return instance;
			}
		}

		public CGError loadDBAtPath (String path)
		{
			CGError ret = CGError.OK;

			// check if exists
			if (!File.Exists (path)) {
				ret = CGError.NOFILE;
			} else {
				// start with database
				openDBFile (path);

			}

			return ret;
		}

		public CGError createDBAtPath (String path)
		{
			CGError ret = CGError.OK;

			// check if exists
			if (File.Exists (path)) {
				ret = CGError.FILEEXISTS;
			} else {
				// create database file
				openDBFile (path);
				WriteDefaultTable ();

			}

			return ret;
		}

		private void openDBFile (String path)
		{
			_DB = new SQLiteConnection ("Data Source=" + path);
			_DBPath = path;
		}

		public void updateRowField (String tableName, String searchColumn, String searchColumnID, String changeColumn, String changeColumnText)
		{
			String[] execStr = {"UPDATE "+tableName+" SET "+changeColumn+"='"+changeColumnText+"' WHERE "+searchColumn+"="+searchColumnID,};
			execDB(execStr);
		}

		private void WriteDefaultTable()
		{

			JSONArrayCollection Face1 = new JSONArrayCollection();
			Face1.Add(CardDefinitions.newCardDef("English", "EN", false));
			Face1.Add(CardDefinitions.newCardDef("English", "EN", true));

			JSONArrayCollection Face2 = new JSONArrayCollection();
			Face2.Add(CardDefinitions.newCardDef("Pinyin", "ZH", false));
			Face2.Add(CardDefinitions.newCardDef("TradChinese", "ZH", false));
			Face2.Add(CardDefinitions.newCardDef("SimpChinese", "ZH", false));
			Face2.Add(CardDefinitions.newCardDef("TradChinese", "ZH", true));

			String strFace1 = Face1.ToString();
			String strFace2 = Face2.ToString();
				
				var commands = new[] {
				"CREATE TABLE CardData ("+COLUMNCardDataID+" INTEGER PRIMARY KEY, English ntext, Pinyin ntext, TradChinese ntext, SimpChinese ntext, German ntext)",
				"INSERT INTO CardData (English, Pinyin, TradChinese, SimpChinese, German) VALUES ('One', 'Yi', '一', '一', 'ein')",
				"INSERT INTO CardData (English, Pinyin, TradChinese, SimpChinese, German) VALUES ('Two', 'Er', '二', '二', 'zwei')",
				"INSERT INTO CardData (English, Pinyin, TradChinese, SimpChinese, German) VALUES ('Three', 'San', '三', '三', 'drei')",

				"CREATE TABLE CardDefinitions ("+COLUMNCardDefinitionID+" INTEGER PRIMARY KEY, FaceCardName ntext, Face1 ntext, Face2 ntext)",
				"INSERT INTO CardDefinitions (FaceCardName, Face1, Face2) VALUES ('Eng->Ch', '"+strFace1+"', '"+strFace2+"')",
				"INSERT INTO CardDefinitions (FaceCardName, Face1, Face2) VALUES ('Ch->Eng', '"+strFace2+"', '"+strFace1+"')",

				"CREATE TABLE CardOutput ("+COLUMNCardOutputID+" INTEGER PRIMARY KEY, "+COLUMNCardDefinitionID+" INTEGER, "+COLUMNCardDataID+" INTEGER)",
				};

			execDB(commands);

			//!
		}

		private void execDB(String[] commands)
		{
			_DB.Open ();
			foreach (var cmd in commands) {
				using (var c = _DB.CreateCommand()) {
					c.CommandText = cmd;
					c.CommandType = CommandType.Text;
					c.ExecuteNonQuery ();
				}
			}
			_DB.Close ();
		}

		public void addCardType(String cardTypeName)
		{
			var commandss = new[] 
			{
				"INSERT INTO "+TABLECardDefinitions+" (FaceCardName, Face1, Face2) VALUES ('"+cardTypeName+"', '', '')",
			};

			execDB(commandss);

		}

		public List<String> GetColumnNames( String tableName)
		{
			List<String> ret = new List<String>();

			var connection = _DB;
			using (var cmd = connection.CreateCommand ()) {
				connection.Open ();
				//cmd.CommandText = "select * from "+tableName+" LIMIT 1";
				cmd.CommandText = "pragma table_info("+tableName+");";
				using (var reader = cmd.ExecuteReader ()) {
					while (reader.Read ()) {
						ret.Add(reader[1].ToString());
					}
				}
			}
			connection.Close ();
			return ret;
		}

		public Dictionary<String, Dictionary<String,String>> GetColumnData( String tableName)
		{
			Dictionary<String, Dictionary<String,String>> ret = new Dictionary<String, Dictionary<String,String>>();

			var connection = _DB;
			using (var cmd = connection.CreateCommand ()) {
				connection.Open ();
				//cmd.CommandText = "select * from "+tableName+" LIMIT 1";
				cmd.CommandText = "pragma table_info("+tableName+");";
				using (var reader = cmd.ExecuteReader ()) {
					while (reader.Read ()) {

						Dictionary<String, String> oneLine = new Dictionary<String, String>();
						for (int i = 0; i < reader.FieldCount; i++)
						{
							oneLine.Add(reader.GetName(i), reader[i].ToString());
						}
						ret.Add(reader[1].ToString(), oneLine);
					}
				}
			}
			connection.Close ();
			return ret;
		}


		public Dictionary<String, String> getRowFromTable(String tableName, String searchColumn, String searchColumnRowValue)
		{
			Dictionary<String, String> ret = new Dictionary<String, String> ();

			// open a database file here
			var connection = _DB;
			using (var cmd = connection.CreateCommand ()) {
				connection.Open ();
				cmd.CommandText = "SELECT * FROM " + tableName + " WHERE "+searchColumn+"='"+searchColumnRowValue+"' LIMIT 1";
				using (var reader = cmd.ExecuteReader ()) {
					if (reader.Read ()) {

						// compile all fields into array
						//String[] rowData = new String[reader.FieldCount];
						for (int i = 0; i < reader.FieldCount; i++) {

							ret.Add(reader.GetName(i), reader[i].ToString());
							//rowData [i] = reader[i].ToString();

						}
					}
				}
			}
			connection.Close ();

			return ret;
		}

		public int GetRowCount (string tableName)
		{
			String ret = null;
			var connection = _DB;
			using (var cmd = connection.CreateCommand ()) {
				connection.Open ();
				cmd.CommandText = "SELECT Count(*) FROM "+tableName;
				using (var reader = cmd.ExecuteReader ()) {
					if (reader.Read ()) {
					
						// compile all fields into array
						ret = reader[0].ToString();
					}
				}
			}
			connection.Close ();

			return Convert.ToInt32(ret);
		}

		public void removeRowFromTable(String tableName, String columnName, String rowText)
		{
			using (var cmd = _DB.CreateCommand ()) {
				_DB.Open();
				cmd.CommandText = "DELETE FROM " + tableName + " WHERE "+columnName+"='"+rowText+"'";
				cmd.ExecuteReader();
				_DB.Close();
			}

		}

		public void addRowToTable(String tableName, params object[] dataIn)
		{
			StringWriter columns = new StringWriter();
			StringWriter datas = new StringWriter();
			for (int i = 0; i < dataIn.Length; i=i+2)
			{
				columns.Write(dataIn[i].ToString());
				if (dataIn[i].GetType() == typeof(String))
				{
					String writeable = dataIn[i+1].ToString();
					writeable = writeable.Replace("'", "''");
					datas.Write("'"+writeable+"'"); 
				}
				else
				{
					datas.Write(dataIn[i+1]); 
				}

				if (i < (dataIn.Length-2)) // i.e. if its NOT on the last pair
				{
					columns.Write(" , ");
					datas.Write(" , ");
				}
			}

			try
			{
				using (var cmd = _DB.CreateCommand ()) {
					_DB.Open();
					cmd.CommandText = "INSERT INTO "+tableName+" ("+columns+") VALUES ("+datas+")";
					cmd.ExecuteReader();
				}
			}
			catch (Exception _ee)
			{
				System.Console.WriteLine("DB ERROR:"+_ee.ToString());
			}
			finally
			{
				_DB.Close();
			}
		}

		public List<String> getSingleColumnDataFromTable (String tableName, String columnName)
		{
			List<String> ret = new List<String> ();

			// open a database file here
			var connection = _DB;
			using (var cmd = connection.CreateCommand ()) {
				connection.Open ();
				cmd.CommandText = "SELECT "+columnName+" FROM " + tableName;
				using (var reader = cmd.ExecuteReader ()) {
					while (reader.Read ()) {

						ret.Add(reader[0].ToString());
					
					}

				}
			}
			connection.Close ();

			return ret;
		}


		public List<Dictionary<String, String>> getDataFromTable (String tableName)
		{
			List<Dictionary<String, String>> ret = new List<Dictionary<String, String>> ();
			//KeyValuePair<string, string> = new KeyValuePair<string,string>("defaultkey", "defaultvalue");

			//List<String> stringList = new List<String>();
			//stringList.Add("Test");

			// open a database file here
			var connection = _DB;
			using (var cmd = connection.CreateCommand ()) {
				connection.Open ();
				cmd.CommandText = "SELECT * FROM " + tableName;
				using (var reader = cmd.ExecuteReader ()) {
					while (reader.Read ()) {

						Dictionary<String, String> newEntry = new Dictionary<String, String>();

						// compile all fields into array
						//String[] rowData = new String[reader.FieldCount];
						for (int i = 0; i < reader.FieldCount; i++) {

							newEntry.Add(reader.GetName(i), reader[i].ToString());
							//rowData [i] = reader[i].ToString();

						}

						ret.Add(newEntry);
					
						}
						
					}
				}
			connection.Close ();

			return ret;
		}

		public void removeDataFromTable(String tableName)
		{
			using (var cmd = _DB.CreateCommand ()) {
				_DB.Open();
				cmd.CommandText = "DELETE FROM " + tableName;
				cmd.ExecuteReader();
				_DB.Close();
			}
		}

		public void addColumn(String tableName, String columnName)
		{
			String[] newLine = {"ALTER TABLE "+tableName+" ADD "+columnName};
			execDB(newLine);
		}

		public void removeColumn(String tableName, String columnName)
		{
			List<String> updatedTableColumns = GetColumnNames(tableName);
			Dictionary<String, Dictionary<String,String>> columnData = GetColumnData(tableName);

			// Remove the unwanted column from the table's list of columns
			updatedTableColumns.Remove(columnName);

			List<String> columnTypes = new List<String>();
			foreach(String item in updatedTableColumns)
			{
				String type = columnData[item]["type"];
				if (type.Equals("INTEGER") && (item.EndsWith("ID")))
				{
					type = "INTEGER PRIMARY KEY"; // rather nasty way of determining Integer primary key, if theres a better way please let me know
				}
				columnTypes.Add(item +" "+ type);
			}

			String columnsSeperated = join(updatedTableColumns, ",");
			String columnsWithTypesSeperated = join(columnTypes, ",");

			//"CREATE TABLE CardData (CardDataID INTEGER NOT NULL, English ntext, Pinyin ntext, TradChinese ntext, SimpChinese ntext, German ntext)",
			String[] execStr =
			{
				"ALTER TABLE " + tableName + " RENAME TO " + tableName + "_old;",

				// Creating the table on its new format (no redundant columns)
				//"CREATE TABLE "+tableName+";",
				"CREATE TABLE " + tableName + "(" + columnsWithTypesSeperated + ");",
				//"CREATE TABLE CardDefinitions ("+COLUMNCardDefinitionID+" INTEGER NOT NULL, FaceCardName ntext, Face1 ntext, Face2 ntext)",

				// Populating the table with the data
				"INSERT INTO " + tableName + "(" + columnsSeperated + ") SELECT " + columnsSeperated + " FROM " + tableName + "_old;" ,
				"DROP TABLE " + tableName + "_old;",
			};

			execDB(execStr);
		}

		static String join(List<String> s, String delimiter) {
			StringWriter builder = new StringWriter();
			bool bFirst = true;

			foreach (String item in s)
			{
				if (!bFirst)
				{
					builder.Write (delimiter);
				}
				else
				{
					bFirst = false;
				}

				builder.Write(item);
			}

			return builder.ToString();
		}
	
 
	}
}

