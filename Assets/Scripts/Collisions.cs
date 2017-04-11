using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Collisions : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionEnter (Collision collision)
	{
		if (this.gameObject.name == "Land") {
			if (collision.gameObject.tag == "navette") {
				if (collision.gameObject.GetComponent<Rigidbody> ().velocity.y > -2 && collision.gameObject.GetComponent<Rigidbody> ().velocity.y <= 0 && collision.gameObject.transform.position.y > this.gameObject.transform.position.y) {
					print ("you win :)");
					GameObject Info = GameObject.FindGameObjectWithTag ("CHealth");
					Text info = Info.GetComponent<Text> ();
					info.text = "Fuel";
					
					foreach (string xx in XMLParser.nUniqueIDs.Keys) {
						foreach (string xxx in XMLParser.nUniqueIDs[xx].Keys) {
							int i = (int)XMLParser.nUniqueIDs [xx] [xxx];
							for (int j=1; j<=i; j++) {
								string Type = (string)XMLParser.nComponents [xx + "_" + xxx + "_" + j] ["Type"];
								string Name = (string)XMLParser.nComponents [xx + "_" + xxx + "_" + j] ["Name"];
								string ID = Type + "_" + Name + "_" + j;
								if (Type != "Fuel" && Type != "Missile") {
									info.text += "\n" + XMLParser.nComponents [ID] ["Type"] + " " + XMLParser.nComponents [ID] ["Name"] + " : " + XMLParser.nComponents [ID] ["Health"];
								}
								if (Type == "Missile") {
									info.text += "\n" + XMLParser.nComponents [ID] ["Type"] + " " + XMLParser.nComponents [ID] ["Name"] + " : " + XMLParser.nComponents [ID] ["Limit"];
								}
								
							}
							
						}
					}
					XMLParser.saveNComponents ();
					GameObject rt = GameObject.FindGameObjectWithTag ("ResultText");
					GameObject rb = GameObject.FindGameObjectWithTag ("ResultButton");
					CanvasGroup cvrt = rt.GetComponent<CanvasGroup> ();
					CanvasGroup cvrb = rb.GetComponent<CanvasGroup> ();
					Text rtt = rt.GetComponent<Text> ();
					rtt.text = "You Won :) !!";
					cvrt.alpha = 1;
					cvrt.interactable = true;
					cvrt.blocksRaycasts = true;
					cvrb.alpha = 1;
					cvrb.interactable = true;
					cvrb.blocksRaycasts = true;
					XMLParser.nUnload ();
					Time.timeScale = 0;
					//Application.LoadLevel("MainMenu");

				} else {
					print ("too fast or wrong direction !!!");
				}
			}
		} else if (this.gameObject.tag == "ennemiFire") {
			if(collision.gameObject.tag != "ennemi"){
			    Destroy (this.gameObject);
			}
		} else if (this.gameObject.name.Contains ("Missile")) {
			if(collision.gameObject.tag != "navette"){
				if(collision.gameObject.tag != "Land"){
			        Destroy(this.gameObject);
					Destroy(collision.gameObject);
				}
			}
		}
		else {
			print (this.gameObject.name);
			if (!this.gameObject.name.Contains("Missile") && this.gameObject.tag != "ennemiFire") {
				string cType = (string)XMLParser.nComponents [this.gameObject.name] ["Type"];
				string cName = (string)XMLParser.nComponents [this.gameObject.name] ["Name"];
				int IDs = (int)XMLParser.nComponents [this.gameObject.name] ["ID"];
				float newHealth = float.Parse ((string)XMLParser.nComponents [this.gameObject.name] ["Health"]);
				newHealth -= 0.2f;
				XMLParser.nComponents [this.gameObject.name] ["Health"] = newHealth + "";
				if(newHealth <= 0){
					Destroy(this.gameObject);
				}
				GameObject Info = GameObject.FindGameObjectWithTag ("CHealth");
				Text info = Info.GetComponent<Text> ();
				info.text = "Fuel";
			
				foreach (string xx in XMLParser.nUniqueIDs.Keys) {
					foreach (string xxx in XMLParser.nUniqueIDs[xx].Keys) {
						int i = (int)XMLParser.nUniqueIDs [xx] [xxx];
						for (int j=1; j<=i; j++) {
							string Type = (string)XMLParser.nComponents [xx + "_" + xxx + "_" + j] ["Type"];
							string Name = (string)XMLParser.nComponents [xx + "_" + xxx + "_" + j] ["Name"];
							string ID = Type + "_" + Name + "_" + j;
							if (Type != "Fuel" && Type != "Missile") {
								//info.text += "\n"+XMLParser.nComponents[ID]["Type"]+" "+XMLParser.nComponents[ID]["Name"]+" : "+XMLParser.nComponents[ID]["Health"];
							}
							if (Type == "Missile") {
								//info.text += "\n" + XMLParser.nComponents [ID] ["Type"] + " " + XMLParser.nComponents [ID] ["Name"] + " : " + XMLParser.nComponents [ID] ["Limit"];
							}
						
						}
					
					}
				}
			}
		}
		/*SpriteRenderer nmsr = this.gameObject.GetComponent<SpriteRenderer> ();

		Color nmsrc = new Color (nmsr.color.r+0.1f, 0f, 0f);
		nmsr.color = nmsrc;
		//Destroy (this.gameObject);*/
	}
}
