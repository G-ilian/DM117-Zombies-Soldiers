using TMPro;
using UnityEngine;

public class TimeComponent : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    private float elapsedTime;
    public float ElapsedTime
    {
        get { return elapsedTime; }
    }

    private void Start()
    {
        elapsedTime = 0;
        timerText.text = "00:00";
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        timerText.text = convertSecondToTime(ElapsedTime);
    }

    string convertSecondToTime(float elapsedTime)
    {
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
