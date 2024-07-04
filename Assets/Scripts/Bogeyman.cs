using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bogeyman : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] int condition;
    [SerializeField] bool isBurn;
    [SerializeField] bool isWet;
    [SerializeField] bool isConditionChanged;
    private Color colorBurn = Color.red;
    private Color colorWet = Color.blue;
    private Material defaultMaterial;
    private Material bogeymanMaterial;

    // Start is called before the first frame update
    void Start()
    {
        defaultMaterial = GetComponent<Renderer>().material;
        ResetProperties();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetProperties();
        }
        UpdateColor();

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    isConditionChanged = true;
        //    Debug.Log("Key E was pressed");
        //}

        //if (isConditionChanged)
        //{
        //    UpdateColor(condition);
        //    isConditionChanged = false;
        //}

    }

    void ResetProperties()
    {
        hp = 1000;
        condition = 0;
        isBurn = false;
        isWet = false;
        bogeymanMaterial = defaultMaterial;
    }

    void UpdateColor()
    {
        float powerEffect = (float)condition / 100;
        if (condition > 0)
        {
            setBogeymanColor(Color.Lerp( defaultMaterial.color, Color.red, powerEffect));
        }
        else if (condition == 0)
        {
            setBogeymanColor(defaultMaterial.color);
            //bogeymanMaterial = defaultMaterial;
        }
        else
        {
            setBogeymanColor(Color.Lerp(defaultMaterial.color, Color.blue, -powerEffect));
            //bogeymanMaterial.color = defaultMaterial.color + new Color(0, 0, 0.4f);
        }

    }
    void setBogeymanColor(Color newColor)
    {
        GameObject[] childs = GameObject.FindGameObjectsWithTag("Bogeyman");
        foreach (var child in childs)
        {
            child.GetComponent<Renderer>().material.color = newColor;
        }
    }
}
