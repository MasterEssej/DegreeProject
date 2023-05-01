using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyDoor : MonoBehaviour, IInteractable
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

        Debug.Log("Can not open this door with key");
        return false;

    }

    public void OpenDoor()
    {
        Debug.Log("Door opened");
        Destroy(gameObject);
    }

}
