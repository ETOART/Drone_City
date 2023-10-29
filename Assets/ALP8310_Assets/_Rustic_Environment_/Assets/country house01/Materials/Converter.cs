using System;
using UnityEditor;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
 
public class MaterialToHDRP : MonoBehaviour
{
 
    [MenuItem("Tools/Convert selected Materials to HDRP...", priority = 0)]
    private static void upgradeSelected()
    {
        foreach(string guid in Selection.assetGUIDs)
        {
            string assetPath= AssetDatabase.GUIDToAssetPath(guid);
            Material m=AssetDatabase.LoadAssetAtPath<Material>(assetPath);
            Material mInstance=Instantiate(AssetDatabase.LoadAssetAtPath<Material>(assetPath));
            mInstance.name = m.name;
            if (convert(mInstance))
                EditorUtility.CopySerialized(mInstance, m); //Makes sure we keep the original GUID      
        }
        AssetDatabase.SaveAssets();
    }
 
    private static bool convert(Material m)
    {
        string shaderName= m.shader.name;
        if (shaderName.Equals("Autodesk Interactive",StringComparison.OrdinalIgnoreCase))
        {
            //Read
            Texture albedo = m.GetTexture("_MainTex");
            Texture metallic = m.GetTexture("_MetallicGlossMap");
            Texture roughness = m.GetTexture("_SpecGlossMap");
            Texture normal = m.GetTexture("_BumpMap");
            float bumpScale = m.GetFloat("_BumpScale");
            Vector2 offset = m.mainTextureOffset;
            Vector2 tiling = m.mainTextureScale;
 
            //Convert
            m.shader= Shader.Find("HDRP/Lit");
            m.SetTexture("_BaseColorMap", albedo);
            m.SetTexture("_NormalMap", normal);
            m.SetFloat("_NormalScale", bumpScale);
            m.mainTextureOffset = offset;
            m.mainTextureScale = tiling;
 
            return true;
        }
        return false;
    }
}
