using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class Player : MonoBehaviour
{
    public BallSpawner ballSpawner;
    public Vector3 forceScale = Vector3.one;
    [ReadOnly]
    public BallScript m_currentBall = null;

    private float m_touchStartTime = 0f;
    private Vector3 m_touchStartPos = Vector3.zero;
    private Vector3 m_touchEndPos = Vector3.zero;
    private Vector3 direction = Vector3.one;

    private void Start()
    {
        SpawnBall();
    }
    
    private void Update()
    {
        if (Input.touchCount > 0 && m_currentBall != null)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    
                    m_touchStartPos = Input.GetTouch(0).position;
                    m_touchStartTime = Time.time;
                    break;
                
                case TouchPhase.Ended:
                    
                    m_touchEndPos = Input.GetTouch(0).position;
                    float timeInterval = Time.time - m_touchStartTime;

                    if (timeInterval < 0.02f) return;
                    
                    direction = (m_touchStartPos - m_touchEndPos).normalized;
                    
                    Vector3 force = new Vector3(direction.x * forceScale.x, direction.y * forceScale.y,
                        forceScale.z / timeInterval);
                    m_currentBall.Throw(force);
                    m_currentBall = null;
                    Invoke("SpawnBall",2f);
                    break;
            }
        }
    }

    private void SpawnBall()
    {
        GameObject ball = ballSpawner.SpawnBall();
        m_currentBall = ball.GetComponent<BallScript>();
    }
}
