# Ollama Integration Plan
## Project: Sands of The Sphinx

---

# 1. Overview

Sands Of The Sphinx is an AI-powered Egyptian-themed riddle game built in Unity using Ollama for local language model inference.

The game presents players with dynamically generated riddles inspired by:
- Ancient Egypt
- Sphinx mythology
- Hieroglyphs
- Pharaohs
- Tombs
- Deserts
- Egyptian gods

Players receive:
- One riddle
- Three multiple-choice answers
- One correct answer

The player's score increases for every correct answer.

The game uses locally hosted AI inference through Ollama to generate infinite replayable riddles without requiring an internet connection.

---

# 2. Model Choice

## Selected Model
gemma3:4b

## Reason For Selection

The Gemma 3:4B model was selected because it provides:
- Fast local inference
- Low hardware requirements
- Good text generation quality
- Reliable JSON formatting
- Small memory footprint
- Suitable performance for real-time gameplay

The model performs well for:
- Short-form text generation
- Riddles
- NPC dialogue
- Multiple choice generation

## Advantages

- Runs locally through Ollama
- No internet required
- No API costs
- Fast response times
- Easy Unity integration
- Good balance between quality and performance

## Limitations

- Occasionally repeats riddles
- Can generate malformed JSON
- Limited reasoning compared to larger models
- Requires prompt engineering for consistent formatting

---

# 3. Inference Timing

## Inference Pipeline

1. Player answers current riddle
2. Unity requests new riddle
3. Ollama generates response
4. Unity parses JSON
5. UI updates with new riddle

## Estimated Timing

| Stage | Estimated Time |
|---|---|
| HTTP request creation | 5-10 ms |
| Ollama processing | 1-4 seconds |
| JSON parsing | <1 ms |
| UI update | <1 ms |

## Performance Notes

Inference speed depends on:
- CPU performance
- GPU acceleration availability
- Prompt length
- Model size
- Current system load

Typical performance:
- CPU-only laptops: 2-6 seconds
- Mid-range GPU systems: 1-3 seconds

## Optimization Strategies

- Keep prompts short
- Limit memory/history size
- Use lightweight models
- Use asynchronous coroutines
- Generate only one riddle at a time

---

# 4. Data Flow

## System Architecture

Unity communicates with Ollama through a local HTTP API.

Player Input
    V
GameManager
    V
OllamaManager
    V HTTP Request
Ollama Local API
    V
gemma3:4b
    V JSON Response
Unity JSON Parsing
    V
UIManager
    V
Player Sees New Riddle

# 5. Prompt Structure

## Purpose

The prompt structure is designed to ensure that the AI:
- Generates unique riddles
- Maintains the Egyptian theme
- Produces valid JSON
- Avoids repeated outputs
- Generates exactly three answers
- Keeps only one correct answer

---

## Prompt Components

### 1. Context Definition

The AI is instructed to behave as an Egyptian sphinx riddle generator.

Example:

```text
Generate a UNIQUE Egyptian sphinx riddle.
```

This establishes:
- Theme
- Tone
- Content type

---

### 2. Theme Injection

A random Egyptian theme is added to increase variety.

Possible themes:
- Pharaohs
- Tombs
- Pyramids
- The Nile
- Scarabs
- Hieroglyphs
- Egyptian gods
- Ancient curses

Example:

```text
Theme:
Pyramids
```

---

### 3. Difficulty Scaling

Difficulty changes depending on player score.

Example:

```text
Difficulty:
Medium
```

This allows:
- Simpler riddles for beginners
- Harder riddles for experienced players

---

### 4. Anti-Repetition Memory

Previous riddles are inserted into the prompt to discourage duplicates.

Example:

```text
Do NOT generate riddles similar to:

- I rise without wings
- I guard kings beneath stone
```

This reduces repeated outputs.

---

### 5. Strict Rules

The AI receives formatting restrictions.

Example:

```text
STRICT RULES:
- EXACTLY 3 answers
- ONLY 1 correct answer
- No explanations
- No markdown
```

This improves JSON consistency.

---

### 6. JSON Output Structure

The AI is forced to output a fixed JSON format.

Example:

```json
{
"question":"Your riddle",
"answers":[
"Answer 1",
"Answer 2",
"Answer 3"
],
"correctIndex":0
}
```

Unity parses this JSON directly into C# objects.

---

## Full Example Prompt

```text
Generate a UNIQUE Egyptian sphinx riddle.

Theme:
Pharaohs

Difficulty:
Medium

Do NOT repeat previous riddles.

STRICT RULES:
- EXACTLY 3 answers
- ONLY 1 correct answer
- Output ONLY raw JSON

FORMAT:

{
"question":"Your riddle",
"answers":[
"Answer 1",
"Answer 2",
"Answer 3"
],
"correctIndex":0
}
```

# 6. Risks

## Risk 1 - Invalid JSON Output

### Description
The AI may generate:
- Extra text
- Markdown formatting
- Missing fields
- Incorrect arrays
- Invalid syntax

This can cause Unity JSON parsing failures.

### Impact
- Game crashes
- Missing answers
- UI errors
- Failed riddle generation

### Mitigation
- Strict prompt instructions
- Validation checks
- Retry system
- Error logging
- JSON cleanup before parsing

---

## Risk 2 - Repetitive Riddles

### Description
Small language models often repeat:
- Sentence structures
- Concepts
- Answers
- Themes

### Impact
- Reduced replayability
- Predictable gameplay
- Lower player engagement

### Mitigation
- Theme randomization
- Difficulty scaling
- Prompt memory
- Temperature increases
- Random seed generation

---

## Risk 3 - Slow Inference

### Description
Local AI inference speed depends heavily on hardware performance.

Lower-end systems may experience delays.

### Impact
- Long loading times
- Interrupted gameplay flow
- Poor user experience

### Mitigation
- Lightweight 4B model
- Short prompts
- Coroutine-based async requests
- Loading indicators

---

## Risk 4 - Incorrect Answers

### Description
The AI may accidentally:
- Create multiple correct answers
- Create impossible riddles
- Generate nonsensical answers

### Impact
- Unfair gameplay
- Player frustration
- Broken game logic

### Mitigation
- Strict prompt constraints
- Validation systems
- Manual testing
- Future answer verification systems

---

## Risk 5 - Memory Growth

### Description
Storing too many previous riddles increases prompt size.

Large prompts slow inference and increase repetition risk.

### Impact
- Reduced performance
- Longer response times
- Increased token usage

### Mitigation
- Limit memory history
- Remove old riddles
- Keep prompts concise

---

## Risk 6 - Model Hallucinations

### Description
Language models sometimes generate:
- Incorrect formatting
- Random text
- Unexpected outputs

### Impact
- Parsing errors
- Broken UI
- Invalid gameplay states

### Mitigation
- Strict JSON-only prompts
- Validation checks
- Retry logic
- Output sanitization

---

## Risk 7 - Hardware Compatibility

### Description
Some systems may lack:
- Enough RAM
- GPU acceleration
- CPU performance

### Impact
- Inference lag
- Crashes
- High system usage

### Mitigation
- Use small local models
- Optimize prompts
- Reduce background processes
- Provide minimum system requirements