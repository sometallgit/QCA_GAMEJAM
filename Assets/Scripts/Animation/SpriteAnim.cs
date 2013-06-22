using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpriteAnim : System.Object 
{
	
	public string name;
	public bool loop = true;
	public string NextAnimIfNoLoop;
	public float frameRate = 24;
	public Texture[] frames;
	
	public Texture play(uint frameNum)
	{
		//Debug.Log((frameRate / frameTime) % 10);
		
		return frames[frameNum];
	}
	
	public string getName()
	{
		return name;
	}
	
	public float getFrameRate()
	{
		return frameRate;
	}
	
	public int getNumFrames()
	{
		int i = frames.Length - 1;
		return i;
	}
	
	public string getNextAnim()
	{
		return NextAnimIfNoLoop;
	}
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	
}

