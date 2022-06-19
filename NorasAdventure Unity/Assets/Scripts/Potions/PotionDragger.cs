using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Potions
{
    public class PotionDragger : MonoBehaviour
    {
        [SerializeField]private Image potionImage;
        [SerializeField]private PotionData potion;
        [SerializeField] private Canvas canvas;

        private Vector3 originalPosition;

        private List<RaycastResult> hitObjects=new List<RaycastResult>();

        private void Start()
        {
            originalPosition = transform.position;
            if (potion==null)
            {
                gameObject.SetActive(false);
                potion = null;
                return;
            }
            gameObject.SetActive(true);
            potionImage.sprite = potion.potionIcon;
        }

        public void SetPotion(PotionData givenPotion=null)
        {
            //if we gave no potions, disable
            if (givenPotion==null)
            {
                gameObject.SetActive(false);
                potion = null;
                return;
            }
            gameObject.SetActive(true);
            potion = givenPotion;
            potionImage.sprite = potion.potionIcon;
        }

        public void DragHandler(BaseEventData data)
        {
            //move to the front (on parent)
            transform.SetAsLastSibling();
            var pointerData = (PointerEventData) data;

            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform) canvas.transform,
                pointerData.position, canvas.worldCamera, out var position);

            transform.position = canvas.transform.TransformPoint(position);
        }

        public void DropHandler(BaseEventData data)
        {
            potionImage.raycastTarget = false;
            var potionTarget = GetDraggableTransformUnderMouse();

            if (potionTarget!=null)
            {
                //get the potion target script
                //give it potion data
                //let it handle it from there
            }
            
            transform.position = originalPosition;
            potionImage.raycastTarget = true;

        }

        private GameObject GetObjectUnderMouse()
        {
            var pointer = new PointerEventData(EventSystem.current) {position = Input.mousePosition};
            EventSystem.current.RaycastAll(pointer,hitObjects);
            
            return hitObjects.Count <= 0 ? null : hitObjects.First().gameObject;
        }

        private Transform GetDraggableTransformUnderMouse()
        {
            var clickedObject = GetObjectUnderMouse();

            if (clickedObject!=null && clickedObject.CompareTag("PotionTarget"))
            {
                return clickedObject.transform;
            }

            return null;
        }
    }
}