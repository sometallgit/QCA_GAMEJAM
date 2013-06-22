using UnityEngine;
using System.Collections;

public class SandParticleBehaviour : MonoBehaviour 
{
	
	//This is the behaviour script for the sand particles
	
	private Rigidbody phys;
	private bool sleep;
	private float lifeTime;
	private float creationTime;
	
	// Use this for initialization
	void Start () 
	{
		//Give the particle a random rotation when it is created
		float randomAngle = Random.Range(0, 360);
		Vector3 randomRot = new Vector3(0f, 0f, randomAngle);
		gameObject.transform.localRotation = Quaternion.Euler(randomRot);
		
		phys = GetComponent<Rigidbody>();
		
		creationTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		lifeTime = Time.time - creationTime;
		if (lifeTime > 5) phys.isKinematic = true;
		if (phys.IsSleeping() && !phys.isKinematic)
		{
			Debug.Log("check");
			//phys.Sleep();
			phys.isKinematic = true;
		}
		else if (!phys.IsSleeping())
		{
			//phys.Sleep();
			if (phys.angularVelocity.z < phys.sleepAngularVelocity && 
				phys.angularVelocity.z > -phys.sleepAngularVelocity &&
				phys.velocity.x < phys.sleepVelocity && 
				phys.velocity.x > -phys.sleepVelocity &&
				phys.velocity.y < phys.sleepVelocity &&
				phys.velocity.y > -phys.sleepVelocity)
			{
				//Debug.Log("Sleep");
				sleep = true;
			}
		}
		else
		{
			
		}

	}
	
	public bool isAsleep()
	{
		return phys.IsSleeping();
	}
	
	void OnCollisionStay(Collision col)
	{
		/*
		SandParticleBehaviour s = col.gameObject.GetComponent<SandParticleBehaviour>();
		if (s != null)
		{
			if (s.isAsleep())
			{
				if (s.transform.position.x > transform.position.x)
				{
					Destroy(gameObject);
				}
			}
		}
		*/
	}
	
}
