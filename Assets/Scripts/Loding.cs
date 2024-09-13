using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Loding : MonoBehaviour
{
    public float moveSpeed; // 移动速度（单位：像素/秒）

    private const int secondsPerDay = 300; // 300 秒为一天
    private const float secondsPerHour = secondsPerDay / 24f; // 计算每小时的秒数，使用浮点数除法
    private const float secondsPerMinute = secondsPerHour / 60f; // 计算每分钟的秒数，使用浮点数除法

    private void OnEnable()
    {
        GameManager.EventWithLoding += HandleBroadcastEventWithParam;
    }

    private void OnDisable()
    {
        GameManager.EventWithLoding -= HandleBroadcastEventWithParam;
    }
    int time;

    private void HandleBroadcastEventWithParam(int totalSeconds)
    {

        Transform loding = transform.GetChild(0).GetChild(1);
        Transform timer = transform.GetChild(0).GetChild(0);

        time = totalSeconds;

        if(!loding.gameObject.activeSelf) {
            StartCoroutine(LodinngMove(loding, time));
        }
        // Start both the countdown and the movement coroutines
        StartCoroutine(CountdownCoroutine(totalSeconds, timer.GetComponent<Text>()));
    }


    public void LodingMove()
    {
        Transform loding = transform.GetChild(0).GetChild(1);
        if (!loding.gameObject.activeSelf) {
            StartCoroutine(LodinngMove(loding, time));
        }
    }

    IEnumerator CountdownCoroutine(int totalSeconds, Text timerText)
    {
        while (totalSeconds > 0)
        {
            // Calculate hours, minutes, and seconds using custom time conversion
            int days = totalSeconds / secondsPerDay;
            int hours = (int)((totalSeconds % secondsPerDay) / secondsPerHour);
            int seconds = (int)((totalSeconds % secondsPerHour) / secondsPerMinute);

            // Update the UI text with the custom time format
            timerText.text = $"距离冒险结束：\n{days:D2}:{hours:D2}:{seconds:D2}";

            // Wait for one second
            yield return new WaitForSeconds(1);

            // Decrease the totalSeconds
            totalSeconds--;

        }

        // Final update to 00:00:00 when the countdown ends
        timerText.text = "距离冒险结束：\n00:00:00";

        timerText.text = "冒险";
        GameManager.instance.newState = newState.StopLoding;

        yield break;
    }

    IEnumerator LodinngMove(Transform loding, int totalSeconds)
    {
        loding.gameObject.SetActive(true);

        Vector3 startPosition = loding.position;
        Vector3 endPosition = loding.transform.GetChild(0).position; // Assuming endPoint is where it should end, update as needed
        Text text = loding.GetComponent<Text>();
        Color color = text.color;

        float elapsedTime = 0f;
        color.a = 1f; // Set alpha to fully opaque
        text.color = color;

        while (elapsedTime < totalSeconds)
        {
            float t = elapsedTime / totalSeconds; // Normalize time to [0, 1]

            color = text.color;
            color.a = (1 - t); // Gradually fade out
            text.color = color;

            loding.position = Vector3.Lerp(startPosition, endPosition, t);

            // Wait for the next frame
            yield return null;

            // Increase elapsed time
            elapsedTime += Time.deltaTime * moveSpeed;
        }

        // Ensure the final position is exact
        loding.position = endPosition;

        loding.position = startPosition;
        loding.gameObject.SetActive(false);
    }
}
