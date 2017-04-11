using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine.UI;

public class Navette : MonoBehaviour
{
	public Canvas ngMain;
	public GameObject ngPlayer;
	public static SortedDictionary<string,bool> Motors = new SortedDictionary<String, bool> ();
	public static GameObject Corp;
	
	public void nPlaceComponents ()
	{
		string ccName = (string)XMLParser.getNComponents () ["Corp_body_1"] ["Name"];
		float ccX = float.Parse ((string)XMLParser.getNComponents () ["Corp_body_1"] ["X"]);
		float ccY = float.Parse ((string)XMLParser.getNComponents () ["Corp_body_1"] ["Y"]);
		Corp = (GameObject)Instantiate ((GameObject)Resources.Load ("Prefabs/" + ccName), new Vector3 (ccX, ccY, 0),transform.rotation);
		Corp.name = "Corp_body_1";
		Corp.AddComponent<BoxCollider>();
		Corp.AddComponent<Rigidbody>();
		Rigidbody nccrb =Corp.GetComponent<Rigidbody>();
		nccrb.useGravity = false;
		nccrb.constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;
		Corp.AddComponent<Collisions> ();
		GameObject Info = GameObject.FindGameObjectWithTag("CHealth");
		Text info = Info.GetComponent<Text>();
		info.text += "\n"+"Corp"+" "+"body"+" : "+(string)XMLParser.getNComponents () ["Corp_body_1"] ["Health"];
		XMLParser.getNComponents () ["Corp_body_1"].Add ("GameObject", Corp);
		
		foreach (string key in XMLParser.getNComponents().Keys) {
			string cType = (string)XMLParser.getNComponents () [key] ["Type"];
			if(cType != "Fuel" && cType != "Corp"){
				
				string cName = (string)XMLParser.getNComponents () [key] ["Name"];
				float cX = float.Parse ((string)XMLParser.getNComponents () [key] ["X"]);
				float cY = float.Parse ((string)XMLParser.getNComponents () [key] ["Y"]);
				GameObject Lgo = (GameObject)Resources.Load ("Prefabs/" + cName);
				GameObject nComponent = (GameObject)Instantiate (Lgo, new Vector3 (cX, cY, 0), Quaternion.identity);

				/*GameObject ctgo = (GameObject)Instantiate ((GameObject)Resources.Load ("Prefabs/EmptyGO"),transform.position, transform.rotation);
				ctgo.name = this.gameObject.name + "_Info";
				ctgo.transform.SetParent (ngMain.transform);
				ctgo.AddComponent<Text> ();
				Text cText = (Text)ctgo.GetComponent<Text> ();
				cText.text = "Pushing";
				cText.color = new Color(1f,0f,0f);
				cText.fontSize = 19;
				//cText.characterSize = 5f;
				ctgo.transform.position = GameObject.FindGameObjectWithTag("CHealth").transform.position;
				*/
				if(cType != "Missile"){
				//info.text += "\n"+cType+" "+cName+" : "+(string)XMLParser.getNComponents () [key] ["Health"];
				}else{
					info.text += "\n"+cType+" "+cName+" : "+(string)XMLParser.getNComponents () [key] ["Limit"];
				}
					//nComponent.transform.SetParent (this.transform);
				nComponent.name = key;
				nComponent.AddComponent<BoxCollider>();
				/*float x_21 = nComponent.gameObject.transform.position.x - Corp.transform.position.x;
				float y_21 = nComponent.gameObject.transform.position.y - Corp.transform.position.y;
				float z_21 = nComponent.gameObject.transform.position.z - Corp.transform.position.z;*/
				// norme le plus petit chaque point du ply avec tous les point du sphere (source)
				//float norme = Mathf.Sqrt((x_21 * x_21) + (y_21 * y_21) +(z_21 * z_21));
				nComponent.AddComponent<FixedJoint>();
				Rigidbody ncrb = nComponent.GetComponent<Rigidbody>();
				ncrb.useGravity = false;
				ncrb.constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;

				/*ncrb.freezeRotation = true;
				ncrb.constraints = RigidbodyConstraints.FreezeRotationX;*/
				FixedJoint ncdj2d = nComponent.GetComponent<FixedJoint>();
				ncdj2d.connectedBody = Corp.GetComponent<Rigidbody>();
				//ncdj2d.distance = norme;
				//ncdj2d.connectedAnchor = new Vector2(2f,3f);
				if (cType == "Missile") {
					nComponent.AddComponent<Missile> ();
				}
				else if (cType == "Motor") {
					//nComponent.transform.Rotate ( Lgo.transform.eulerAngles.x,  Lgo.transform.eulerAngles.y,  Lgo.transform.eulerAngles.z);
					nComponent.AddComponent<Moteur> ();
					string cForce = (string)XMLParser.getNComponents () [key] ["Force"];
					bool e = true;
					if (!Motors.TryGetValue (cForce, out e)) {
						Motors [cForce] = e;
					}
				}else{
					nComponent.AddComponent<Collisions> ();
				}
				XMLParser.getNComponents () [key].Add ("GameObject", nComponent);
			}
		}
		this.gameObject.AddComponent<Rigidbody> ();
		Rigidbody crg = this.gameObject.GetComponent<Rigidbody> ();
		crg.useGravity = false;
		//this.gameObject.AddComponent<PolygonCollider2D> ();
		
	}
	
	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1;
		ngMain = (Canvas)GameObject.FindObjectOfType<Canvas> ();
		ngMain.GetComponentInChildren<RectTransform> ().transform.position = Vector3.zero;
		nPlaceComponents ();
	}
	
	void nmIntel ()
	{
		bool nmE = false;
		if (Input.GetKey ("z")) {
			if (!Motors.TryGetValue ("Forward", out nmE)) {
				print ("you don't have motors to move Forward");
			}
		}
		if (Input.GetKey ("s")) {
			if (!Motors.TryGetValue ("Backward", out nmE)) {
				print ("you don't have motors to move Backward");
			}
		}
		if (Input.GetKey ("d")) {
			if (!Motors.TryGetValue ("Right", out nmE)) {
				print ("you don't have motors to move Right");
			}
		}
		if (Input.GetKey ("q")) {
			if (!Motors.TryGetValue ("Left", out nmE)) {
				print ("you don't have motors to move Left");
			}
		}
	}
	// Update is called once per frame
	void Update ()
	{
		nmIntel ();
		//print (this.gameObject.GetComponent<Rigidbody2D> ().velocity.y);
	}
}
