using System;
using System.Collections.Generic;
using System.Linq;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.UX.ToolTips;
using UnityEngine;

public class DataDisplayer : MonoBehaviour, IInputClickHandler, IFocusable
{
    public GameObject ToolTip;

    private GameObject _createdToolTip;
    
    private Dictionary<MeshRenderer, Color[]> _originalColors = new Dictionary<MeshRenderer, Color[]>();

    void Start()
    {
        SaveOriginalColors();
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (_createdToolTip == null)
        {
            _createdToolTip = Instantiate(ToolTip);
            var toolTip = _createdToolTip.GetComponent<ToolTip>();
            toolTip.ShowOutline = false;
            toolTip.ShowBackground = true;
            toolTip.ToolTipText = gameObject.name;
            toolTip.transform.position = transform.position + Vector3.up * 0.2f;
            toolTip.transform.parent = transform.parent;
            toolTip.AttachPointPosition = transform.position;
            toolTip.ContentParentTransform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            var connector = toolTip.GetComponent<ToolTipConnector>();
            connector.Target = _createdToolTip;
            _createdToolTip.SetActive(true);
        }
        else
        {
            Destroy(_createdToolTip);
            _createdToolTip = null;
        }


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
        foreach (var component in _originalColors.Keys) 
        {
            for (var i = 0; i < component.materials.Length; i++)
            {
                component.materials[i].color = status ? targetColor : _originalColors[component][i];
            }
        }
    }
}
