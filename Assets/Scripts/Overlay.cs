using UnityEngine;
using System;

public class Overlay : MonoBehaviour
{
  public TMPro.TMP_Text nameUI;
  public TMPro.TMP_Text weatherUI;
  public TMPro.TMP_Text soilUI;
  public TMPro.TMP_Text goodGrantText;
  public TMPro.TMP_Text badGrantText;
  public TMPro.TMP_Text harvestTimeText;


  public void SetSeedData(Seed seed){
    nameUI.text= "Crop Name: " + seed.name;
    weatherUI.text= "Preferred Weather: " + seed.PreferredWeather.name;
    soilUI.text= "Preferred Soil Type:" + seed.PreferredSoil.name;


    TimeSpan harvestT = TimeSpan.FromSeconds( seed.HarvestTime );
    string harvestTStr = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
        harvestT.Hours,
        harvestT.Minutes,
        harvestT.Seconds);
    harvestTimeText.text= "Harvest Time:" + harvestTStr;

    TimeSpan goodT = TimeSpan.FromSeconds( seed.GoodGrantedTime );
    TimeSpan badT = TimeSpan.FromSeconds( seed.BadGrantedTime );

    string goodTStr = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
        goodT.Hours,
        goodT.Minutes,
        goodT.Seconds);
    string badTStr = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
        badT.Hours,
        badT.Minutes,
        badT.Seconds);

    goodGrantText.text= "Good Quality Reward: " + goodTStr;
    badGrantText.text= "Bad Quality Reward: " + badTStr;
  }
}
