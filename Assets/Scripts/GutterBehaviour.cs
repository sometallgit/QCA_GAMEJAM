using UnityEngine;
using System.Collections;

public class GutterBehaviour : MonoBehaviour 
{

	public bool awake;

	public Vector3 sleepRot;
	public Vector3 awakeRot;

	private Quaternion sleepR;
	private Quaternion awakeR;

	// Use this for initialization
	void Start () 
	{
		sleepR.eulerAngles = sleepRot;
		awakeR.eulerAngles = awakeRot;
		Debug.Log(awakeR);
		transform.rotation = sleepR;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(awake) transform.rotation = Quaternion.Lerp(transform.rotation, awakeR, Time.deltaTime*10);
		else transform.rotation = Quaternion.Lerp(transform.rotation, sleepR, Time.deltaTime*10);
	}
}
