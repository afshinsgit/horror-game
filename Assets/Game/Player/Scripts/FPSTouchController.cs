using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class FPSTouchController : MonoBehaviour
{
    public FixedJoystick FixedJoystick;
    public FixedTouchField TouchField;
    public FirstPersonController FPSController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FPSController.RunAxis = FixedJoystick.Direction;
        FPSController.m_MouseLook.LookAxis = TouchField.TouchDist;
    }
}
