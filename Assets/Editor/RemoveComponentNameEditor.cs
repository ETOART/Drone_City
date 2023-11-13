using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (RemoveComponent_MeshCollider))]
public class RemoveComponentNameEditor : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector ();

        RemoveComponent_MeshCollider rmc = (RemoveComponent_MeshCollider) target;

        if (GUILayout.Button ("Remove ComponentName")) {
            rmc.RemoveComponents ();
        }
    }
}