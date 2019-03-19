using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    Rigidbody2D rb;
    [Range(10f,50f)]
    public float maxVelocity = 50f;
    public float range = 5;
    public float angleOfView = 160;

    SpriteRenderer sr;
    public Color slow = Color.green;
    public Color fast = Color.cyan;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Random.insideUnitCircle * 10;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //calculate the velocity vectors for all different criterias
        Vector2 s = Separation();
        Vector2 a = Alignment();
        Vector2 c = Cohesion();
        Vector2 t = FollowMouse();

        rb.velocity += s + a + c + t; 
        //if you want you can also limit the velocity to a maximum value (remember to change the value in the prefab)
        //LimitVelocity(); 
        
        //this bit of code rotates the sprite so it points to the direction of the velocity
        Vector2 v = rb.velocity;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //small color lerp to show how fast the boid is moving :)
        sr.color = Color.LerpUnclamped(slow, fast, rb.velocity.magnitude/maxVelocity);
    }

    Vector2 Separation()
    {
        throw new UnityException("Unimplemented method");
    }

    Vector2 Alignment()
    {
        throw new UnityException("Unimplemented method");
    }

    Vector2 Cohesion()
    {
        throw new UnityException("Unimplemented method");
    }

    //makes the boid attracted to the current mouse position by calculating the direction vector and "pushing" the boid by 1/300th of the distance
    Vector2 FollowMouse()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (target - transform.position) / 300;
    }

    //clamps the velocity to the value specified in maxVelocity
    void LimitVelocity()
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = (rb.velocity / rb.velocity.magnitude) * maxVelocity;
        }
    }

    //returns true if the boid in input can be seen by this boid, else false
    bool CanSee(GameObject boid)
    {
        return (Vector3.Distance(transform.position, boid.transform.position) < range &&
            Vector3.Angle(rb.velocity, boid.transform.position - transform.position) < angleOfView);
    }
}
