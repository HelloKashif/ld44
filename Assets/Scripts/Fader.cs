using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
  public void OnFadeComplete()
  {
    var current = SceneManager.GetActiveScene().buildIndex;
    if(SceneManager.sceneCountInBuildSettings > current + 1){
      SceneManager.LoadScene(current+1);
    }else{
      Debug.Log("All scenes complete");
    }
  }

  //@Todo put final game win screen
}
