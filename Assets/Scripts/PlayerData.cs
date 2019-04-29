using System;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName="PlayerData")]
public class PlayerData : ScriptableObject {
  public float InitialTime = 50f;
  public float TimeBalance;
  public float GameSpeed = 5.0f;
  public float GameSlowSpeed = 1f;

  public void Reset(){
    TimeBalance = InitialTime;
  }
}
