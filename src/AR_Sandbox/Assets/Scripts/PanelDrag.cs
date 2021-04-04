using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelDrag : MonoBehaviour, IDragHandler {
	private Vector3 delta;

	public void BeginDrag() {
		delta.x = transform.position.x - Input.mousePosition.x;
		delta.y = transform.position.y - Input.mousePosition.y;
		delta.z = 0f;
	}

	public void OnDrag(PointerEventData eventDataa) {
		transform.position = Input.mousePosition + delta;
	}
}
