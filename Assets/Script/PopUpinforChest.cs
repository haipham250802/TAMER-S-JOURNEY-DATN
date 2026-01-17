using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUpinforChest : MonoBehaviour
{
    public Button ExitButton;

    private void Awake()
    {
        ExitButton.onClick.AddListener(OnClickButtonExit);
    }
    void OnClickButtonExit()
    {
        gameObject.SetActive(false);
    }
}
