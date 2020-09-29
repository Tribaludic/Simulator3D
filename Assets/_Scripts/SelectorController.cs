using UnityEngine;

public class SelectorController : MonoBehaviour
{
    PlayerController playerController;
    ShadowProjector shadowProjector;

    float angle;
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        shadowProjector = GetComponent<ShadowProjector>();
    }

    public void setEnable(bool value)
    {
        playerController.SetManualMode(value);
        shadowProjector.enabled = value;
        enabled = value;
    }

    // Update is called once per frame
    void Update()
    {
        angle += 1;

        if (angle == 360) angle = 0;

        shadowProjector.RotationAngleOffset = Quaternion.Euler(0, angle, 0);
        
    }
}
