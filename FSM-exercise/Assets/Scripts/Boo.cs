using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boo : Agent {

    private Transform mario;
    public float minimumDistance = 6f;
    public float minimumAngle = 80f;

    private void Start()
    {
        mario = GameObject.Find("Mario").transform;
    }

    protected override void FiniteStateMachine()
    {
        throw new System.NotImplementedException();
    }

    bool CanMarioSeeMe()
    {
        //Debug.Log(Vector3.Distance(transform.position, mario.position));
        //Debug.Log(Vector3.Angle(transform.position, mario.forward));
        if (Vector3.Distance(transform.position, mario.position) < minimumDistance &&
            Vector3.Angle(transform.position, mario.forward) < minimumAngle)
        {
            Debug.Log("He sees me!");
            return true;
        }
        else
            return false;
    }
    
}
