using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class changeParameters : MonoBehaviour {
    public Text steps;
    public Slider family;
    public Slider friend;
    public Slider girl;
    public Slider classmatess;
    public int family_change;
    public int friend_change;
    public int girl_change;
    public int classmatess_change;
    public GameObject familyFutureCircle;
    public GameObject friendFutureCircle;
    public GameObject girlFutureCircle;
    public GameObject classFutureCircle;
    public GameObject winPopUp;
    public GameObject losePopUp;

    public void backOfCircleSize()
    {
        familyFutureCircle.transform.localScale -= new Vector3(System.Math.Abs(family_change) * 1F, System.Math.Abs(family_change) * 1F, System.Math.Abs(family_change) * 1F);
        friendFutureCircle.transform.localScale -= new Vector3(System.Math.Abs(friend_change) * 1F, System.Math.Abs(friend_change) * 1F, System.Math.Abs(friend_change) * 1F);
        girlFutureCircle.transform.localScale -= new Vector3(System.Math.Abs(girl_change) * 1F, System.Math.Abs(girl_change) * 1F, System.Math.Abs(girl_change) * 1F);
        classFutureCircle.transform.localScale -= new Vector3(System.Math.Abs(classmatess_change) * 1F, System.Math.Abs(classmatess_change) * 1F, System.Math.Abs(classmatess_change) * 1F);
    }

    public void hideFuture()
    {
        familyFutureCircle.SetActive(false);
        friendFutureCircle.SetActive(false);
        girlFutureCircle.SetActive(false);
        classFutureCircle.SetActive(false);
    }

    public void Lose()
    {
        if (family.value == 0 || friend.value == 0 || girl.value == 0 || classmatess.value == 0 || classmatess.value == 10 || friend.value == 10 || family.value == 10 || girl.value == 10)
        {
            Debug.Log("Lose");
            winPopUp.SetActive(true);
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
    }

    IEnumerator LoseAndWin()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
    }

    public void Win()
    {
        //Debug.Log(Convert.ToInt32(steps.text));
        if (Convert.ToInt32(steps.text)==0)
        {
            Debug.Log("Win");
            winPopUp.SetActive(true);
            //StartCoroutine(LoseAndWin());
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
    }

    public void changeParam()
    {
        hideFuture();
        backOfCircleSize();
        family.value += family_change;
        friend.value += friend_change;
        girl.value += girl_change;
        classmatess.value += classmatess_change;
        int stepsInt = Convert.ToInt32(steps.text)-1;
        steps.text = Convert.ToString(stepsInt);
        Lose();
        Win();


    }

    public void showAnyFuture()
    {
        if (family_change != 0)
        {
            familyFutureCircle.SetActive(true);
        }
        if (friend_change != 0)
        {
            friendFutureCircle.SetActive(true);
        }
        if (girl_change != 0)
        {
            girlFutureCircle.SetActive(true);
        }
        if (classmatess_change != 0)
        {
            classFutureCircle.SetActive(true);
        }
        familyFutureCircle.transform.localScale += new Vector3(System.Math.Abs(family_change) * 1F, System.Math.Abs(family_change) * 1F, System.Math.Abs(family_change) * 1F);
        friendFutureCircle.transform.localScale += new Vector3(System.Math.Abs(friend_change) * 1F, System.Math.Abs(friend_change) * 1F, System.Math.Abs(friend_change) * 1F);
        girlFutureCircle.transform.localScale += new Vector3(System.Math.Abs(girl_change) * 1F, System.Math.Abs(girl_change) * 1F, System.Math.Abs(girl_change) * 1F);
        classFutureCircle.transform.localScale += new Vector3(System.Math.Abs(classmatess_change) * 1F, System.Math.Abs(classmatess_change) * 1F, System.Math.Abs(classmatess_change) * 1F);
    }

    public void dontShowFuture()
    {
        hideFuture();
        backOfCircleSize();
    }

    void Update () {
		
	}
}
