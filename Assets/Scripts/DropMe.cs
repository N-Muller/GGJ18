using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMe : MonoBehaviour
{
	/*public GameObject container;
	private Color normalColor;
	public Color highlightColor = Color.grey;
	public int maxElements = int.MaxValue;*/


	/*public void OnEnable ()
	{
		normalColor = GetComponent<Image>().color;
	}

	public void OnDrop(PointerEventData data)
	{
		GetComponent<Image>().color = normalColor;

		GameObject dropItem = GetDropItem(data);
		if (dropItem != null && dropItem.GetComponent<DragMe> () != null && container.transform.childCount < maxElements) {
			dropItem.GetComponent<DragMe>().target.transform.SetParent (container.transform);
			dropItem.GetComponent<DragMe>().target.transform.SetAsFirstSibling ();

		}
	}

	public void OnPointerEnter(PointerEventData data)
	{
		GameObject dropItem = GetDropItem(data);
		if (dropItem != null && dropItem.GetComponent<DragMe> () != null && container.transform.childCount < maxElements) {
			dropItem.GetComponent<DragMe> ().dropable = true;

			GetComponent<Image> ().color = highlightColor;
		}
	}

	public void OnPointerExit(PointerEventData data)
	{		
		GameObject dropItem = GetDropItem(data);
		if (dropItem != null && dropItem.GetComponent<DragMe> () != null && container.transform.childCount < maxElements) {
			dropItem.GetComponent<DragMe> ().dropable = false;
		}
		GetComponent<Image>().color = normalColor;
	}
	
	private GameObject GetDropItem(PointerEventData data)
	{
		var originalObj = data.pointerDrag;

		return originalObj;
	}*/
}
