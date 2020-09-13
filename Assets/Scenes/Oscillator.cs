using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[DisallowMultipleComponent] //"modifies" class below it
public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);

    //todo remove from inspector later
    [Range(0,1)] // this and the line below modify the movementFactor variable
    [SerializeField] 
    float movementFactor; //0 for not moved, 1 for fully moved

    //store starting position
    Vector3 startingPos;

    //store the period
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //set movement factor here so that it is automatically in a sinusoidal motion (slow at the edges and fast in the middle)

        // todo protect against period = 0
        float cycles = Time.time / period; // grows continually from 0

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave/2f + 0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
    }
}
