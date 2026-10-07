using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class game : MonoBehaviour
{
    // ===== 原有引用不变 =====
    public GameObject uiReady;
    public GameObject uiInGame;
    public GameObject uiGameOver;
    public Player player;
    public PipelineManager pipelineManager;

    // ===== 新增两个TMP文字引用 =====
    public TextMeshProUGUI scoreInGameText;    // PanelsGame 下的 ScoreInGame
    public TextMeshProUGUI scoreFinalText;     // PanelGameOver->Panel 下的 ScoreFinal
    public int score;

    enum Game_STATUS
    {
        Ready,
        IsGame,
        GameOver
    }
    private Game_STATUS status;
    private Game_STATUS Status
    {
        get { return status; }
        set
        {
            this.status = value;
            this.UpdateUI();
        }
    }

    void Start()
    {
        uiReady.SetActive(true);
        Status = Game_STATUS.Ready;
        this.player.OnDeath += Player_OnDeath;
        score = 0;
        UpdateScoreUI();
    }

    private void Player_OnDeath()
    {
        this.Status = Game_STATUS.GameOver;
        this.pipelineManager.Stop();
        // 死亡赋值结算分数
        if (scoreFinalText != null)
        {
            scoreFinalText.text = $"Score: {score}";
        }
    }

    void UpdateUI()
    {
        uiReady.SetActive(this.Status == Game_STATUS.Ready);
        uiInGame.SetActive(this.Status == Game_STATUS.IsGame);
        uiGameOver.SetActive(this.Status == Game_STATUS.GameOver);
        if (Status == Game_STATUS.IsGame)
        {
            player.canJump = true;
        }
        else
        {
            player.canJump = false;
        }
    }

    void Update()
    {
    }

    public void StartGame()
    {
        this.Status = Game_STATUS.IsGame;
        Debug.LogFormat("startgame:{0}", this.Status);
        pipelineManager.StartRun();
        player.Fly();
    }

    public void Resetgame()
    {
        this.Status = Game_STATUS.Ready;
        this.pipelineManager.Init();
        this.player.Init();
        score = 0;
        UpdateScoreUI();
    }

    // 给ScoreArea调用的加分方法
    public void AddScore(int num)
    {
        score += num;
        UpdateScoreUI();
    }

    // 更新游戏内实时分数
    void UpdateScoreUI()
    {
        if (scoreInGameText != null)
        {
            scoreInGameText.text = $"Score: {score}";
        }
    }
}
