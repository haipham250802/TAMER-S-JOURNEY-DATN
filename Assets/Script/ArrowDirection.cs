using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ArrowDirection : Singleton<ArrowDirection>
{
    public Transform Target;
    bool isMove;
    private void Awake()
    {
    }
    private void Start()
    { 
       /* gameObject.SetActive(false);
*/
    }
    private void Update()
    {
        /* if (!DataPlayer.GetIsCheckDoneTutorial())
         {*/
        if (Target)
        {
            Vector3 dir = Target.position - transform.position;
            Quaternion qua = Quaternion.LookRotation(Vector3.forward, dir);
            transform.rotation = qua;
            transform.localPosition = dir;
            Debug.DrawRay(transform.localPosition, dir);
        }
        /* }*/
    }
    /*  public void MoveUp(Transform trans)
      {
          trans.DOMove(new Vector3(trans.position.x, trans.position.y + 1, 0), 0.3f).OnComplete(() =>
          {
              trans.DOMove(new Vector3(trans.position.x, trans.position.y - 1, 0), 0.3f).OnComplete(() =>
              {
                  isMove = false;
              });
          });
      }*/
    
}
