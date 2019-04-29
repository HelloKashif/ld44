using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(PlayerData))]


public class PlayerDataEditor : Editor
{

  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();

    PlayerData pd = (PlayerData)target;
    if (GUILayout.Button("Reset Player Data"))
    {
      pd.Reset();
    }


  }

}
