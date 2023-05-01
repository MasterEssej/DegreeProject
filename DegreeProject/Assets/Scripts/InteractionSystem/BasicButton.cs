using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicButton : MonoBehaviour, IInteractable
{

    [SerializeField] private string interactionPrompt;
    public string InteractionPrompt => interactionPrompt;

    [SerializeField] private GameObject doorToOpen;

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
            Debug.Log("Interacting with button");
            if(doorToOpen == null)
            {
                Debug.Log("No door to open");
                return false;
            }
            inventory.UseKey();
            Debug.Log("Key used");
            doorToOpen.GetComponent<HeavyDoor>().OpenDoor();
            return true;
        }

        Debug.Log("Need key to interact with button");
        return false;

    }
}
