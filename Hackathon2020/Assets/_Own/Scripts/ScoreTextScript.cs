using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextScript : MonoBehaviour
{

    private TextMeshProUGUI m_textMesh;
    private float scoreValue = 0;
    private void Awake()
    {
        m_textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScoreText()
    {
        scoreValue += 100;
        m_textMesh.text = "Score: " + scoreValue;
    }
}
