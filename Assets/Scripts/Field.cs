using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
  public TMPro.TMP_Text nameUI;
  void Start()
  {
    nameUI = GetComponentInChildren<TMPro.TMP_Text>();
  }

  public void SetFieldCrop(Seed seed){
    nameUI.text = seed.name;
  }
}
