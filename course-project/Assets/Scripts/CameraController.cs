using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
    private Vector3 offset;

    // Use this for initialization
    void Start () 
    {
        offset = new Vector3(0, 6, 7);
    }
    
    // LateUpdate is called after Update each frame
    void FixedUpdate () 
    {
        transform.position = player.transform.position + offset;
		transform.LookAt(player.transform);

		//transform.Rotate(0, player.transform, 0);

    }
}
