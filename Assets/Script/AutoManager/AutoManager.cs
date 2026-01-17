using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AutoManager : MonoBehaviour
{
    public Button Auto;
    public Text ButtonAuto_Txt;

    public bool AutoOn;
    public bool AutoOff;
    public bool isTurnAutoDone;

    public UI_Battle m_UIBattle;

    private void Awake()
    {
        isTurnAutoDone = true;
        AutoOn = false;
        AutoOff = true;
        ButtonAuto_Txt.text = "Auto: Off";
        Auto.onClick.AddListener(OnClickButtonAuto);
    }

    public void OnClickButtonAuto()
    {
        if (!AutoOn)
        {
            AutoOn = true;
            AutoOff = false;
            ButtonAuto_Txt.text = "Auto: On";
        }
        else if (AutoOn)
        {
            AutoOff = true;
            AutoOn = false;
            ButtonAuto_Txt.text = "Auto: Off";
        }
    }
    private void Update()
    {
        if (AutoOn && !isTurnAutoDone && m_UIBattle.AllidBase.GetComponent<AllidBase>().HP > 0)
        {
            if(!m_UIBattle.CheckAttack)
            {
                StartCoroutine(IE_DelayAuto());
            }
           
        }
    }

    IEnumerator IE_DelayAuto()
    {
        yield return 0.5f;
        m_UIBattle.AllidBase.GetComponent<AllidBase>().ATTACK_ALLID();
        isTurnAutoDone = true;
        m_UIBattle.CheckAttack = true;
    }
}
