using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    //Declare your member variables (make sure that is the right name)
    Rigidbody rigidBody;
    AudioSource audioSource;
    bool m_Play; //Play the music
    bool m_ToggleChange; //Detect when you sue the toggle, ensures music isn't played multiple times

    // Start is called before the first frame update
    void Start()
    {
        //New piece of code: allows you to extract component of the game object to which your script is attached.
        //In this case, you are getting the component (get component) of your object related to it's rigid body (<Rigidbody>)
        //characteristics. From there you can manipulate the physics of that rigid body
        //update: you have also extracted the AudioSource component
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        //Ensure the toggle is set to true for the music to play at start-up
        m_Play = true;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        RocketNoise();
    }

    private void RocketNoise()
    {
        if (m_Play == true && m_ToggleChange == true)//Check to see if you just set toggle to positive
        {
            audioSource.Play();//Play the audio you attach to the AudioSource component
            m_ToggleChange = false;//Ensure the audio doesn't play more than once
        }
        if (m_Play == false && m_ToggleChange == true)//Check if you just set the toggle to false
        {
            audioSource.Stop();//Stop the audio
            m_ToggleChange = false;//Ensure audio doesn't play anymore
        }
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up);
            m_Play = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back);
        }
        else
        {
            m_Play = false;
        }
    }
}
