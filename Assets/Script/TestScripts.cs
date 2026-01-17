using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour
{

    public PolyNavAgent agent;
    void Start()
    {

        agent = GetComponent<PolyNavAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            agent.SetDestination(transform.position + (Vector3)Random.insideUnitCircle * 5);
        }
    }
}
