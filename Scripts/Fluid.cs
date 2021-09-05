using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fusion.Fluid
{
    public class Fluid : MonoBehaviour
    {
        public float density = 1;
        public float Density { get { return density; } set { density = value; } }

        public float drag = 1;
        public float Drag { get { return drag; } set { drag = value; } }

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
                fluidInteractor.ExitFluid();
            }
        }
    }
}
