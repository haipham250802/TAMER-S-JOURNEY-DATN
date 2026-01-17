using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CommanButton : MonoBehaviour
{
    private void OnEnable()
    {
        if (this.GetComponent<Button>())
            this.GetComponent<Button>().onClick.AddListener(SoundButton);
    }
    public void SoundButton()
    {
        if (gameObject != null)
            AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectButton);
    }
    private void OnDisable()
    {
        if (gameObject.transform.childCount > 0)
        {
            if (gameObject.transform.GetChild(0).gameObject != null)
                transform.GetChild(0).transform.localScale = Vector3.one;
        }
            
        if (this.GetComponent<Button>())
            this.GetComponent<Button>().onClick.RemoveListener(SoundButton);
    }
}
