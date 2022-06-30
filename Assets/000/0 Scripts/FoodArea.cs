using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodArea : MonoBehaviour, IInteractable
{
    [Header("References")] [Space] [SerializeField]
    private Activator activator;
    
    [Header("Settings")]
    [Space]
    private float _stayedTime;

    public static int indexStandHamburger = 0;
    public static int indexStandHotDog = 0;
    public static int indexStandIceCream = 0;
    
    [SerializeField] internal List<Transform> standPoints;

    public void OnEnter()
    {
        IndexResetWithActivatorID();
    }

    public void OnExit()
    {
        CanProduceWithActivatorID(true);
        StandCountResetWithActivatorID();
    }

    public void OnStay()
    {
        CanProduceWithActivatorID(false);
    }

    private void CanProduceWithActivatorID(bool state)
    {
        switch (activator.activatorId)
        {
            case 0:
                Hamburger.Instance.canProduce = state;
                break;
            case 1:
                HotDog.Instance.canProduce = state;
                break;
            case 2:
                IceCream.Instance.canProduce = state;
                break;
        }
    }
    private void IndexResetWithActivatorID()
    {
        switch (activator.activatorId)
        {
            case 0:
                indexStandHamburger = 0;
                break;
            case 1:
                indexStandHotDog = 0;
                break;
            case 2:
                indexStandIceCream = 0;
                break;
        }
    }
    private void StandCountResetWithActivatorID()
    {
        switch (activator.activatorId)
        {
            case 0:
                Hamburger.Instance.countSpawnedHamburger = 0;
                break;
            case 1:
                HotDog.Instance.countSpawnedHotDog = 0;
                break;
            case 2:
                IceCream.Instance.countSpawnedIceCream = 0;
                break;
        }
    }
}
