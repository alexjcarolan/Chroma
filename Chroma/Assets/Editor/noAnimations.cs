using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class CustomImporter : AssetPostprocessor
{
    void OnPreprocessModel()
    {
        ModelImporter modelImporter = assetImporter as ModelImporter;
        modelImporter.importAnimation = false;
    }
}
