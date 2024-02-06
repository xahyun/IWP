using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class Agent : MonoBehaviour
{
    //private AgentAnimations agentAnimations;
    private AgentMover agentMover;

    public Transform playerpos;

    //private WeaponParent weaponParent;

    healthManeger healthMan;
    Rigidbody2D rb2d;
    [SerializeField] int dmg;
    private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerpos = GameObject.FindGameObjectWithTag("Player").transform;
        healthMan = GetComponent<healthManeger>();
    }

    private void Update()
    {
        //pointerInput = GetPointerInput();
        //movementInput = movement.action.ReadValue<Vector2>().normalized;

        agentMover.MovementInput = MovementInput;
        //weaponParent.PointerPosition = pointerInput;
        AnimateCharacter();
    }

    public void PerformAttack()
    {
        Vector3 diff = transform.position - playerpos.position;
        HealthHeartBar.hb.DetuctFromHealth(dmg, diff*500);


    }

    private void Awake()
    {
        //agentAnimations = GetComponentInChildren<AgentAnimations>();
        //weaponParent = GetComponentInChildren<WeaponParent>();
        agentMover = GetComponent<AgentMover>();
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
       //agentAnimations.RotateToPointer(lookDirection);
       //agentAnimations.PlayAnimation(MovementInput);
    }

    private void OnMouseDown()
    {
        Item item = InventoryManager.instance.GetSelectedItem(false);
        if (Vector3.Distance(playerpos.position , transform.position ) < item.range.x)
        {
            healthMan.health -= item.damage;
            Vector3 diff = transform.position - playerpos.position;
            rb2d.AddForce(diff * 500);

            audioManeger.ins.PlayAudio(4);


            Debug.Log(diff);
        }
    }



}