using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneController;

public class GameController : MonoBehaviour
{
    static public bool gamePlaying = false;
    static public bool gameOver = false;
    static bool gamePlayedOnce = false;
    static public bool timeOver = false;
    static public float gameScore = 0.0f;
    static public int gameMenu = 1;
    static public int gameLevel = 1;
    static public int remainingBeeNum = 3;
    static public bool remainingChanged = false;
    float gameTime;
    bool gameInitialized = false;

    public GameObject BeeObject;
    public GameObject InvisibleWall;

    UIController uiC;
    RuleController ruleC;
    TimeController timeC;
    ScoreController scoreC;
    OtherBeeController otherBeeC;
    HiveController hiveC;
    SoundController soundC;
    AudioSource BGM;
    Bee bee;

    // Start is called before the first frame update
    void Start()
    {
        uiC = GetComponent<UIController>();
        ruleC = GetComponent<RuleController>();
        timeC = GetComponent<TimeController>();
        scoreC = GetComponent<ScoreController>();
        otherBeeC = GetComponent<OtherBeeController>();
        hiveC =  GetComponent<HiveController>();
        soundC = GetComponent<SoundController>();
        BGM = GetComponent<AudioSource>();
        bee = BeeObject.GetComponent<Bee>();
        scene = SCENE.Home;
        gamePlayedOnce = false;
    }

    void Update()
    {
        if (scene == SCENE.Home)
        {
            if (!gameInitialized)
            {
                gameInitialized = InitializeGame();
            }
            SelectHomeMenu();
        }
        else if (scene == SCENE.Rule)
        {
            if (!LookThourghRule())
            {
                soundC.PlaySelect();
                scene = SCENE.Home;
            }
        }
        else if (scene == SCENE.SelectLevel)
        {
            if (SelectLevel())
            {
                scene = SCENE.Play;
                uiC.UpdateHomeUI(false);
                uiC.UpdateSelectLevelPanel(false);
                InvisibleWall.SetActive(true);
            }
        }
        else if (scene == SCENE.Play)
        {
            // Getting Ready for Game
            if (!gamePlaying)
            {
                // Set gamePlaying (true) in Coroutine
                StartCoroutine(uiC.ReadyClusterUI());
                // Initializing Time
                timeC.InitializeTime(ref gameTime);
                uiC.InitializeTimeSlider(gameTime);
                uiC.UpdateGameUI(true);
                // Instantiating OtherBee
                if (!otherBeeC.initialized)
                {
                    otherBeeC.InitializeOtherBee();
                }
                bee.StartBuzzing();
            }
            // GamePlaying
            else
            {
                if (gameOver)
                {
                    BGM.Stop();
                    bee.StopBuzzing();
                    soundC.PlayWhistleSE();
                    scoreC.SetScore(gameTime);
                    otherBeeC.DestroyOtherBee();
                    StartCoroutine(ChangeToResultWithDelay(1.5f));
                    gamePlayedOnce = true;
                    InvisibleWall.SetActive(false);
                }
                else
                {
                    // Updating Time
                    timeC.UpdateTime(ref gameTime);
                    uiC.UpdateTimeSlider(gameTime);
                    // Move Bee
                    bee.MoveHorizontally();
                    bee.MoveVertically();
                    if (remainingChanged)
                    {
                        UpdateRemainingBee();
                    }
                }
            }
        }
        else if (scene == SCENE.Result)
        {
            uiC.ShowResultUI();
            if (timeOver)
            {
                soundC.PlayGameLose();
                BackToHome();
            }
            else
            {
                soundC.PlayGameWin();
                SelectResultMenu();
            }
        }
        else if (scene == SCENE.Ranking)
        {
            // Do nothing   
        }
    }

    private bool InitializeGame()
    {
        uiC.UpdateHomeUI(true);
        uiC.UpdateSelectLevelPanel(false);
        uiC.UpdateRulePanel(false);
        uiC.UpdateGameUI(false);
        uiC.initialCursorPosition = true;
        if (gamePlayedOnce)
        {
            uiC.ShowBeeHunterText();
        }
        scoreC.Initialize();
        soundC.Initialize();
        BGM.Play();
        InvisibleWall.SetActive(false);
        gamePlaying = false;
        gameOver = false;
        bee.Initialize();
        return true;
    }

