using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamFoodArea : MonoBehaviour ,IInteractable
{
    [Header("References")] [Space] [SerializeField]
    private Activator activator;

    [SerializeField]
    private IceCream iceCreamRef;
    
    [Header("Settings")]
    [Space]
    private float _stayedTime;

    public int indexStandIceCream = 0;
    
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
        iceCreamRef.canProduce = state;
    }
    private void IndexResetWithActivatorID()
    {
        indexStandIceCream = 0;
    }
    private void StandCountResetWithActivatorID()
    {
        iceCreamRef.countSpawnedIceCream = 0;
    }
}
