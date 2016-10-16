using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Serialization;

public static class RSACryptoServiceProviderExtensions
{
	public static void FromXmlString(this RSACryptoServiceProvider rsa, string xmlString)
	{
		RSAParameters parameters = new RSAParameters();

		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(xmlString);

		if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
		{
			foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
			{
				switch (node.Name)
				{
					case "Modulus": parameters.Modulus = Convert.FromBase64String(node.InnerText); break;
					case "Exponent": parameters.Exponent = Convert.FromBase64String(node.InnerText); break;
					case "P": parameters.P = Convert.FromBase64String(node.InnerText); break;
					case "Q": parameters.Q = Convert.FromBase64String(node.InnerText); break;
					case "DP": parameters.DP = Convert.FromBase64String(node.InnerText); break;
					case "DQ": parameters.DQ = Convert.FromBase64String(node.InnerText); break;
					case "InverseQ": parameters.InverseQ = Convert.FromBase64String(node.InnerText); break;
					case "D": parameters.D = Convert.FromBase64String(node.InnerText); break;
				}
			}
		}
		else
		{
			throw new Exception("Invalid XML RSA key.");
		}

		rsa.ImportParameters(parameters);
	}

	public static string ToXmlString(this RSACryptoServiceProvider rsa, bool includePrivateParameters)
	{
		var parameters = rsa.ExportParameters(includePrivateParameters);
		var serializer = new XmlSerializer(parameters.GetType());
		var stringWritter = new StringWriter();
		serializer.Serialize(stringWritter, parameters);
		return stringWritter.ToString();
	}
}