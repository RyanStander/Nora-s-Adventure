using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Potions
{
    public class PotionBag : MonoBehaviour
    {
        [SerializeField] private PotionDragger[] potionDraggers;
        [SerializeField]private PotionData[] createdPotions=new PotionData[3];

        public void AddPotionToInventory(PotionData potion)
        {

            for(var i = 0; i<createdPotions.Length; i++)
            {
                if (createdPotions[i] != null) continue;
                createdPotions[i] = potion;
                break;
            }
            
            UpdatePotionDisplay();
        }

        public void RemovePotionFromInventory(PotionData potion,PotionDragger dragger=null)
        {
            if (dragger==null)
            {
                for (var i = 0; i < createdPotions.Length; i++)
                {
                    if (createdPotions[i] != potion) continue;
                    createdPotions[i] = null;
                    break;
                }
            }
            else
            {
                var potionIndex = Array.IndexOf(potionDraggers, dragger);
                if (createdPotions[potionIndex]==potion)
                {
                    createdPotions[potionIndex] = null;
                }
            }
            UpdatePotionDisplay();
        }

        private void UpdatePotionDisplay()
        {
            for (var i = 0; i < potionDraggers.Length; i++)
            {
                potionDraggers[i].SetPotion(createdPotions[i]);
            }
        }
    }
}
