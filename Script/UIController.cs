using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject HomePanel;
    public GameObject HomeCursor;
    public GameObject RulePanel;
    public GameObject BeeHunterText;
    public GameObject SelectLevelPanel;
    public GameObject SelectLevelCursor;
    public GameObject ReadyCluster;
    public GameObject TimeSlider;
    public GameObject RemainingBeePanel;
    public Text RemainingBeeText;
    public Text TimeText;
    public GameObject ResultPanel;
    public Text LevelText;
    public GameObject WinnerResultPanel;
    public Text ScoreText;
    public GameObject LoserResultPanel;
    public GameObject ResultCursor;
    public bool initialCursorPosition = true;

    // Updating UI
    public void UpdateHomeUI(bool value)
    {
        HomePanel.SetActive(value);
    }
    public void UpdateRulePanel(bool value)
    {
        RulePanel.SetActive(value);
    }
    public void UpdateSelectLevelPanel(bool value)
    {
        SelectLevelPanel.SetActive(value);
    }
    public int UpdateHomeCursor()
    {
        int menuValue;
        RectTransform rect = HomeCursor.GetComponent<RectTransform>();
        if (initialCursorPosition)
        {
            rect.anchoredPosition = new Vector2(-12.0f, -48.0f);
            menuValue = 2;
        }
        else
        {
            rect.anchoredPosition = new Vector2(-12.0f, -3.0f);
            menuValue = 1;
        }
        initialCursorPosition = !initialCursorPosition; 
        return menuValue;
    }
    public void UpdateSelectLevelCursor(int level)
    {
        RectTransform rect = SelectLevelCursor.GetComponent<RectTransform>();
        switch (level)
        {
            case 1:
                rect.anchoredPosition = new Vector2(-105.0f, 43.0f); 
                break;
            case 2:
                rect.anchoredPosition = new Vector2(0.0f, 43.0f); 
                break;
            case 3:
                rect.anchoredPosition = new Vector2(105.0f, 43.0f);
                break;
            default:
                break;
        }
    }

    public void UpdateGameUI(bool value)
    {
        TimeSlider.SetActive(value);
        RemainingBeePanel.SetActive(value);
    }

    public IEnumerator ReadyClusterUI()
    {
        ReadyCluster.SetActive(true);
        ReadyCluster.GetComponent<Text>().text = "Ready??";
        yield return new WaitForSeconds(1.0f);
        ReadyCluster.GetComponent<Text>().text = "Cluster!!";
        GameController.gamePlaying = true;
        yield return new WaitForSeconds(1.0f);
        ReadyCluster.SetActive(false);
    }

    public void InitializeTimeSlider(float time)
    {
        Slider slider = TimeSlider.GetComponent<Slider>();
        slider.maxValue = time;
        slider.value = time;
        TimeText.text = time.ToString();
    }

    public void UpdateTimeSlider(float time)
    {
        TimeSlider.GetComponent<Slider>().value = time;
        TimeText.text = time.ToString("f1");
    }

    public void UpdateRemainingBeeText()
    {
        RemainingBeeText.text = GameController.remainingBeeNum.ToString();
    }

    public void ShowResultUI()
    {
        string level = "";
        switch (GameController.gameLevel)
        {
            case 1:
                level = "Easy";
                break;
            case 2:
                level = "Normal";
                break;
            case 3:
                level = "Hard";
                break;
            default:
                break;
        }
        LevelText.text = level;

        if (GameController.timeOver)
        {
            LoserResultPanel.SetActive(true);
        }
        else
        {
            ScoreText.text = GameController.gameScore.ToString("f2");
            WinnerResultPanel.SetActive(true);
        }
        ResultPanel.SetActive(true);
    }

    public int UpdateResultCursor()
    {
        int menuValue;
        RectTransform rect = ResultCursor.GetComponent<RectTransform>();
        if (initialCursorPosition)
        {
            rect.anchoredPosition = new Vector2(10.0f, -111.0f);
            menuValue = 2;
        }
        else
        {
            rect.anchoredPosition = new Vector2(-128.0f, -111.0f);
            menuValue = 1;
        }
        initialCursorPosition = !initialCursorPosition; 
        return menuValue;
    }

    public void HideResultUI()
    {
        ResultPanel.SetActive(false);
        LoserResultPanel.SetActive(false);
        WinnerResultPanel.SetActive(false);
    }

    /// Hunting Bee
    public void ShowBeeHunterText()
    {
        BeeHunterText.SetActive(true);
    }
    public IEnumerator ReadyHuntingUI()
    {
        ReadyCluster.SetActive(true);
        ReadyCluster.GetComponent<Text>().text = "Ready??";
        yield return new WaitForSeconds(1.0f);
        ReadyCluster.GetComponent<Text>().text = "Hunting!!";
        BeeHunterGameController.gamePlaying = true;
        yield return new WaitForSeconds(1.0f);
        ReadyCluster.SetActive(false);
    }

    public void UpdateHuntedBeeText()
    {
        RemainingBeeText.text = BeeHunterGameController.huntedBeeNum.ToString();
    }
}
