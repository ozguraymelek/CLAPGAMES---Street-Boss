using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornFoodArea : MonoBehaviour,IInteractable
{
    [Header("References")] [Space] [SerializeField]
    private Activator activator;

    [SerializeField]
    private Popcorn popcornRef;
    
    [Header("Settings")]
    [Space]
    private float _stayedTime;

    public int indexStandPopcorn = 0;
    
    [SerializeField] internal List<Transform> standPoints;

    public void OnEnter()
    {
        UI_Manager.Instance.popcornCount += popcornRef.countSpawnedPopcorn;
        UI_Manager.Instance.popcornCountInfo.text = UI_Manager.Instance.popcornCount.ToString();
        
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
        popcornRef.canProduce = state;
    }
    private void IndexResetWithActivatorID()
    {
        indexStandPopcorn = 0;
    }
    private void StandCountResetWithActivatorID()
    {
        popcornRef.countSpawnedPopcorn = 0;
    }
}
