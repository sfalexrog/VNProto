using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardGenerator : MonoBehaviour {
    public List<GameObject> cards = new List<GameObject>();
    public GameObject familyFutureCircle;
    public GameObject friendFutureCircle;
    public GameObject girlFutureCircle;
    public GameObject classFutureCircle;
    void Start () {
	}
	
    public void offFutureIndicator()
    {
        familyFutureCircle.SetActive(false);
        girlFutureCircle.SetActive(false);
        girlFutureCircle.SetActive(false);
        classFutureCircle.SetActive(false);
    }

    public void getNewCard()
    {
        int nextCard = Random.Range(0, cards.Count - 1);
        cards[nextCard].SetActive(true);
        offFutureIndicator();
    }
	void Update () {

    }
}
