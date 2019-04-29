using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SeedDetailUI : MonoBehaviour
{
  public Overlay overlay;
  public Seed seed;
  public Manager mgr;
  public Animator anim;
  public Image sr;

  void Start(){
    anim = GetComponentInParent<Animator>();
    sr.sprite = seed.Icon;
  }

  public void OnEnter()
  {
    if(seed){
      overlay.SetSeedData(seed);
    }
    overlay.gameObject.SetActive(true);
  }
  public void OnExit()
  {
    overlay.gameObject.SetActive(false);
  }
  public void OnSelect()
  {
    anim.SetTrigger("Inventory_Close");
    overlay.gameObject.SetActive(false);
    if (mgr){
      mgr.OnSeedSelected(seed);
    }
  }
}
