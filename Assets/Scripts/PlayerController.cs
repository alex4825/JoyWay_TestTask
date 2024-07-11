using System.Collections;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] TextMeshProUGUI textSelectHand;
    [SerializeField] float speed = 10f;

    GameObject leftHandWeapon;
    GameObject rightHandWeapon;

    //[SerializeField] GameObject gun;
    //[SerializeField] GameObject rock_water;
    //[SerializeField] GameObject rock_fire;

    bool isLeftHandBusy = false;
    bool isRightHandBusy = false;
    bool playerInTrigger = false;
    string triggerTag = "";

    /*bool haveGun = false;
    bool haveRockWater = false;
    bool haveRockFire = false;
    bool isLeftHandFree = true;
    bool isRightHandFree = true;
    string leftHandWeapon = "Empty weapon";
    string rightHandWeapon = "Empty weapon";*/
    private void Start()
    {

    }
    // Update is called once per frame
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
        /*if (playerInTrigger)
        {
            SelectingWeapon();
        }*/

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
            if (isLeftHandBusy)
            {
                Destroy(leftHandWeapon);
                isLeftHandBusy = false;
            }
            else if (playerInTrigger)
            {
                leftHandWeapon = Instantiate(GameObject.FindGameObjectWithTag(triggerTag), transform, false);
                leftHandWeapon.GetComponent<Collider>().enabled = false;
                leftHandWeapon.transform.localPosition = new Vector3(-1.35f, -1f, 0f);
                leftHandWeapon.transform.localScale *= 0.5f;
                isLeftHandBusy = true;
            }

            /*switch (triggerTag)
            {
                case "Gun": leftHandWeapon = Instantiate(gun); break;
                case "Rock_water": leftHandWeapon = Instantiate(rock_water); break;
                case "Rock_fire": leftHandWeapon = Instantiate(rock_fire); break;
            }
            leftHandWeapon = Instantiate(GameObject.FindGameObjectWithTag(weaponTag));
            SpawnWeapon(leftHandWeapon);*/
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isRightHandBusy)
            {
                Destroy(rightHandWeapon);
                isRightHandBusy = false;
            }
            else if (playerInTrigger)
            {
                rightHandWeapon = Instantiate(GameObject.FindGameObjectWithTag(triggerTag), transform, false);
                rightHandWeapon.GetComponent<Collider>().enabled = false;
                rightHandWeapon.transform.localPosition = new Vector3(1.35f, -1f, 0f);
                rightHandWeapon.transform.localScale *= 0.5f;
                isRightHandBusy = true;
            }
        }
    }
    /*if (Input.GetKeyDown(KeyCode.Q))
    {
        if (isLeftHandFree)
        {
            leftHandWeapon = weapon;
            isLeftHandFree = false;
        }
        else
        {
            leftHandWeapon = "Empty weapon";
            isLeftHandFree = true;
        }
        Debug.Log("In left hand " + leftHandWeapon);
    }
    if (Input.GetKeyDown(KeyCode.E))
    {
        if (isRightHandFree)
        {
            rightHandWeapon = weapon;
            isRightHandFree = false;
        }
        else
        {
            rightHandWeapon = "Empty weapon";
            isRightHandFree = true;
        }
        Debug.Log("In right hand " + rightHandWeapon);
    }*/


    //void SpawnWeapon(GameObject weapon)
    //{
    //    //spawn weapon like a child of the Player
    //    Instantiate(weapon, gameObject.transform);
    //}

}
