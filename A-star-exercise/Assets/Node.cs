using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public List<Node> neighbors;
    public Dictionary<Node, GameObject> edges = new Dictionary<Node, GameObject>();
    public GameObject edgePrefab;

    public float cost = float.PositiveInfinity;

    // Use this for initialization
    void Awake () {
        foreach (Node n in neighbors)
        {
            //create graphical edge
            if (!edges.ContainsKey(n))
            {
                GameObject edge = Instantiate(edgePrefab);
                LineRenderer lineRenderer = edge.AddComponent<LineRenderer>();
                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.widthMultiplier = 0.2f;
                lineRenderer.SetPositions(new Vector3[] { transform.position, n.transform.position });
                edges.Add(n, edge);
                n.edges.Add(this, edge);
            }
            

            //make sure the neighbor has the connection as well (meaning we have no one-way edges)
            if (!n.neighbors.Contains(this))
            {
                n.neighbors.Add(this);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
