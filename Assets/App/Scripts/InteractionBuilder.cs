using UnityEngine;

public class InteractionBuilder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    foreach (var child in GetComponentsInChildren<MeshFilter>())
	    {
	        child.gameObject.AddComponent<MeshCollider>();
	        child.gameObject.AddComponent<DataDisplayer>();
	    }
	}
}
