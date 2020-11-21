using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class FPSTouchController : MonoBehaviour
{
    public FixedJoystick m_FixedJoystick;
    public FixedTouchField m_TouchField;
    public FirstPersonController m_FPSController;

    private CharacterController m_CharacterController;

    private float m_TargetHeight = 1.8f;
    public float m_CrouchDownSpeed = 8f;
    public bool m_IsCrouching = false;
    private Camera m_Camera;


    // Start is called before the first frame update
    private void Start()
    {
        m_Camera = GameObject.FindWithTag("FPSCamera").GetComponent<Camera>();
        m_CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        m_FPSController.RunAxis = m_FixedJoystick.Direction;
        m_FPSController.m_MouseLook.LookAxis = m_TouchField.TouchDist;

        CrouchSmoothing();
        HandleHeadBob();
        HandlePlayerWalkAndRunSpeed();
    }

    public void Crouch()
    {
        if (m_IsCrouching == false)
        {
            m_IsCrouching = true;
            m_FPSController.m_StepInterval = 2.5f;
            m_FPSController.m_RunSpeed = 1.5f;
            m_TargetHeight = 0.90f;
            m_FPSController.m_AudioSource.volume = 0.004f;
        }
        else if (m_IsCrouching == true && !Physics.Raycast(transform.position, Vector3.up, 1))
        {
            m_IsCrouching = false;
            m_FPSController.m_StepInterval = 5f;
            m_FPSController.m_RunSpeed = 4f;
            m_TargetHeight = 1.8f;
            m_FPSController.m_AudioSource.volume = 0.17f;
        }
    }

    private void CrouchSmoothing()
    {
        m_CharacterController.height = Mathf.Lerp(m_CharacterController.height, m_TargetHeight, m_CrouchDownSpeed * Time.deltaTime);

        m_Camera.transform.position = Vector3.Lerp(m_Camera.transform.position,
        new Vector3(m_Camera.transform.position.x, m_CharacterController.transform.position.y +
        m_TargetHeight / 2.24f, m_Camera.transform.position.z), m_CrouchDownSpeed * Time.deltaTime);
    }

    private void HandleHeadBob()
    {
        if (m_FPSController.RunAxis.y == 0f || m_FPSController.RunAxis.x == 0f)
        {
            m_FPSController.m_UseHeadBob = false;
        }
        else if (m_FPSController.RunAxis.y > 0f && m_FPSController.m_RunSpeed == 4f || m_FPSController.RunAxis.x > 0 && m_FPSController.m_RunSpeed == 4f)
        {
            m_FPSController.m_UseHeadBob = true;
        }
        else if (m_FPSController.RunAxis.y < -0.1f && m_FPSController.m_RunSpeed == 4f)
        {
            m_FPSController.m_UseHeadBob = true;
        }
        else if (m_IsCrouching && m_FPSController.m_RunSpeed == 1.5f)
        {
            m_FPSController.m_UseHeadBob = false;
        }
    }

    private void HandlePlayerWalkAndRunSpeed()
    {
        if (m_IsCrouching == false && m_FPSController.RunAxis.y > 0.85f)
        {
            m_FPSController.m_RunSpeed = 6f;
        } 
        else if (m_IsCrouching == false && m_FPSController.RunAxis.y <= 0.85f)
        {
            m_FPSController.m_RunSpeed = 4f;
        }
    }
}
