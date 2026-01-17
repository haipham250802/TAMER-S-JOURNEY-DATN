using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUpRate : MonoBehaviour
{
    public int ID;

    public Button ExitBtn;
    public Button Yes;
    public Button[] buttons;

    public Sprite BlackStar;
    public Sprite YeallowStar;

    public GameObject PopUpRateThankYou;

#if UNITY_ANDROID
    private Google.Play.Review.ReviewManager _reviewManager;
    private Google.Play.Review.PlayReviewInfo _playReviewInfo;
#endif

    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().sprite = BlackStar;
        }
        ExitBtn.onClick.AddListener(OnclickOutButton);
        Yes.onClick.AddListener(OnClickButtonYes);
    }
    private void OnEnable()
    {
        Yes.gameObject.SetActive(false);
        player.Instance.GetComponent<Collider2D>().enabled = false;
        ID = 5;
        for (int i = 0; i < ID; i++)
        {
            if (buttons[i].GetComponent<Image>().sprite == BlackStar)
            {
                buttons[i].GetComponent<Image>().sprite = YeallowStar;
            }
        }
        Yes.gameObject.SetActive(true);

    }

    public void SetSelectStar()
    {
        Yes.gameObject.SetActive(true);

        if (buttons[ID - 1].GetComponent<Image>().sprite == BlackStar)
        {
            for (int i = 0; i < ID; i++)
            {
                if (buttons[i].GetComponent<Image>().sprite == BlackStar)
                {
                    buttons[i].GetComponent<Image>().sprite = YeallowStar;
                }
            }
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i >= ID - 1)
                {
                    if (buttons[i].GetComponent<Image>().sprite == YeallowStar)
                    {
                        buttons[i].GetComponent<Image>().sprite = BlackStar;
                        buttons[ID - 1].GetComponent<Image>().sprite = BlackStar;
                    }
                }
            }
        }
    }

    public void OnclickOutButton()
    {
        DataPlayer.SetOutPopUpRate(true);
        gameObject.SetActive(false);
    }
    public void OnClickButtonYes()
    {
      /*  if (buttons.Length == ID)
        {
            Debug.Log("da rate");*/
            SelectYes();
       /*     DataPlayer.SetRate(true);

        }*/
        //  gameObject.SetActive(false);

    }
    private void OnDisable()
    {
#if UNITY_ANDROID
        StopCoroutine(RequestForReview());
        _reviewManager = null;
        _playReviewInfo = null;
#endif
    }
    public void SelectYes()
    {
        DataPlayer.SetRate(true);
        //RateController.Instance.RateUs();
#if UNITY_ANDROID
        if (ID < 5)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(RequestForReview());
        }
#elif UNITY_IPHONE
           if (panelSelectStar.GetMaxStar() > panelSelectStar.GetTotalStar())
            {
                contentOpenRate.SetActive(false);
                contentThankRate.SetActive(true);
            }
            else
            {
                Application.OpenURL("itms-apps://itunes.apple.com/app/" + RocketConfig.Apple_App_ID);
                contentOpenRate.SetActive(false);
                contentThankRate.SetActive(true);
            };
#endif
        //Application.OpenURL("itms-apps://itunes.apple.com/app/" + RocketConfig.Apple_App_ID);

    }
#if UNITY_ANDROID
    private IEnumerator RequestForReview()
    {
        Debug.Log("link store");
        //GameManager.Instance.ActivePanelLoadingNetwork(true);

        //AppOpenAdManager.ResumeFromAds = true;
        _reviewManager = new Google.Play.Review.ReviewManager();
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != Google.Play.Review.ReviewErrorCode.NoError)
        {
            //Debug.Log(requestFlowOperation.Error.ToString());
            //GameManager.Instance.ActivePanelLoadingNetwork(false);
            Destroy(gameObject);
            yield break;
        }
        _playReviewInfo = requestFlowOperation.GetResult();

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null;
        if (launchFlowOperation.Error != Google.Play.Review.ReviewErrorCode.NoError)
        {
            //Debug.Log(requestFlowOperation.Error.ToString());
            //GameManager.Instance.ActivePanelLoadingNetwork(false);
            Destroy(gameObject);
            yield break;
        }
        //GameManager.Instance.ActivePanelLoadingNetwork(false);
    }
#endif
}
