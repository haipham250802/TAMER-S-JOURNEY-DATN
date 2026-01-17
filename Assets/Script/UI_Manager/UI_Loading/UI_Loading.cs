using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using DG.Tweening;
using Spine.Unity;
public class UI_Loading : MonoBehaviour
{
    public Image Fill;
    public SkeletonGraphic skeleton1;
    public SkeletonGraphic skeleton2;
    public SkeletonGraphic skeleton3;

    public GameObject BG_Parent;
    public GameObject BGMove;

    public Transform StartPosBG;
    public Transform EndPosBG;

    public bool isMoveBGDone = true;


    private static UI_Loading instance;
    public static UI_Loading Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UI_Loading();
            }
            return instance;
        }
    }


    const int MAX_VALUE = 100;
    float value;

    //   public Slider slider_Loading;
    public Text NumLoading_txt;

    public Button TestBtn;
    private void Awake()
    {
        instance = this;
        TestBtn.onClick.AddListener(Loading);
        isMoveBGDone = false;
        BG_Parent.transform.position = StartPosBG.position;
    }
    private void OnEnable()
    {
        StartCoroutine(IE_delayLoad());
    }
    IEnumerator IE_delayLoad()
    {
        yield return null;
        isMoveBGDone = false;
        BG_Parent.transform.position = StartPosBG.position;
        DontDestroyOnLoad(gameObject);
        skeleton1.AnimationState.SetAnimation(0, "Idle", true);
        skeleton2.AnimationState.SetAnimation(0, "Idle", true);
        skeleton3.AnimationState.SetAnimation(0, "Idle", true);

        isMoveBGDone = false;
        BG_Parent.transform.position = StartPosBG.position;
    }
    private void Update()
    {
        if (!isMoveBGDone)
        {
            MoveBackGround(BG_Parent.transform);
            isMoveBGDone = true;
        }
       
    }
    private void Start()
    {
        SCalePlayerX = player.Instance.transform.localScale.x;
        SCalePlayerY = player.Instance.transform.localScale.y;
        Loading();
       
    }
    Tweener twenner;
    void KillTween()
    {
        twenner.Kill();
    }
    public void MoveBackGround(Transform BG)
    {
        twenner = BG.DOMove(EndPosBG.position, 60).SetEase(Ease.Linear).OnComplete(() =>
        {
            BG.position = StartPosBG.position;
            isMoveBGDone = false;
        });
    }

    public void Loading()
    {
        value = 0;
        //  slider_Loading.value = value;
        //  slider_Loading.maxValue = MAX_VALUE;

        Fill.fillAmount = value / 100;
        StartCoroutine(IE_Loading());
    }
    public float SCalePlayerX;
    public float SCalePlayerY;
    IEnumerator IE_Loading()
    {
        player.Instance.transform.localScale = Vector3.zero;
        // player.Instance.transform.localScale = Vector3.zero;

        while (value < 20)
        {
            yield return new WaitForSeconds(0.0002f);
            value++;
            NumLoading_txt.text = "Loading " + value.ToString() + "%";
            // slider_Loading.value = value;
            Fill.fillAmount = value / 100;
        }
        yield return new WaitForSeconds(0.5f);
        while (value < 75)
        {
            yield return new WaitForSeconds(0.0005f);
            value++;
            NumLoading_txt.text = "Loading " + value.ToString() + "%";
            // slider_Loading.value = value;
            Fill.fillAmount = value / 100;
        }
        yield return new WaitForSeconds(0.2f);
        while (value < MAX_VALUE)
        {
            yield return new WaitForSeconds(0.0005f);
            value++;
            NumLoading_txt.text = "Loading " + value.ToString() + "%";
            //   slider_Loading.value = value;
            Fill.fillAmount = value / 100;
            if (value >= MAX_VALUE)
            {
                //  slider_Loading.value = 0;
                Fill.fillAmount = 0;

                NumLoading_txt.text = "Loading " + "0%";
                Debug.LogWarning("Bat len");
                Controller.Instance.ApppearFx.transform.position = player.Instance.transform.position;
                Controller.Instance.ApppearFx.SetActive(true);
                AudioManager.Instance.PlaySound(AudioManager.instance.SoundAppearCharacter);
                Controller.Instance.ApppearFx.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "Appear", false);
                // Controller.Instance.ApppearFx.GetComponent<SkeletonAnimation>().AnimationState.Start += StartAppearPlayer;

                Controller.Instance.ApppearFx.GetComponent<SkeletonAnimation>().AnimationState.Complete += HiddenEffectAppearPlayer;
                isActivePlayer = true;

                this.gameObject.SetActive(false);
            }
        }
    }
    public bool isActivePlayer;
    IEnumerator IE_DelayActivePlayer()
    {
        yield return new WaitForSeconds(0.2f);
        // Controller.Instance.ApppearFx.GetComponent<SkeletonAnimation>().AnimationState.Start -= StartAppearPlayer;
    }
    void HiddenEffectAppearPlayer(Spine.TrackEntry tracks)
    {
        Controller.Instance.ApppearFx.GetComponent<SkeletonAnimation>().AnimationState.Complete -= HiddenEffectAppearPlayer;
        if (tracks != null)
        {
            if (tracks.Animation.Name == "Appear")
            {
                Controller.Instance.ApppearFx.SetActive(false);
                //   player.Instance.transform.localScale = new Vector3((SCalePlayerX), SCalePlayerY, 0);
            }
        }
    }
    private void OnDisable()
    {
        BagManager.Instance.m_RuleController.MoveDoorAtStartGame();
    }
}
