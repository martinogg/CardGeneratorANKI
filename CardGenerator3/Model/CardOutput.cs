using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;


namespace CardGenerator
{
	public class CardOutput
	{
		public CardOutput ()
		{
		}

		public enum eCARDORDERTYPE {
			eCARDORDERVOCAB,
			eCARDORDERTYPES,
			eCARDORDERRANDOM,
		};

		public static void saveToAnkiFile(String fileName)
		{
			StreamWriter fileout = new StreamWriter(fileName);

			List<Dictionary<String, String>> outputTable = CardGeneratorDB.Instance.getDataFromTable(CardGeneratorDB.TABLECardOutput);

			foreach (Dictionary<String, String> outputEntry in outputTable)
			{
				String CardDataID = outputEntry[CardGeneratorDB.COLUMNCardDataID];
				String CardDefinitionID = outputEntry[CardGeneratorDB.COLUMNCardDefinitionID];

			
				// ("+COLUMNCardOutputID+" INTEGER NOT NULL, "+COLUMNCardDefinitionID+" INTEGER, "+COLUMNCardDataID+" INTEGER)",
				Dictionary<String, String> cardDefinition = CardGeneratorDB.Instance.getRowFromTable(CardGeneratorDB.TABLECardDefinitions, CardGeneratorDB.COLUMNCardDefinitionID, CardDefinitionID);
				Dictionary<String, String> cardData = CardGeneratorDB.Instance.getRowFromTable(CardGeneratorDB.TABLECardData, CardGeneratorDB.COLUMNCardDataID, CardDataID);

				StringWriter faceOutput = new StringWriter();
				int iFace = 1;
				bool bFirstFaceRun = true;
				bool bContinue = true;
				while (bContinue)
				{
					String faceColumn = "Face"+(iFace++);
					if (cardDefinition.ContainsKey(faceColumn))
					{
						if (!bFirstFaceRun)
						{
							faceOutput.Write(";");
						}
						else
						{
							bFirstFaceRun = false;
						}

						String faceDefinition = cardDefinition[faceColumn];
						List<CardDefinitions.CardDefClass> faceLanguages = CardDefinitions.faceCardStructureFromJSONString(faceDefinition);
						bool bFirstRun = true; // first entry in a face

						foreach (CardDefinitions.CardDefClass item in faceLanguages)
						{
							if (!bFirstRun)
							{
								faceOutput.Write("<BR>");
							}
							else
							{
								bFirstRun = false;
							}

							if (item.TYPE == false)
							{
								faceOutput.Write(cardData[item.COLUMNSOURCE]);
							}
							else
							{
								// a sound, get the sound instead
								String filename = AudioFiles.generateFilename(item.LANGUAGECODE, cardData[item.COLUMNSOURCE]);
								faceOutput.Write("[sound:" + filename + "]");
							}
						}
					}
					else
					{
						bContinue = false;
					}
				}

				String outLn = faceOutput.ToString();
				fileout.WriteLine(outLn);

				// ok, now build the faces from each 
				// get faces data

				// DE-JSON into class data

				// build string for each JSON context

			}
			fileout.Close();
		}

		public static void BuildCardOutputTable(List<String> cardTypeNames, eCARDORDERTYPE order)
		{
			// build every permutation of carddata and carddefinitions justnow.
			// use the permutations to build the face card data later

			CardGeneratorDB.Instance.removeDataFromTable(CardGeneratorDB.TABLECardOutput);

			List<String> cardDataIDs = CardGeneratorDB.Instance.getSingleColumnDataFromTable(CardGeneratorDB.TABLECardData, CardGeneratorDB.COLUMNCardDataID);
			List<String> cardDefinitionIDs = new List<String>();
			{
				List<Dictionary<String, String>> cardDefinitions = CardGeneratorDB.Instance.getDataFromTable(CardGeneratorDB.TABLECardDefinitions);

				foreach(Dictionary<String, String> item in cardDefinitions)
				{
					if (cardTypeNames.Contains(item[CardGeneratorDB.COLUMNCardDefinitionName]))
					{
						cardDefinitionIDs.Add(item[CardGeneratorDB.COLUMNCardDefinitionID]);
					}
				}
				//List<String> cardDefinitionIDs = CardGeneratorDB.Instance.getSingleColumnDataFromTable(CardGeneratorDB.TABLECardDefinitions, CardGeneratorDB.COLUMNCardDefinitionID);
			}

			if (order == eCARDORDERTYPE.eCARDORDERRANDOM)
			{ // any output will be randomized

				//cardDataIDs = ShuffleList(cardDataIDs);
				//cardDefinitionIDs = ShuffleList(cardDefinitionIDs);

				List<String[]> dataout = new List<string[]>();

				foreach (String cardDataID in cardDataIDs)
				{
					foreach (String cardDefinitionID in cardDefinitionIDs)
					{
						String[] data = {CardGeneratorDB.COLUMNCardOutputID, "0",
							CardGeneratorDB.COLUMNCardDefinitionID , cardDefinitionID,
							CardGeneratorDB.COLUMNCardDataID , cardDataID};

						dataout.Add(data);
					}
				}

				dataout = ShuffleList(dataout);

				int iID = 0;
				foreach (String[] data in dataout)
				{
					iID++;
					data[1] = iID.ToString();
					CardGeneratorDB.Instance.addRowToTable(CardGeneratorDB.TABLECardOutput, data);
				}

			}
			else if (order == eCARDORDERTYPE.eCARDORDERVOCAB)
			{ // order by data
				int iID = 0;
				foreach (String cardDataID in cardDataIDs)
				{
					foreach (String cardDefinitionID in cardDefinitionIDs)
					{
						iID++;
						String[] data = {CardGeneratorDB.COLUMNCardOutputID, iID.ToString(),
							CardGeneratorDB.COLUMNCardDefinitionID , cardDefinitionID,
							CardGeneratorDB.COLUMNCardDataID , cardDataID};

						CardGeneratorDB.Instance.addRowToTable(CardGeneratorDB.TABLECardOutput, data);
					}
				}
			}
			else
			{ // order by type
				int iID = 0;
				foreach (String cardDefinitionID in cardDefinitionIDs)
				{
					foreach (String cardDataID in cardDataIDs)
					{
						iID++;
						String[] data = {CardGeneratorDB.COLUMNCardOutputID, iID.ToString(),
							CardGeneratorDB.COLUMNCardDefinitionID , cardDefinitionID,
							CardGeneratorDB.COLUMNCardDataID , cardDataID};

						CardGeneratorDB.Instance.addRowToTable(CardGeneratorDB.TABLECardOutput, data);
					}
				}
			}

		}

		private static List<T> ShuffleList<T>(List<T> source)
		{
			List<T> sortedList = new List<T>();
			Random generator = new Random();

			while (source.Count > 0)
			{
				int position = generator.Next(source.Count);
				sortedList.Add(source[position]);
				source.RemoveAt(position);
			}

			return sortedList;
		}
	}
}

