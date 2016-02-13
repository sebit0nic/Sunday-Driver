using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : ScriptableObject {

	private GameObject pooledObject;
	private bool willGrow;

	private List<GameObject> pooledObjects;

	public ObjectPool(GameObject pooledObject, int pooledAmount, bool willGrow) {
		this.pooledObject = pooledObject;
		this.willGrow = willGrow;

		pooledObjects = new List<GameObject> ();
		for (int i = 0; i < pooledAmount; i++) {
			GameObject obj = (GameObject)Instantiate(pooledObject);
			obj.SetActive(false);
			pooledObjects.Add (obj);
		}
	}

	public GameObject GetPooledObject() {
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects[i].activeInHierarchy) {
				return pooledObjects[i];
			}
		}

		if (willGrow) {
			GameObject obj = (GameObject)Instantiate(pooledObject);
			pooledObjects.Add (obj);
			return obj;
		}

		return null;
	}
}
