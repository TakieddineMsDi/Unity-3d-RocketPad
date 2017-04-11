using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Moteur : Collisions
{
	private float minus = 0f;
	private Vector3 ForwardVelocity = new Vector3 (0f, 10f,0f);
	private Vector3 BackwardVelocity = new Vector3 (0f, -10f,0f);
	private Vector3 RightVelocity = new Vector3 (10f, 0f,0f);
	private Vector3 LeftVelocity = new Vector3 (-10f, 0f,0f);
	public GameObject navette;
	public Canvas ngMain;
	string mForce = "";
	string mKey = "";
	bool eF = false;
	bool moving = false;
	Color Last = new Color();

	// Use this for initialization
	void Start ()
	{
		mForce = (string)XMLParser.getNComponents () [this.name] ["Force"];
		if (mKey == "") {
			if (mForce == "Forward") {
				mKey = "z";
			} else if (mForce == "Backward") {
				mKey = "s";
			} else if (mForce == "Right") {
				mKey = "d";
			} else if (mForce == "Left") {
				mKey = "q";
			}
		}
		navette = GameObject.FindGameObjectWithTag ("nPlayer");
		ngMain = (Canvas)GameObject.FindObjectOfType<Canvas> ();

		/*GameObject go = (GameObject)Instantiate ((GameObject)Resources.Load ("Prefabs/MButton"), new Vector3 (0f, -240f, 0f), transform.rotation);
		go.transform.SetParent (ngMain.transform);
		go.transform.position = new Vector3 (0f, -240f, 0f);
		go.transform.localScale = new Vector3 (1f, 1f, 1f);
		go.name = this.name + "_Control";
		Button gob = go.GetComponent<Button> ();

		gob.GetComponentInChildren<Text> ().text = (string)XMLParser.getNComponents()[this.name]["Force"]+"("+(string)XMLParser.getNComponents()[this.name]["Name"]+")";

		gob.onClick.AddListener(() =>move ());
*/
	}

	void move ()
	{
		GameObject Fuel = GameObject.FindGameObjectWithTag("Fuel");
		if (Fuel.GetComponent<Slider> ().value > 0) {
			if (Input.GetKey (mKey)) {

				if (minus > 1) {
					Fuel.GetComponent<Slider> ().value -= 1;
					if(Fuel.GetComponent<Slider>().value == 0){
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
										info.text += "\n"+XMLParser.nComponents[ID]["Type"]+" "+XMLParser.nComponents[ID]["Name"]+" : "+XMLParser.nComponents[ID]["Health"];
									}
									if (Type == "Missile") {
										info.text += "\n" + XMLParser.nComponents [ID] ["Type"] + " " + XMLParser.nComponents [ID] ["Name"] + " : " + XMLParser.nComponents [ID] ["Limit"];
									}
									
								}
								
							}
						}
						XMLParser.saveNComponents();
						GameObject rt = GameObject.FindGameObjectWithTag("ResultText");
						GameObject rb = GameObject.FindGameObjectWithTag("ResultButton");
						CanvasGroup cvrt = rt.GetComponent<CanvasGroup>();
						CanvasGroup cvrb = rb.GetComponent<CanvasGroup>();
						Text rtt = rt.GetComponent<Text>();
						rtt.text = "You Lost :( !!";
						cvrt.alpha = 1;
						cvrt.interactable = true;
						cvrt.blocksRaycasts = true;
						cvrb.alpha = 1;
						cvrb.interactable = true;
						cvrb.blocksRaycasts = true;
						Time.timeScale = 0;
					}
					//XMLParser.nComponents["Fuel_Fuel_1"]["Limit"] = Fuel.GetComponent<Slider> ().value;
					minus = 0f;
				} else {
					minus += 0.01f;
				}
				if (mKey == "z") {
					//ForwardVelocity.x = navette.GetComponent<Rigidbody2D> ().velocity.x;
					gameObject.GetComponent<Rigidbody> ().AddRelativeForce (ForwardVelocity);
					//gameObject.GetComponent<Rigidbody> ().velocity += ForwardVelocity;
				} else if (mKey == "s") {
					//BackwardVelocity.x = navette.GetComponent<Rigidbody2D> ().velocity.x;
					gameObject.GetComponent<Rigidbody> ().AddRelativeForce (BackwardVelocity);
				} else if (mKey == "d") {
					//RightVelocity.y = navette.GetComponent<Rigidbody2D> ().velocity.y;
					gameObject.GetComponent<Rigidbody> ().AddRelativeForce (RightVelocity);
				} else if (mKey == "q") {
					//LeftVelocity.y = navette.GetComponent<Rigidbody2D> ().velocity.y;
					gameObject.GetComponent<Rigidbody> ().AddRelativeForce (LeftVelocity);
				}
				if (!moving) {
					moving = true;
					/*GameObject ctgo = (GameObject)Instantiate ((GameObject)Resources.Load ("Prefabs/EmptyGO"), new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0), transform.rotation);
				ctgo.name = this.gameObject.name + "_Info";
				ctgo.transform.SetParent (navette.transform);
				ctgo.AddComponent<TextMesh> ();
				TextMesh cText = (TextMesh)ctgo.GetComponent<TextMesh> ();
				cText.text = "Pushing";
				cText.fontSize = 19;
				cText.characterSize = 0.1f;*/
					SpriteRenderer nmsr = this.gameObject.GetComponent<SpriteRenderer> ();
					Last = nmsr.color;
					Color nmsrc = new Color (1f, 0f, 0f);
					nmsr.color = nmsrc;
				}
			}
			if (Input.GetKeyUp (mKey)) {
				if (moving) {
					moving = false;
					/*GameObject ctgo = GameObject.Find (this.gameObject.name + "_Info");
				TextMesh cText = (TextMesh)ctgo.GetComponent<TextMesh> ();
				Destroy (cText);
				Destroy (ctgo.gameObject);*/
					SpriteRenderer nmsr = this.gameObject.GetComponent<SpriteRenderer> ();
					nmsr.color = Last;
				}
			}
		} else {
			if (moving) {
				moving = false;
				/*GameObject ctgo = GameObject.Find (this.gameObject.name + "_Info");
				TextMesh cText = (TextMesh)ctgo.GetComponent<TextMesh> ();
				Destroy (cText);
				Destroy (ctgo.gameObject);*/
				SpriteRenderer nmsr = this.gameObject.GetComponent<SpriteRenderer> ();
				nmsr.color = Last;
			}
		}
	}
	// Update is called once per frame
	void Update ()
	{
		move ();
	}
}
