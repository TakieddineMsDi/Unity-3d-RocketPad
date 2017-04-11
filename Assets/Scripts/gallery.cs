using UnityEngine;
using System.Collections;

public class gallery : MonoBehaviour {

	private Vector3 mousePosition;
	private Transform shuttle;
	// Use this for initialization
	void Start () {
		shuttle = GameObject.Find ("Shuttle").transform;
	}
	
	// Update is called once per frame
	public void getComponent () {

	}


	void OnMouseDown()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

		if (hit.collider.name != null) {
			GameObject component = Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			component.name = gameObject.name;
			component.GetComponent<component_construction>().enabled = true;
			//component.GetComponent<BoxCollider2D>().enabled = true;

			Destroy(component.GetComponent<gallery>());


			component.transform.SetParent(shuttle);
		}
	}



}
