using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MenuScript : MonoBehaviour
{
    public ARPlaneManager arPlaneManager;
    public GameObject tutorialCanvas;
    public Image juthImage;
    public Image tapToPlayImage;
    public Image PongImage;
    
    void Start()
    {
        arPlaneManager.detectionMode = PlaneDetectionMode.None;

        tapToPlayImage.DOFade(0, 0.01f);
        PongImage.DOFade(0, 0.01f);
        juthImage.DOFade(0, 0.01f);
        
        AnimateIcons();
    }
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            StartGame();
            gameObject.SetActive(false);
        }
    }

     
    public void AnimateIcons()
    {
        Sequence s = DOTween.Sequence();
        
        tapToPlayImage.DOFade(1, 0.5f);
        PongImage.DOFade(1, 0.3f);

        s.Append(juthImage.DOFade(1, 0.5f));
        s.Append(PongImage.transform.DOPunchScale(new Vector2(1.1f,1.2f), 0.5f,10,0.2f));
        
        s.Play();
        
    }

    public void EnableTracking()
    {
        arPlaneManager.detectionMode = PlaneDetectionMode.Horizontal;
    }

    public void EnableTutorialCanvas()
    {
        tutorialCanvas.SetActive(true);
    }

    public void StartGame()
    {
        EnableTracking();
        EnableTutorialCanvas();
    }
}
