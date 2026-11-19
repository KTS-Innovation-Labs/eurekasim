# WindTurbine Simulation Plugin | EurekaSim Python SDK

A custom 3D simulation add-in developed on the EurekaSim Python SDK platform. This plugin renders an interactive 3D wind turbine model with smooth, continuous blade rotation powered by OpenGL.

## Overview

The 3D wind turbine is constructed programmatically from four main parts: a sturdy ground pad, a tapered rectangular tower column, a top nacelle block housing the generator unit, and a central rotor hub connected to three evenly spaced blades angled at 120 degrees.

## Project Status

- [x] Generated base plugin structure via EurekaSim Addin Wizard.
- [x] Added `WindTurbine` object selection and property controls.
- [x] Implemented continuous 3D rotor blade animation.
- [x] Built identical plugin functionality using C# SDK.
- [x] Built identical plugin functionality using Python SDK.

## Key Implementation Details

* **COM Server Integration:** Connects Python scripts to EurekaSim's core C++ simulation engine via `pywin32` and `EurekaSimLib` COM interfaces to manage experiment trees, property windows, and graphics display lists.
* **OpenGL 3D Geometry & Animation:** Draws the turbine structure using OpenGL primitives (`GL_QUADS`, `GL_QUAD_STRIP`, `GL_TRIANGLE_FAN`) and isolates blade rotation using matrix stack transformations (`glPushMatrix`, `glRotatef`, `glPopMatrix`) so the tower remains fixed.

## Video Demonstration

Watch the working Python plugin with running blade rotations in the EurekaSim application:

[WindTurbinePy Simulation](https://www.youtube.com/watch?v=Y0q0YEKmL9Q)
