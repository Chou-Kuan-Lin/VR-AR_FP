using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void Update()
    {
        // reload this scene
        if (Input.GetKey(KeyCode.R))
        {
            print("Restart " + SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            // re-compile
            /*
            AssetDatabase.StartAssetEditing();
            string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
            foreach (string assetPath in allAssetPaths)
            {
                MonoScript script = AssetDatabase.LoadAssetAtPath(assetPath, typeof(MonoScript)) as MonoScript;
                if (script != null)
                {
                    AssetDatabase.ImportAsset(assetPath);
                }
            }
            AssetDatabase.StopAssetEditing();
            */
        }
    }
}
