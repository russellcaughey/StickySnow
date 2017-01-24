using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider), typeof(FixedJoint), typeof(AudioSource))]
public class CollectableObject : MonoBehaviour {

    public float m_Size = 1;    
    
    protected FixedJoint m_Joint;
    protected MeshCollider m_MCollider;
    protected Mesh m_Mesh;
    protected Rigidbody m_Rb;
    protected AudioSource m_AudioSource;

    protected bool isInit = false;
    protected bool isTriggered = false;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        if (!isInit)
        {
            m_Joint = GetComponent<FixedJoint>();
            m_MCollider = GetComponent<MeshCollider>();
            m_Rb = GetComponent<Rigidbody>();
            m_AudioSource = GetComponent<AudioSource>();
            m_MCollider.sharedMesh = GetComponent<MeshFilter>() ? GetComponent<MeshFilter>().mesh : GetComponentInChildren<MeshFilter>().mesh;

            m_Rb.isKinematic = true;
            m_MCollider.convex = true;
            gameObject.layer = LayerMask.NameToLayer(LayerName.Collectable);
            isInit = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(!isTriggered)
            return;

        var player = col.gameObject.GetComponentInParent<PlayerController>();
        if (player.Snowball.SnowballSize < m_Size)
            return;

        m_Joint.connectedBody = col.gameObject.GetComponent<Rigidbody>();
        gameObject.transform.parent = player.CollectedObjects;
        gameObject.layer = LayerMask.NameToLayer(LayerName.Collected);
        player.Snowball.Grow(1);
        m_Rb.isKinematic = false;
        PlaySfx();
        
        Destroy(this);
    }

    void PlaySfx()
    {
        m_AudioSource.clip = (AudioClip)Resources.Load(ResourcePath.CollectSFX);
        m_AudioSource.volume = 0.3f;
        m_AudioSource.Play();
    }

    void OnTriggerEnter(Collider col)
    {
        isTriggered = true;
    }

    void OnTriggerExit(Collider col)
    {
        isTriggered = false;
    }
}
