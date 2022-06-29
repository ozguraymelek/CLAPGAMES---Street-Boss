using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DamagePopUpManager : Singleton<DamagePopUpManager>
{
    [SerializeField] RectTransform canvasRectT;
    [SerializeField] GameObject[] damagePopUps;
    [SerializeField] int damagePopUpIndex = 0;

    public void DamagePopUp(Transform tr, int amountOfDamage)
    {
        damagePopUps[damagePopUpIndex].SetActive(true); // Going to false in ColorGradient_UI script
        damagePopUps[damagePopUpIndex].GetComponent<TextMeshProUGUI>().text = $"{amountOfDamage}";
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, tr.position);
        damagePopUps[damagePopUpIndex].GetComponent<RectTransform>().anchoredPosition = screenPoint - new Vector2(Screen.width * .5f, Screen.height * .5f);

        StartCoroutine(Up(damagePopUps[damagePopUpIndex]));

        IncreasePopUpIndex();
    }
    void IncreasePopUpIndex()
    {
        damagePopUpIndex++;
        if (damagePopUpIndex == damagePopUps.Length)
            damagePopUpIndex = 0;
    }

    IEnumerator Up(GameObject popUp)
    {
        bool isUpdating = true;
        float t = 0;
        while (isUpdating)
        {
            t += Time.deltaTime;
            popUp.transform.position += Vector3.up * 2;

            if (t > 1)
            {
                isUpdating = false;
            }

            yield return null;
        }
    }

}