using System.Collections;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] TextMeshProUGUI textSelectHand;
    [SerializeField] float speed = 10f;

    bool playerInTrigger = false;
    string triggerTag = "";

    Hand leftHand = new Hand();
    Hand rightHand = new Hand();
    public class Hand
    {
        public bool isBusy = false;
        public GameObject weapon;
    }

    private void Start()
    {

    }
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            characterController.Move(direction * speed * Time.deltaTime);
        }
        SelectingWeapon();
    }

    private void OnTriggerEnter(Collider weapon)
    {
        Debug.Log("Collide with " + weapon.name);
        textSelectHand.gameObject.SetActive(true);
        playerInTrigger = true;
        triggerTag = weapon.tag;
        Debug.Log("triggerTag = " + triggerTag);
    }

    private void OnTriggerExit(Collider weapon)
    {
        textSelectHand.gameObject.SetActive(false);
        playerInTrigger = false;
        triggerTag = "";
    }
    void SelectingWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UsingHand(ref leftHand, new Vector3(-1.35f, -1f, 0f));
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UsingHand(ref rightHand, new Vector3(1.35f, -1f, 0f));
        }
    }

    void UsingHand(ref Hand hand, Vector3 weaponPosition)
    {
        if (hand.isBusy)
        {
            Destroy(hand.weapon);
            hand.isBusy = false;
        }
        else if (playerInTrigger)
        {
            hand.weapon = Instantiate(GameObject.FindGameObjectWithTag(triggerTag), transform, false);
            hand.weapon.GetComponent<Collider>().enabled = false;
            hand.weapon.transform.localPosition = weaponPosition;
            hand.weapon.transform.localScale *= 0.5f;
            hand.isBusy = true;
        }
        
    }
}


