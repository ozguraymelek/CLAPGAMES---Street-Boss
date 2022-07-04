using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotDogFoodArea : MonoBehaviour, IInteractable
{
    [Header("References")] [Space] [SerializeField]
    private Activator activator;
    
    [SerializeField]
    private HotDog hotDogRef;
    
    [Header("Settings")]
    [Space]
    private float _stayedTime;

    public int indexStandHotDog = 0;
    
    [SerializeField] internal List<Transform> standPoints;

    public void OnEnter()
    {
        UI_Manager.Instance.hotdogCount += hotDogRef.countSpawnedHotDog;
        UI_Manager.Instance.hotdogCountInfo.text = UI_Manager.Instance.hotdogCount.ToString();
        
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
        hotDogRef.canProduce = state;
    }
    private void IndexResetWithActivatorID()
    {
        indexStandHotDog = 0;
    }
    private void StandCountResetWithActivatorID()
    {
        hotDogRef.countSpawnedHotDog = 0;
    }
}
