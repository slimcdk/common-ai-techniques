using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    public int swarmSize = 100;
    public GameObject boidPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i<swarmSize; i++)
        {
            Vector2 position = Random.insideUnitCircle * 20;
            Instantiate(boidPrefab, position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (GameObject boid in GameObject.FindGameObjectsWithTag("Boid"))
            {
                Vector3 toBoid = boid.transform.position - mousePosition;
                boid.GetComponent<Rigidbody2D>().velocity = (toBoid / toBoid.magnitude) * 50;
            }
        }
    }
}
