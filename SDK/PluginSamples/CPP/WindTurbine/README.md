# WindTurbine Simulation Plugin | EurekaSim C++ SDK

This project is a custom simulation plugin built using the EurekaSim SDK platform. It implements a fully functional 3D WindTurbine object featuring real-time continuous blade rotation logic built with OpenGL.

## Overview
The 3D model is programmatically assembled using a tapered rectangular tower column that anchors the structure, a rectangular nacelle block housing the simulated generator system at the apex, and a central rotor hub that drives three aerodynamic blades spaced uniformly at 120-degree angles.
## Project Status

- [x] Generated C++ skeleton via EurekaSim Addin Wizard.
- [x] Added `WindTurbine` object configuration parameters.
- [x] Programmed continuous, dynamic 3D blade rotation routines.
- [ ] Implement identical plugin logic using C# SDK.
- [ ] Implement identical plugin logic using Python SDK.

## Key Implementation Details

* **Dynamic Animation Engine:** Uses structural rotational matrix transformations to update blade movement continuously on each frame render.
* **Isolated Environment:** The plugin has been customized so that only the WindTurbine simulation components remain active in the system context.

## Video Demonstration

Watch the working C++ plugin with running blade rotations in the EurekaSim application:

[![WindTurbine Simulation Demo](https://youtube.com)](https://youtu.be/Budim0jOzn0)

