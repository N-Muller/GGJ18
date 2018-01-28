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


	public void OnBeginDrag(PointerEventData eventData)
	{		
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
		Ray ray = Camera.main.ScreenPointToRay(pos);
		if (Physics.Raycast (ray, out info)) {
			t.position = new Vector3 (info.point.x, info.point.y, t.position.z);
			if (info.collider.gameObject.GetComponent<DropMe> ()) {
				dropable = true;
			} else {
				dropable = false;
			}
		} else {
			dropable = false;
		}
		Debug.Log ("mtw " + Input.mousePosition);
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		
		if (!dropable) {
			target.transform.position = GetComponent<Card>().startPos;

		}

		dropable = false;
	}
}
