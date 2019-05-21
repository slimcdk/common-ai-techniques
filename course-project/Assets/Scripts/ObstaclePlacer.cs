using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePlacer : MonoBehaviour {

	public GameObject groundParent, obstacleParent;

	public float thresholdDistance = 10;
	public int cubes = 10, spheres = 10;
	public List<GameObject> obstacles;

	private float groundSize;

	void Awake () {
		groundParent = GameObject.FindGameObjectWithTag("Ground");
		obstacleParent = GameObject.FindGameObjectWithTag("Obstacle");
		groundSize = groundParent.transform.localScale.x * 10;

		PopulateCubes();
		PopulateSpheres();
	}

	void PopulateCubes()
	{
		// Generate cubes
		while(GameObject.FindGameObjectsWithTag("Obstacle:Cube").Length < cubes)
		{
			// Create cube primitive
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.tag = "Obstacle:Cube";
			cube.AddComponent<Rigidbody>();

			// Set random size
			cube.transform.localScale = new Vector3 (
				Random.Range(2, 10), 
				10, 
				Random.Range(2, 10)
			);

			// Set random position within ground space
			cube.transform.position = new Vector3 (
				Random.Range(-groundSize*0.45f, groundSize*0.45f),
				cube.transform.localScale.y / 2,
				Random.Range(-groundSize*0.45f, groundSize*0.45f)
			);

			// Verify of position is valid for placement
			bool positionVerified = false;
			while ( !positionVerified )
			{
				if ( !ValidPosition(cube.transform.position) )
				{
					cube.transform.position = new Vector3 (
						Random.Range(-groundSize*0.45f, groundSize*0.45f),
						cube.transform.localScale.y / 2,
						Random.Range(-groundSize*0.45f, groundSize*0.45f)
					);
				} 
				else
				{
					positionVerified = true;
				}
			}
			cube.transform.parent = obstacleParent.transform;
			obstacles.Add(cube);
		}
	}

	
	void PopulateSpheres()
	{
		// Generate spheres
		while(GameObject.FindGameObjectsWithTag("Obstacle:Sphere").Length < spheres)
		{
			// Create sphere primitive
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.tag = "Obstacle:Sphere";
			sphere.AddComponent<Rigidbody>();

			// Set random size
			float scale = Random.Range(2, 10);
			sphere.transform.localScale = new Vector3 (scale, scale, scale);

			// Set random position within ground space
			sphere.transform.position = new Vector3 (
				Random.Range(-groundSize*0.45f, groundSize*0.45f),
				sphere.transform.localScale.y / 2,
				Random.Range(-groundSize*0.45f, groundSize*0.45f)
			);


			// Verify of position is valid for placement
			bool positionVerified = false;
			while ( !positionVerified )
			{
				if ( !ValidPosition(sphere.transform.position) )
				{
					scale = Random.Range(2, 10);
					sphere.transform.localScale = new Vector3 (scale, scale, scale);
				} 
				else
				{
					positionVerified = true;
				}
			}
			sphere.transform.parent = obstacleParent.transform;
			obstacles.Add(sphere);
		}
	}
	

	bool ValidPosition(Vector3 pos)
	{
		foreach (GameObject obstacle in obstacles)
		{
			if ( Vector3.Distance(pos, obstacle.transform.position) < thresholdDistance)
			{
				return false;
			}
		}
		return true;
	}
}
