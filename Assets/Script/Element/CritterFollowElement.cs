using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
public class CritterFollowElement : MonoBehaviour
{
    public int ID;
    public int HP;
    public ECharacterType CritterFollowType;

    public SkeletonAnimation ICON;
    public SpriteRenderer Avatar;
    public CritterFollowController critterFollow;

    public float StartSpeed;
    public Vector3 OffSet;

    public GameObject shadow;
    public PolyNavAgent agent;
    Rigidbody2D rb;

    public E_StateCritterController EstateMove;
    public GameObject ChatPrefabsClone;
    public GameObject ChatPrefabsClone2;

    private void OnEnable()
    {
        if (agent.map == null)
        {
            agent.map = critterFollow.polyNav2d;
        }
        if (CritterFollowType != ECharacterType.NONE)
            ICON.AnimationState.SetAnimation(0, "Idle", true);
        InstanceStory();
    }
    void InstanceStory()
    {/*
        ChatPrefabsClone2 = SimplePool.Spawn(ChatPrefabsClone, Vector3.zero, Quaternion.identity);
        ChatPrefabsClone2.transform.SetParent(Controller.Instance.gameObject.transform);
        ChatPrefabsClone2 = ChatPrefabsClone2.gameObject;*/
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<PolyNavAgent>();
        StartSpeed = agent.maxSpeed;
        if (CritterFollowType == ECharacterType.NONE)
        {
            gameObject.SetActive(false);
            shadow.SetActive(false);
        }
        EstateMove = E_StateCritterController.Move;
    }

    private void Update()
    {
        if(gameObject.activeInHierarchy)
        {
        }
    }
   
    private void OnDisable()
    {
        //  critterFollow.L_CritterFollowElement.Remove(this);
       // SimplePool.Despawn(ChatPrefabsClone2);
    }
    public bool isDone = false;
    public void MoveCritterElement(Vector3 Target, Vector3 Offset)
    {
        isDone = true;
        UpdatePosShadow();
        if (agent != null)
        {
            agent.SetDestination(Target + Offset);
        }
    }
    public void UpdatePosShadow()
    {
        shadow.transform.position = this.transform.position;
    }
    public void Go(Vector3 Target, System.Action<bool> callback = null)
    {
        agent.SetDestination(Target, callback);
    }
    public void StopMove()
    {
        agent.Stop();
    }

}
public enum E_StateCritterController
{
    Idle,
    Move
}
