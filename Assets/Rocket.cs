using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    //Declare your member variables (make sure that is the right name)
    Rigidbody rigidBody;
    AudioSource audioSource;
    //bool m_Play; //Play the music

    // Start is called before the first frame update
    void Start()
    {
        //New piece of code: allows you to extract component of the game object to which your script is attached.
        //In this case, you are getting the component (get component) of your object related to it's rigid body (<Rigidbody>)
        //characteristics. From there you can manipulate the physics of that rigid body
        //update: you have also extracted the AudioSource component
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void RocketNoiseOn()
    {
        if (!audioSource.isPlaying)//So it doesn't layer
        {
            audioSource.Play();//Play the audio you attach to the AudioSource component
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up);
            RocketNoiseOn();
        }
        else
        {
            audioSource.Stop();//Stop the audio
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; //take manual control of rotation
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back);
        }
        rigidBody.freezeRotation = false; //resume physics control
    }


}
