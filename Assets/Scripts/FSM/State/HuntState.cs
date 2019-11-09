using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntState : State
{
    public override void UpdateState()
    {
        if (Mathf.Abs(transform.position.x - playerTransform.position.x) <= 3.0f && Mathf.Abs(transform.position.z - playerTransform.position.z) <= 3.0f)
        {
            GetComponent<AIFsm>().SetNewState(gameObject.AddComponent<AttackState>());
        }
        else if (Mathf.Abs(transform.position.x - playerTransform.position.x) >= 5.0f || Mathf.Abs(transform.position.z - playerTransform.position.z) >= 5.0f)
        {
            GetComponent<AIFsm>().SetNewState(gameObject.AddComponent<PatrolState>());
        }
        Quaternion targetRot = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _curRotSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, _curSpeed * Time.deltaTime);
    }
}
