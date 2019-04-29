using UnityEngine;

[CreateAssetMenu(fileName="Seed")]
public class Seed : ScriptableObject {
  public int Cost = 5;
  public int GoodGrantedTime = 55;
  public int BadGrantedTime = 55;
  public int HarvestTime = 30;
  public Weather PreferredWeather;
  public Soil PreferredSoil;

  public Sprite Icon;
  public Sprite ProgressStartSprite;
  public Sprite ProgressMiddleSprite;
  public Sprite ProgressGoodSprite;
  public Sprite ProgressBadSprite;
}
