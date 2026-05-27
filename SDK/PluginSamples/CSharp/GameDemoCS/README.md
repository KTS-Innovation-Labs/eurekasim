
### GameDemoCS|EurekaSim (C# Version)



A high-performance interactive 3D simulation plugin developed in C# using the EurekaSim Addin Wizard.



### Overview



ObjectDemoCSharp brings smooth and engaging 3D experiences into EurekaSim. It features two impressive real-time simulations:



* Interactive Football Penalty Kick Game with realistic physics and intelligent goalkeeper AI.

* Animated Elephant walking in a beautiful savannah environment.



This is the C# implementation, which offers a good balance between performance and ease of development.



### Features



Interactive Football Penalty Kick Game

- Keyboard controls: A (Left), S (Center), D (Right)

- Realistic ball physics with gravity, bounce, air resistance, and spin

- Smart goalkeeper with predictive diving mechanics based on ball trajectory

- Score tracking, attempt counter, and clear result messages (GOAL!, SAVED!, MISSED!)

- Animated kicker with realistic kicking motion



Animated Elephant in Savannah

- Lifelike walking animation with synchronized leg movement

- Dynamic animations: trunk swing, ear flapping, head bob, and tail sway

- Trumpeting animation

- Rich procedural environment including pond with water ripples & lily pads, acacia trees, animated clouds, sun with rays, grass clumps with wind effect, and hills



Additional Simulation Modes

- Object Rotation Mode

- Random Movement Mode

- Penalty Kick View (Dedicated immersive mode)



### How It Works



The plugin uses a main simulation loop with optimized OpenGL rendering through EurekaSim's API. 

The penalty kick game implements projectile physics and AI-based goalkeeper reactions. 

The elephant animation uses mathematical sine/cosine functions for natural organic movement.



### Usage Instructions



1\. Load the ObjectDemoCSharp plugin in EurekaSim.

2\. Go to Settings and select the experiment.

3\. From the list, select your preferred Experiment:

 - Football Penalty Kick

 - Elephant View

4\. Click Start Simulation.

5\. For Penalty Kick: Focus the simulation window and press A, S, or D to shoot.

6\. For Elephant: Select "Elephant View" from the list.



### Usage Warnings



* Avoid resizing or minimizing the simulation window during runtime to prevent freezing.

* Keyboard controls work only when the 3D view is active and focused.

* Ensure your system has proper OpenGL support.



### Demo Video



Watch ObjectDemo (C# Version)

https://youtu.be/-YaDDUxXz2Q



