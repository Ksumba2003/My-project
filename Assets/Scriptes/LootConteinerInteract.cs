using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootConteinerInteract : Interactable
{
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openChest;
    [SerializeField] bool opend;

    public override void Interact(Character character)
    {
        if (opend == false)
        {
            opend = true;
            closedChest.SetActive(false);
            openChest.SetActive(true);
        }
        else
        {
            opend = false;
            closedChest.SetActive(true);
            openChest.SetActive(false);
        }
    }
}
