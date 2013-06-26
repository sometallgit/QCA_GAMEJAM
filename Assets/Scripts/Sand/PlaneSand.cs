using UnityEngine;
using System.Collections;

public class PlaneSand : MonoBehaviour 
{
	
	private Vector3[] newVertices;
	private Vector3[] normals;
    private Vector2[] newUV;
    private int[] newTriangles;
	
	public int divisions;
	public GameObject collisionVolumePref;
	
	private float sandLength;
	private Vector3 leftSidePos;
	private Vector3 rightSidePos;
	private float divisionSize;
	
	private Vector3[] vertices;
	
	private GameObject[] collisionVolumes;
	
	//Only update the collision volumes if needed
	private bool collisionVolumeOutOfDate = true;
	
	Mesh mesh;
	// Use this for initialization
	void Start () 
	{
		//Grab the start position
		Vector3 tempStartPos = transform.position;
		//Move the sandplane to the origin to circumnavigate bug
		transform.position = new Vector3(0f,0f,0f);
		
		//////////////
		//Build Mesh//
		//////////////
		mesh = GetComponent<MeshFilter>().mesh;
		
		//Get start and end position
		getVertexBounds();
		
		//Divide returned distance by the division count
		divisionSize = sandLength/(divisions + 1);
		//Debug.Log(divisionSize);
		//Create verts
		buildVerts();
		
		//Create tris
		buildTris();
		
		//Create normals
		buildNormals();
		
		//Create UVs (gonna be a bitch)
		buildUVs();
		
		//Assign colliders
		buildCollisionVolumes();
		
		//Move the finished plane back to where it started
		transform.position = tempStartPos;
		vertices = mesh.vertices;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (collisionVolumeOutOfDate) updateCollisionVolumes();
	}
	
	void getVertexBounds()
	{
		Vector3[] vertices = mesh.vertices;
		leftSidePos = transform.TransformPoint(vertices[2]);
		rightSidePos = transform.TransformPoint(vertices[0]);
		sandLength = rightSidePos.x - leftSidePos.x;
		//Debug.Log(leftSidePos + ", " + rightSidePos);
		//Debug.Log(sandLength);
		Vector3 scale = new Vector3(1,1,1);
		transform.localScale = scale;
	}
	
	void buildVerts()
	{
		int numVerts = ((divisions + 2) * 2);
		newVertices = new Vector3[numVerts];
		
		int i =0;
		//Position the top row
		while (i < divisions + 2)
		{
			Vector3 vertPos = new Vector3(leftSidePos.x + (divisionSize * i),0.5f,0);
			newVertices[i] = vertPos;
			//Debug.Log(newVertices[i]);
			i++;
		}
		
		//Position bottom row
		i = 0;
		while (i < divisions + 2)
		{
			Vector3 vertPos = new Vector3(leftSidePos.x + (divisionSize * i),-0.5f,0);
			newVertices[i + divisions + 2] = vertPos;
			//Debug.Log(newVertices[i]);
			i++;
		}
		/*
		i = 0;
		while (i < newVertices.Length)
		{
			//Debug.Log(newVertices[i]);
			i++;
		}
		*/
		
        mesh.vertices = newVertices;
	}
	
	void buildTris()
	{
		int numTris = (newVertices.Length - 2) * 3;
		
		newTriangles = new int[numTris];
		
		for (int i = 0; i < ((newTriangles.Length / 3) / 2); i++)
		{
			newTriangles[i * 6 + 0] = i + 0;
			newTriangles[i * 6 + 1] = i + 1;
			newTriangles[i * 6 + 2] = i + divisions + 2;
			
			newTriangles[i * 6 + 3] = i + 1;
			newTriangles[i * 6 + 4] = i + divisions + 3;
			newTriangles[i * 6 + 5] = i + divisions + 2;
		}
		
		mesh.triangles = newTriangles;
	}
	
