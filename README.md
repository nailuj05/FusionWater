# FusionWater

A simple way to add buoyancy / water physics in Unity, built on the existing physics engine.
This package can be downloaded from the releases section and dragged into any Unity Project to install.
There are Shaders/Materials provided for both URP and Standard RP.

![BoatDemo_repeat](https://user-images.githubusercontent.com/57530068/132723739-422869ae-c104-4038-a6ef-3d7cbfd650fd.gif)


## Setup Guide

Add the ```Fluid.cs``` to your water (or any fluids) plane, be sure to have a collider for the volume setup, add a ```FluidInteractor``` to all your objects, that you want to be affected.

### The Base Fluid Interactor

This class can and should NOT be used as a component, as it has no actual code, that gets executed and is only internal.
```BaseFluidInteractor.cs```, not to be confused with ```BasicFluidInteractor.cs```, is the base class all FluidInteractors should derive from the BaseFluidInteractor, as it handles entering/exiting a fluid and calls the ```FluidUpdate()``` function. Global Functions, that are needed on all FluidInteractors, like the Turbulence Functions, are also defined here.

The BaseFluidInteractor also defines the basic settings all Interactors inherit. Those are 
```
- Custom Volume: Used to set a custom volume. 0 means the volume will be calculated using the colliders
- Dampening Factor: The dampening applied to the object
- Simulate Water Turbulence: Adds random disturbence forces generated from perlin noise
- Turbulence Strength: The strength of the applied forces
- Float Strength: Use this for changing the buoyant force without changing the mass
```


### The Basic Fluid Interactor

```BasicFluidInteractor.cs``` this is the simplest and easiest FluidInteractor. It calculates everything from the center of the objects, this can cause weird behaviours with more streched out objects, like a wooden plank for example. There for it is great for smaller objects, backgroud props, cuboids and spheres. Using the simulated turbulence you can easily add some slight movement, so your objects float, rotate and move a bit and aren't just staying "frozen" after a while. 

### The Complex Fluid Interactor

```ComplexFluidInteractor.cs``` this a more complex FluidInteractor to get a more realistic/belivable result, especially with larger, more complex objects. This is great for boats, ships, large debris or long wooden planks ;). Instead of calulating the buoyant forces for the center of an object, the ComplexFluidInteractor calculates it for multiple points, that can be placed on your object. Those "floaters" calculate their buoyant forces based on their own submergance and then apply it on the main objects rigidbody at the according position. 
This allows one to place floaters on the edges of a longer object or along the hull of a ship to better model its submerged volume and buoyancy.  
In the Unity Editor the floaters can be automatically placed at the corners of the colliders bounding box or placed manually. A floater is nothing more than an empty gameobject, that is added to the ComplexFluidInteractors ```Floaters``` List. The floaters are drawn as spheres in the scene view, green if in air, red if submerged.

![BoatFloaters_repeat](https://user-images.githubusercontent.com/57530068/132723767-227807fd-65e4-4f0a-8c63-6c03e4aa73d3.gif)

