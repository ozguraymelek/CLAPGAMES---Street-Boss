using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipFoodArea : MonoBehaviour,IInteractable
{
    [Header("References")] [Space] [SerializeField]
    private Activator activator;

    [SerializeField]
    private Chip chipRef;
    
    [Header("Settings")]
    [Space]
    private float _stayedTime;

    public int indexStandChip = 0;
    
    [SerializeField] internal List<Transform> standPoints;

    public void OnEnter()
    {
        UI_Manager.Instance.chipCount += chipRef.countSpawnedChip;
        UI_Manager.Instance.chipCountInfo.text = UI_Manager.Instance.chipCount.ToString();
        
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
        chipRef.canProduce = state;
    }
    private void IndexResetWithActivatorID()
    {
        indexStandChip = 0;
    }
    private void StandCountResetWithActivatorID()
    {
        chipRef.countSpawnedChip = 0;
    }
}
