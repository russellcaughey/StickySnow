using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour {

    protected Rigidbody m_Rb;
    protected Vector3 m_Torque = new Vector3();
    protected Vector3 m_CurrentScale;
    protected Vector3 m_ScaleTo;
    protected Vector3 m_LastMoveDir;
    protected ParticleSystem m_ParticleSystem;

    protected float m_CurrentTorqueFactor = 50;
    protected float m_TorqueSizeFactor = 50;
    protected float m_TorqueBurstFactor = 100;
    protected float m_JumpFactor = 10;
    protected float m_Timer = 5;
    protected int m_SnowballSize;
    protected bool isGrounded = false;
    protected bool isGrowing = false;

    public delegate void SnowballEvent(int num);
    public static event SnowballEvent GrowSize;

    public int SnowballSize { get { return m_SnowballSize; } }
    public Vector3 LastMove { get { return m_LastMoveDir; } }

    protected void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
        m_ParticleSystem = GetComponentInChildren<ParticleSystem>();
        m_CurrentScale = gameObject.transform.localScale;
        m_ScaleTo = m_CurrentScale;
        m_SnowballSize = (int)Mathf.Floor(m_CurrentScale.x);
    }

    public void Move(Vector3 move, bool jump, bool burst)
    {
        // Adapt move vector to torque
        m_Torque = new Vector3(move.z, 0, -move.x);
        m_CurrentTorqueFactor = !burst ? m_SnowballSize * m_TorqueSizeFactor : m_SnowballSize * m_TorqueBurstFactor;
        m_Torque *= m_CurrentTorqueFactor;
        m_Rb.AddTorque(m_Torque);

        // Stop rolling if there is no input
        if (move.magnitude == 0) { 
            m_Rb.angularDrag = Mathf.Lerp(m_Rb.angularDrag, 50, Time.deltaTime); }
        else { m_Rb.angularDrag = 1; }
        
        // Jump
        if (jump && isGrounded)
        {
            m_Rb.AddForce(new Vector3(0, m_SnowballSize*m_JumpFactor, 0), ForceMode.Impulse);
        }

        m_LastMoveDir = move;
    }

    void Update()
    {
        if (!isGrowing || transform.localScale.magnitude >= m_ScaleTo.magnitude)
            return;

        transform.localScale = Vector3.Lerp(transform.localScale, m_ScaleTo, Time.deltaTime);

        if (transform.localScale.magnitude >= m_ScaleTo.magnitude)
            isGrowing = false;
    }

    public void Grow(float increase)
    {
        isGrowing = true;
        m_ScaleTo.x += increase;
        m_ScaleTo.y += increase;
        m_ScaleTo.z += increase;
        m_SnowballSize = (int)Mathf.Floor(m_ScaleTo.x);
        m_ParticleSystem.startSize = m_SnowballSize;
        m_ParticleSystem.Play();
        GrowSize(m_SnowballSize);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer(LayerName.Ground))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer(LayerName.Ground))
        {
            isGrounded = false;
        }
    }
}
