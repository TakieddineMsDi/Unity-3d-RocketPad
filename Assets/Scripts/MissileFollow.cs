using UnityEngine;
using System.Collections;

public class MissileFollow : Collisions {

	public GameObject navette;
	public Canvas ngMain;
	// Use this for initialization
	void Start () {
		navette = GameObject.FindGameObjectWithTag ("nPlayer");
		ngMain = (Canvas)GameObject.FindObjectOfType<Canvas> ();
	}

	// Update is called once per frame
	void Update () {
		Vector3 nPos = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0f);
		float nX;
		float nY;
		this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, Navette.Corp.gameObject.transform.position, 0.1f);
		//this.gameObject.GetComponent<Rigidbody> ().AddRelativeForce(Vector3.Lerp(this.gameObject.transform.position, Navette.Corp.gameObject.transform.position, 0.1f));
		/*if(this.gameObject.transform.position.x > navette.gameObject.transform.position.x){
			nX = nPos.x - 0.1f;
			nPos.x = nX;
		}
		if(this.gameObject.transform.position.x < navette.gameObject.transform.position.x){
			nX = nPos.x + 0.1f;
			nPos.x = nX;
		}
		if(this.gameObject.transform.position.y > navette.gameObject.transform.position.y){
			nY = nPos.y - 0.1f;
			nPos.y = nY;
		}
		if(this.gameObject.transform.position.y < navette.gameObject.transform.position.y){
			nY = nPos.y + 0.1f;
			nPos.y = nY;
		}
		this.gameObject.transform.position = nPos;*/
	}
}
