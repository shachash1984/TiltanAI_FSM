using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportState : State
{
    private float teleportMinX = -20;
    private float teleportMaxX = 20;
    private float teleportMinZ = -20;
    private float teleportMaxZ = 20;
    private float teleportTimeInterval = 5f;
    private float teleportTimer;

    public override void UpdateState()
    {
        if (Time.time - teleportTimeInterval > teleportTimer)
        {
            StartCoroutine(Teleport());
            teleportTimer = Time.time;
        }
    }

    private IEnumerator Teleport()
    {
        List<Material> mats = new List<Material>();
        Renderer[] rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
        {
            mats.Add(r.material);
        }
        Color fadeColor = mats[0].color;
        while (mats[0].color.a > 0)
        {
            fadeColor.a -= Time.deltaTime * 2;
            foreach (Material mat in mats)
            {
                mat.color = fadeColor;
            }
            yield return null;
        }

        Vector3 randomPos = new Vector3(Random.Range(teleportMinX, teleportMaxX), transform.position.y, Random.Range(teleportMinZ, teleportMaxZ));
        transform.position = randomPos;
        yield return new WaitForSeconds(0.5f);
        while (mats[0].color.a < 1)
        {
            fadeColor.a += Time.deltaTime;
            foreach (Material mat in mats)
            {
                mat.color = fadeColor;
            }
            yield return null;
        }
        GetComponent<AIFsm>().SetNewState(gameObject.AddComponent<PatrolState>());
    }
}
