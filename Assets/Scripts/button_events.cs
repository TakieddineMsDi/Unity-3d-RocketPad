using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class button_events : MonoBehaviour {
	public GameObject panel_repairMessage;
	public GameObject panel_checkToSave;
	
	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {
		
	}
	public void RrepairMessage_Disable() {
		
		panel_repairMessage.GetComponent<CanvasGroup>().alpha = 0;
		panel_repairMessage.GetComponent<CanvasGroup>().interactable = false;
		panel_repairMessage.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
	public void goToScene(string name) {
		
		Application.LoadLevel (name);
	}
	
	public void CheckToSave_Enable() {
		
		panel_checkToSave.GetComponent<CanvasGroup>().alpha = 1;
		panel_checkToSave.GetComponent<CanvasGroup>().interactable = true;
		panel_checkToSave.GetComponent<CanvasGroup>().blocksRaycasts = true;
	}
	
	public void CheckToSave_Disable() {
		
		panel_checkToSave.GetComponent<CanvasGroup>().alpha = 0;
		panel_checkToSave.GetComponent<CanvasGroup>().interactable = false;
		panel_checkToSave.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
	
	public void CheckToSave_Yes() {
		GameObject.Find ("Garage").GetComponent<garage> ().garage_build ();
		panel_checkToSave.transform.GetChild(0).GetComponent<Text>().text = "Good! now you can leave.";
		
		panel_checkToSave.transform.GetChild(1).GetComponent<CanvasGroup>().alpha = 1;
		panel_checkToSave.transform.GetChild(1).GetComponent<CanvasGroup>().interactable = true;
		panel_checkToSave.transform.GetChild(1).GetComponent<CanvasGroup>().blocksRaycasts = true;
		
		panel_checkToSave.transform.GetChild(2).GetComponent<CanvasGroup>().alpha = 0;
		panel_checkToSave.transform.GetChild(2).GetComponent<CanvasGroup>().interactable = false;
		panel_checkToSave.transform.GetChild(2).GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
	
}