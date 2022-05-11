using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fusion.Fluid {
    [AddComponentMenu("FusionWater/BasicFluidInteractor")]
    public class BasicFluidInteractor : BaseFluidInteractor
    {
        public override void FluidUpdate()
        {
            var fluidSurface = fluid.coll ? fluid.coll.bounds.max.y : fluid.transform.position.y;
            float difference = transform.position.y - fluidSurface;

            if(difference < 0)
            {
                Vector3 buoyancy = Vector3.up * floatStrength * Mathf.Abs(difference) * Physics.gravity.magnitude * volume * fluid.density;

                if (simulateWaterTurbulence)
                {
                    buoyancy += GenerateTurbulence();

                    rb.AddTorque(GenerateTurbulence() * 0.5f);
                }

                rb.AddForceAtPosition(buoyancy, transform.position, ForceMode.Force);
                rb.AddForceAtPosition(-rb.velocity * dampeningFactor * volume, transform.position, ForceMode.Force);
            }
        }
    }
}