using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseAction : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler {
    // 给定一个参数保存鼠标按下时候的位置
    private Vector3 startPos;
    // 给定一个参数来保存鼠标移动时候的位置
    private Vector3 movePos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
     * 鼠标进入时候的回调
    */
    public void OnPointerEnter(PointerEventData eventData) {
        print("鼠标进入 ... ");
    }

    /*
     * 鼠标按下时的回调
    */
    public void OnPointerDown(PointerEventData eventData) {
        // 给起始和移动参数保存值
        startPos = eventData.position;
        movePos = startPos;
    }

    /*
     * 鼠标抬起时的回调
    */
    public void OnPointerUp(PointerEventData eventData) {
        print("鼠标抬起 ... ");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        print("开始拖拽 ... ");
    }

    /*
     * 鼠标拖拽时的回调
    */
    public void OnDrag(PointerEventData eventData) {
        // 首先记录下抬起时候的位置
        Vector3 endPos = eventData.position;
        // 计算出卡片拖拽时候的位置，由原位置加上最终位置减去移动位置
        Vector3 pos = new Vector3(transform.position.x + (endPos.x - movePos.x),
                                  transform.position.y + (endPos.y - movePos.y), 0);

        // 赋值给当前组件
        transform.position = pos;
        // 再更新移动位置
        movePos = endPos;
    }
}
