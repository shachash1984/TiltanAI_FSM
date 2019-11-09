using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFsm : FSM
{
    
    public GameObject bullet;

    private int _health;
    
    public State currentState;

    protected override void Initialize()
    {
        _health = 100;
        SetNewState(gameObject.AddComponent<PatrolState>());
    }

    protected override void FSMUpdate()
    {
        currentState.UpdateState();

        if (_health <= 0)
        {
            SetNewState(gameObject.AddComponent<DeadState>());
        }
    }

    public void SetNewState(State state)
    {
        if (currentState)
            currentState.OnStateExit();
        Destroy(currentState);
        currentState = state;
        currentState.OnStateEnter();
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _health -= collision.gameObject.GetComponent<Bullet>().damage;
            SetNewState(gameObject.AddComponent<TeleportState>());
        }
    }
}
