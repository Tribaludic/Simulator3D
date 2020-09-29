using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    [SerializeField]
    Transform[] rotors;

    [SerializeField]
    float rotorSpeed;

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < rotors.Length; i++)
        {
            rotors[i].Rotate(0,rotorSpeed * Time.deltaTime,0,Space.Self);
        }

        
    }
}
