using UnityEngine;
using UnityEngine.UI;

public class SelectedArrowUI : MonoBehaviour
{
    [Header("COMPONENTS")]
    public Image _arrowImage;

    private void Awake()
    {
        Deselect();
    }

    internal void Deselect()
    {
        _arrowImage.enabled = false;
    }

    internal void Select()
    {
        _arrowImage.enabled = true;
    }  
}
