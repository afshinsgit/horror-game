using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SurfaceDetection : MonoBehaviour
{
    [SerializeField] private List<SurfaceType> m_SurfaceTypes = new List<SurfaceType>();
    [SerializeField] private FirstPersonController m_FPSController;
    [SerializeField] private string m_currentSurface;

    void Start() { setGroundType(m_SurfaceTypes[0]); }

    void Update() {}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Assign different footstep sounds to surfaces here

        if (hit.transform.tag == "Concrete")
        {
            setGroundType(m_SurfaceTypes[1]);
        } 
        else
        {
            setGroundType(m_SurfaceTypes[0]);
        }
    }

    void setGroundType(SurfaceType surface)
    {
        if (m_currentSurface != surface.m_name)
        {
            m_FPSController.m_FootstepSounds = surface.m_footstepSounds;
            m_currentSurface = surface.m_name;
        }
    }
}

[System.Serializable]
public class SurfaceType 
{
    public string m_name;
    public AudioClip[] m_footstepSounds;
}
