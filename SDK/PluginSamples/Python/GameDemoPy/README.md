### GameDemoPy | EurekaSim

A feature-rich interactive 3D simulation plugin developed in Python using the EurekaSim Addin Wizard.


### Overview


GameDemoPy brings fun and engaging 3D experiences into EurekaSim. It features two impressive real-time simulations:

* Interactive Football Penalty Kick Game with realistic physics and intelligent goalkeeper AI.
* Animated Elephant walking in a beautiful savannah environment.



### Features


# Interactive Football Penalty Kick Game


Keyboard controls: A (Left), S (Center), D (Right)

Realistic ball physics with gravity, bounce, and spin

Smart goalkeeper with predictive diving mechanics

Score tracking, attempt counter, and clear result messages (GOAL!, SAVED!, MISSED!)



# Animated Elephant in Savannah



Lifelike walking animation with synchronized leg movement

Dynamic animations: trunk swing, ear flapping, head bob, and tail sway

Trumpeting animation

Rich procedural environment including pond with water ripples \& lily pads, acacia trees, clouds, sun, and grass



# Additional Objects



Multiple simulation modes: Object Rotation, Random Movement, and Penalty Kick View





### How It Works



The plugin uses a main simulation loop with advanced OpenGL rendering. The penalty game implements projectile physics and AI-based goalkeeper reactions, while the elephant uses mathematical sine/cosine functions for natural organic animations.

Usage Instructions



* Load the GameDemoPy plugin in EurekaSim.
* Select the experiment from Settings.
* From the List select preferred Experiment
* Click Start Simulation.
* For Penalty Kick: Focus the simulation window and press A, S, or D to shoot.
* For Elephant view Just select Elephant View Option from List



### Usage Warnings



* The Python version is more resource-intensive than the C++ version.
* Avoid resizing or minimizing the simulation window during runtime to prevent freezing.
* Keyboard controls work only when the 3D view is active and focused.



### Demo Video

Watch GameDemoPy

https://youtu.be/t-9VymkrAag?si=DEUHE\_e04xbddLsJ

