using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleController : MonoBehaviour
{
    public List<GameObject> RulePage = new List<GameObject>();
    private int maxPageNum;
    private int currentPageNum = 0;

    private void Start()
    {
        maxPageNum = RulePage.Count;
    }

    public bool NextPage()
    {
        if (currentPageNum == 0)
        {
            RulePage[currentPageNum].SetActive(true);
            currentPageNum++;
            return false;
        }
        else if (currentPageNum < maxPageNum)
        {
            RulePage[currentPageNum-1].SetActive(false);
            RulePage[currentPageNum].SetActive(true);
            currentPageNum++;
            return false;
        }
        else
        {
            RulePage[currentPageNum-1].SetActive(false);
            currentPageNum = 0;
            return true;
        }
    }

    public void BackPage()
    {
        if (currentPageNum > 1)
        {
            RulePage[currentPageNum - 1].SetActive(false);
            RulePage[currentPageNum - 2].SetActive(true);
            currentPageNum--;
        }
    }
}
