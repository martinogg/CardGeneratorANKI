using System;
using System.Collections.Generic;

namespace CardGenerator
{
	public class AudioFiles
	{
		public String audioFilesPath = "";

		public class AudioEntry
		{
			public String languageCode { get; set; }
			public String text { get; set; }
			public String filename;
		}

		public AudioFiles ()
		{
		}

		public static List<AudioEntry> generateAudioFileNames()
		{
			List<AudioEntry> ret = new List<AudioEntry>();

			// list of audio languages to generate
			List<CardDefinitions.CardDefClass> languages = getLanguages();
			List<String> DuplicateFiles = new List<String>();

			// list of datas
			List<Dictionary<String, String>> data = CardGeneratorDB.Instance.getDataFromTable(CardGeneratorDB.TABLECardData);

			// for each audio language build filename
			foreach (CardDefinitions.CardDefClass language in languages)
			{

				foreach (Dictionary<String, String> item in data)
				{
					AudioEntry newEntry = new AudioEntry();
					newEntry.languageCode = language.LANGUAGECODE;
					newEntry.text = item[language.COLUMNSOURCE];
					newEntry.filename = generateFilename(newEntry.languageCode, newEntry.text);

					if (!DuplicateFiles.Contains(newEntry.filename))
					{
						ret.Add(newEntry);
						DuplicateFiles.Add(newEntry.filename);
					}
				}
			}

			return ret;
		}


		public static String generateFilename(String languageCode, String entryText)
		{
			// maybe do some hashing thing here
			String ret = languageCode + entryText;
			ret = ret.Replace(" ", String.Empty);
			ret = ret+".mp3";
			return ret;
		}

		private static List<CardDefinitions.CardDefClass> getLanguages()
		{
			List<CardDefinitions.CardDefClass> ret = new List<CardDefinitions.CardDefClass>();

			List<Dictionary<String, String>> cardDefinitions = CardGeneratorDB.Instance.getDataFromTable(CardGeneratorDB.TABLECardDefinitions);

			foreach (Dictionary<String, String> item in cardDefinitions)
			{
				for (int i = 0; i < 100; i++)
				{
					String keyName = CardGeneratorDB.COLUMNFace + i;
					if (item.ContainsKey(keyName))
					{
						String faceJSONData = item[keyName];
						List<CardDefinitions.CardDefClass> audioLanguages = CardDefinitions.audioLanguagesFromJSONString(faceJSONData);
						ret.AddRange(audioLanguages);
					}
				}
			}
			return ret;
		}
	}
}

