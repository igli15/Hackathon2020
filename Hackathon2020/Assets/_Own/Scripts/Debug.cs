using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug : MonoBehaviour
{
   public InputField XYInputField;
   public InputField ZInputField;
   public Player player;
   
   public void Submit()
   {
      if (player.m_currentBall != null)
      {
         player.forceScale.x = float.Parse(XYInputField.text);
         player.forceScale.y = float.Parse(XYInputField.text);
         player.forceScale.z = float.Parse(ZInputField.text);
      }
   }
   
}
