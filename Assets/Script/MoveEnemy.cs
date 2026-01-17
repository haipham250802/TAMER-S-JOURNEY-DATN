using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public PolyNavAgent agent;
    public void Go(Vector3 pos, System.Action<bool> callBackMoveDone = null)
    {
        if (agent != null)
            agent.SetDestination(pos, (callBackMoveDone));
    }
    public void StopMove()
    {
        agent.Stop();
    }
}