	void buildNormals()
	{
		normals = new Vector3[newVertices.Length];
		for (int i = 0; i < normals.Length; i++)
		{
			normals[i] = -Vector3.forward;	
		}
		
		mesh.normals = normals;
	}
	
	void buildUVs()
	{
		//I hope I don't have to write this
	}
	
	void buildCollisionVolumes()
	{
		collisionVolumes = new GameObject[divisions + 1];
		
		vertices = mesh.vertices;
		CollisionVolumeBehaviour vol;
		
		for (int i = 0; i < collisionVolumes.Length; i++)
		{
			collisionVolumes[i] = Instantiate(collisionVolumePref, transform.position, transform.rotation) as GameObject;
			//Set the new object as a child of the object
			collisionVolumes[i].transform.parent = transform;
			vol = collisionVolumes[i].GetComponent<CollisionVolumeParent>().getChild();
			PlaneSand thisObject = GetComponent<PlaneSand>();
			vol.receiveParent(thisObject);
			
		}
	}
	
	void updateCollisionVolumes()
	{
		//Grab the most recent vertices information
		vertices = mesh.vertices;
		
		//Run through every volume and update their position and rotation
		for (int i = 0; i < collisionVolumes.Length; i++)
		{
			//position
			collisionVolumes[i].transform.localPosition = vertices[i];
			//rotation
			
			Vector3 pos1 = transform.TransformPoint(collisionVolumes[i].transform.localPosition);
			Vector3 pos2 = transform.TransformPoint(vertices[i+1]);
			
			float deltaY = pos2.y - pos1.y;
			float deltaX = pos2.x - pos1.x;
			
			float angleOffset = Mathf.Atan2(deltaY, deltaX) * 180f / Mathf.PI;
			
			collisionVolumes[i].transform.localRotation = Quaternion.Euler(0,0,angleOffset);
			
			//scale
			Vector3 newScale = new Vector3(Vector3.Distance(pos1,pos2), 1, 1);
			collisionVolumes[i].transform.localScale = newScale;
		}
			
		collisionVolumeOutOfDate = false;
	}
	/*
	void OnTriggerEnter(Collider col)
	{
		float collidingParticleX = col.gameObject.transform.position.x;

		
		
		//Mesh mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
         normals = mesh.normals;
        
        for (int i = 0;i < vertices.Length/2; i++) 
		{
			
			float falloff = 1;
			
			Vector3 currentSandPointPos = transform.TransformPoint(vertices[i]);
			
			float distance = collidingParticleX - currentSandPointPos.x;
			if (distance < 0f) distance = -distance;
			
			falloff = (Mathf.InverseLerp(1, 0, (distance/(sandLength * 0.5f))));
			
			//Debug.Log(falloff);
			
			
			
            vertices[i] += Vector3.up * Time.deltaTime * falloff * 0.2f;
        }
        
        mesh.vertices = vertices;
		Destroy(col.gameObject);
		//Debug.Log(transform.TransformPoint(vertices[0]));
		
		collisionVolumeOutOfDate = true;
	}
	*/
	public void collisionRegistered(Collision col)
	{
		float collidingParticleX = col.gameObject.transform.position.x;
		
        vertices = mesh.vertices;
        normals = mesh.normals;
        
        for (int i = 0;i < vertices.Length/2; i++) 
		{
			float falloff = 1;
			
			Vector3 currentSandPointPos = transform.TransformPoint(vertices[i]);
			
			float distance = collidingParticleX - currentSandPointPos.x;
			if (distance < 0f) distance = -distance;
			
			falloff = (Mathf.InverseLerp(1, 0, (distance/(sandLength * 0.5f))));
			
            vertices[i] += Vector3.up * Time.deltaTime * falloff * 0.2f;
        }
        	
        mesh.vertices = vertices;
		//Destroy(col.gameObject);
		//Debug.Log(transform.TransformPoint(vertices[0]));
		
		collisionVolumeOutOfDate = true;
	}
}
