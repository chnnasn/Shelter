using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.name == "adventure") {

            if (GameManager.instance.newState == newState.NoLoding) {
                GameManager.instance.DispatchUi.SetActive(true);

            }

        }
        if (transform.name == "Set") {
            Debug.Log("ll");
        }
        if (transform.name == "BuCancel") {

            GameManager.instance.BuildUi.SetActive(false);
        }
        if (transform.name == "DisCancel") {

            GameManager.instance.DispatchUi.SetActive(false);

        }
        if (transform.name == "BuBuild") {

            GridManager.instance.grid.makeBuild(transform.parent.parent.localPosition);
            GameManager.instance.BuildUi.SetActive(false);
        }
        if (transform.name == "DisFin") {
            GameManager.instance.DispatchUi.SetActive(false);

            if (GameManager.instance.newState == newState.NoLoding)
            {
                GameManager.instance.newState = newState.startLoding;

            }
           
        }

        if (transform.name == "FinishBu") {
            GameManager.instance.addNewObeject();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
