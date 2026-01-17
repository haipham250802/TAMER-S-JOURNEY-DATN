using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CritterSelectMapElement : MonoBehaviour
{
    public Text HP;
    public Text Rarity;
    public Text Attack;
    public Image Avatar;
    public ECharacterType CharacterType;

    public void HiddenAvatar()
    {
        Avatar.color = Color.black;
    }
    public void ActiveAvatar()
    {
        Avatar.color = Color.white;
    }
    public void SetData(ECharacterType type)
    {
        this.CharacterType = type;
        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(type);
        Avatar.sprite = enemyStat.Avatar;
        Rarity.text = enemyStat.Rarity.ToString();
        HP.text = Controller.Instance.enemyData.GetHPEmemy(CharacterType).ToString();
        Attack.text = Controller.Instance.enemyData.GetDamageEnemy(CharacterType).ToString();
    }
}
