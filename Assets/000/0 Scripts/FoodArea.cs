using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodArea : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [Space]
    private float _stayedTime;

    void SetList()
    {
        foreach (Transform hamburger in StackManager.Instance.collectedHamburgers)
        {
            hamburger.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    public void OnEnter()
    {
        Hamburger.Instance.hamburgerLevel1PosY = 0;
        Hamburger.Instance.hamburgerLevel2PosY = 0;
        Hamburger.Instance.hamburgerLevel3PosY = 0;
        Hamburger.Instance.spawnTime = 0;
    }

    public void OnExit()
    {
        
        SetList();
        Hamburger.Instance.canProduce = true;
    }

    public void OnStay()
    {
        Hamburger.Instance.canProduce = false;
        
        StackManager.Instance.Throw(FindObjectOfType<CkyBehaviour>());
    }
}
