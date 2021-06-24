using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float m_speed;
    uint m_lifes;
    uint m_numBombs;
    Collider m_collider;
    private CharacterController m_controller;
    // Start is called before the first frame update
    void Start() {

        m_controller = gameObject.AddComponent<CharacterController>();
        m_lifes = 3;
        m_numBombs = 1;
    }

    // Update is called once per frame
    void Update() {
        //movement of the character
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"),  
                                   0,
                                   Input.GetAxis("Vertical"));

        m_controller.Move(move * Time.deltaTime * m_speed);

        //inputs to character mecanics
        inputEvent();
    }

    void
    inputEvent() {

        if (Input.GetKeyDown(KeyCode.Space)) {
            print("bomba puesta");
        }
    }
}
