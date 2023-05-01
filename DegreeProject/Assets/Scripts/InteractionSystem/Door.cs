using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
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
            inventory.UseKey();
            OpenDoor();
            return true;
        }

        Debug.Log("Need key to open door");
        return false;

    }

    public void OpenDoor()
    {
        Debug.Log("Door opened");
        Destroy(gameObject);
    }

}
