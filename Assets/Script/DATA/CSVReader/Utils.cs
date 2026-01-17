using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static T ToEnum<T>(this string value)
    {
        return (T)System.Enum.Parse(typeof(T), value, true);
    }
#if UNITY_EDITOR
    public static IEnumerator IELoadData(string urlData, System.Action<string> actionComplete, bool showAlert = false)
    {
        var www = new WWW(urlData);
        float time = 0;
        //TextAsset fileCsvLevel = null;
        while (!www.isDone)
        {
            time += 0.001f;
            if (time > 10000)
            {
                yield return null;
                Debug.Log("Downloading...");
                time = 0;
            }
        }
        if (!string.IsNullOrEmpty(www.error))
        {
            UnityEditor.EditorUtility.DisplayDialog("Notice", "Load CSV Fail", "OK");
            yield break;
        }
        yield return null;
        actionComplete?.Invoke(www.text);
        yield return null;
        UnityEditor.AssetDatabase.SaveAssets();
        if (showAlert)
            UnityEditor.EditorUtility.DisplayDialog("Notice", "Load Data Success", "OK");
        else
            Debug.Log("<color=yellow>Download Data Complete</color>");
    }
#endif
}
