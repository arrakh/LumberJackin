using UnityEngine;

public class ToggleGameobject : MonoBehaviour
{
    public void Toggle(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
