using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
public class FPSTouchController : MonoBehaviour
{
    [SerializeField] private FixedJoystick m_FixedJoystick = null;
    [SerializeField] private FixedTouchField m_TouchField = null;
    [SerializeField] private FirstPersonController m_FPSController = null;
    [SerializeField] private float m_TargetHeight = 1.8f;
    [SerializeField] private float m_CrouchDownSpeed = 8f;
    [SerializeField] private bool m_IsCrouching = false;
    [SerializeField] private Image m_CrouchEffect = null;
    [HideInInspector] public Camera m_FPSCamera;
    private CharacterController m_CharacterController;


    // Start is called before the first frame update
    private void Start()
    {
        m_FPSCamera = GameObject.FindWithTag("FPSCamera").GetComponent<Camera>();
        m_CharacterController = GetComponent<CharacterController>();
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
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
        if (!m_IsCrouching)
        {
            m_IsCrouching = true;
            m_FPSController.m_StepInterval = 1.2f;
            m_FPSController.m_RunSpeed = 0.75f;
            m_TargetHeight = 0.72f;
            m_FPSController.m_AudioSource.volume = 0.004f;
            m_CrouchEffect.DOColor(new Color32(255, 255, 255, 125), 0.3f);
        }
        else if (m_IsCrouching && !Physics.Raycast(transform.position, Vector3.up, 1))
        {
            m_IsCrouching = false;
            m_FPSController.m_StepInterval = 2.98f;
            m_FPSController.m_RunSpeed = 2.5f;
            m_TargetHeight = 1.8f;
            m_FPSController.m_AudioSource.volume = 0.17f;
            m_CrouchEffect.DOColor(new Color32(255, 255, 255, 0), 0.3f);
        }
    }

    private void CrouchSmoothing()
    {
        m_CharacterController.height = Mathf.Lerp(m_CharacterController.height, m_TargetHeight, m_CrouchDownSpeed * Time.deltaTime);

        m_FPSCamera.transform.position = Vector3.Lerp(m_FPSCamera.transform.position,
        new Vector3(m_FPSCamera.transform.position.x, m_CharacterController.transform.position.y +
        m_TargetHeight / 2f, m_FPSCamera.transform.position.z), m_CrouchDownSpeed * Time.deltaTime);
    }

    private void HandleHeadBob()
    {
        if (m_FPSController.RunAxis.y == 0f || m_FPSController.RunAxis.x == 0f)
        {
            m_FPSController.m_UseHeadBob = false;
        }
        else if (m_FPSController.RunAxis.y > 0f && m_FPSController.m_RunSpeed == 2.5f || m_FPSController.RunAxis.x > 0 && m_FPSController.m_RunSpeed == 2.5f)
        {
            m_FPSController.m_UseHeadBob = true;
        }
        else if (m_FPSController.RunAxis.y < -0.1f && m_FPSController.m_RunSpeed == 2.5f)
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
        if (!m_IsCrouching && m_FPSController.RunAxis.y > 0.85f)
        {
            m_FPSController.m_RunSpeed = 4f;
        } 
        else if (!m_IsCrouching && m_FPSController.RunAxis.y <= 0.85f)
        {
            m_FPSController.m_RunSpeed = 2.5f;
        }
    }
}
