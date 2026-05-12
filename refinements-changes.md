# Refinements & Changes Log
## Project: Sands Of The Sphinx

---

# 1. Project Overview

Sands Of The Sphinx is an AI-powered Egyptian-themed riddle game developed in Unity using Ollama and the Gemma 3 4B language model.

The player is presented with dynamically generated sphinx riddles and must choose the correct answer from three multiple-choice options.

The game includes:
- Infinite AI-generated riddles
- Score tracking
- High score saving
- Three-life system
- Game Over system
- Egyptian/Sphinx atmosphere
- Local offline AI generation

The project uses:
- Unity
- Ollama
- gemma3:4b
- Local HTTP API communication
- JSON-based AI responses

This document tracks:
- Scope refinements
- Gameplay changes
- Technical changes
- AI-assisted development decisions
- Stability improvements

---

# 2. Initial Game Scope

## Original Gameplay

The initial project scope included:
- One AI-generated riddle
- Three answer choices
- One correct answer
- Score increase for correct answers

The main gameplay loop was:
1. Generate a riddle
2. Player selects an answer
3. Check correctness
4. Generate another riddle

---

# 3. Theme Refinement

## Egyptian Theme Added

The project scope was expanded to include a stronger visual and thematic identity.

### Theme Direction
The game was redesigned around:
- Ancient Egypt
- Sphinx mythology
- Hieroglyphs
- Tombs
- Pharaohs
- Pyramids
- Deserts
- Ancient curses

### Reason For Change
The theme refinement improved:
- Visual identity
- Player immersion
- Gameplay atmosphere
- Brand consistency

### AI-Assisted Decisions
AI assistance was used to:
- Design Egyptian-themed prompts
- Suggest thematic UI direction
- Create Sphinx-inspired dialogue
- Improve atmosphere consistency

---

# 4. Gameplay Refinements

## Lives System Added

### Scope Change
A life system was introduced to create challenge and tension.

### Current Rules
- Player starts with 3 lives
- Wrong answer removes 1 life
- Reaching 0 lives triggers Game Over

### Reason For Change
The original game lacked failure conditions and long-term tension.

The life system improved:
- Risk/reward gameplay
- Replayability
- Difficulty balance
- Player engagement

### Technical Changes
Added:
- Life tracking variable
- Life UI display
- Game Over state
- Incorrect answer penalty system

### AI-Assisted Decisions
AI assistance was used to:
- Plan gameplay flow
- Structure game state logic
- Design life system implementation

---

## High Score System Added

### Scope Change
Persistent high score tracking was introduced.

### New Features
- Current score tracking
- High score saving
- High score display
- Persistent local storage

### Reason For Change
The original score system lacked long-term progression.

The high score system improved:
- Replay value
- Player motivation
- Competitive gameplay

### Technical Changes
Implemented:
- PlayerPrefs save system
- Score comparison logic
- High score UI

### AI-Assisted Decisions
AI assistance was used to:
- Design save logic
- Implement PlayerPrefs
- Improve score structure

---

# 5. AI System Refinements

## Model Selection

### Selected Model
gemma3:4b

### Reason For Selection
The model was chosen because it:
- Runs efficiently on consumer hardware
- Provides fast local inference
- Produces acceptable text quality
- Works well for short-form riddles
- Has low memory requirements

### AI-Assisted Decisions
AI assistance was used to:
- Evaluate lightweight model options
- Improve prompt compatibility
- Optimize inference settings

---

## Prompt Engineering Improvements

### Problems Encountered
Early AI outputs frequently:
- Repeated riddles
- Generated invalid JSON
- Returned incorrect answer counts
- Produced inconsistent formatting

### Refinements Added
The prompt system was expanded to include:
- Strict JSON formatting
- Anti-repetition memory
- Theme randomization
- Difficulty scaling
- Random seed generation
- Retry systems

### Example Improvements
The AI was instructed to:
- Output only raw JSON
- Generate exactly 3 answers
- Avoid repeating previous riddles
- Maintain Egyptian themes

### AI-Assisted Decisions
AI assistance was heavily used to:
- Improve prompt reliability
- Reduce repetition
- Increase response variety
- Improve JSON consistency

---

# 6. Runtime Stability Improvements

## Problems Encountered

During development, several runtime issues occurred:
- IndexOutOfRangeException
- NullReferenceException
- Missing answers
- Invalid JSON parsing

### Cause
The AI occasionally generated:
- Missing arrays
- Incorrect answer counts
- Malformed JSON

---

## Stability Fixes Added

### Validation Systems
Implemented:
- JSON validation
- Array length checks
- Retry generation systems
- Error logging

### Safer UI Logic
Added:
- Bounds checking
- Null checking
- Fallback answer text

### AI-Assisted Decisions
AI assistance was used to:
- Diagnose parsing errors
- Improve defensive programming
- Refactor validation systems

---

# 7. Current Gameplay Features

## Current Systems

### Gameplay
- Infinite AI-generated riddles
- Three answer choices
- One correct answer
- Score tracking
- High score saving
- Three-life system
- Game Over system

### AI Systems
- Local Ollama inference
- Dynamic riddle generation
- Theme randomization
- Difficulty scaling
- Anti-repetition memory

### Technical Systems
- Unity integration
- JSON parsing
- Validation systems
- Error handling
- Offline inference

### Theme
- Egyptian atmosphere
- Sphinx-inspired presentation
- Hieroglyph-inspired UI
- Ancient tomb aesthetic

---

# 8. Current Prompt Structure

## Prompt Goals

The prompt system is designed to:
- Generate unique riddles
- Maintain Egyptian themes
- Prevent repeated outputs
- Produce valid JSON
- Generate exactly three answers

---

## Prompt Components

### Context Definition

The AI is instructed to act as an Egyptian sphinx riddle generator.

Example:

```text
Generate a UNIQUE Egyptian sphinx riddle.