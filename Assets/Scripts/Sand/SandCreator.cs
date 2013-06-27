//using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SandCreator : MonoBehaviour 
{
	//This creates and deletes sand particles
	
	//Public Variables
	public int AddCount; //The time in seconds between the creation of particles
	public GameObject SandPrefab;
	GameObject PARENT;
	GameObject sPrefab;

	public int sandCap = 2000;
	private int sandCurr = 0;
	
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
		createSand(AddCount);
	}
	

	void createSand(int count)
	{
		sandCurr = ObjectPool.instance.pooledObjects[0].Count;
		if(sandCurr+count<sandCap)
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
			}
	}
	

}
