using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fusion.Fluid
{
    [AddComponentMenu("FusionWater/Fluid")]
    public class Fluid : MonoBehaviour
    {
        public float density = 1;

        public float drag = 1;

        public float angularDrag = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out BaseFluidInteractor fluidInteractor))
            {
                fluidInteractor.EnterFluid(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out BaseFluidInteractor fluidInteractor))
            {
                fluidInteractor.ExitFluid(this);
            }
        }
    }
}
