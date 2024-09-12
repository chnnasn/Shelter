// GameManager.cs
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum newState
{
    Loding,
    startLoding,
    NoLoding,
    StopLoding
}

public enum timerState { 
    TimerIng,
    NoIng

}

public class GameManager : MonoBehaviour
{[HideInInspector]
    public GameObject DispatchUi;
    [HideInInspector]
    public GameObject FinishUi;
    [HideInInspector]
    public GameObject BuildUi;
    [HideInInspector]
    public Text food;
    [HideInInspector]
    public Text wood;
    [HideInInspector]
    public Text rock;
    [HideInInspector]
    public Slider stay;

    public int woodNum;
    public int RockNum;
    public int pepleNum;
    public int foodNUm;
    [HideInInspector]
    public newState newState;
    [HideInInspector]
    public timerState timerState;

    public static GameManager instance;
    [HideInInspector]
    public static int NewDays = 1;

    public static int MaxOut;



    public static event Action<int> EventWithLoding; // 定义带参数的事件
                                                               
    public static event Action<int> EventWithIng; // 定义带参数的事件


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        timerState = timerState.NoIng;
        newState = newState.NoLoding;
        stay.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void Update()
    {

        if (timerState == timerState.NoIng) {

            StartCoroutine(AfterIng(NewDays));

            timerState = timerState.TimerIng;
        }


        if (newState == newState.startLoding)
        {
            int peple = (int)(stay.value * pepleNum);

            if (peple != 0 && peple <= pepleNum && peple * 200 <= foodNUm)
            {

                pepleNum -= peple;
                foodNUm -= peple * 200;
                GetNewObejects(peple);
                StartCoroutine(AfterLoding(peple));
                stay.value = 0;

                newState = newState.Loding;

            }
            else
            {
                stay.value = 0;
                newState = newState.NoLoding;

            }

        }

        if (newState == newState.StopLoding)
        {

            newState = newState.NoLoding;
            FinishUi.SetActive(true);

        }

        food.text = foodNUm.ToString();
        wood.text = woodNum.ToString();
        rock.text = RockNum.ToString();
    }

    void OnSliderValueChanged(float value)
    {
        if (pepleNum <= 29)
        {
            int peple = (int)(value * pepleNum);
            stay.transform.GetChild(3).GetComponent<Text>().text = "派遣人数：" + peple;
            stay.transform.GetChild(4).GetComponent<Text>().text = "需要消耗食物：" + (peple * 200);
        }
        else {

            int peple = (int)(value * 29);
            stay.transform.GetChild(3).GetComponent<Text>().text = "派遣人数：" + peple;
            stay.transform.GetChild(4).GetComponent<Text>().text = "需要消耗食物：" + (peple * 200);
        }
      
       
    }

    private IEnumerator AfterLoding(int x)
    {

        yield return null;

        // Trigger the event with a parameter
        EventWithLoding?.Invoke(x * 8 + 50); // 传递秒数

    }

    private IEnumerator AfterIng(int x)
    {
        yield return null;

        // Trigger the event with a parameter
        EventWithIng?.Invoke(x); // 传递秒数

    }
    int WodNum, RocNum, losePeople, getPeople;
    void GetNewObejects(int x) {

        int AllObejects = (int)((NewDays * 0.8 * (x * 8 + 50)) / 0.7);
         WodNum = (int)(AllObejects * 0.2);
         RocNum = AllObejects - WodNum;

         losePeople = 0;
         getPeople = 0;


        if (x < 29)
        {
             losePeople = 1;
             getPeople =  2;

        }
        else {
             losePeople = 0;
             getPeople = 1;
        }

        Transform reduce = FinishUi.transform.GetChild(0).GetChild(0);
        Transform add = FinishUi.transform.GetChild(0).GetChild(1);

        reduce.GetChild(0).GetComponent<Text>().text = "人员损耗:" + losePeople;
        reduce.GetChild(1).GetComponent<Text>().text = "食物消耗:" + losePeople * 200;

        add.GetChild(0).GetComponentInChildren<Text>().text = WodNum.ToString(); 
        add.GetChild(1).GetComponentInChildren<Text>().text = RocNum.ToString();
        add.GetChild(2).GetComponentInChildren<Text>().text = getPeople.ToString();

    }

    public void addNewObeject() {

        FinishUi.SetActive(false);
        woodNum += WodNum;
        RockNum += RocNum;
        pepleNum -= losePeople;
        pepleNum += getPeople;

    } 
    
}
