using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public Transform playerTransform;
    protected float _curSpeed;
    protected float _curRotSpeed;
    protected Rigidbody _rigidbody;
    public abstract void UpdateState();
    public virtual void OnStateEnter()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void OnStateExit()
    {

    }

}
