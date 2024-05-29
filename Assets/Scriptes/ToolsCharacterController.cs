using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsCharacterController : MonoBehaviour
{
    CharacterController2D character;
    Rigidbody2D rgbd2d;
    ToolBarController toolbarController;
    Animator animator;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] private float sizeOfInteractebleArea = 1.2f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] float mixDistance = 1.5f;


    Vector3Int selectedTilePosition;
    bool selectable;

    private void Awake()
    {
        character = GetComponent<CharacterController2D>();
        rgbd2d = GetComponent<Rigidbody2D>();
        toolbarController = GetComponent<ToolBarController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SelectTile();
        CanSelectCheck();
        Marker();
        if (Input.GetMouseButtonDown(0))
        {
            if (UseToolWorld() == true)
            {
                return;
            }
            UseToolGrid();
        }
    }

    private void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    void CanSelectCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < mixDistance;
        markerManager.Show(selectable);

    }
    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
    }

    private bool UseToolWorld()
    {
        Vector2 position = rgbd2d.position + character.lastMotionVector * offsetDistance;

        Item item = toolbarController.GetItem;
        if (item == null) { return false; }
        if (item.onAction == null) { return false; }
        if (item.Name == "Axe")
        {
            animator.SetTrigger("act_axe");
        }

        if (item.Name == "Pickaxe")
        {
            animator.SetTrigger("act_pickaxe");
        }
        
        bool complete = item.onAction.OnApply(position);

        if (complete == true)
        {
            if (item.onItemUsed != null)
            {
                item.onItemUsed.OnItemUsed(item, GameManager.Instance.inventoryContainer);
            }
        }

        return complete;
    }

    private void UseToolGrid()
    {
        Debug.Log("Using tool");

        if (selectable == true)
        {
            Debug.Log("selectable");
            Item item = toolbarController.GetItem;
            if (item == null) { return; }
            if (item.onTileMapAction == null) { return; }
            Debug.Log(item);
            animator.SetTrigger("act_pickaxe");
            bool complete = item.onTileMapAction.OnApplyToTileMap(
                selectedTilePosition, 
                tileMapReadController, 
                item
                );

            if(complete == true)
            {
                if(item.onItemUsed != null)
                {
                    item.onItemUsed.OnItemUsed(item, GameManager.Instance.inventoryContainer);
                }
            }
        }
    }
}
