using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnEnter();
    void OnStay();
    void OnExit();
}
public interface IActivatorDesk
{
    void OnEnter();
    void OnStay();
    void OnExit();
}
public interface IActivatorMoney
{
    void OnEnter();
    void OnStay(Prince prince);
    void OnExit();
}
public interface IStackable
{
    void Stack();
    void Scatter();
    void Change(Door.DoorType _doorType, GameSettings gameSettings);
}
public interface ICustomer
{
    void Check();
}