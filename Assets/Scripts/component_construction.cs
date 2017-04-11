using UnityEngine;
using System.Collections;

public class component_construction : MonoBehaviour {
	

	private Vector3 mousePosition;
	private bool isClicked;

	[HideInInspector]
	public GameObject container_box;

	public GameObject garage;


	public Vector2 startPosition;
	public string type_of_component;
	public string name_of_component;
	public string health_of_component;
	public string limit_of_component;
	public string force_of_component;
	public string speed_of_component;


	void Start () {
		isClicked = true; //false
		startPosition = gameObject.transform.position;
	}


	void Update () {
		//if (Input.GetMouseButton(0) && isClicked) {
		if (Input.GetMouseButton(0) && isClicked) {

			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector2 (mousePosition.x, mousePosition.y);
		}
	}


	void OnMouseDown()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

		if (hit.collider.name != null) {
			isClicked = true;
			if ( (container_box != null) && (transform.position.x != startPosition.x) ) {
				container_box.GetComponent<BoxCollider2D>().enabled = true;
			}
		}
	
	}



	void OnMouseUp()
	{
		print ("up"+container_box);
		isClicked = false;

		garage.GetComponent<garage> ().garage_fill (gameObject, container_box, container_box.GetComponent<box>().line, container_box.GetComponent<box>().column);

		print ("condition: " + garage.GetComponent<garage> ().garage_diagnostic () +" : "+ type_of_component);
		if( garage.GetComponent<garage> ().garage_diagnostic () == "error" && type_of_component != "Corp"){

			gameObject.transform.position = startPosition;

			garage.GetComponent<garage> ().garage_mat[container_box.GetComponent<box>().line, container_box.GetComponent<box>().column] = null;
		    container_box.gameObject.GetComponent<box>().isEmpty = true;
			container_box.GetComponent<BoxCollider2D>().enabled = true;
		}

		garage.GetComponent<garage> ().garage_display ();
	}



	void OnTriggerEnter2D (Collider2D other) {

		if(other.gameObject.name=="trash"){
			Destroy(gameObject);
		}else{
			if( other.gameObject.GetComponent<box>().isEmpty){
				container_box = other.gameObject;
			}
		}

	}


	void OnTriggerExit2D (Collider2D other) {
		if( container_box != null ){

			if( other.gameObject.name == container_box.name ){
				garage.GetComponent<garage> ().garage_mat[container_box.GetComponent<box>().line, container_box.GetComponent<box>().column] = null;
				other.gameObject.GetComponent<box>().isEmpty = true;
			}
		}
	}

}
