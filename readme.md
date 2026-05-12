# Sands Of The Sphinx

An AI-powered Egyptian-themed riddle game built in Unity using Ollama and the Gemma 3:4B language model.

---

# Overview

Sands Of The Sphinx is a procedural riddle game where players must answer dynamically generated sphinx riddles to survive.

The game uses a locally hosted AI model through Ollama to generate infinite Egyptian-themed riddles in real time.

Players:
- Start with 3 lives
- Lose a life for incorrect answers
- Gain score for correct answers
- Attempt to achieve the highest score possible

The game features:
- Infinite replayability
- AI-generated riddles
- Offline local AI inference
- Egyptian/Sphinx atmosphere
- Dynamic difficulty scaling
- High score tracking

---

# Features

## Gameplay Features
- AI-generated riddles
- Three multiple-choice answers
- Three-life system
- Score tracking
- High score saving
- Game Over system

## AI Features
- Local Ollama inference
- Dynamic content generation
- Theme randomization
- Anti-repetition systems
- Difficulty scaling

## Technical Features
- Unity integration
- JSON parsing
- Local HTTP API communication
- Runtime validation systems
- Offline functionality

---

# Installation Instructions

## 1. Install Unity

Download Unity Hub and install Unity 2022 LTS or newer.

Recommended modules:
- Windows Build Support
- TextMeshPro

Unity Download:
https://unity.com/download

---

## 2. Install Ollama

Download and install Ollama.

Ollama Download:
https://ollama.com/download

After installation, verify Ollama works:

```bash
ollama --version