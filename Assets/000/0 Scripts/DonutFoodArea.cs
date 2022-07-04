using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutFoodArea : MonoBehaviour,IInteractable
{
    [Header("References")] [Space] [SerializeField]
    private Activator activator;

    [SerializeField]
    private Donut donutRef;
    
    [Header("Settings")]
    [Space]
    private float _stayedTime;

    public int indexStandDonut = 0;
    
    [SerializeField] internal List<Transform> standPoints;

    public void OnEnter()
    {
        UI_Manager.Instance.donutCount += donutRef.countSpawnedDonut;
        UI_Manager.Instance.donutCountInfo.text = UI_Manager.Instance.donutCount.ToString();
        
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
        donutRef.canProduce = state;
    }
    private void IndexResetWithActivatorID()
    {
        indexStandDonut = 0;
    }
    private void StandCountResetWithActivatorID()
    {
        donutRef.countSpawnedDonut = 0;
    }
}
