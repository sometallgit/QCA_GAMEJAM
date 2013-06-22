using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SandCreator : MonoBehaviour 
{
	//This creates and deletes sand particles
	
	//Public Variables
	public float AddSpeed; //The time in seconds between the creation of particles
	public GameObject SandPrefab;
	
	//Private Variables
	private List<GameObject> sand = new List<GameObject>(); //The sand
	private float mouse1Axis; //LMB
	private float mouse2Axis; //RMB
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		getInput();
		addSand();
		removeSand();
	}
	
	//Grab the status of any input devices
	void getInput()
	{
		//LMB - Add sand
		mouse1Axis = Input.GetAxis("Fire1");
		//RMB - Remove sand
		mouse2Axis = Input.GetAxis("Fire2");

	}
	
	//If the LMB is pressed, add sand to the list
	void addSand()
	{
		if (mouse1Axis > 0)
		{
			//Grab the mouse pos to get the pos to add the sand particle on the screen
			//Convert screen coordinates to world space
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			//Create a temporary new particle that will be fed into the List
			GameObject particle = (GameObject)Instantiate(SandPrefab, new Vector3(ray.origin.x, ray.origin.y, 0), new Quaternion());
			
			//Set the newly created object's parent to the empty snake gameobject
			GameObject parent = GameObject.Find("SandParent");
			particle.transform.parent = parent.transform;
			
			//Grow the list with a new sand prefab
			sand.Add(particle);
			//Add the sand prefab into the sand parent
			
			
			
			
		}
	}
	
	void removeSand()
	{
		
	}
}
