using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(GameObject player);

}
public interface IHit
{
    void Hit();
}