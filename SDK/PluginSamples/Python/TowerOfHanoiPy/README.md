# Tower of Hanoi Python Plugin | EurekaSim

A **Tower of Hanoi simulation plugin** developed in **Python** using the **EurekaSim Addin Wizard**.  
This plugin demonstrates the classical **Tower of Hanoi problem** with a step-by-step OpenGL visualization inside the EurekaSim environment.

---

## Overview

The Tower of Hanoi is a mathematical puzzle that involves moving a set of disks from one column to another, following specific rules.  
This plugin brings the puzzle to life by automatically solving it step by step, making the recursive solution easy to observe.

---

## Features

- **Adjustable Simulation Speed**  
  Control the pace of the puzzle solution with customizable simulation intervals.  

- **Configurable Number of Disks**  
  Select the number of disks (between **3 and 8**) to be placed on the starting column.  

- **Automated Solution**  
  The plugin solves the puzzle automatically, moving all disks from the starting column to the ending column.  

---

## How It Works

The puzzle is governed by three rules:

1. Only one disk can be moved at a time.  
2. A disk may only be placed on top of a larger disk or on an empty column.  
3. All disks must be moved from the first column to the last column.  

The solution is **recursive**:

- Move `n-1` disks from the starting column to an auxiliary column.  
- Move the largest disk to the destination column.  
- Move the `n-1` disks from the auxiliary column to the destination column.  

This sequence continues until all disks are transferred.

---

## Usage Warnings

- The **Python version is slower and more resource-intensive** compared to the C++ plugin.  
- Any **interaction or interruption** with the simulation window may cause the simulation to **freeze or crash**.  
- Avoid resizing or minimizing the simulation window during execution.  

---

## Demo Video

Watch the Tower of Hanoi Python plugin in action:  

[![Tower of Hanoi Python Demo](https://img.youtube.com/vi/c01nmhN3S7k/0.jpg)](https://www.youtube.com/watch?v=c01nmhN3S7k)  

[Click to watch on YouTube](https://www.youtube.com/watch?v=c01nmhN3S7k)
