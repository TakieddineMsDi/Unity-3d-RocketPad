using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Missile : Collisions
{

	public GameObject navette;
	public Canvas ngMain;

	void Start ()
	{
		navette = GameObject.FindGameObjectWithTag ("nPlayer");
		ngMain = (Canvas)GameObject.FindObjectOfType<Canvas> ();
	}

	void mFire ()
	{

		if (Input.GetKeyDown ("f")) {
			string ccType = (string)XMLParser.nComponents [this.gameObject.name] ["Type"];
			string ccName = (string)XMLParser.nComponents [this.gameObject.name] ["Name"];
			int IDss = (int)XMLParser.nComponents [this.gameObject.name] ["ID"];

			int Limit = int.Parse ((string)XMLParser.getNComponents () [this.gameObject.name] ["Limit"]);
			if (Limit > 0) {
				//this.transform.SetParent(ngMain.transform);
				//this.gameObject.transform.SetParent (ngMain.transform);
				Limit--;
				XMLParser.nComponents [this.gameObject.name] ["Limit"] = Limit + "";
				GameObject Info = GameObject.FindGameObjectWithTag("CHealth");
				Text info = Info.GetComponent<Text>();
				info.text = "Fuel";
				
				foreach(string xx in XMLParser.nUniqueIDs.Keys){
					foreach (string xxx in XMLParser.nUniqueIDs[xx].Keys){
						int i = (int)XMLParser.nUniqueIDs[xx][xxx];
						for(int j=1;j<=i;j++){
							string Type = (string)XMLParser.nComponents[xx+"_"+xxx+"_"+j]["Type"];
							string Name = (string)XMLParser.nComponents[xx+"_"+xxx+"_"+j]["Name"];
							string ID = Type+"_"+Name+"_"+j;
							//print (ID);
							if(Type != "Fuel" && Type != "Missile"){
								//info.text += "\n"+XMLParser.nComponents[ID]["Type"]+" "+XMLParser.nComponents[ID]["Name"]+" : "+XMLParser.nComponents[ID]["Health"];
							}
							if(ID == this.gameObject.name){
								info.text += "\n"+Type+" "+Name+" : "+XMLParser.nComponents [this.gameObject.name] ["Limit"];
							}
							
						}
						
					}
				}
				if (Limit > 0) {

					XMLParser.nComponents [this.gameObject.name] ["Limit"] = Limit + "";
				
					string cName = (string)XMLParser.getNComponents () [this.gameObject.name] ["Name"];
				
					string cType = (string)XMLParser.getNComponents () [this.gameObject.name] ["Type"];

					int iD = (int)XMLParser.getNComponents () [this.gameObject.name] ["ID"];

					string key = cType + "_" + cName + "_" + iD + "_" + Limit;

					GameObject nComponent = (GameObject)Instantiate ((GameObject)Resources.Load ("Prefabs/" + cName), new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0), transform.rotation);
					//nComponent.transform.SetParent (navette.transform);
					nComponent.gameObject.name = key;
					nComponent.name = key;
					nComponent.AddComponent<Missile>();
					nComponent.AddComponent<BoxCollider>();
					nComponent.AddComponent<FixedJoint>();
					Rigidbody ncrb = nComponent.GetComponent<Rigidbody>();
					ncrb.useGravity = false;

					FixedJoint ncdj2d = nComponent.GetComponent<FixedJoint>();
					ncdj2d.connectedBody = Navette.Corp.GetComponent<Rigidbody>();
					SortedDictionary<string,object> ncComponent = new SortedDictionary<String, object> ();
					foreach (string ao in XMLParser.nComponents [this.gameObject.name].Keys) {
						ncComponent.Add (ao, XMLParser.nComponents [this.gameObject.name] [ao]);
					}
					ncComponent ["GameObject"] = nComponent;
					ncComponent ["Limit"] = Limit + "";
					ncComponent ["ID"] = iD;
					XMLParser.getNComponents ().Add (key, ncComponent);

					Missile asM = (Missile)this.gameObject.GetComponent<Missile> ();
					Destroy (asM);
					BoxCollider pc2d = (BoxCollider)this.gameObject.GetComponent<BoxCollider> ();
					Destroy(pc2d);

					FixedJoint fj = (FixedJoint)this.gameObject.GetComponent<FixedJoint> ();
					Destroy(fj);
					Rigidbody rbc = (Rigidbody)this.gameObject.GetComponent<Rigidbody> ();
					rbc.freezeRotation = true;
					rbc.constraints = RigidbodyConstraints.FreezeRotationX;
					this.gameObject.AddComponent<MissileFire> ();
				}else{
					/*GameObject Info = GameObject.FindGameObjectWithTag("CHealth");
					Text info = Info.GetComponent<Text>();
					info.text = "Fuel";
					
					foreach(string xx in XMLParser.nUniqueIDs.Keys){
						foreach (string xxx in XMLParser.nUniqueIDs[xx].Keys){
							int i = (int)XMLParser.nUniqueIDs[xx][xxx];
							for(int j=1;j<=i;j++){
								string Type = (string)XMLParser.nComponents[xx+"_"+xxx+"_"+j]["Type"];
								string Name = (string)XMLParser.nComponents[xx+"_"+xxx+"_"+j]["Name"];
								string ID = Type+"_"+Name+"_"+j;
								print (ID);
								if(Type != "Fuel" && Type != "Missile"){
									//info.text += "\n"+XMLParser.nComponents[ID]["Type"]+" "+XMLParser.nComponents[ID]["Name"]+" : "+XMLParser.nComponents[ID]["Health"];
								}
								if(Type == "Missile"){
									info.text += "\n"+Type+" "+Name+" : "+XMLParser.nComponents[ID]["Limit"];
								}
								
							}
							
						}
					}*/
					Missile asM = (Missile)this.gameObject.GetComponent<MonoBehaviour> ();
					Destroy (asM);
					BoxCollider pc2d = (BoxCollider)this.gameObject.GetComponent<BoxCollider> ();
					Destroy(pc2d);
					Destroy(this.gameObject.GetComponent<FixedJoint>());
					this.gameObject.AddComponent<MissileFire> ();
				}

			}
		}

	}
	// Update is called once per frame
	void Update ()
	{
		mFire ();
	}
}
