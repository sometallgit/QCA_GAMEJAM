using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class SandSpawner : MonoBehaviour 
{
//This creates and deletes sand particles
	
	//Public Variables
	public int AddCount=4;
	public GameObject SandPrefab;
	public Vector3 spawnDirection;
	GameObject PARENT;
	GameObject sPrefab;

	public int sandCap;
	private int sandCurr = 0;

	public double sleepTime;
	public bool awake;

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
		if(Time.time>sleepTime&&awake)
		{
			if(sandCurr<sandCap)
			{
			addSand(AddCount);
			sandCurr+=AddCount;
			}
		}
	
	}
	
	void addSand(int count)
	{
		for(int i = 0;i<count;i++)
		{
			GameObject newSand = ObjectPool.instance.GetObjectForType("SandParticle",true);
			newSand.transform.position = new Vector3(transform.position.x+(float)(i*0.3),transform.position.y, 0);
			newSand.transform.parent = PARENT.transform;
			newSand.GetComponent<Rigidbody>().AddForce(spawnDirection);
			//newSand.SetActive(true);
		}
	}
	
}
