using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonEvent : MonoBehaviour, IPointerClickHandler
{
    private Dictionary<string, System.Action> buttonActions;

    void Awake()
    {
        InitializeButtonActions();
    }

    private void InitializeButtonActions()
    {
        buttonActions = new Dictionary<string, System.Action>
        {
            {"adventure", HandleAdventureButton},
            {"Set", () => { GameManager.instance.SetUi.SetActive(true); Time.timeScale = 0; }},
            {"BuCancel", () => GameManager.instance.BuildUi.SetActive(false)},
            {"DisCancel", () => GameManager.instance.DispatchUi.SetActive(false)},
            {"BuBuild", HandleBuildButton},
            {"DisFin", HandleDispatchFinishButton},
            {"FinishBu", () => GameManager.instance.addNewObeject()},
            {"Quit", Application.Quit},
            {"Continue", () => { Time.timeScale = 1; GameManager.instance.SetUi.SetActive(false); }}
        };
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        string buttonName = transform.name;
        if (buttonActions.TryGetValue(buttonName, out System.Action action))
        {
            action.Invoke();
        }
    }

    private void HandleAdventureButton()
    {
        if (GameManager.instance.newState == newState.NoLoding)
        {
            GameManager.instance.DispatchUi.SetActive(true);
        }
    }

    private void HandleBuildButton()
    {
        if (transform.GetComponent<Image>().color != Color.red)
        {
            GridManager.instance.grid.makeBuild(transform.parent.parent.localPosition);
        }
        GameManager.instance.BuildUi.SetActive(false);
    }

    private void HandleDispatchFinishButton()
    {
        GameManager.instance.DispatchUi.SetActive(false);
        if (GameManager.instance.newState == newState.NoLoding)
        {
            GameManager.instance.newState = newState.startLoding;
        }
    }
}