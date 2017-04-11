using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine.UI;

public class XMLParser
{
	public static bool Loaded = false;
	public static SortedDictionary<string, SortedDictionary<string,object>> nComponents = new SortedDictionary<string, SortedDictionary<string,object>> ();
	public static SortedDictionary<string,SortedDictionary<string,int>> nUniqueIDs = new SortedDictionary<string,SortedDictionary<string,int>> ();
	XmlDocument xmlDoc = new XmlDocument ();

	public static SortedDictionary<string, SortedDictionary<string,object>> getNComponents ()
	{
		if (Loaded) {
			return nComponents;
		} else {
			XMLParser xmlParser = new XMLParser ();
			xmlParser.nReadXMLFile ();
			return nComponents;
		}
	}

	public static void saveNComponents ()
	{
		if (Loaded) {
			XMLParser xmlParser = new XMLParser ();
			xmlParser.nSaveXMLFile ();
		} else {
			XMLParser xmlParser = new XMLParser ();
			xmlParser.nReadXMLFile ();
			xmlParser.nSaveXMLFile ();
		}
	}

	public static SortedDictionary<string, SortedDictionary<string,int>> getNUniqueIDs ()
	{
		if (Loaded) {
			return nUniqueIDs;
		} else {
			XMLParser xmlParser = new XMLParser ();
			xmlParser.nReadXMLFile ();
			return nUniqueIDs;
		}
	}
	public static void nUnload (){
		Loaded = false;
		nComponents = null;
		nComponents = new SortedDictionary<String, SortedDictionary<String, object>> ();
		nUniqueIDs = null;
		nUniqueIDs = new SortedDictionary<String, SortedDictionary<String, int>> ();
	}
	public string nGetXMLPath ()
	{
		return Application.dataPath + "/Data/XML.xml";
	}

	XmlNodeList nLoadComponents ()
	{
		return xmlDoc.GetElementsByTagName ("Component");
	}

	public string nGenerateID (SortedDictionary<string,string> cArgs, out int iD)
	{
		string cType = (string)cArgs ["Type"];
		string cName = (string)cArgs ["Name"];
		SortedDictionary<string,int> nIDs = new SortedDictionary<String, int> ();
		int cOID = 1;
		if (nUniqueIDs.TryGetValue (cType, out nIDs)) {
			if (nUniqueIDs [cType].TryGetValue (cName, out cOID)) {
				nUniqueIDs [cType] [cName] += 1;
			} else {
				cOID = 1;
				nUniqueIDs [cType].Add (cName, cOID);
			}
		} else {
			nIDs = new SortedDictionary<String, int> ();
			nIDs.Add (cName, cOID);
			nUniqueIDs.Add (cType, nIDs);
		}
		iD = nUniqueIDs [cType] [cName];
		string generated = cType + "_" + cName + "_" + nUniqueIDs [cType] [cName];
		return generated;
	}

	public void nReadXMLFile ()
	{
		xmlDoc.Load (nGetXMLPath ());
		XmlNodeList cList = nLoadComponents ();

		foreach (XmlNode cComponentAttrs in cList) {
			SortedDictionary<string,object> nComponent = new SortedDictionary<String, object> ();

			SortedDictionary<string,string> cArgs = new SortedDictionary<String, string> ();

			XmlNodeList cComponentAttr = cComponentAttrs.ChildNodes;

			foreach (XmlNode cAttr in cComponentAttr) {
				cArgs.Add (cAttr.Name, cAttr.InnerText);
			}
			foreach (string cItem in cArgs.Keys) {
				string cValue;
				cArgs.TryGetValue (cItem, out cValue);
				nComponent.Add (cItem, cValue);
			}
			int iD;
			string gID = nGenerateID (cArgs, out iD);
			nComponent.Add ("ID", iD);
			Debug.Log(gID);
			nComponents.Add (gID, nComponent);

		}
		Loaded = true;
	}

	public void nSaveXMLFile ()
	{
		XmlNode xmlroot = xmlDoc.CreateElement ("Shuttle");
		foreach (string cType in nUniqueIDs.Keys) {
			foreach (string cName in nUniqueIDs[cType].Keys) {
				for (int i=1; i<=nUniqueIDs[cType][cName]; i++) {
					string ID = cType + "_" + cName + "_" + i;
					XmlNode ngComponent = xmlDoc.CreateElement ("Component");
					foreach (string key in nComponents[ID].Keys) {
						if (key != "ID" && key != "GameObject") {
							XmlNode nkey = xmlDoc.CreateElement (key);
							nkey.InnerText = (string)nComponents [ID] [key];
							ngComponent.AppendChild (nkey);
						}
					}
					xmlroot.AppendChild (ngComponent);
				}
			}
		}
		xmlDoc.AppendChild (xmlroot);
		xmlDoc.Save (nGetXMLPath());
	}
}
