# FusionWater

A simple way to add buoyancy / water physics in Unity, build on the existing physics engine.
This package can be downloaded from the releases section and dragged into any Unity Project to install.
There are Shaders/Materials provided for both URP and Standard RP.

![BoatDemo (1)](https://user-images.githubusercontent.com/57530068/132409499-81a7b967-cc93-4b6a-aefd-6f1ba1a82a80.gif)

## Setup Guide

Add the ```Fluid.cs``` to your water (or any fluids) plane, be sure to have a collider for the volume setup, add a ```FluidInteractor``` to all your objects, that you want to be affected.

### The Base Fluid Interactor

This class can and should NOT be used as a component, as it has no actual code, that gets executed and is only internal.
```BaseFluidInteractor.cs```, not to be confused with ```BasicFluidInteractor.cs```, is the base class all FluidInteractors should derive from the BaseFluidInteractor, as it handles entering/exiting a fluid and calls the ```FluidUpdate()``` function. Global Functions, that are needed on all FluidInteractors, like the Turbulence Functions, are also defined here.

The BaseFluidInteractor also defines the basic settings all Interactors inherit. Those are 
```
- Custom Volume: Used to set a custom volume, 0 means the volume will be calculated using the colliders
- Dampening Factor: The dampening applied to the object
- Simulate Water Turbulence: Adds random disturbence forces generated from perlin noise
- Turbulence Strength: The strength of the applied forces
- Float Strength: Use this for changing the buoyant force without changing the mass
```


### The Basic Fluid Interactor

```BasicFluidInteractor.cs``` this is the simplest and easiest FluidInteractor. It calculates everything from the center of the objects. There for it is great for smaller objects, backgroud props, cuboids and spheres. 
