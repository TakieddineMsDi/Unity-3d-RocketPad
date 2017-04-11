using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MissileFire : Collisions
{

	public bool Follow = false;
	float timeRemaining = 0;
	float collide = 1;
	bool collidet = false;
	int i = 0;
	public GameObject navette;
	public Canvas ngMain;
	// Use this for initialization
	void Start ()
	{
		navette = GameObject.FindGameObjectWithTag ("nPlayer");
		ngMain = (Canvas)GameObject.FindObjectOfType<Canvas> ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!Follow) {
			collide -= Time.deltaTime;
			if (collide > 0) {
				if(collidet == true){
				collide = 1;
				}
			} else {
				if(collidet == false){
					collidet = true;
					this.gameObject.AddComponent<BoxCollider> ();
					collide = 1;
				}
			}

			/*Vector3 nPos = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0f);
			float nY;
			nY = nPos.y + 0.1f;
			nPos.y = nY;
			this.gameObject.transform.position = nPos;*/
			gameObject.GetComponent<Rigidbody> ().AddRelativeForce(new Vector3(0f,20f,0f));
			SpriteRenderer nmsr = this.gameObject.GetComponent<SpriteRenderer> ();
			Color nmsrc = new Color (1f, 0f, 0f);
			nmsr.color = nmsrc;
		} else {
			if(this.gameObject.tag == "ennemi"){
				//this.gameObject.transform.LookAt(Navette.Corp.transform);
				Vector3 worldCorpPosition = Navette.Corp.transform.position;
				// Get the direction
				Vector3 direction = worldCorpPosition - gameObject.transform.position;
				// Calculate the angle for that direction
				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
				// Angle the missile towards the pos finally
			    //direction.z=0;
				//direction.y=0;
				//direction.x=0;
				//Quaternion rotation = Quaternion.LookRotation(direction);
				//Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime*0.2f);//rotation;//
				gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			}
			timeRemaining -= Time.deltaTime;
			if (timeRemaining > 0) {
			} else {
				i++;
				timeRemaining = 2;
				GameObject nComponent = (GameObject)Instantiate ((GameObject)Resources.Load ("Prefabs/cartouche"), new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0), transform.rotation);
				nComponent.gameObject.name = "Follow" + i;
				nComponent.name = "Follow" + i;
				nComponent.tag = "ennemiFire";
				nComponent.AddComponent<BoxCollider>();
				nComponent.AddComponent<Rigidbody>();
				Rigidbody rbf = nComponent.GetComponent<Rigidbody>();
				rbf.useGravity = false;
				rbf.constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;

				nComponent.AddComponent<MissileFollow> ();
			}

		}
	}
}
