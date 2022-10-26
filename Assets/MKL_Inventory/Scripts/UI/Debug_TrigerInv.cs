using UnityEngine;

namespace MKL.InventoryUI
{
    public class Debug_TrigerInv : MonoBehaviour
    {        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Inventory.Inventory>(out Inventory.Inventory invFinded))
            {
                GameObject.FindObjectOfType<UI_InventoryManager>().UpdateInventory(invFinded);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Inventory.Inventory>(out Inventory.Inventory invFinded))
            {                
                GameObject.FindObjectOfType<UI_InventoryManager>().UpdateInventory(null);
            }
            
        }
    }
}
