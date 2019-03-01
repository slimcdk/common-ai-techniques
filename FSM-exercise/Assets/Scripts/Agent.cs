using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Agent : MonoBehaviour {

    public NavMeshAgent nm;
    public Transform target;

    public int state = 0;

    private void Update()
    {
        FiniteStateMachine();
    }

    //Abstract method that defines the behaviour 
    protected abstract void FiniteStateMachine(); 

    //Go to
    public void MoveTo(Transform destination)
    {
        nm.SetDestination(destination.position);
    }
}
