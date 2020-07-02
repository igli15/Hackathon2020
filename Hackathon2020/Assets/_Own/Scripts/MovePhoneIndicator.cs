using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovePhoneIndicator : MonoBehaviour
{
    [SerializeField] private GameObject _indicator;

    [SerializeField] private float _animationTime = 3f;

    [SerializeField] private float _waitTimeBetweenAnimations = 0.5f;

    [SerializeField] private float _moveDistance = 150;
    
    private Sequence _animSequence;
    private RectTransform _rectTransform;
    private Vector2 _initPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = _indicator.GetComponent<RectTransform>();
        _initPos = _rectTransform.anchoredPosition;
        
        animate();

    }

    private void OnEnable()
    {
        animate();
    }

    private IEnumerator waitForAnimation()
    {
        yield return new WaitForSeconds(_waitTimeBetweenAnimations);
        animate();
        
    }

    private void animate()
    {
        _animSequence = DOTween.Sequence();
        _animSequence.Append(_rectTransform.DOAnchorPos(_initPos + new Vector2(-_moveDistance, 0), _animationTime / 6f));
        _animSequence.Append(_rectTransform.DOAnchorPos(_initPos + new Vector2(_moveDistance, 0), _animationTime / 6f));
        _animSequence.Append(_rectTransform.DOAnchorPos(_initPos , _animationTime / 6f));
        _animSequence.Append(_rectTransform.DOAnchorPos(_initPos + new Vector2(0, _moveDistance), _animationTime / 6f));
        _animSequence.Append(_rectTransform.DOAnchorPos(_initPos + new Vector2(0, -_moveDistance), _animationTime / 6f));
        _animSequence.Append(_rectTransform.DOAnchorPos(_initPos, _animationTime / 6f));

        _animSequence.OnComplete(delegate { StartCoroutine(nameof(waitForAnimation)); });
    }
}
