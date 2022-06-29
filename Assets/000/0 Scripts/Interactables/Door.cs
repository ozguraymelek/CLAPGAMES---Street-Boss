using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameSettings _gameSettings;
    public enum DoorType { Hamburger, HotDog, IceCream, Chips, Donut, Popcorn }
    [SerializeField] DoorType doorType;

    private BoxCollider _coll;
    

    private void OnTriggerEnter(Collider other)
    {
        IStackable _stackable = other.GetComponent<IStackable>();

        if (_stackable == null) return;

        if (other.GetComponent<Food>().isOnList == false) return;

        Destroy(_coll);

        _stackable.Change(doorType, _gameSettings);

        transform.DOLocalMoveY(-3, 1).SetEase(Ease.InBounce);
    }
}