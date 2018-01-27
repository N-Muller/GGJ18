using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler {

	public bool selected = false;
	public float upFactor = 100.0f;
	public float scaleFactor = 2.0f;
	public Vector3 startPos;
	public List<Zone> zones;

	[ContextMenu("Initialize")]
	void ContexMenuInit()
	{
		zones = new List<Zone> (GetComponentsInChildren<Zone> ());
	}


	// Update is called once per frame
	void Start() {
		setActiveZone (false);
		startPos = transform.position;
	}

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		selected = true;
		setActiveZone (true);
		transform.position = transform.position +Vector3.up*upFactor;
		transform.localScale += new Vector3(scaleFactor,scaleFactor,0f);
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		selected = false;
		setActiveZone (false);


		transform.localScale -= new Vector3(scaleFactor,scaleFactor,0f);
		transform.position = startPos;
	}

	#endregion

	void setActiveZone(bool b){
		foreach(Zone zone in zones)
		{
			zone.enabled = b;	
			zone.GetComponent<Image> ().raycastTarget = b;
		}
	}

}
