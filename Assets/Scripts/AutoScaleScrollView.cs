using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScaleScrollView : MonoBehaviour {

	public bool scaleWidth = false;
	public bool scaleHeight = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RectTransform selfRect = GetComponent<RectTransform> ();
	
		/*if (scaleHeight) {
			float size = 0.0f;
			foreach (Transform tr in transform) {
				size += tr.gameObject.GetComponent<RectTransform> ().rect.height;
			}
			selfRect.sizeDelta = new Vector2 (selfRect.sizeDelta.x, size);
		}*/


		//if (scaleWidth) {
			float size = 0.0f;
			foreach (Transform tr in transform) {
				size += tr.gameObject.GetComponent<RectTransform> ().rect.width;
			}
			selfRect.sizeDelta = new Vector2 (size, selfRect.sizeDelta.y);
		//}
	}
}
