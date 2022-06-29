using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonies : MonoBehaviour
{
    [SerializeField] Transform moniesParent;
    [SerializeField] GameObject moneyPrefab;

    private void Start()
    {
        for(int i = 0; i < 150; i++)
        {
            GameObject money = Instantiate(moneyPrefab, moniesParent);
            money.transform.localPosition = Vector3.zero;
            money.transform.localEulerAngles = Vector3.zero;
            money.transform.localScale = Vector3.one;
            StackManager.Instance.moneys.Add(money.transform);
        }
    }
}
