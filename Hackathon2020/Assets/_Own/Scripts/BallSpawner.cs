using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
   public GameObject ball;
   public Transform ballSpawnTransform;
   
   private void Update()
   {
   }

   public GameObject SpawnBall()
   {
      GameObject b = Instantiate(ball);
      return Instantiate(b,ballSpawnTransform);
   }
}
