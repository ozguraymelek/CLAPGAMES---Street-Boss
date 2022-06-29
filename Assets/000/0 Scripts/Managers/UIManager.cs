using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;


[System.Serializable]
public struct IconHolder
{
    public GameObject iconGO;
    public TextMeshProUGUI TMP;
    public int quantity;
}


public class UIManager : Singleton<UIManager>
{
    public enum IconTypes { ShieldIcon, BowIcon, SwordIcon, SpearIcon, HorseIcon }

    [SerializeField] IconHolder shield, bow, sword, spear, horse;

    private void Start()
    {
        UpdateIconTMP(shield);
        UpdateIconTMP(bow);
        UpdateIconTMP(sword);
        UpdateIconTMP(spear);
        UpdateIconTMP(horse);
    }

    public void IncreaseQuantityOf(IconTypes _iconType)
    {
        switch (_iconType)
        {
            case IconTypes.ShieldIcon:
                shield.quantity++;
                UpdateUI_Icon(shield);
                break;

            case IconTypes.BowIcon:
                bow.quantity++;
                UpdateUI_Icon(bow);
                break;

            case IconTypes.SwordIcon:
                sword.quantity++;
                UpdateUI_Icon(sword);
                break;

            case IconTypes.SpearIcon:
                spear.quantity++;
                UpdateUI_Icon(spear);
                break;

            case IconTypes.HorseIcon:
                horse.quantity++;
                UpdateIconTMP(horse);
                break;
        }
    }

    public void DecreaseQuantityOf(IconTypes _iconType)
    {
        switch (_iconType)
        {
            case IconTypes.ShieldIcon:
                shield.quantity--;
                UpdateUI_Icon(shield);
                break;

            case IconTypes.BowIcon:
                bow.quantity--;
                UpdateUI_Icon(bow);
                break;

            case IconTypes.SwordIcon:
                sword.quantity--;
                UpdateUI_Icon(sword);
                break;

            case IconTypes.SpearIcon:
                spear.quantity--;
                UpdateUI_Icon(spear);
                break;

            case IconTypes.HorseIcon:
                horse.quantity--;
                UpdateUI_Icon(horse);
                break;
        }
    }

    private void UpdateUI_Icon(IconHolder iconHolder)
    {
        //iconHolder.iconGO.transform.localScale = Vector3.one;

        iconHolder.iconGO.transform.DOScale(Vector3.one * 1.25f, 0.3f).SetEase(Ease.InOutSine).OnComplete(() => iconHolder.iconGO.transform.DOScale(Vector3.one, 0.3f));

        UpdateIconTMP(iconHolder);
    }

    private void UpdateIconTMP(IconHolder iconHolder)
    {
        iconHolder.TMP.text = iconHolder.quantity.ToString();
    }
}