using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class SandCreator : MonoBehaviour 
{
	//This creates and deletes sand particles
	
	//Public Variables
	public float AddSpeed; //The time in seconds between the creation of particles
	public GameObject SandPrefab;
	GameObject PARENT;
	GameObject sPrefab;

	private int sandCap = 5000;
	private int sandCurr = 0;
	
	//Private Variables
//	private List<GameObject> sand = new List<GameObject>(); //The sand
	private float mouse1Axis; //LMB
	private float mouse2Axis; //RMB
	
	// Use this for initialization
	void Start () 
	{
		PARENT = GameObject.Find("SandParent");
		sPrefab = Instantiate(SandPrefab) as GameObject;
		sPrefab.name = SandPrefab.name;
		sPrefab.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		getInput();
		createSand(100);

		if(mouse1Axis>0)
		{
			addSand(6);
			createSand(2);
		}
		if(mouse2Axis>0)
		{
			//removeSand();
		}
		//else 
	}
	
	//Grab the status of any input devices
	void getInput()
	{
		//LMB - Add sand
		mouse1Axis = Input.GetAxis("Fire1");
		//RMB - Remove sand
		mouse2Axis = Input.GetAxis("Fire2");
	}

	void createSand(int count)
	{
		sandCurr = ObjectPool.instance.pooledObjects[0].Count;
		if(sandCurr<sandCap)
		{
			for(int i = 0;i<count;i++)
			{
				ObjectPool.instance.PoolObject(SandPrefab);
			}
		}
	}
	
	//If the LMB is pressed, add sand to the list
	void addSand(int count)
	{
		//Grab the mouse pos to get the pos to add the sand particle on the screen
		//Convert screen coordinates to world space
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		for(int i = 0;i<count;i++)
		{
			GameObject newSand = ObjectPool.instance.GetObjectForType("SandParticle",true);
			newSand.transform.position = new Vector3(ray.origin.x+(float)(1-i*0.1), ray.origin.y, 0);
			newSand.transform.parent = PARENT.transform;
			//newSand.SetActive(true);
		}
	}

	//If the LMB is pressed, add sand to the list
	void addSand(GameObject sandCurr,double offset)
	{
		GameObject parent = GameObject.Find("SandParent");
		//Grab the mouse pos to get the pos to add the sand particle on the screen
		//Convert screen coordinates to world space
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		sandCurr.transform.position =  new Vector3(ray.origin.x+(float)offset, ray.origin.y, 0);
		sandCurr.transform.parent = parent.transform;
		//sand[sCurr].SetActive(true);
	}
	
	void removeSand()
	{
		
	}
}