    // For UI Cursor
    private int UpdateCursorValue(bool increase, int cursorValue)
    {
        soundC.PlayMoveCursor();
        if (increase)
        {   
            if (cursorValue < 3)
            {
                cursorValue++;
            }
            else
            {
                cursorValue = 1;
            }
        }
        else if (!increase)
        {
            if (cursorValue > 1)
            {
                cursorValue--;
            }
            else
            {
                cursorValue = 3;
            }
        }
        return cursorValue;
    }
    
    private void SelectHomeMenu()
    {
        if (InputController.UP_KEY_UP)
        {
            soundC.PlayMoveCursor();
            gameMenu = uiC.UpdateHomeCursor();
        }
        if (InputController.DOWN_KEY_UP)
        {
            soundC.PlayMoveCursor();
            gameMenu = uiC.UpdateHomeCursor();
        }
        if (InputController.SPACE_KEY_UP && InputController.SHIFT && gameMenu == 1)
        {
            SceneManager.LoadScene("BeeHunter");
        }
        if (InputController.SPACE_KEY_UP)
        {
            soundC.PlaySelect();
            switch (gameMenu)
            {
                // Play
                case 1:
                    scene = SCENE.SelectLevel;
                    uiC.UpdateSelectLevelPanel(true);
                    break;
                // Rule
                case 2:
                    scene = SCENE.Rule;
                    uiC.UpdateRulePanel(true);
                    ruleC.NextPage();
                    break;
                default:
                    break;
            }
        }
    }

    private bool SelectLevel()
    {
        if (InputController.LEFT_KEY_UP)
        {
            gameLevel = UpdateCursorValue(increase: false, gameLevel);
            uiC.UpdateSelectLevelCursor(gameLevel);
        }
        if (InputController.RIGHT_KEY_UP)
        {
            gameLevel = UpdateCursorValue(increase: true, gameLevel);
            uiC.UpdateSelectLevelCursor(gameLevel);
        }
        if (InputController.SPACE_KEY_UP)
        {
            soundC.PlaySelect();
            remainingChanged = false;
            InitializeLevel();
            return true;
        }
        return false;
    }

    private bool LookThourghRule()
    {
        if (InputController.RIGHT_KEY_UP || InputController.SPACE_KEY_UP)
        {
            soundC.PlayMoveCursor();
            bool BookRead = ruleC.NextPage();
            if (BookRead)
            {
                uiC.UpdateRulePanel(false);
                return false;
            }
        }
        if (InputController.LEFT_KEY_UP)
        {
            soundC.PlayMoveCursor();
            ruleC.BackPage();
        }
        return true;
    }

    private void InitializeLevel()
    {
        hiveC.InitializeHiveSpot();
        switch (gameLevel)
        {
            // Easy
            case 1:
                remainingBeeNum = 3;
                timeC.maxTime = 90.0f;
                break;
            // Normal
            case 2:
                remainingBeeNum = 5;
                timeC.maxTime = 60.0f;
                break;
            // Hard
            case 3:
                remainingBeeNum = 10;
                timeC.maxTime = 90.0f;
                break;
            default:
                break;
        }
        uiC.UpdateRemainingBeeText();
    }

    private void UpdateRemainingBee()
    {
        remainingBeeNum--;
        if (remainingBeeNum == 0)
        {
            // Show Direction to Hive
        }
        uiC.UpdateRemainingBeeText();
        remainingChanged = false;
    }

    private void SelectResultMenu()
    {
        if (InputController.RIGHT_KEY_UP)
        {
            soundC.PlayMoveCursor();
            gameMenu = uiC.UpdateResultCursor();
        }
        if (InputController.LEFT_KEY_UP)
        {
            soundC.PlayMoveCursor();
            gameMenu = uiC.UpdateResultCursor();
        }
        if (InputController.SPACE_KEY_UP)
        {
            soundC.PlaySelect();
            uiC.HideResultUI();
            gameInitialized = false;
            switch (gameMenu)
            {
                // Home
                case 1:
                    scene = SCENE.Home;
                    break;
                // Ranking
                case 2:
                    scene = SCENE.Ranking;
                    LoadRankingScene();
                    gameMenu = 1;
                    break;
                default:
                    break;
            }
        }
    }

    private void LoadRankingScene()
    {
        int boardId = gameLevel - 1;
        double rankingScore = double.Parse((gameScore).ToString("0.00"));
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(rankingScore, boardId);
    }

    public void BackToHome()
    {
        if (InputController.SPACE_KEY_UP)
        {
            soundC.PlaySelect();
            uiC.HideResultUI();
            gameInitialized = false;
            scene = SCENE.Home;
        }
    }

    private IEnumerator ChangeToResultWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        scene = SCENE.Result;
    }

}
