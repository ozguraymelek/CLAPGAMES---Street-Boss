using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Player Settings",menuName ="Scriptable Objects/Player/Create a new")]
public class PlayerSettings : ScriptableObject
{
    public int playerMoney;
    public int takedDeck;
}
