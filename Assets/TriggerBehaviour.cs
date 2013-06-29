using UnityEngine;
using System.Collections;

public class TriggerBehaviour : MonoBehaviour 
{
	public GameObject[] spawners;
	public GameObject[] puzzles;
	public GameObject[] triggers;

	public bool keyPress;
	public bool inputSign;

	public bool pCollide;
	
	public bool triggered = false;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(pCollide)
		{
			inputSign=true;
			if(Input.GetKeyDown("e"))
			{
				triggered = true;
			}
		}
		triggerCheck();
	}


	void triggerCheck()
	{
		if (triggered)
		{
			inputSign = false;
			for (int i = 0; i < spawners.Length; i++)
			{
				spawners[i].GetComponent<SandSpawner>().awake = true;
			}
			for (int i = 0; i < puzzles.Length; i++)
			{
				puzzles[i].GetComponent<GutterBehaviour>().awake = !puzzles[i].GetComponent<GutterBehaviour>().awake;
			}
			triggered=false;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(keyPress)  pCollide=true;
		else triggered=true;
	}

	void OnTriggerExit(Collider col)
	{
		if(keyPress) pCollide=false;
	}
}
