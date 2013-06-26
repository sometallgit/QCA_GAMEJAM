using UnityEngine;
using System.Collections;

public class CollisionVolumeParent : MonoBehaviour 
{
	public CollisionVolumeBehaviour child;
	//private PlaneSand parent;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void connectChildToParent(PlaneSand _parent)
	{
		//parent = _parent;
	}
	
	public CollisionVolumeBehaviour getChild()
	{
		return child;
	}
}
