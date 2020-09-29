using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputActions inputActions;

    Vector2 inputMotionVector;
    
    CharacterController characterController;

    Animator animator;

    [SerializeField]
    float motionSpeed;

    [SerializeField]
    float rotationSpeed;

    Transform thisTransform;

    bool isDying;
    bool inputEnabled = true;
    bool manualMode;
    private void Awake()
    {
        thisTransform = GetComponent<Transform>();

        inputActions = new PlayerInputActions();
        inputActions.PlayerControls.Move.performed += Move_performed;        
        inputActions.PlayerControls.FallDown.performed += FallDown_performed;
        
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

   
    private void FallDown_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (manualMode && inputEnabled)
        {
            animator.SetBool("walk", false);
            animator.SetTrigger("fallDown");           
            inputEnabled = false;           
        }

    }

    public void stopDying()
    {
        isDying = false;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        inputMotionVector = obj.ReadValue<Vector2>();
    }

    void Start()
    {
        
    }

    public void SetManualMode(bool value)
    {
        manualMode = value;
        animator.SetBool("walk", false);
    }
    
    void Update()
    {               

        if (manualMode && inputEnabled)
        {
            if (inputMotionVector.x != 0)
            {
                thisTransform.Rotate(Vector3.up, inputMotionVector.x * rotationSpeed * Time.deltaTime);
            }

            Vector3 motion = Vector3.zero;
            if (inputMotionVector.y != 0)
            {
                //movement
                animator.SetBool("walk", true);

                motion.z = 1;
                motion = thisTransform.TransformDirection(Vector3.forward);
                motion *= motionSpeed;
                characterController.SimpleMove(motion);

            }
            else
            {
                animator.SetBool("walk", false);
            }

        }
        else if (!inputEnabled)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Stand Up") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {                   
                inputEnabled = true;
            }

        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Stand Up") && Math.Round(animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 0)==1)
        {
            inputEnabled = true;
        }



    }
}
