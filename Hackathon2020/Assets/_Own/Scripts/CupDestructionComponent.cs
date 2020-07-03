using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class CupDestructionComponent : MonoBehaviour
{
    public GameObject particleObject;
    
    public void RemoveSelf()
    {
        particleObject.SetActive(true);
        Tween shakeTween = transform.DOShakePosition(0.5f, 0.4f, 10,10);
        Tween scaleTween = transform.DOScale(new Vector3(0, 0, 0),0.5f);
        Invoke("DestroySelf",1.1f);
        
        //removeSequence.Append()
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
