using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private string m_SelectableTag = "Selectable";
    [SerializeField] private FPSTouchController m_FPSTouchController;
    [SerializeField] private Material m_HighlightMaterial;
    [SerializeField] private Material m_DefaultMaterial;

    private Transform m_Selection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Selection != null)
        {
            var selectionRenderer = m_Selection.GetComponent<Renderer>();
            selectionRenderer.material = m_DefaultMaterial;
            m_Selection = null;
        }

        Transform cameraTransform = m_FPSTouchController.m_FPSCamera.transform;
        RaycastHit hit;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 100.0f))
        {
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 100.0f, Color.yellow);
            var selection = hit.transform;
            if(selection.CompareTag(m_SelectableTag))
            {
                var selectionRendered = selection.GetComponent<Renderer>();
                if (selectionRendered != null)
                {
                    selectionRendered.material = m_HighlightMaterial;
                }

                m_Selection = selection;
            }
        }
    }
}
