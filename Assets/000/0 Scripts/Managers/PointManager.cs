using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManager : Singleton<PointManager>
{
    [SerializeField] RectTransform canvasRectT;
    [SerializeField] GameObject[] pointPopUps;
    [SerializeField] int pointPopUpIndex = 0;

    public void PointPopUp(Vector3 _pos, int amountOfPoint)
    {
        pointPopUps[pointPopUpIndex].SetActive(true); // Going to false in ColorGradient_UI script
        pointPopUps[pointPopUpIndex].GetComponent<TextMeshProUGUI>().text = $"{amountOfPoint}";
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, _pos);
        pointPopUps[pointPopUpIndex].GetComponent<RectTransform>().anchoredPosition = screenPoint - new Vector2(Screen.width * .5f, Screen.height * .5f);

        StartCoroutine(Up(pointPopUps[pointPopUpIndex]));

        IncreasePopUpIndex();
    }
    void IncreasePopUpIndex()
    {
        pointPopUpIndex++;
        if (pointPopUpIndex == pointPopUps.Length)
            pointPopUpIndex = 0;
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