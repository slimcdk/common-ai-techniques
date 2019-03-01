using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarExercise : MonoBehaviour {

    public Node start;
    public Node goal;

	// Use this for initialization
	void Start () {

        List<Node> closedSet = new List<Node>();

        List<Node> openSet = new List<Node>();

        //initially we set the openSet as our starting node
        openSet.Add(start);

        //cost to go to start is 0 so the cost is just the heuristic
        start.cost = 0 + HeuristicCostToGoal(start);

        while (openSet.Count > 0)
        {
            //IMPLEMENT A* HERE
        }
	}

    void ReconstructPath(Node current)
    {
        if (current == start)
            return;
        Node bestNode = null;
        float minCost = float.PositiveInfinity;
        foreach (Node n in current.neighbors)
        {
            if (n.cost < minCost)
            {
                bestNode = n;
                minCost = n.cost;
            }
        }
        Debug.Log("coloring edge between " + current + " and " + bestNode);
        current.edges[bestNode].GetComponent<LineRenderer>().startColor=Color.red;
        current.edges[bestNode].GetComponent<LineRenderer>().endColor = Color.red;
        ReconstructPath(bestNode);
    }

    float CostToGetToTheNode(Node from, Node to)
    {
        return from.cost + Vector3.Distance(from.gameObject.transform.position, to.gameObject.transform.position);
    }

    float HeuristicCostToGoal(Node n)
    {
        return Vector3.Distance(n.gameObject.transform.position, goal.gameObject.transform.position);
    }
	
}
