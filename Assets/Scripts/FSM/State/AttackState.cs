using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] protected Transform turret;
    [SerializeField] protected Transform bulletSpawn;
    float elapsedTime = 0.0f;
    float shootRate = 3.0f;

    public override void UpdateState()
    {
        
        if ((Mathf.Abs(transform.position.x - playerTransform.position.x) >= 3.0f && Mathf.Abs(transform.position.z - playerTransform.position.z) >= 3.0f) && (Mathf.Abs(transform.position.x - playerTransform.position.x) <= 5.0f && Mathf.Abs(transform.position.z - playerTransform.position.z) <= 5.0f))
        {
            Quaternion targetRot = Quaternion.LookRotation(playerTransform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _curRotSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, _curSpeed * Time.deltaTime);
            GetComponent<AIFsm>().SetNewState(gameObject.AddComponent<AttackState>());
        }
        else if (Mathf.Abs(transform.position.x - playerTransform.position.x) >= 5.0f || Mathf.Abs(transform.position.z - playerTransform.position.z) >= 5.0f)
        {
            GetComponent<AIFsm>().SetNewState(gameObject.AddComponent<PatrolState>());
        }
        Quaternion turretRotation = Quaternion.LookRotation(playerTransform.position - turret.position);
        turret.rotation = Quaternion.Slerp(turret.rotation, turretRotation, _curRotSpeed * Time.deltaTime);
        ShootBullet();
        elapsedTime += Time.deltaTime;
    }

    private void ShootBullet()
    {
        if (elapsedTime >= shootRate)
        {
            Instantiate(_bullet, bulletSpawn.position, bulletSpawn.rotation);
            elapsedTime = 0.0f;
        }
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        AIFsm ai = GetComponent<AIFsm>();
        _bullet = ai.bullet;
        bulletSpawn = ai.bulletSpawn;
        turret = ai.turret;
    }
}
