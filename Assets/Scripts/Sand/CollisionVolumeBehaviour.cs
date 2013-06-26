using UnityEngine;
using System.Collections;

public class CollisionVolumeBehaviour : MonoBehaviour 
{
	private PlaneSand parent;
	
	public void receiveParent(PlaneSand _parent)
	{
		parent = _parent;
	}
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnCollisionEnter(Collision col)
	{
		SandParticleBehaviour temp = col.gameObject.GetComponent<SandParticleBehaviour>();
		if (temp != null)
		{
			parent.collisionRegistered(col);
		}
	}
}
