using UnityEngine;

public class InteractionBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject _toolTip;
    
    void Start () {
	    foreach (var child in GetComponentsInChildren<MeshFilter>())
	    {
	        child.gameObject.AddComponent<MeshCollider>();
	        var displayer = child.gameObject.AddComponent<DataDisplayer>();
	        displayer.ToolTip = _toolTip;
	    }
	}
}
