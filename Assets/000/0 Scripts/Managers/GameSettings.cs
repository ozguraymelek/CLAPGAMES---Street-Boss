using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Scriptable Objects/Game/New Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Player")]
    [Space]
    public float speed = 400;
    public float rotationSpeed = 7;
    //public float visDist = 25;
    //public float visAngle = 120;

    [Header("Camera")]
    [Space]
    public float cameraTrasitionTime = 1.5f;

    [Header("Building Indexes")]
    public int hamburgerBuildingIndex;
    public int hotdogbuildingIndex;
    public int iceCreambuildingIndex;
    public int chipsbuildingIndex;
    public int donutbuildingIndex;
    public int popcornbuildingIndex;

    [Header("Hamburger Prizes")]
    [Space]
    public int hamburgerPrizeLVL0;
    public int hamburgerPrizeLVL1;
    public int hamburgerPrizeLVL2;
    public int hamburgerPrizeLVL3;

    [Header("HotDog Prizes")]
    [Space]
    public int hotDogPrizeLVL0;
    public int hotDogPrizeLVL2;
    public int hotDogPrizeLVL3;
    public int hotDogPrizeLVL1;

    [Header("IceCream Prize")]
    [Space]
    public int iceCreamPrizeLVL0;
    public int iceCreamPrizeLVL1;
    public int iceCreamPrizeLVL2;
    public int iceCreamPrizeLVL3;

    [Header("Donut Prize")]
    [Space]
    public int donutPrizeLVL0;
    public int donutPrizeLVL1;
    public int donutPrizeLVL2;
    public int donutPrizeLVL3;

    [Header("Popcorn Prize")]
    [Space]
    public int popcornPrizeLVL0;
    public int popcornPrizeLVL1;
    public int popcornPrizeLVL2;
    public int popcornPrizeLVL3;

    [Header("Chip Prize")]
    [Space]
    public int chipPrizeLVL0;
    public int chipPrizeLVL1;
    public int chipPrizeLVL2;
    public int chipPrizeLVL3;

}