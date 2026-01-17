using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloatingJoystick : Joystick
{
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);

        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
        player.Instance.SetSpeed();
        player.Instance.skeleton.AnimationState.SetAnimation(1, "Move", true);
        if (player.Instance.isPurchaseBagBtn)
        {
            player.Instance.isPurchaseBagBtn = false;
            player.Instance.Option.SetActive(false);
            player.Instance.imgBag.sprite = player.Instance.bagClose.sprite;
            player.Instance.ShadowBag.SetActive(true);
        }
        if(player.Instance.gameObject.GetComponent<Collider2D>().enabled == false)
        {
           player.Instance.gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }
    IEnumerator IE_delay()
    {
        yield return new WaitForSeconds(10f);
        player.Instance.GetComponent<Collider2D>().enabled = true;

    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        player.Instance.skeleton.AnimationState.SetAnimation(1, "Idle", true);
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
        DataPlayer.SetIsSlideToMove(true);

      /*  if (DataPlayer.GetIsSlideToMove())
        {
            GetComponent<Canvas>().sortingLayerName = "UI";
            GetComponent<Canvas>().sortingOrder = 0;
        }*/
    }
}