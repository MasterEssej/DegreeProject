using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBox : MonoBehaviour, IInteractable
{

    [SerializeField] private string interactionPrompt;
    public string InteractionPrompt => interactionPrompt;

    public bool Interact(Interactor interactor)
    {

        var inventory = interactor.GetComponent<Inventory>();

        if(inventory == null)
        {
            Debug.LogError("No Inventory found on Interactor");
            return false;
        }
        if(inventory.HasKey)
        {
            Debug.Log("You already have a key");
            return false;
        }
        else if(!inventory.HasKey)
        {
            inventory.AddKey();
            return true;
        }

        return false;

    }
}
