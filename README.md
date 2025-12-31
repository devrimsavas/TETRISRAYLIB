# Tetris (Raylib / C#)

## Why another Tetris?

Today, you can find Tetris almost everywhere — even on a washing machine display.  
So the obvious question is: **why write yet another one?**

The purpose of this project was *not* to recreate the “official” or “perfect” Tetris.
It was created for a very different reason.

These days, many of us spend most of our time writing industrial-scale software:
framework-heavy backends, enterprise architectures, endless diagrams, roadmaps,
and “follow these X steps to become a Y developer” posts.

Somewhere along the way, the excitement of *old-school programming* starts to fade.

This project is an attempt to reclaim that feeling.

---

## The Real Goal

This Tetris was written **without following tutorials, guides, or existing implementations**.

The core question behind the project was simple:

> *“If I only looked at classic games, and designed everything myself,  
> how would I build this?”*

In that sense, this project is a form of **reverse engineering through design**.

I was not interested in whether this implementation matches the original Tetris
specification exactly — and honestly, I don’t even want to know.
What matters is the thinking process, not the historical accuracy.

A programmer should first ask:

> **“How can I solve this problem?”**

Not:
- which framework,
- which roadmap,
- which predefined architecture diagram.

---

## Part of a Series

This Tetris is the **first entry** in a small series of rewriting classic games
with a modern mindset but old-school discipline.

I have already written other experimental games as well — especially focusing on
how far you can push a *backend-oriented mindset* into game development.
Once those projects are properly optimized, they will be published too.

---

## Technical Overview

- **Language:** C# (.NET Console)
- **Rendering / Game Engine:** [Raylib](https://www.raylib.com/)
- **Architecture:**  
  - Clear separation between:
    - game state
    - rendering
    - input handling
    - animations
    - UI overlays (pause, game over, messages)
  - Event-driven score handling
  - Frame-time–based timing (no system clock hacks)
  - Pause-safe timers
- **Features:**
  - Classic falling block mechanics
  - Rotation & collision handling
  - Line clearing with scoring
  - Level progression with increasing difficulty
  - Pause system
  - Intro screen with animations
  - Starfield background animation
  - Persistent high-score storage (JSON)
  - Player name input screen

---

## Final Words

This project was never about proving that I can write Tetris.

It was about building a **structure**, an **architecture**, and answering the question:

> *“If I wrote this from scratch, how would **I** write it?”*

If you enjoy digging into the code, experimenting, or just running it for fun —
that’s a bonus.

Have fun.

---

## Screenshots

### Intro Screen
![Intro Screen](screenshots/intro.png)

### Gameplay
![Gameplay](screenshots/gameplay.png)
![Gameplay 2](screenshots/gameplay1.png)

### Pause Screen
![Pause Screen](screenshots/pause.png)

### Game Over
![Game Over Screen](screenshots/gameover.png)
