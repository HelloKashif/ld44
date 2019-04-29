using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{
  //@Cleanup @Todo cleanup all this junk code

  public Weather weather;
  public Soil soil;
  public Seed[] seedInventory;
  public Transform GridPanel;
  public GameObject SeedItemPrefab;
  public Overlay overlay;
  public PlayerData playerData;
  public float HarvestProgress = 0.0f;
  public float MaxTime = 30.0f;

  public Image cropProgressImage;
  public Image weatherImage;
  public TMPro.TMP_Text soilTypeText;
  public TMPro.TMP_Text cropNameText;

  public bool ResetPlayerData = false;
  public Slider harvestProgressSlider;
  public TMPro.TMP_Text timeLeftText;
  public bool timerIsRunning = false;

  public bool grantingTime = false;
  public float grantingFinalAmt = 0.0f;

  public Seed selectedSeed;
  public Animator fadeAnimator;
  public GameObject openInvButton;

  public void OnSeedSelected(Seed seed){
    //Disable the open inv button as well. Coz we'll never need it again.
    openInvButton.SetActive(false);
    //Disable grid panel since we already have a seed sowed
    selectedSeed = seed;
    GridPanel.gameObject.SetActive(false);
    cropNameText.text = seed.name;
    startTimer();

    harvestProgressSlider.gameObject.SetActive(true);
    harvestProgressSlider.value = HarvestProgress;
    harvestProgressSlider.maxValue = selectedSeed.HarvestTime;
    harvestProgressSlider.minValue = 0;

    cropProgressImage.sprite = selectedSeed.ProgressStartSprite;
    cropProgressImage.gameObject.SetActive(true);
  }

  void startTimer(){
    timerIsRunning = true;
  }
  void stopTimer(){
    timerIsRunning = false;
  }

  void Start()
  {
    if(ResetPlayerData) playerData.Reset();

    weatherImage.sprite =weather.Icon;
    soilTypeText.text = "Soil Type: " + soil.name;

    harvestProgressSlider.value = 0;
    harvestProgressSlider.maxValue = 0;
    harvestProgressSlider.minValue = 0;
    harvestProgressSlider.gameObject.SetActive(false);

    foreach(var s in seedInventory){
      var go = Instantiate(SeedItemPrefab);
      var seedDetailUI = go.GetComponent<SeedDetailUI>();
      seedDetailUI.seed =s;
      seedDetailUI.overlay = overlay;
      seedDetailUI.mgr = this;
      go.transform.SetParent(GridPanel);
    }
  }

  void UpdateTimeLeftText(){
    TimeSpan t = TimeSpan.FromSeconds( playerData.TimeBalance );
    timeLeftText.text = string.Format("{0:D2}h:{1:D2}:{2:D2}",
        t.Hours,
        t.Minutes,
        t.Seconds
        );
  }

  void progressTimer(){
    playerData.TimeBalance -= playerData.GameSpeed*Time.deltaTime;
    HarvestProgress += playerData.GameSpeed*Time.deltaTime;
    harvestProgressSlider.value = HarvestProgress;

    //Update crop image based on progress
    var progressPcnt = (selectedSeed.HarvestTime - HarvestProgress)/selectedSeed.HarvestTime;
    if(progressPcnt > 0.6){
      cropProgressImage.sprite = selectedSeed.ProgressStartSprite;
    }else{
      cropProgressImage.sprite = selectedSeed.ProgressMiddleSprite;
      if(progressPcnt < 0.3){
        cropProgressImage.sprite = selectedSeed.ProgressGoodSprite;
      }
    }

    if(HarvestProgress >= selectedSeed.HarvestTime){
      gameWin();
    }


  }
  void progressSlowTimer(){
    playerData.TimeBalance -= playerData.GameSlowSpeed*Time.deltaTime;
  }

  float GrantTimeBasedOnCropQuality(){
    var quality = 0;

    if (selectedSeed.PreferredWeather == weather){
      quality += 1;
    }
    if (selectedSeed.PreferredSoil == soil){
      quality += 1;
    }

    var result = 0f;

    switch(quality){
      case 2:
        //Good
        result = selectedSeed.GoodGrantedTime;
        cropProgressImage.sprite = selectedSeed.ProgressStartSprite;
        break;
      case 1:
        //Average
        result = (selectedSeed.GoodGrantedTime+selectedSeed.BadGrantedTime)/2f;
        cropProgressImage.sprite = selectedSeed.ProgressGoodSprite;
        break;
      case 0:
        //Bad;
        result = selectedSeed.BadGrantedTime;
        cropProgressImage.sprite = selectedSeed.ProgressBadSprite;
        break;
    }

    return result;
  }

  void gameWin(){
    stopTimer();
    grantingTime = true;
    grantingFinalAmt = playerData.TimeBalance +  GrantTimeBasedOnCropQuality();
  }

  void gameOver(){
    stopTimer();
    Debug.Log("Game Over");
    SceneManager.LoadScene("Level_GameOver");
  }


  void Update(){
    if(grantingTime){
      playerData.TimeBalance = Mathf.Lerp(playerData.TimeBalance, grantingFinalAmt, 2f*Time.deltaTime);

      //Once all time granted then move to next level
      if(playerData.TimeBalance >= grantingFinalAmt - 10f){
        playerData.TimeBalance = grantingFinalAmt;
        grantingTime = false;
        grantingFinalAmt = 0.0f;

        //Once all time granted then move to next level
        fadeAnimator.SetTrigger("Fade_Out");
      }
    }
    if(timerIsRunning){
      progressTimer();
    }else{
      progressSlowTimer();
    }

    UpdateTimeLeftText();

    if(playerData.TimeBalance <= 0){
      stopTimer();
      playerData.TimeBalance = 0;
      gameOver();
    }
  }
}

