using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilUI : MonoBehaviour
{
  public Manager manager;

  TMPro.TMP_Text text;

  void Start(){
    manager = FindObjectOfType<Manager>();
    text = GetComponent<TMPro.TMP_Text>();
    text.text = manager.soil.ToString();
  }
}
