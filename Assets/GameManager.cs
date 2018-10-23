using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    public UnityEvent onReset;

    public static GameManager instance;

    public GameObject readyPannel;

    public Text scoreText;

    public Text bestScoreText;

    public Text messageText;

    public bool isRoundActive = false;

    private int score = 0;

    public ShooterRotator shooterRotator;

    public CamFollow cam;

	//初期化のためにこれを使用
	void Awake () {
        instance = this;
        UpdateUI();
	}

    void Start()
    {
        StartCoroutine("RoundRoutine");
    }


    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateBestScore();
        UpdateUI();
    }
	
    void UpdateBestScore()
    {
        if (GetBsetScore() < score)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
    }

    int GetBsetScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        return bestScore;
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        bestScoreText.text = "Best Score: " + GetBsetScore();
    }

    public void OnBallDestroy()
    {
        UpdateUI();
        isRoundActive = false;
    }

    public void Reset()
    {
        score = 0;
        UpdateUI();

	//ラウンドを再びスタート
        StartCoroutine("RoundRoutine");
    }

    IEnumerator RoundRoutine()
    {
	 //レディー
        onReset.Invoke();

        readyPannel.SetActive(true);
        cam.SetTarget(shooterRotator.transform,CamFollow.State.Idle);
        shooterRotator.enabled = false;

        isRoundActive = false;

        messageText.text = "Ready...";

        yield return new WaitForSeconds(3f);

	//プレー
        isRoundActive = true;
        readyPannel.SetActive(false);
        shooterRotator.enabled = true;

        cam.SetTarget(shooterRotator.transform, CamFollow.State.Ready);

        while(isRoundActive == true)
        {
            yield return null;
        }

	//エンド
        readyPannel.SetActive(true);
        shooterRotator.enabled = false;

        messageText.text = "Wait For Next Round...";
        yield return new WaitForSeconds(3f);

        Reset();
    }
}
