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
```

---

# 9. UI & UX Refinements

## Tutorial Screen Added

### Scope Change
A tutorial screen was added to the main menu to help new players understand the game's mechanics before starting.

### New Features
- Dedicated tutorial panel accessible from the main menu
- Question mark button on the main menu opens the tutorial screen
- Tutorial explains core rules: riddle answering, lives system, and scoring

### Reason For Change
New players had no in-game guidance on how to play.

The tutorial screen improved:
- Onboarding experience
- Player clarity
- Accessibility for first-time players

### Technical Changes
Added:
- Tutorial UI panel
- Question mark button with onClick event
- Panel show/hide logic

---

## Game UI Redesign

### Scope Change
The in-game UI layout for riddle questions and answer choices was redesigned.

### Changes Made
- Question text now properly fits within its border container
- Answer button text now properly fits within its border containers
- Text overflow and clipping issues resolved

### Reason For Change
Previously, long riddle questions and answer text would overflow or clip outside their UI boundaries, reducing readability and visual polish.

The redesign improved:
- Text legibility
- Visual consistency
- Overall UI cleanliness

### Technical Changes
Updated:
- Text component sizing and wrapping settings
- Layout element constraints on question and answer containers
- Content size fitter adjustments where applicable

---

## Lives Display Replaced With Ankh Icons

### Scope Change
The numerical lives counter was replaced with Ankh icon images to represent remaining lives.

### Changes Made
- Three Ankh icons displayed as the life indicator
- Ankhs are hidden or visually deactivated as lives are lost
- Removed plain numeric text life counter

### Reason For Change
The numeric counter did not align with the Egyptian theme aesthetic.

The Ankh icons improved:
- Thematic consistency
- Visual atmosphere
- Player immersion

### Technical Changes
Updated:
- Life UI system to toggle Ankh image GameObjects instead of updating text
- Ankh icons added as UI Image elements
- Life deduction logic updated to deactivate corresponding Ankh images

---

## Custom Mouse Cursor Added

### Scope Change
A custom mouse cursor was implemented to replace the default system cursor.

### Changes Made
- Custom cursor image applied throughout the game
- Cursor reflects the Egyptian theme of the project

### Reason For Change
The default system cursor broke visual immersion and felt inconsistent with the game's themed presentation.

The custom cursor improved:
- Thematic immersion
- Visual polish
- Overall presentation quality

### Technical Changes
Implemented:
- Cursor.SetCursor() call on game initialisation
- Custom cursor texture assigned in the Unity Inspector
- Hotspot configured for accurate click registration

---

# 10. Compatibility Improvements

## Dynamic AI Model Detection

### Scope Change
The game was updated to automatically detect which Ollama AI models the player has installed and select the most suitable one at runtime.

### New Features
- Game queries the local Ollama API for installed models on startup
- Installed models are evaluated and ranked by suitability for riddle generation
- Best available model is automatically selected
- Fallback handling if no compatible model is found

### Reason For Change
Previously the game was hardcoded to use gemma3:4b, which required players to have that specific model installed. Players with different models installed could not use the game without manually modifying settings.

The dynamic detection improved:
- Player compatibility across different hardware setups
- Flexibility for players who have alternative models installed
- Resilience against missing or renamed model versions

### Technical Changes
Implemented:
- HTTP GET request to Ollama `/api/tags` endpoint on startup
- JSON parsing of returned model list
- Model scoring and selection logic based on name matching and preference priority
- Selected model passed dynamically into all riddle generation requests
- Error handling for cases where Ollama is not running or no models are available

### AI-Assisted Decisions
AI assistance was used to:
- Design the model detection and ranking logic
- Structure the compatibility fallback system
- Improve robustness of the model selection pipeline