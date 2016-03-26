using UnityEngine;
using System.Collections;

public class PaintJobber : MonoBehaviour {

	public Material paintJobs;
	private Material instantiatedMaterial;
	private MeshRenderer meshRenderer;
	public Vector2 uvCoordinates;

	private void Awake() {
		instantiatedMaterial = Instantiate (paintJobs) as Material;
		meshRenderer = GetComponent<MeshRenderer> ();
	}

	private void Start() {
		instantiatedMaterial.SetTextureOffset ("_MainTex", uvCoordinates);
		meshRenderer.material = instantiatedMaterial;
	}
}
