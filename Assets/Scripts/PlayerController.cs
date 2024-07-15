using System.Collections;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;
using System;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] TextMeshProUGUI textSelectHand;
    [SerializeField] float speed = 10f;
    [SerializeField] GameObject waterBall;
    [SerializeField] GameObject fireBall;

    bool playerInTrigger = false;
    string triggerTag = "";

    public class Hand
    {
        public bool isBusy = false;
        public GameObject weapon;
        public string weaponName;
    }
    Hand leftHand = new Hand();
    Hand rightHand = new Hand();


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

        if (Input.GetMouseButtonDown(0))
        {
            UsingWeapon(ref leftHand);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            UsingWeapon(ref rightHand);
        }
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

    /// <summary>
    /// Tracks key pressing when selecting or deselecting a hand
    /// </summary>
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

    /// <summary>
    /// Initiates Hand object or destroyed it
    /// </summary>
    /// <param name="hand">Left or right hand object</param>
    /// <param name="weaponPosition">Position of weapon on Player body</param>
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
            hand.weaponName = triggerTag;
            hand.isBusy = true;
        }

    }

    void UsingWeapon(ref Hand hand)
    {
        switch (hand.weaponName)
        {
            case "Rock_water":
                {
                    Instantiate(waterBall, hand.weapon.transform.position, hand.weapon.transform.rotation);

                }; break;
            case "Rock_fire":
                {
                    Instantiate(fireBall, hand.weapon.transform.position, hand.weapon.transform.rotation);

                }; break;
        }
    }
}


