using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherUI : MonoBehaviour
{
  public Manager manager;

  TMPro.TMP_Text text;

  void Start(){
    manager = FindObjectOfType<Manager>();
    text = GetComponent<TMPro.TMP_Text>();
    text.text = manager.weather.ToString();
  }

  void Update()
  {
  }
}
