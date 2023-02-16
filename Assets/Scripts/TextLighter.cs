using UnityEngine;
using UnityEngine.UI;

public class TextLighter : MonoBehaviour
{
    public bool IncludeChild = true;

    private void Update()
    {
        if (IncludeChild)
        {
            LightChildren(transform);
            return;
        }

        if (transform.TryGetComponent(out Graphic graphic) && graphic.material.shader.name == "Text")
            graphic.material.SetVector("_Coordinate", new Vector4(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 0, 0));
    }
    private void LightChildren(Transform transform)
    {
        if (transform.gameObject.activeInHierarchy)
        {
            if (transform.TryGetComponent(out Graphic graphic) && graphic.material.shader.name == "Text")
                graphic.material.SetVector("_Coordinate", new Vector4(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 0, 0));

            foreach (Transform child in transform)
                LightChildren(child);
        }
    }
}
