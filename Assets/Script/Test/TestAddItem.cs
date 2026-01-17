using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAddItem : MonoBehaviour
{
    public InputField inputField;
    public InputField inputField2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Add()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            DataPlayer.Log();
            return;
        }
        int Str = int.Parse(inputField.text);
        DataPlayer.Add((ECharacterType)Str);
        DataPlayer.AddCritter((ECharacterType)Str);
    }
    public void RemoveElemt()
    {
        int Str = int.Parse(inputField.text);
        int Str2 = int.Parse(inputField2.text);
        DataPlayer.Remove((ECharacterType)Str, Str2);
    }
}
