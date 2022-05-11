#if UNITY_EDITOR

using DTT.Utils.EditorUtilities.Exceptions;
using UnityEditor;
using UnityEngine;

namespace DTT.Utils.EditorUtilities
{
    internal class AssetEditorScript : Editor
    {
        private void OnEnable()
        {
            UpdateSettingsInProject();
            GetComponentInPrefab();
            GetOrCreateSettingsInProject();
            AddSceneToBuildSettings();
            ApplyPreProcessingOnScriptAsset();
        }

        /// <summary>
        /// Using the AssetDatabaseUtility, you can load all assets of an certain type into an array.
        /// </summary>
        private void UpdateSettingsInProject()
        {
            // Loads all settings scriptable objects in the project.
            SettingsScriptableObject[] settings = AssetDatabaseUtility.LoadAssets<SettingsScriptableObject>();

            // Changes the settings on the scriptable object that were found.
            foreach (SettingsScriptableObject setting in settings)
            {
                setting.UpdateSetting("New Setting");
                
                // Set the changed scriptable object to a dirty state so its changes can be saved.
                EditorUtility.SetDirty(setting);
            }

            // Save the changes made.
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// Using the AssetDatabaseUtility, get a scriptable object from a certain path.
        /// </summary>
        private void GetOrCreateSettingsInProject()
        {
            // Retrieve the settings from the project. This method will create the scriptable object for you if 
            // the it could not find the asset in the project.
            string path = "Packages/dtt.editorutilities/Examples/ScriptableObject/ExampleSettings.asset";
            SettingsScriptableObject settings =
                AssetDatabaseUtility.GetOrCreateScriptableObjectAsset<SettingsScriptableObject>(path);
            
            settings.UpdateSetting("New Setting");
            
            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// Using the AssetDatabaseUtility, get the component from a certain prefab.
        /// </summary>
        private void GetComponentInPrefab()
        {
            // Load a component instance on a prefab. You can use the try-catch clause to catch any errors that
            // might happen during this operation.
            string path = "Packages/dtt.editorutilities/Examples/Prefabs/ExamplePrefab.prefab";
            try
            {
                PrefabBehaviour prefabBehaviour = AssetDatabaseUtility.GetComponentInPrefab<PrefabBehaviour>(path);
                prefabBehaviour.UpdateValue(15);
                
                EditorUtility.SetDirty(prefabBehaviour);
                AssetDatabase.SaveAssets();
            }
            catch (AssetDatabaseException exception)
            {
                // It could be that we failed to load our component, in this case we want to know exactly what happened.
                Debug.LogWarning($"Failed retrieving the prefab behaviour at path {path}. {exception.Message}");
            }
        }

        /// <summary>
        /// Using the AssetUtility, add a scene to the build settings in script.
        /// </summary>
        private void AddSceneToBuildSettings()
        {
            // Provided you have the asset path of the scene, you can add it to the build settings from script.
            string scenePath = "Packages/dtt.editorutilities/Examples/Scenes/Demo.unity";
            if (!AssetUtility.IsScenePartOfBuildSettings(scenePath))
            {
                // Add the demo scene to the build settings.
                AssetUtility.AddSceneToBuildSettings(scenePath);
            }
        }

        /// <summary>
        /// Using the AssetUtility, apply preprocessing on an asset with the given path.
        /// </summary>
        private void ApplyPreProcessingOnScriptAsset()
        {
            // Update a script with a preprocessing statement (e.g. UNITY_EDITOR for editor scripts).
            string scriptPath = "Packages/dtt.editorutilities/Examples/Scripts/Editor/AssetEditorScript.cs";
            AssetUtility.ApplyPreProcessingOnAssetAtPath(scriptPath, "UNITY_EDITOR");
        }
    }
}

#endif
