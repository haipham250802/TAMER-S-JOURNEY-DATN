using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTutorial : MonoBehaviour
{
    public Transform target;
    public Vector3 Offset;
    private void Update()
    {
        transform.localPosition = target.position + Offset;
    }
}
