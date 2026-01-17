using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class TestSpine : MonoBehaviour
{
    // public SkeletonAnimation skeletonAnimation;
    public SkeletonGraphic skeletonGraphic;
    private void Start()
    {
        skeletonGraphic.startingAnimation = "Attack";
        skeletonGraphic.AnimationState.SetAnimation(0,skeletonGraphic.startingAnimation, true);
        skeletonGraphic.AnimationState.SetAnimation(1, skeletonGraphic.startingAnimation, true);

    }
}
