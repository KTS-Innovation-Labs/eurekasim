# Tower of Hanoi C++ Plugin | EurekaSim

A **Tower of Hanoi simulation plugin** developed using the **EurekaSim Addin Wizard**.  
This plugin visually demonstrates the classical **Tower of Hanoi problem** using OpenGL inside the EurekaSim environment.

---

## Overview

The Tower of Hanoi is a famous mathematical puzzle that involves moving a set of disks from one column to another, following specific rules. This plugin brings the puzzle to life by automatically solving it step by step, allowing users to observe the recursive solution in action.

---

## Features

- **Adjustable Simulation Speed**  
  Control how fast the puzzle is solved with customizable simulation intervals.  

- **Configurable Number of Disks**  
  Choose the number of disks (between **3 and 8**) placed on the starting column.  

- **Automated Solution**  
  The simulation automatically solves the puzzle from the starting column to the ending column, showing each move in sequence.  

---

## How It Works

The Tower of Hanoi puzzle follows three simple rules:

1. Only one disk may be moved at a time.  
2. A disk can only be placed on top of a larger disk or on an empty column.  
3. All disks start on the first column and must be moved to the last column.

The algorithm is solved **recursively**:

- Move `n-1` disks from the starting column to the auxiliary column.  
- Move the largest disk to the destination column.  
- Move the `n-1` disks from the auxiliary column to the destination column.  

This recursive process is repeated until the puzzle is fully solved.

---

## Usage Warning

- Do **not** interact with or resize the simulation window while the puzzle is running.  
  Doing so may cause an **OpenGL crash**.  

---

## Demo Video

Watch the Tower of Hanoi plugin in action:  

[![Tower of Hanoi Demo](https://img.youtube.com/vi/HIW4j50asns/0.jpg)](https://www.youtube.com/watch?v=HIW4j50asns)  

[Click to watch on YouTube](https://www.youtube.com/watch?v=HIW4j50asns)
