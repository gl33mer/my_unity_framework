using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

using UnityEngine;

namespace Com.Nravo.FlipTheBoard.PersistantStorage
{
    /*
     *  Tested on Standalone, Android, iOS
     * 
     *  Class to save and read encrypted XML files.
     */
	public static class EncryptedXmlSerializer
	{
		private static readonly string PrivateKey = SystemInfo.deviceUniqueIdentifier.Replace("-", string.Empty);

		public static T Load<T>(string path)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			var reader = new StreamReader(path);
			string decryptedData = DecryptData(reader.ReadToEnd());
			writer.AutoFlush = true;
			writer.WriteLine(decryptedData);
			stream.Flush();
			stream.Position = 0;
			var serializer = new XmlSerializer(typeof(T));
			T result = (T) serializer.Deserialize(stream);
			reader.Close();
			writer.Close();
			return result;
		}

		public static void Save<T>(string path, object value)
		{
			var serializer = new XmlSerializer(typeof(T));
			using (var stream = new MemoryStream())
			{
				serializer.Serialize(stream, value);
				stream.Flush();
				stream.Position = 0;
				string sr = new StreamReader(stream).ReadToEnd();
				var fileStream = new FileStream(path, FileMode.Create);
				var streamWriter = new StreamWriter(fileStream);
				streamWriter.WriteLine(EncryptData(sr));
				streamWriter.Close();
				fileStream.Close();
			}
		}

		private static string EncryptData(string toEncrypt)
		{
			byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt); 
			RijndaelManaged rDel = CreateRijndaelManaged();
			ICryptoTransform cTransform = rDel.CreateEncryptor(); 
			byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length); 
			return Convert.ToBase64String(resultArray, 0, resultArray.Length); 
		}

		private static string DecryptData(string toDecrypt)
		{ 
			byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
			RijndaelManaged rDel = CreateRijndaelManaged();
			ICryptoTransform cTransform = rDel.CreateDecryptor(); 
			byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length); 
			return Encoding.UTF8.GetString(resultArray); 
		}

		private static RijndaelManaged CreateRijndaelManaged()
		{
			byte[] keyArray = Encoding.UTF8.GetBytes(PrivateKey); 
			var result = new RijndaelManaged();

			var newKeysArray = new byte[16];
			Array.Copy(keyArray, 0, newKeysArray, 0, 16);

			result.Key = newKeysArray;
			result.Mode = CipherMode.ECB;
			result.Padding = PaddingMode.PKCS7;
			return result;
		}
	}
}