using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Fusion.Fluid
{
    [RequireComponent(typeof(Rigidbody))]
    public class BaseFluidInteractor : MonoBehaviour
    {
        [HideInInspector] public Rigidbody rb;
        [HideInInspector] public Collider coll;

        [HideInInspector] public float volume;

        public float customVolume = 0;

        public float dampeningFactor = .1f;

        public float waterDrag = 3f;
        public float waterAngularDrag = 1f;

        [HideInInspector] float airDrag;
        [HideInInspector] float airAngularDrag;

        [HideInInspector] public bool inFluid;
        [HideInInspector] public Fluid fluid;

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
        #endregion

        #region Functions for FluidUpdate
        private void FixedUpdate()
        {
            if (inFluid)
                FluidUpdate();
        }

        public void EnterFluid(Fluid enteredFluid)
        {
            fluid = enteredFluid;
            inFluid = true;

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