using System;
using UnityEngine;

namespace Shop
{
    public class ItemShop : MonoBehaviour
    {
        private Animator _shopList;

        private bool _enteringShop;
        private bool _leavingShop;
        // Start is called before the first frame update
        void Start()
        {
            _shopList = GameObject.Find("ShopItemsCanvas").GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_enteringShop) OpenShop();
            
            if (_leavingShop) CloseShop();

        }

        public void BuyTrail()
        {
            Debug.Log("Bought!!!");
        }

        private void OpenShop()
        {
            _shopList.SetBool("OpenShop", true);
            _enteringShop = false;
        }
        
        private void CloseShop()
        {
            _shopList.SetBool("OpenShop", false);
            _leavingShop = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _enteringShop = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _leavingShop = true;
            }
        }
    }
}
