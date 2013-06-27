using UnityEngine;
using System.Collections;

public class PBucket : MonoBehaviour {

	private bool mouse1Axis; //LMB
	private bool mouse2Axis; //RMB
	private bool mouse2Recheck;

	private GameObject PARENT;
	private GameObject sandParent;
	
	public float sandCount = 0;
	public float sandMax   = 1600;
	public float sandInc   = 80;

	private float mouseRange = 14;
	
	private RaycastHit hit;	

	// Use this for initialization
	void Start () {
		PARENT = GameObject.Find("Player");
		sandParent = GameObject.Find("SandParent");
	}
	
	// Update is called once per frame
	void Update () {
		
		getInput ();
		if(mouse1Axis)
		{
			if(sandCount>0){
				addSand(6);
				sandCount -=6;
			}
		}
		if(mouse2Axis)
		{
			collectSand();
		}
	}

		//If the LMB is pressed, add sand to the list
	void addSand(int count)
	{
		//Grab the mouse pos to get the pos to add the sand particle on the screen
		//Convert screen coordinates to world space
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float dist = Vector3.Distance(transform.position,mPos);

		if(dist<mouseRange){
			for(int i = 0;i<count;i++)
			{
				GameObject newSand = ObjectPool.instance.GetObjectForType("SandParticle",true);
				newSand.transform.position = new Vector3(ray.origin.x+(float)(1-i*0.1), ray.origin.y, 0);
				newSand.transform.parent = sandParent.transform;
			}
		}
	}
	
	void collectSand()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		if(Physics.Raycast(ray,out hit))
		{
			GameObject target = hit.collider.gameObject;
			if(target.name == "Volume")
			{
				float dist = Vector3.Distance(PARENT.transform.position,target.transform.position);
				GameObject sPlane = target.transform.parent.parent.gameObject;
				if(dist<mouseRange&&sandCount<sandMax)
				{
					sPlane.GetComponent<PlaneSand>().decreaseVolume(mPos);
					mouse2Recheck=true;
					sandCount+=sandInc;
				}
			}
				
		}
	}


		//Grab the status of any input devices
	void getInput()
	{
		//LMB - Add sand
		mouse1Axis = Input.GetMouseButton(0);
		//RMB - Remove sand
		if(!mouse2Recheck) mouse2Axis = Input.GetMouseButton(1);
		else mouse2Axis = false;
		if(Input.GetMouseButtonUp(1)) mouse2Recheck=false;
	}

}
