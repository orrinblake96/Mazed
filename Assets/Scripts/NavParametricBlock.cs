using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIDCITY
{
    public class NavParametricBlock : MonoBehaviour
    {

        #region Fields
        
        public Transform basePrefab, startPoint, endPoint;
        
        private NavigationCityManager _cityManager;
        private int _bottomLeftIndX, _bottomLeftIndZ, _bottomLeftIndY;
        private int _topRightIndX, _topRightIndZ, _topRightIndY;
        private Vector3 _midPoint, _scaleVec;
        
        #endregion

        #region Properties	
        
        #endregion

        #region Methods


        public void Initialize(Vector3 startVec, Vector3 endVec)
        {
            int startX = Mathf.RoundToInt(startVec.x + 20.01f);
            int startY = Mathf.RoundToInt(startVec.y);
            int startZ = Mathf.RoundToInt(startVec.z + 20.01f);
            
            int endX = Mathf.RoundToInt(endVec.x + 20.01f);
            int endY = Mathf.RoundToInt(endVec.y);
            int endZ = Mathf.RoundToInt(endVec.z + 20.01f);

            if (startX < endX)
            {
                _bottomLeftIndX = startX;
                _topRightIndX = endX;
            }
            else
            {
                _bottomLeftIndX = endX;
                _topRightIndX = startX;
            }
            
            if (startY < endY)
            {
                _bottomLeftIndY = startY;
                _topRightIndY = endY;
            }
            else
            {
                _bottomLeftIndY = endY;
                _topRightIndY = startY;
            }
            
            if (startZ < endZ)
            {
                _bottomLeftIndZ = startZ;
                _topRightIndZ = endZ;
            }
            else
            {
                _bottomLeftIndZ = endZ;
                _topRightIndZ = startZ;
            }

            for (int x = _bottomLeftIndX; x < _topRightIndX; x++)
            {
                for (int y = _bottomLeftIndY; y < _topRightIndY; y++)
                {
                    for (int z = _bottomLeftIndZ; z < _topRightIndZ; z++)
                    {
                        _cityManager.SetSlot(x, y, z, true);
                    }

                    _midPoint = (startVec + endVec) / 2.0f;
                    _scaleVec = endVec - _midPoint;
                    _midPoint.y = Mathf.Min(startVec.y, endVec.y);

                    Transform newBlocks = Instantiate(basePrefab, _midPoint, Quaternion.identity, _cityManager.transform);
                    newBlocks.localScale = _scaleVec;
                }
            }
        }

        #region Unity Methods

        // Use this for internal initialization
        void Awake()
        {
        }

        // Use this for external initialization
        void Start()
        {

            _cityManager = NavigationCityManager.Instance;
            Initialize(startPoint.position, endPoint.position);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        #endregion
        #endregion
    }
}

