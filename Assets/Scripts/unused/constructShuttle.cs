using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public class constructShuttle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		construction ();
	}
	
	// Update is called once per frame
	void construction () {
		XmlDocument xmlDoc = new XmlDocument();

		XmlNode rootNode = xmlDoc.CreateElement("Shuttle");
		xmlDoc.AppendChild(rootNode);


		XmlNode componentNode = xmlDoc.CreateElement("Component");
		rootNode.AppendChild(componentNode);

		XmlNode name = xmlDoc.CreateElement("Name");
		name.InnerText = "kio";
		componentNode.AppendChild(name);

		XmlNode type = xmlDoc.CreateElement("Type");
		componentNode.AppendChild(type);

		XmlNode posX = xmlDoc.CreateElement("PosX");
		componentNode.AppendChild(posX);

		XmlNode posY = xmlDoc.CreateElement("PosY");
		componentNode.AppendChild(posY);

		XmlNode posZ = xmlDoc.CreateElement("PosZ");
		componentNode.AppendChild(posZ);

	/*	XmlNode userNode;

		userNode = xmlDoc.CreateElement("component");
		XmlAttribute attribute = xmlDoc.CreateAttribute("name");
		attribute.Value = "xxx1";
		userNode.Attributes.Append(attribute);
		userNode.InnerText = "John Doe";
		rootNode.AppendChild(userNode);*/

		
		xmlDoc.Save("./Assets/Scripts/shuttle.xml");
	}


}
