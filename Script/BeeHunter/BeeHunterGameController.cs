using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneController;

public class BeeHunterGameController : MonoBehaviour
{
    static public bool gamePlaying = false;
    static public bool gameOver = false;
    static public bool timeOver = false;
    static public int gameScore = 0;
    static public int gameMenu = 1;
    static public bool hunted = false;
    static public int huntedBeeNum = 0;
    float gameTime;
    UIController uiC;
    RuleController ruleC;
    TimeController timeC;
    ScoreController scoreC;
    OtherBeeController otherBeeC;
    SoundController soundC;
    AudioSource BGM;
    BeeHunter beeHunter;
    public GameObject BeeHunterObject;

    void Start()
    {
        uiC = GetComponent<UIController>();
        ruleC = GetComponent<RuleController>();
        timeC = GetComponent<TimeController>();
        scoreC = GetComponent<ScoreController>();
        otherBeeC = GetComponent<OtherBeeController>();
        soundC = GetComponent<SoundController>();
        BGM = GetComponent<AudioSource>();
        beeHunter = BeeHunterObject.GetComponent<BeeHunter>();
        InitializeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (scene == SCENE.Home)
        {
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
        else if (scene == SCENE.Play)
        {
            // Getting Ready for Game
            if (!gamePlaying)
            {
                // Set gamePlaying (true) in Coroutine
                StartCoroutine(uiC.ReadyHuntingUI());
                // Initializing Time
                timeC.InitializeTime(ref gameTime);
                uiC.InitializeTimeSlider(gameTime);
                uiC.UpdateGameUI(true);
                // Instantiating OtherBee
                if (!otherBeeC.initialized)
                {
                    otherBeeC.InitializeOtherBee();
                }
                beeHunter.StartBuzzing();
            }
            // GamePlaying
            else
            {
                if (gameOver)
                {
                    BGM.Stop();
                    beeHunter.StopBuzzing();
                    soundC.PlayWhistleSE();
                    scoreC.SetScore(gameScore);
                    otherBeeC.DestroyOtherBee();
                    StartCoroutine(ChangeToResultWithDelay(0.5f));
                }
                else
                {
                    // Updating Time
                    timeC.UpdateTimeForBeeHunter(ref gameTime);
                    uiC.UpdateTimeSlider(gameTime);
                    // Move Hunter
                    beeHunter.Move();
                }
                if (hunted)
                {
                    UpdateHuntedBee();
                }

            }
        }
        else if (scene == SCENE.Result)
        {
            soundC.PlayGameWin();
            if (LoadRankingScene())
            {
                scene = SCENE.Ranking;
            }
        }
        else if (scene == SCENE.Ranking)
        {
            // Do nothing   
        }
    }

    private void InitializeGame()
    {
        scene = SCENE.Home;
        gamePlaying = false;
        gameOver = false;
        hunted = false;
        huntedBeeNum = 0;
        gameScore = 0;
        uiC.UpdateHomeUI(true);
        uiC.UpdateRulePanel(false);
        uiC.UpdateGameUI(false);
        uiC.initialCursorPosition = true;
        timeC.maxTime = 45.0f;
        scoreC.Initialize();
        soundC.Initialize();
        BGM.Play();
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
        if (InputController.SPACE_KEY_UP)
        {
            soundC.PlaySelect();
            switch (gameMenu)
            {
                // Play
                case 1:
                    scene = SCENE.Play;
                    uiC.UpdateHomeUI(false);
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

    private void UpdateHuntedBee()
    {
        huntedBeeNum++;
        uiC.UpdateHuntedBeeText();
        hunted = false;
    }

    private IEnumerator ChangeToResultWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        scene = SCENE.Result;
    }

    private bool LoadRankingScene()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(gameScore, 3);
        return true;
    }

}
