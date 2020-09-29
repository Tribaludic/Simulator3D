using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimulatorManager : MonoBehaviour
{
    PlayerInputActions inputActions;

    SelectorController currentSelectorController;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.PlayerControls.MouseClick.performed += MouseClick_performed;
        inputActions.PlayerControls.Exit.performed += Exit_performed;
    }

    private void Exit_performed(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void MouseClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {        
        Vector3 mousePosition = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0);

        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out hitInfo);
        if (hit)
        {
            if (hitInfo.collider.tag == "Avatar")
            {
                if (currentSelectorController != null)
                {
                    currentSelectorController.setEnable(false);
                }

                currentSelectorController = hitInfo.collider.GetComponent<SelectorController>();

                currentSelectorController.setEnable(true);
            }
            else
            {
                if (currentSelectorController != null)
                {
                    currentSelectorController.setEnable(false);
                    currentSelectorController = null;
                }
            }
            
            /* if (hitInfo.transform.gameObject.tag == "Construction")
             {
                 Debug.Log("It's working!");
             }
             else
             {
                 Debug.Log("nopz");
             }*/
        }
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
