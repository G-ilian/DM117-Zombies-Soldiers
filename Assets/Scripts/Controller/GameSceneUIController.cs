using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneUIController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject endGamePannel;
    [SerializeField] HPComponent hpComponent;
    [SerializeField] TimeComponent timeComponent;


    public static GameSceneUIController Instance;

    private int scoreCount;

    public int ScoreCount
    {
        get { return scoreCount; }
        set
        {
            scoreCount = value;
            scoreText.text = $"{scoreCount}/100";

            if (scoreCount >= 100)
            {
                GameSceneUIController.Instance.ShowEndGamePannel();
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        var lifeStatus = hpComponent.HpValue;
        var elapsedTime = timeComponent.ElapsedTime;

        if (elapsedTime >= 90f)
        {
            ShowEndGamePannel();
        }

        if (lifeStatus == 0)
        {
            ShowEndGamePannel();
        }
    }

    public void ShowEndGamePannel()
    {
        Time.timeScale = 0;
        endGamePannel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
