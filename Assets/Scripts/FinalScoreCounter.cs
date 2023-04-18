using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScoreCounter : MonoBehaviour
{
    [SerializeField] private ScoreTracker score;
    [SerializeField] private GameObject goodend;
    [SerializeField] private GameObject mediumend;
    [SerializeField] private GameObject badend;

    // Start is called before the first frame update
    void Start() {
        if (score.Score >= 5)
        {
            goodend.SetActive(true);
        }
        else if (score.Score >= 2)
        {
            mediumend.SetActive(true);
        }
        else
        {
            badend.SetActive(true);
        }
    }
}
