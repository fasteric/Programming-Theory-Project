using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact(PlayerInteraction playerInteraction)
    {
        Debug.Log(name + " is being interacted by " + playerInteraction.name);
    }
}
