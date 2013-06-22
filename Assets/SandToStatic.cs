using UnityEngine;
using System.Collections;

public class SandToStatic : MonoBehaviour 
{
	public int sandCount = 0;
	private Collider asd;
	private bool clear = false;
	//300
	
	// Use this for initialization
	void Start () 
	{
		asd = gameObject.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (sandCount > 45) clear = true;
	}
	
	void OnTriggerStay(Collider col)
	{
		if (clear)
		{
			Destroy(col.gameObject);
			asd.isTrigger = false;
			gameObject.GetComponent<MeshRenderer>().enabled = true;;
			
			//clear = false;
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		sandCount++;
	}
	
	void OnTriggerExit(Collider col)
	{
		sandCount--;
	}
}
