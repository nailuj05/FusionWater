using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fusion.Fluid {
    public class BasicFluidInteractor : BaseFluidInteractor
    {
        public float floatStrength = 2;

        public override void FluidUpdate()
        {

            float difference = transform.position.y - fluid.transform.position.y;

            if(difference < 0)
            {
                rb.AddForceAtPosition(Vector3.up * floatStrength * Mathf.Abs(difference) * Physics.gravity.magnitude * volume, transform.position, ForceMode.Force);
                rb.AddForceAtPosition(rb.velocity * dampeningFactor * volume, transform.position, ForceMode.Force);
            }
        }
    }
}