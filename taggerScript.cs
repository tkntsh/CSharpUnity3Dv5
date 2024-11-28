using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class taggerScript : MonoBehaviour
{
    public Transform select_object;
    public RaycastHit touch_object;
    public Transform outline_object;
    private Color originalColor;

    public Text selectionText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Select
        if (Input.GetMouseButtonDown(0))
        {
            if (outline_object)
            {
                if (select_object != null)
                { 
                    select_object.gameObject.GetComponent<Outline>().enabled = false;
                    select_object.gameObject.GetComponent<Renderer>().material.color = originalColor;
                    selectionText.text = "";
                }
                select_object = touch_object.transform;
                select_object.gameObject.GetComponent<Outline>().enabled = true;
                originalColor = select_object.gameObject.GetComponent<Renderer>().material.color;
                outline_object = null;
                if(select_object != null)
                {
                    selectionText.text = select_object.name;
                }
            }
            else
            {
                if (select_object)
                {
                    select_object.gameObject.GetComponent<Outline>().enabled = false;
                    select_object.gameObject.GetComponent<Renderer>().material.color = originalColor;
                    selectionText.text = "";
                    select_object = null;
                }
            }
        }

        //outline
        if (outline_object != null)
        {
            outline_object.gameObject.GetComponent<Outline>().enabled = false;
            outline_object.gameObject.GetComponent<Renderer>().material.color = originalColor;
            outline_object = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out touch_object))
        {
            outline_object = touch_object.transform;
            if (outline_object.CompareTag("OutlineObject") && outline_object != select_object)
            {
                if (outline_object.gameObject.GetComponent<Outline>() != null)
                {
                    outline_object.gameObject.GetComponent<Outline>().enabled = true;
                    outline_object.gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
                else
                {
                    Outline outline = outline_object.gameObject.AddComponent<Outline>();
                    outline.enabled = true;

                    outline_object.gameObject.GetComponent<Outline>().OutlineColor = Color.red;
                    outline_object.gameObject.GetComponent<Outline>().OutlineWidth = 8.0f;
                    outline_object.gameObject.GetComponent<Renderer>().material.color = Color.red;

                    selectionText.text = outline_object.gameObject.name;
                    Debug.Log(outline_object.gameObject.name);
                    print(outline_object.gameObject.name);
                }
                if(select_object != null)
                {
                    selectionText.text = outline_object.gameObject.name;
                    Debug.Log(outline_object.gameObject.name);
                    print(outline_object.gameObject.name);
                }
            }
            else
            {
                outline_object = null;
            }
        }
        else
        {
            if (select_object == null)
            {
                selectionText.text = "";
            }
        }
    }
}
