using System;
using System.Collections.Generic;
using System.Linq;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class DataDisplayer : MonoBehaviour, IInputClickHandler, IFocusable
{
    private Vector3 _originalScale;

    private Dictionary<MeshRenderer, Color[]> _originalColors = new Dictionary<MeshRenderer, Color[]>();

    void Start()
    {
        SaveOriginalColors();
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Click " + gameObject.name);
    }

    public void OnFocusEnter()
    {
        SetHighlight(true);
    }

    public void OnFocusExit()
    {
        SetHighlight(false);
    }

    private void SaveOriginalColors()
    {
        if (!_originalColors.Any())
        {
            foreach (var component in GetComponentsInChildren<MeshRenderer>())
            {
                var colorList = new List<Color>();

                foreach (var t in component.materials)
                {
                    colorList.Add(t.color);
                }
                _originalColors.Add(component, colorList.ToArray());
            }
        }
    }

    private void SetHighlight(bool status)
    {
        var targetColor = Color.red;
        foreach (var component in GetComponentsInChildren<MeshRenderer>())
        {
            for (var i = 0; i < component.materials.Length; i++)
            {
                component.materials[i].color = status ? targetColor : _originalColors[component][i];
            }
        }
    }
}
