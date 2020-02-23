using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(CharacterControl))]
public class ChangeCharacterMaterials : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        if (GUILayout.Button("切换材质按钮"))
        {
            CharacterControl cc = (CharacterControl)target;
            cc.ChangeMaterails();
        }

    }

}
