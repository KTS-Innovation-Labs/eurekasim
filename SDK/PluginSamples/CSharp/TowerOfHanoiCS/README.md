# Tower of Hanoi C# Plugin | EurekaSim

A **Tower of Hanoi simulation plugin** developed in **C#** using the **EurekaSim Addin Wizard**.  
This plugin demonstrates the classical **Tower of Hanoi problem** with a real-time OpenGL visualization inside the EurekaSim environment.

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

## Usage Warning

- Do **not** interact with or resize the simulation window while the puzzle is running.  
  Doing so may cause an **OpenGL crash**.  

---

## Demo Video

Watch the Tower of Hanoi C# plugin in action:  

[![Tower of Hanoi C# Demo](https://img.youtube.com/vi/xizAMvTeVw0/0.jpg)](https://www.youtube.com/watch?v=xizAMvTeVw0)  

[Click to watch on YouTube](https://www.youtube.com/watch?v=xizAMvTeVw0)
