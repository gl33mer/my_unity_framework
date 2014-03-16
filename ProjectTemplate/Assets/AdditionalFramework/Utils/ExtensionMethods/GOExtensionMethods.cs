using UnityEngine;

public static class GOExtensionMethods
{
    public static void DestroyAllChildren(this Transform parent)
    {
        foreach (Transform childTransform in parent.transform)
        {
            GameObject.Destroy(childTransform.gameObject);
        }
    }

    public static void DestroyAllChildren(this GameObject parent)
    {
        foreach (Transform childTransform in parent.transform)
        {
            GameObject.Destroy(childTransform.gameObject);
        }
    }
}
