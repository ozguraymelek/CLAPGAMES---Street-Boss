using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamburgerFoodArea : MonoBehaviour, IInteractable
{
    [Header("References")] [Space] [SerializeField]
    private Activator activator;

    [SerializeField]
    private Hamburger hamburgerRef;
    
    [Header("Settings")]
    [Space]
    private float _stayedTime;

    public int indexStandHamburger = 0;
    
    [SerializeField] internal List<Transform> standPoints;

    public void OnEnter()
    {
        IndexResetWithActivatorID();
        StandCountResetWithActivatorID();
    }

    public void OnExit()
    {
        CanProduceWithActivatorID(true);
        StandCountResetWithActivatorID();
        IndexResetWithActivatorID();
    }

    public void OnStay()
    {
        CanProduceWithActivatorID(false);
    }

    private void CanProduceWithActivatorID(bool state)
    {
        hamburgerRef.canProduce = state;
    }
    private void IndexResetWithActivatorID()
    {
        indexStandHamburger = 0;
    }
    private void StandCountResetWithActivatorID()
    {
        hamburgerRef.countSpawnedHamburger = 0;
    }
}
