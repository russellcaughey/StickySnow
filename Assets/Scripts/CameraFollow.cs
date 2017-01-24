using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private GameObject m_FollowObject;
    [SerializeField]
    private Vector3 m_Offset;
    private Vector3 m_NewOffset;

    public Vector3 Offset { set { m_Offset = value; } }

    void OnEnable()
    {
        gameObject.transform.position = m_FollowObject.transform.position + m_Offset;
        m_NewOffset = m_Offset;
        transform.LookAt(m_FollowObject.transform.position);
        Snowball.CollectEvent += UpdateOffset;
    }

	void Update () 
    {
        transform.position = m_FollowObject.transform.position + m_Offset;
        transform.LookAt(m_FollowObject.transform.position);
        if (m_Offset != m_NewOffset)
        {
            m_Offset = Vector3.Lerp(m_Offset, m_NewOffset, Time.deltaTime);
        }
	}

    void UpdateOffset()
    {
        m_NewOffset.y = m_FollowObject.GetComponent<Snowball>().SnowballSize - 1;
        m_NewOffset.z = -m_FollowObject.GetComponent<Snowball>().SnowballSize;
    }

    void OnDisable()
    {
        Snowball.CollectEvent -= UpdateOffset;
    }
}
