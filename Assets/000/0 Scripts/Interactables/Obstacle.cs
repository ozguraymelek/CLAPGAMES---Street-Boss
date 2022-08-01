using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    [Header("References")]
    [Space]
    public CkyBehaviour behaviour;

    public Reklam ReklamScript;

    public void Start()
    {
        ReklamScript = GameObject.FindObjectOfType<Reklam>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.GetComponent<Prince>() != null) return;
        
        IStackable _interactable = other.GetComponent<IStackable>();
        
        if (_interactable == null) return;

        _interactable.Scatter();

        FindObjectOfType<CkyEvents>().OnInteractWithObstcleObject();

        ReklamScript = GameObject.FindObjectOfType<Reklam>();
        ReklamScript.showInterstitialAd();

        Destroy(gameObject);
    }
    public abstract void KnifeMovement();
    public abstract void SawMovement();
}