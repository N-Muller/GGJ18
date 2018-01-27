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

		Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		t.position = new Vector3(mouseToWorld.x,mouseToWorld.y,t.position.z);
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		
		if (!dropable) {
			target.transform.position = GetComponent<Card>().startPos;

		}

		dropable = false;
	}
}
