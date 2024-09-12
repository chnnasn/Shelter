using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Maker : MonoBehaviour
{
    List<GameObject> allObjects = new List<GameObject>();
    public int newReduce;
    public float moveSpeed; 
    public int totalSeconds;
    // Start is called before the first frame update
    void Start()
    {
        finAllSameName();

        if (allObjects.Count <= 2)
        {
            newReduce = allObjects.Count;
        }
        else {

            if (transform.name.Contains("FoodMaker")) {
                newReduce = getNum(5);

            }
            if(transform.name.Contains("Wall")) {
                newReduce = getNum(2);
            }

        }

        if (transform.name.Contains("FoodMaker"))
        {
            StartCoroutine(foodMade());

        }

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator foodMade() {

        Text text = transform.GetChild(0).GetChild(0).GetComponent<Text>();

        Vector3 startPosition = text.transform.position;
        Vector3 endPosition = text.transform.GetChild(0).position;

        Color color = text.color;

        while (true) // Infinite loop to keep repeating
        {
           
            int x = 0;
            while (x <= 60)
            {
                yield return new WaitForSeconds(1f);
                x++;
            }

            int num = GameManager.NewDays * 20 * 3 + 3;
            text.text = $"生产:{num}:个食物";
            yield return new WaitForSeconds(0.2f); // Delay before restarting the loop
            text.gameObject.SetActive(true);

            float elapsedTime = 0f;
            color.a = 1f; // Set alpha to fully opaque
            text.color = color;

            while (elapsedTime < totalSeconds)
            {

                float t = elapsedTime / totalSeconds; // Normalize time to [0, 1]

                color = text.color;
                color.a = (1 - t); // Gradually fade out
                text.color = color;

                text.transform.position = Vector3.Lerp(startPosition, endPosition, t);

                // Wait for the next frame
                yield return null;

                // Increase elapsed time
                elapsedTime += Time.deltaTime * moveSpeed;
            }

            text.gameObject.SetActive(false);
            text.transform.position = startPosition;

            GameManager.instance.foodNUm += num;

        }
    }

    int getNum(int x)
    {
        int a = 1;

        int b = 2;

        int temp = 0;

        for (int i= 0;i< allObjects.Count -2;i++) {
            temp = (a + b) * x;
            a = b;
            b = temp;
        
        }

        return temp;
    }

    void finAllSameName() {
        // 获取场景中所有的 GameObject
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        // 遍历所有对象并找到名字匹配的对象
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == transform.name)
            {
                this.allObjects.Add(obj);
            }
        }
    }

}
