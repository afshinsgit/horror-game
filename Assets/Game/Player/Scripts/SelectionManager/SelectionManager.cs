using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectionManager : MonoBehaviour
{
    public float distanceToTarget;
    private float m_RayLenght = 50f;
    private bool m_IsActionButtonPressed = false;
    private string m_SelectableTag = "Selectable";
    [SerializeField] private string m_ItemName;
    [SerializeField] private FPSTouchController m_FPSTouchController = null;
    [SerializeField] private RectTransform m_CorsshairSmall = null;
    [SerializeField] private RectTransform m_CorsshairBig = null;
    [SerializeField] private GameObject m_ActionButton = null;

    void Update()
    {
        Transform cameraTransform = m_FPSTouchController.m_FPSCamera.transform;
        RaycastHit hit;
       
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, m_RayLenght))
        {
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * m_RayLenght, Color.red);
            Transform selection = hit.transform;
            distanceToTarget = hit.distance;

            if (selection.CompareTag(m_SelectableTag) && distanceToTarget <= 1.3f)
            {
                m_ActionButton.SetActive(true);
                CrosshairScaleAnimation(true);

                if (m_IsActionButtonPressed)
                {
                    Debug.Log("Action key pressed");
                    Destroy(hit.transform.gameObject);
                    m_ActionButton.SetActive(false);
                    m_IsActionButtonPressed = false;
                }
            }
            else
            {
                m_ActionButton.SetActive(false);
                CrosshairScaleAnimation(false);
            }
        }
        else
        {
            m_ActionButton.SetActive(false);
            CrosshairScaleAnimation(false);
        }
    }

    public void ActionButtonPressed()
    {
        m_IsActionButtonPressed = true;
    }

    private void CrosshairScaleAnimation(bool isScaled)
    {
        if(isScaled)
        {
            m_CorsshairSmall.DOScale(new Vector3(0f, 0f, 1), 0.2f);
            m_CorsshairBig.DOScale(new Vector3(0.01f, 0.02f, 1), 0.3f);
        }
        else
        {
            m_CorsshairSmall.DOScale(new Vector3(0.005f, 0.009f, 1), 0.5f);
            m_CorsshairBig.DOScale(new Vector3(0f, 0f, 1), 0.8f);
        }
    }
}
