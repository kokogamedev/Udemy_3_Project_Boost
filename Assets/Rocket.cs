//using System;
//using System.Collections;
//using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement; //gives access to unity scene managment code
//deals with namespaces, which requires understanding classes --> future sections!!


//TODO: fix lighting bug
public class Rocket : MonoBehaviour
{

    //Declare your member variables (make sure that is the right name)
    Rigidbody rigidBody;
    AudioSource audioSource;
    //bool m_Play; //Play the music
    [SerializeField] float rcsThrust = 120f;
    [SerializeField] float thrustThisFrame = 750f;
    
    //Audioclips here
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip booster;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip victory;

    //Particle Effects here
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem victoryParticles;

    enum State {Alive, Dead, Transcending};
    State state = State.Alive;


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
        // somwhere, stop sound on death
        if (state == State.Alive)
        {
            RespondtoThrustInput();
            RespondtoRotateInput();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; } //means stop execution of this function at this point
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;
            case "Finish":
                StartVictorySequence();
                break;
            default:
                StartDeathSequence();
                //kill player
                break;
        }
        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}
        //if (collision.relativeVelocity.magnitude > 2)
        //{ 
        //    audioSource.Play();
        //}
    }

    private void StartDeathSequence()
    {
        state = State.Dead;
        audioSource.Stop();
        mainEngineParticles.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", 1f);
    }

    private void StartVictorySequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        mainEngineParticles.Stop();
        audioSource.PlayOneShot(victory);
        victoryParticles.Play();
        Invoke("LoadNextScene", 5f); //parametrize time
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1); //todo allow for more than 2 levels
    }

    private void RocketEffectsOn()
    {
        if (!audioSource.isPlaying)//So it doesn't layer
        {
            audioSource.PlayOneShot(mainEngine);//Play the audio you attach to the AudioSource component
            audioSource.PlayOneShot(booster);
            mainEngineParticles.Play();
        }
    }

    private void RespondtoThrustInput()
    { 
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();//Stop the audio
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        float mainThrust = thrustThisFrame * Time.deltaTime;
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        RocketEffectsOn();
    }

    private void RespondtoRotateInput()
    {
        rigidBody.freezeRotation = true; //take manual control of rotation
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; //resume physics control of rotation
    }


}
