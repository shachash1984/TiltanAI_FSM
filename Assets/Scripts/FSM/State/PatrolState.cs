using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    private GameObject[] wayPoints;
    private int indexOfWayPoints;

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _curSpeed = 15.0f;
        _curRotSpeed = 2.0f;
        wayPoints = GameObject.FindGameObjectsWithTag("Way Point");
        FindNextPoint();
    }

    public override void UpdateState()
    {
        if (Mathf.Abs(transform.position.x - wayPoints[indexOfWayPoints].transform.position.x) < 1 && Mathf.Abs(transform.position.z - wayPoints[indexOfWayPoints].transform.position.z) < 1)
        {
            FindNextPoint();
        }
        else if (Mathf.Abs(transform.position.x - playerTransform.position.x) <= 5.0f && Mathf.Abs(transform.position.z - playerTransform.position.z) <= 5.0f)
        {
            gameObject.GetComponent<AIFsm>().SetNewState(gameObject.AddComponent<HuntState>());
        }

        Quaternion targetRot = Quaternion.LookRotation(wayPoints[indexOfWayPoints].transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _curRotSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[indexOfWayPoints].transform.position, _curSpeed * Time.deltaTime);
    }

    protected void FindNextPoint()
    {
        int randomIndex = GetRandomIndex();

        if (randomIndex == indexOfWayPoints)
        {
            if (indexOfWayPoints == wayPoints.Length - 1)
            {
                indexOfWayPoints--;
            }
            else
            {
                indexOfWayPoints++;
            }
        }
        else
        {
            indexOfWayPoints = randomIndex;
        }
    }

    private int GetRandomIndex()
    {
        return Random.Range(0, wayPoints.Length - 1);
    }
}
