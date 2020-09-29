using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform positionTarget;

    [SerializeField]
    Transform lookAtTarget;
    
    [SerializeField]
    float followSpeed;

    [SerializeField]
    float rotationSpeed;

    [SerializeField]
    float lookAtHeight;


    Transform thisTransform;

    private void Awake()
    {
        thisTransform = GetComponent<Transform>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        thisTransform.position = Vector3.Lerp(thisTransform.position, positionTarget.position, Time.deltaTime * followSpeed);

        Vector3 lookDirection = lookAtTarget.position - thisTransform.position;
        lookDirection.y += lookAtHeight;

        Quaternion toRotation = Quaternion.LookRotation(lookDirection);

        thisTransform.rotation = Quaternion.Lerp(thisTransform.rotation, toRotation, Time.deltaTime * rotationSpeed);


    }
}
