using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    [SerializeField]
    public Transform turret;
    [SerializeField]
    public Transform bulletSpawn;

    protected Transform playerTransform;
    [SerializeField]
    protected GameObject[] wayPoints;
    [SerializeField]
    protected int indexOfWayPoints;
    protected float shootRate;
    protected float elapsedTime;

    protected virtual void Initialize() { }
    protected virtual void FSMUpdate() { }
    protected virtual void FSMFixedUpdate() { }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        FSMUpdate();
    }

    private void FixedUpdate()
    {
        FSMFixedUpdate();
    }

}
