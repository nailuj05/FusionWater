using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Fusion.Fluid
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseFluidInteractor : MonoBehaviour
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

        [HideInInspector] public int inFluidCount;
        [HideInInspector] public Fluid fluid;

        public bool simulateWaterTurbulence;
        [Range(0, 5)] public float turbulenceStrength = 1;

        [HideInInspector] public float[] rndTimeOffset = new float[6];

        private float time;

        public float floatStrength = 2;

        public abstract void FluidUpdate();

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
            time += Time.fixedDeltaTime / 4;

            if (inFluidCount > 0)
                FluidUpdate();
        }

        public Vector3 GenerateTurbulence()
        {
            Vector3 turbulence = new Vector3(Mathf.PerlinNoise(time + rndTimeOffset[0], time + rndTimeOffset[1]) * 2 - 1,
                                        0,
                                        Mathf.PerlinNoise(time + rndTimeOffset[4], time + rndTimeOffset[5]) * 2 - 1);

            Debug.DrawRay(transform.position, turbulence);

            return turbulence * turbulenceStrength;
        }

        public void EnterFluid(Fluid enteredFluid)
        {
            fluid = enteredFluid;
            inFluidCount++;

            waterDrag = fluid.drag;
            waterAngularDrag = fluid.angularDrag;

            rb.drag = waterDrag;
            rb.angularDrag = waterAngularDrag;
        }

        public void ExitFluid(Fluid fluidToExit)
        {
            if(fluid == fluidToExit)
                fluid = null;

            inFluidCount--;

            if(inFluidCount == 0)
            {
                rb.drag = airDrag;
                rb.angularDrag = airAngularDrag;
            }
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