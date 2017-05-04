using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Color startColor = Color.red;
    public Color endColor = Color.blue;
    public float colorCycleSpeed = 10f;
    public float bobspeed = 10;
    public float bobSpread = 0.5f;

    private MeshRenderer rend;
    private float colortimer = 0;
    private float bobTimer = 0;
    private float startY = 0;

    // Update is called once per frame
    void Update()
    {
        ChangeColor(); // Calling a function
        Bob();

    }

    void ChangeColor() // Defining a function
    {
        colortimer += colorCycleSpeed * Time.deltaTime;
        float lerp = Mathf.PingPong(colortimer, 1.0f) / 1.0f;
        transform.Rotate(new Vector2(0, 0));

    }


    void Bob()
    {
        bobTimer += bobspeed * Time.deltaTime;
        float lerp = Mathf.PingPong(bobTimer, 1.0f) / 1.0f;
        // Get the position of powerup
        Vector3 position = transform.position; // Copy the position 
        // Change the position
        position.y = startY + Mathf.Lerp(-bobSpread, bobSpread, lerp);
        transform.position = position; // Set the Position
    }
}


