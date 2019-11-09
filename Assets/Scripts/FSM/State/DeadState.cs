using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    private bool _dead = false;
    public override void UpdateState()
    {
        if (!_dead)
        {
            _dead = true;
            Destroy(gameObject, 1.5f);
        }
    }
}
