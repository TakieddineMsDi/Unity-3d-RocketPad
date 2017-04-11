using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections;
using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public class garage : MonoBehaviour {


	public int _line;
	public int _column;
	public static int line;
	public static int column;

	[HideInInspector]
	public GameObject[,] garage_mat;

	private float vertical;
	public float verticalStep;

	private float horizontalSave;
	private float horizontal;
	public float horizontalStep;

	public GameObject box_background;
	public Transform spawnerPosition;

	public Transform shuttle;
	public Text money;

	public GameObject panel_RepearMessage;


	// Use this for initialization
	void Start () {
		horizontalSave = spawnerPosition.position.x;
		horizontal = spawnerPosition.position.x;
		vertical = spawnerPosition.position.y;
		shuttle = GameObject.Find ("Shuttle").transform;

		money.text = Player.money.ToString()+"dt";

		garage_init ();
	}



	
	// Update is called once per frame
	void Update () {
	
	}


	void garage_init() {

		line = _line;
		column = _column;
		garage_mat = new GameObject[line, column];

		for(int i=0; i<line; i++){
			for(int j=0; j<column; j++){
				garage_mat[i,j]=null;
				GameObject box = Instantiate(box_background, spawnerPosition.position = new Vector2(horizontal, vertical), spawnerPosition.rotation) as GameObject;

				box.GetComponent<box>().line = i;
				box.GetComponent<box>().column = j;
				box.GetComponent<box>().isEmpty = true;

				horizontal += horizontalStep;
			}
			horizontal = horizontalSave;
			vertical -= verticalStep;
		}
		
	}
	
	public void garage_repear(){
		
		if(Player.money>=100){
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.Load ( Application.dataPath + "/Data/XML.xml" );
			
			XmlNodeList cList = xmlDoc.GetElementsByTagName ("Component");
			
			foreach (XmlNode cComponentAttrs in cList) {
				
				XmlNodeList cComponentAttr = cComponentAttrs.ChildNodes;
				
				foreach (XmlNode cAttr in cComponentAttr) {
					if(cAttr.Name == "Health"){
						cAttr.InnerText = "100"; 
					}
				}
			}
			
			Player.money -= 100;
			money.text = Player.money.ToString()+"dt";
			
			xmlDoc.Save(Application.dataPath+"/Data/XML.xml");
		}else{
			
			panel_RepearMessage.GetComponent<CanvasGroup>().alpha = 1;
			panel_RepearMessage.GetComponent<CanvasGroup>().interactable = true;
			panel_RepearMessage.GetComponent<CanvasGroup>().blocksRaycasts = true;
		}
	}
	public void garage_fill(GameObject component, GameObject container_box, int line, int column) {

		print ("container box, empty: "+container_box.GetComponent<box> ().isEmpty);
		if( container_box.GetComponent<box> ().isEmpty ){

			component.transform.position = container_box.transform.position;
			garage_mat[line,column]=component.gameObject;

			container_box.GetComponent<box> ().isEmpty = false;
			container_box.GetComponent<BoxCollider2D>().enabled = false;
		}
	
	}


	public void garage_display() {
		
		for(int i=0; i<line; i++){
			for(int j=0; j<column; j++){
				if( garage_mat[i,j] != null ){
					print (garage_mat[i,j].GetComponent<component_construction> ().container_box.GetComponent<box> ().isEmpty+":"+i+" : "+j);
				}else{	

				}
			}
		}
	}




	public string garage_diagnostic() {
		int number_Of_Component = 0;

		string[] failures_report = new string[4] {"not equipped", "not equipped", "not equipped", "not equipped"};

		for(int i=0; i<line; i++){
			for(int j=0; j<column; j++){

				if( garage_mat[i,j] != null ){
					failures_report = new string[4] {"not equipped", "not equipped", "not equipped", "not equipped"};

					number_Of_Component ++;

						
						if (j>0){
							if (garage_mat[i,j-1]!=null){
								failures_report[0] = "equipped";
							}
						}

						
						if (i>0){
							if (garage_mat[i-1,j]!=null){
								failures_report[1] = "equipped";
							}
						}


						if (j<column-1){
							if (garage_mat[i,j+1]!=null){
								failures_report[2] = "equipped";
							}
						}

						
						if (i<line-1){
							if (garage_mat[i+1,j]!=null){
								failures_report[3] = "equipped";
							}
						}


						if( failures_report[0] == "not equipped" && failures_report[1] == "not equipped" && failures_report[2] == "not equipped" && failures_report[3] == "not equipped" ){
							return "error";
						}

				
				}
				
			}
		}

		print ("number_Of_Component: "+number_Of_Component);
		if( number_Of_Component==1 ){
			return "The body is not enough to fly";
		}

		return null;
	
	}
	

	public void garage_build(){

		XmlDocument xmlDoc = new XmlDocument();
		
		XmlNode rootNode = xmlDoc.CreateElement("Shuttle");
		xmlDoc.AppendChild(rootNode);
		XmlNode userNode;

		foreach(Transform componentShuttle in shuttle){
	
			XmlNode componentNode = xmlDoc.CreateElement("Component");
			rootNode.AppendChild(componentNode);
			
			/*XmlNode name = xmlDoc.CreateElement("Name");
			name.InnerText = componentShuttle.name;
			componentNode.AppendChild(name);
*/



			if(componentShuttle.GetComponent<component_construction>().name_of_component != ""){
				XmlNode type = xmlDoc.CreateElement("Name");
				type.InnerText = componentShuttle.GetComponent<component_construction>().name_of_component;
				componentNode.AppendChild(type);
			}


			if(componentShuttle.GetComponent<component_construction>().type_of_component != ""){
				XmlNode type = xmlDoc.CreateElement("Type");
				type.InnerText = componentShuttle.GetComponent<component_construction>().type_of_component;
				componentNode.AppendChild(type);
			}


			if(componentShuttle.GetComponent<component_construction>().health_of_component != ""){
				XmlNode type = xmlDoc.CreateElement("Health");
				type.InnerText = componentShuttle.GetComponent<component_construction>().health_of_component;
				componentNode.AppendChild(type);
			}

			if(componentShuttle.GetComponent<component_construction>().limit_of_component != ""){
				XmlNode type = xmlDoc.CreateElement("Limit");
				type.InnerText = componentShuttle.GetComponent<component_construction>().limit_of_component;
				componentNode.AppendChild(type);
			}
			
			
			if(componentShuttle.GetComponent<component_construction>().force_of_component != ""){
				XmlNode type = xmlDoc.CreateElement("Force");
				type.InnerText = componentShuttle.GetComponent<component_construction>().force_of_component;
				componentNode.AppendChild(type);
			}

			if(componentShuttle.GetComponent<component_construction>().speed_of_component != ""){
				XmlNode type = xmlDoc.CreateElement("Speed");
				type.InnerText = componentShuttle.GetComponent<component_construction>().speed_of_component;
				componentNode.AppendChild(type);
			}
			



			
			XmlNode posX = xmlDoc.CreateElement("X");
			posX.InnerText = componentShuttle.transform.position.x.ToString();
			componentNode.AppendChild(posX);
			
			XmlNode posY = xmlDoc.CreateElement("Y");
			posY.InnerText = componentShuttle.transform.position.y.ToString();
			componentNode.AppendChild(posY);
			
			XmlNode posZ = xmlDoc.CreateElement("Z");
			posZ.InnerText = componentShuttle.transform.position.z.ToString();
			componentNode.AppendChild(posZ);
		}	
		
		xmlDoc.Save(Application.dataPath+"/Data/XML.xml");

	}


}
