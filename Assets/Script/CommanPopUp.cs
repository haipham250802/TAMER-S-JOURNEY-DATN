using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanPopUp : MonoBehaviour
{
    public Animator anim;

    private void OnEnable()
    {
        anim.Play("AnimPopUp");
    }
}
