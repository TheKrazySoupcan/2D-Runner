using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class User_Control : MonoBehaviour
{
    private Player character;
    private bool jump;
    private bool crouch;

    void Awake()
    {
        character = GetComponent<Player>();

    }

    void Update()
    {
        if (jump == false)
        {
            // Read jump input in update
            // Presses arent missed
            jump = Input.GetButtonDown("Jump");

        }
    }

    void FixedUpdate()
    {
        crouch = Input.GetKey(KeyCode.LeftControl);
        float h = Input.GetAxis("Horizontal");
        character.Move(h, crouch, jump);
        jump = false;

    }
}
