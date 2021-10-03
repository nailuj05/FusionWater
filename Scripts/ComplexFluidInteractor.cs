using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Fusion.Fluid {
    [AddComponentMenu("FusionWater/ComplexFluidInteractor")]
    public class ComplexFluidInteractor : BaseFluidInteractor
    {
        public List<Transform> floaters = new List<Transform>();

        public override void Start()
        {
            coll = GetComponent<Collider>();
            rb = GetComponent<Rigidbody>();

            if (customVolume != 0)
                volume = customVolume;
            else
                volume = CalculateVolume();
        }

        public override void FluidUpdate()
        {
            foreach (Transform floater in floaters)
            {
                float difference = floater.position.y - fluid.transform.position.y;

                if(difference < 0)
                {
                    Vector3 buoyancy = (Vector3.up * floatStrength * Mathf.Abs(difference) * Physics.gravity.magnitude * volume * fluid.density);

                    if (simulateWaterTurbulence)
                    {
                        buoyancy += GenerateTurbulence();
                    }

                    rb.AddForceAtPosition(buoyancy, floater.position, ForceMode.Force);
                    rb.AddForceAtPosition(rb.velocity * (dampeningFactor / floaters.Count) * volume, floater.position, ForceMode.Force);
                }
            }
        }

        private void OnDrawGizmos()
        {
            //Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);

            foreach (Transform floater in floaters)
            {
                float difference = 0;

                if (inFluidCount > 0)
                    difference = floater.position.y - fluid.transform.position.y;

                if (difference < 0)
                {
                    Gizmos.color = Color.green;    
                }
                else
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawSphere(floater.position, .1f);
            }
        }

        #region Functions

        public bool IsPointUnderWater(Vector3 point)
        {
            return fluid.GetComponent<Collider>().bounds.Contains(point);
        }

        public Vector3[] DefineCorners()
        {
            Vector3 extents = coll.bounds.size / 2;

            Vector3[] corners = new Vector3[] {
                new Vector3(extents[0], extents[1], extents[2]),
                new Vector3(extents[0], extents[1], extents[2]) * -1,
                new Vector3(-extents[0], extents[1], extents[2]),
                new Vector3(-extents[0], extents[1], extents[2]) * -1,
                new Vector3(extents[0], -extents[1], extents[2]),
                new Vector3(extents[0], -extents[1], extents[2]) * -1,
                new Vector3(extents[0], extents[1], -extents[2]),
                new Vector3(extents[0], extents[1], -extents[2]) * -1,
            };

            return corners;
        }
        #endregion
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ComplexFluidInteractor))]
    public class ComplexFluidInteractorEditor : Editor
    {
        [Range(0.1f, 2f)]
        public float boundsScaler = 1;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ComplexFluidInteractor complexFluidInteractor = (ComplexFluidInteractor)target;

            if(GUILayout.Button("Use Bounding Box Corners"))
            {
                complexFluidInteractor.coll = complexFluidInteractor.GetComponent<Collider>();

                Vector3[] corners = complexFluidInteractor.DefineCorners();

                complexFluidInteractor.floaters = new List<Transform>();

                foreach (Vector3 corner in corners)
                {
                    GameObject cornerObj = new GameObject("CornerFloater");
                    cornerObj.transform.position = complexFluidInteractor.transform.position + corner;
                    cornerObj.transform.parent = complexFluidInteractor.transform;

                    complexFluidInteractor.floaters.Add(cornerObj.transform);
                }
            }
        }
    }
#endif
}