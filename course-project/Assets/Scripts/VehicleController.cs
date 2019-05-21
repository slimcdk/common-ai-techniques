using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour {

	public float moveSpeed = 15.0f;
    public float rotationSpeed = 5.0f;
    public float boostPower = 1000.0f;

    private Material mat;
    private Color defaultColor, boostedColor;


    void Start()
    {
        mat = GetComponent<Renderer>().material;
        defaultColor = mat.GetColor("_BaseColor");
        boostedColor = Color.Lerp(Color.red, defaultColor, 0.40f);
    }

    void FixedUpdate()
    {
        // Get input axis values
        float move = Input.GetAxis("Vertical") * moveSpeed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Control the boost ability
        boostPower += Input.GetButton("Boost") ? -10 : 10;
        boostPower = Mathf.Clamp(boostPower, 0.0f, 1000.0f);

        // Apply boost effect
        move *= (Input.GetButton("Boost") && boostPower > 0) ? 3.0f : 1.0f;
        rotation *= (Input.GetButton("Boost") && boostPower > 0) ? 1.2f : 1.0f;
        mat.SetColor("_BaseColor", Color.Lerp(boostedColor, defaultColor, boostPower / 1000) );


        // Move along the object's z-axis and rotate itself
		transform.position -= transform.forward * Time.deltaTime * move;
        transform.Rotate(0, rotation, 0);
    }
}
