using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Fusion.Fluid
{
    public class SetupFluid : EditorWindow
    {
        [MenuItem("Fusion/Fluid/Setup Rigidbodys (Basic)")]
        static void SetupBasic()
        {
            int setupObjects = 0;
            Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();

            foreach(Rigidbody rb in rigidbodies)
            {
                try
                {
                    if(rb.gameObject.GetComponent<BaseFluidInteractor>() == null && rb.gameObject.GetComponent<Fluid>() == null)
                    {
                        rb.gameObject.AddComponent<BasicFluidInteractor>();

                        setupObjects++;
                    }
                }
                catch (ArgumentException e)
                {
                    Debug.LogWarning(e);
                }
            }

            Debug.Log("Successfull Setup of " + setupObjects + " Rigidbody(s)");
        }

        [MenuItem("Fusion/Fluid/Setup Rigidbodys (Complex)")]
        static void SetupComplex()
        {
            int setupObjects = 0;
            Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();

            foreach (Rigidbody rb in rigidbodies)
            {
                try
                {
                    if (rb.gameObject.GetComponent<BaseFluidInteractor>() == null && rb.gameObject.GetComponent<Fluid>() == null)
                    {
                        rb.gameObject.AddComponent<ComplexFluidInteractor>();

                        setupObjects++;
                    }
                }
                catch (ArgumentException e)
                {
                    Debug.LogWarning(e);
                }
            }

            Debug.Log("Successfull Setup of " + setupObjects + " Rigidbody(s)");
        }

        [MenuItem("Fusion/Fluid/Setup Fluid")]
        static void Fluid()
        {
            GameObject[] gameObjects = Selection.gameObjects;
            int setupObjects = 0;

            foreach (GameObject obj in gameObjects)
            {
                try
                {
                    if (obj.gameObject.GetComponent<Fluid>() == null && obj.gameObject.GetComponent<BaseFluidInteractor>() == null)
                    {
                        obj.gameObject.AddComponent<Fluid>();

                        setupObjects++;
                    }

                }
                catch (ArgumentException e)
                {
                    Debug.LogWarning(e);
                }
            }

            Debug.Log("Successfull Setup of " + setupObjects + " Object(s)");
        }
    }
}