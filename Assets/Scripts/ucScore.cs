using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ucScore : MonoBehaviour {
    public static int m_scoreValue = 0;
    Text m_scoreText;
    // Start is called before the first frame update
    void Start() {
        m_scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        m_scoreText.text = "Score: " + m_scoreValue;
    }
}