using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Fusion.Fluid
{
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu("FusionWater/_Internal/BaseFluidInteractor")]
    public class BaseFluidInteractor : MonoBehaviour
    {
        [HideInInspector] public Rigidbody rb;
        [HideInInspector] public Collider coll;

        [HideInInspector] public float volume;

        public float customVolume = 0;

        public float dampeningFactor = .1f;

        private float waterDrag = 3f;
        private float waterAngularDrag = 1f;

        [HideInInspector] float airDrag;
        [HideInInspector] float airAngularDrag;

        [HideInInspector] public bool inFluid;
        [HideInInspector] public Fluid fluid;

        public bool simulateWaterTurbulence;
        [Range(0, 5)] public float turbulenceStrength = 1;

        [HideInInspector] public float[] rndTimeOffset = new float[6];

        private float time;

        public float floatStrength = 2;

        public virtual void FluidUpdate()
        {
            //Do the Fluid Update
        }

        #region UnityFunctions
        public virtual void Start()
        {
            coll = GetComponent<Collider>();
            rb = GetComponent<Rigidbody>();

            airDrag = rb.drag;
            airAngularDrag = rb.angularDrag;

            if (customVolume != 0)
                volume = customVolume;
            else
                volume = CalculateVolume();
        }

        private void Update()
        {
            if (customVolume != 0)
                volume = customVolume;
        }

        public void Awake()
        {
            rndTimeOffset = new float[6];

            for (int i = 0; i < 6; i++)
            {
                rndTimeOffset[i] = Random.Range(0f, 6f);
            }
        }

        #endregion

        #region Functions for FluidUpdate
        private void FixedUpdate()
        {
            time += Time.fixedDeltaTime / 2;

            if (inFluid)
                FluidUpdate();
        }

        public Vector3 GenerateTurbulence()
        {
            Vector3 turbulence = new Vector3(Mathf.PerlinNoise(time + rndTimeOffset[0], time + rndTimeOffset[1]) * 2 - 1,
                                        Mathf.PerlinNoise(time + rndTimeOffset[2], time + rndTimeOffset[3]) * 2 - 1,
                                        Mathf.PerlinNoise(time + rndTimeOffset[4], time + rndTimeOffset[5]) * 2 - 1);

            Debug.DrawRay(transform.position, turbulence);

            return turbulence * turbulenceStrength;
        }

        public void EnterFluid(Fluid enteredFluid)
        {
            fluid = enteredFluid;
            inFluid = true;

            waterDrag = fluid.drag;
            waterAngularDrag = fluid.angularDrag;

            rb.drag = waterDrag;
            rb.angularDrag = waterAngularDrag;
        }

        public void ExitFluid()
        {
            fluid = null;
            inFluid = false;

            rb.drag = airDrag;
            rb.angularDrag = airAngularDrag;
        }
        #endregion

        #region Functions
        public float CalculateVolume()
        {
            return coll.bounds.size.x * coll.bounds.size.y * coll.bounds.size.z;
        }
        #endregion
    }
}