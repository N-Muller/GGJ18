using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragMe : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
	//public Vector3 oldPosition; 

	//TODO public enum type to handle allowed drop areas, link todo with DropMe
	public bool dropable = false;
	public GameObject target;
	public static GameObject point = null;
	public Vector3 oldPos;

	void Start(){
		oldPos = transform.position;
	}
		


	public void OnBeginDrag(PointerEventData eventData)
	{		
		if(point == null)
			point = new GameObject();
		target = this.gameObject;
		//oldPosition = transform.position;
		SetDraggedPosition(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		SetDraggedPosition(eventData);
	}

	private void SetDraggedPosition(PointerEventData eventData)
	{
		var t = target.GetComponent<Transform>();
		RaycastHit info;
		Vector3 pos = Input.mousePosition;
		pos.x = Mathf.Max (Mathf.Min(1300,pos.x),70);
		pos.y = Mathf.Max (Mathf.Min(700,pos.y),40);
		pos.z = 1.49f;
		//Debug.Log (pos);
		Ray ray = Camera.main.ScreenPointToRay(pos);
		if (Physics.Raycast (ray, out info)) {
			point.transform.position = info.point;
			t.position = new Vector3 (info.point.x, info.point.y, t.position.z);
			DropMe dm = info.collider.gameObject.GetComponent<DropMe> ();
			if (dm) {
				if(dm.container.transform.childCount < dm.maxElements)
				dropable = true;
				GetComponent<Card> ().startPos = info.collider.gameObject.transform.position;
				transform.position = info.collider.gameObject.transform.position;
				Debug.Log ("Send drop");

			} else {
				dropable = false;
			}
		} else {
			dropable = false;
		}
		//Debug.Log ("mtw " + Input.mousePosition);
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		
		if (!dropable) {
			target.transform.position = oldPos;

		} else {
		}

		dropable = false;
	}
}
