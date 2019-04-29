using System;
using UnityEngine;

public class TimeUpdater : MonoBehaviour
{
  public PlayerData playerData;
  void Start()
  {
    var textUI = GetComponent<TMPro.TMP_Text>();
    TimeSpan t = TimeSpan.FromSeconds( playerData.TimeBalance );
    textUI.text = string.Format("{0:D2}h:{1:D2}:{2:D2}",
        t.Hours,
        t.Minutes,
        t.Seconds
        );
  }
}
