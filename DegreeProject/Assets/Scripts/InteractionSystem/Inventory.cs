using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public bool HasKey;

    private void Awake()
    {
        HasKey = false;
    }

    public void AddKey()
    {
        HasKey = true;
        Debug.Log("Key added");
    }

    public void RemoveKey()
    {
        HasKey = false;
    }

    public bool CheckKey()
    {
        return HasKey;
    }

    public void UseKey()
    {
        HasKey = false;
        Debug.Log("Key used");
    }

    private void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            HasKey = !HasKey;
            Debug.Log("Key: " + HasKey);
        }
    }


}
