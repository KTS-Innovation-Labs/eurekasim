# WindTurbine Simulation Plugin | EurekaSim C# SDK

This project is a custom simulation plugin built using the EurekaSim C#.NET SDK platform. It implements a fully functional 3D WindTurbine object featuring real-time continuous blade rotation logic built with OpenGL.

## Overview
The 3D model is programmatically assembled using a tapered rectangular tower column that anchors the structure, a rectangular nacelle block housing the simulated generator system at the apex, and a central rotor hub that drives three aerodynamic blades spaced uniformly at 120-degree angles.

## Project Status

- [x] Generated C++ skeleton via EurekaSim Addin Wizard.
- [x] Added `WindTurbine` object configuration parameters.
- [x] Programmed continuous, dynamic 3D blade rotation routines.
- [x] Implement identical plugin logic using C# SDK.
- [ ] Implement identical plugin logic using Python SDK.



## Key Implementation Details

* **COM Interop Architecture:** Bridges managed C# `.NET Framework` logic with EurekaSim's native C++ engine using COM-visible wrappers (`EurekaSim.Net`) to handle experiment trees, property grids, and display lists.
* **OpenGL Rendering & Matrix Transformations:** Uses OpenGL graphics primitives (`GL_QUADS`, `GL_TRIANGLE_STRIP`, `GL_TRIANGLE_FAN`) to build the 3D geometry and matrix stack operations (`glPushMatrix`, `glPopMatrix`, `glRotatef`) to spin only the rotor blades while keeping the tower stationary.


## Video Demonstration

Watch the working C# plugin with running blade rotations in the EurekaSim application:

[![WindTurbineCS Simulation1](https://youtube.com)](https://youtu.be/YhagKaaCnkU)


