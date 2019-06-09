using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTree {

	public Boundary boundary;
	public int capacity;
	public List<GameObject> nodes;

	public List<QuadTree> quadtrees;

	public QuadTree(Boundary b, int c)
	{
		boundary = b;
		capacity = c;
		quadtrees = new List<QuadTree>(0);
	}	

	public void InsertNode(GameObject node) {
		Debug.Log("Inserting new node");
		
		if (nodes.Count < capacity)
		{
			nodes.Add(node);
			return;
		}
		Debug.Log("Quadtree full. Subdividing");

		return;
	}
}


// Center origin
public class Boundary {
	public float east, north, west, south;
	public Boundary (float _east, float _north, float _west, float _south)
	{
		east = _east; 
		north = _north;
		west = _west;
		south = _south;
	}

	public void Print(){
		Debug.Log("Boundary ("+east+", "+north+", "+west+", "+south+")");
	}
}