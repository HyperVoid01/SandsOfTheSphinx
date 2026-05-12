# Prompts Used & Iteration Log
## Project: Sands Of The Sphinx

---

# 1. Overview

This document records:
- Prompt experiments
- Successful prompts
- Failed prompts
- Iteration notes
- AI behavior issues
- Prompt engineering refinements
- Reasoning behind changes

The project used prompt engineering extensively to improve:
- JSON reliability
- Gameplay consistency
- AI fairness
- Riddle variety
- Output structure
- Runtime stability

The project also experimented with detective-style prompts before finalizing the Egyptian riddle gameplay direction.

---

# 2. Initial Riddle Prompt

## Prompt

```text
Generate an Egyptian themed sphinx riddle.

Rules:
- Give EXACTLY 1 riddle
- Give EXACTLY 3 multiple choice answers
- Only 1 answer is correct
- Make the riddle short
- Respond ONLY in JSON

JSON FORMAT:
{
    "question": "",
    "answers": [
        "",
        "",
        ""
    ],
    "correctIndex": 0
}
```

---

## Problems Encountered

The model frequently:
- Repeated riddles
- Ignored answer count rules
- Generated invalid JSON
- Added markdown formatting
- Added explanations outside JSON

Example failures:
- Returned only 2 answers
- Returned 4 answers
- Wrapped JSON inside markdown
- Added commentary before JSON

---

## Iteration Notes

This prompt worked for basic functionality but lacked:
- Randomness
- Variety
- Anti-repetition logic
- Strict formatting enforcement

---

# 3. Improved JSON Enforcement Prompt

## Prompt

```text
You are generating riddles for a Unity game.

RULES:
- Output ONLY raw JSON
- No markdown
- No explanation
- No extra text
- EXACTLY 3 answers
- ONLY 1 correct answer

FORMAT:
{
"question":"",
"answers":["A","B","C"],
"correctIndex":0
}
```

---

## Improvements

This significantly improved:
- JSON reliability
- Parsing stability
- Unity compatibility

---

## Remaining Problems

The model still occasionally:
- Repeated concepts
- Reused wording
- Generated predictable riddles

---

# 4. Anti-Repetition Prompt

## Prompt

```text
Generate a UNIQUE Egyptian sphinx riddle for a video game.

Requirements:
- Every riddle must be different from previous ones
- Use themes like pyramids, pharaohs, tombs, scarabs, deserts, gods, the Nile, hieroglyphs, curses, ancient magic
- Make the answer choices varied
- Avoid repeating the same wording
- EXACTLY 3 multiple choice answers
- ONLY 1 correct answer

IMPORTANT:
- Output ONLY raw JSON
- No markdown
- No explanation

FORMAT:
{
"question":"...",
"answers":["A","B","C"],
"correctIndex":0
}
```

---

## Improvements

This improved:
- Theme variety
- Vocabulary variety
- Replayability

---

## Remaining Problems

Small model limitations still caused:
- Similar sentence structures
- Similar answer patterns
- Repeated concepts after long sessions

---

# 5. Difficulty Scaling Prompt

## Prompt

```text
Generate a UNIQUE Egyptian sphinx riddle.

Theme:
Pyramids

Difficulty:
Medium

STRICT RULES:
- EXACTLY 3 answers
- ONLY 1 correct answer
- Use Egyptian themes
- Avoid repeating previous riddles

OUTPUT FORMAT:

{
"question":"Your riddle",
"answers":[
"Answer 1",
"Answer 2",
"Answer 3"
],
"correctIndex":0
}

ONLY output raw JSON.
```

---

## Purpose

This prompt introduced:
- Difficulty scaling
- Theme injection
- Better gameplay progression

---

## Result

Difficulty scaling produced:
- Simpler early riddles
- More abstract later riddles
- Better replay progression

---

# 6. Runtime Validation Refinements

## Problem

The model sometimes generated:
- Missing answer arrays
- Incorrect answer counts
- Invalid indexes
- Empty questions

This caused:
- IndexOutOfRangeException
- NullReferenceException
- Failed parsing

---

## Solution

Validation systems were added after parsing:

```csharp
if (data.answers == null || data.answers.Length != 3)
{
    GenerateRiddle(callback);
    yield break;
}
```

---

## Result

This improved:
- Stability
- Runtime safety
- Error recovery

---

# 7. Prompt Memory System

## Problem

The model frequently repeated:
- Previous riddles
- Similar wording
- Similar answers

---

## Solution

Previous riddles were stored and injected into prompts.

Example:

```text
Do NOT generate riddles similar to:

- I rise without wings
- I guard kings beneath stone
```

---

## Result

This improved:
- Output variety
- Replayability
- Theme diversity

---

# 8. Temperature & Randomness Refinements

## Initial Problem

Low temperature values caused:
- Repeated outputs
- Predictable wording
- Similar structures

---

## Solution

Inference settings were changed:

```csharp
temperature = 1.35f
top_p = 0.97f
seed = Random.Range(0, 999999)
```

---

## Result

This improved:
- Creativity
- Sentence variety
- Unpredictability

---

# 9. Detective Prompt Experiments

Before the final riddle gameplay direction, multiple detective game prompts were tested.

These prompts explored:
- Dynamic murder mystery generation
- AI fairness
- Logical deduction systems
- Anti-bias prompting

---

# 10. Detective Prompt 1

## Prompt

```text
I am a detective trying to solve a murder case. Give me the scenario with hints, clues and provide 3 options. Only 1 of the options are correct, the other 2 are wrong. If i choose the wrong option, the game ends.
```

---

## Problems

The AI frequently:
- Made the player's answer automatically correct
- Changed clues after accusations
- Created obvious answers
- Failed logical consistency

---

## Iteration Notes

This prompt was too simple and lacked:
- Anti-bias rules
- Structured reasoning
- Internal consistency enforcement

---

# 11. Detective Prompt 2

## Improvements Added

This prompt introduced:
- Case structure
- Evidence sections
- Witness statements
- Hidden clues
- Realistic motives

---

## Problems

The AI still:
- Sometimes favored the player
- Sometimes adjusted outcomes after accusations
- Occasionally contradicted itself

---

# 12. Detective Prompt 3

## Major Refinements

This prompt introduced:
- Secretly determined murderer
- Fixed timelines
- Logical deduction rules
- Anti-player-bias instructions
- Failure conditions

---

## Improvements

This significantly improved:
- Logical consistency
- Fairness
- Deduction quality

---

## Remaining Problems

The AI occasionally:
- Still rewarded weak logic
- Overcorrected and became unfair
- Made every accusation incorrect

---

# 13. Detective Prompt 4

## Key Additions

This prompt introduced:
- Anti-confirmation bias instructions
- Contradiction analysis
- Internal reasoning checks
- Alternative explanation analysis

---

## Important Problem Encountered

The AI began:
- Disagreeing with almost every accusation
- Becoming overly skeptical
- Treating nearly all player answers as incorrect

This created unfair gameplay.

---

## Reason For Failure

The anti-bias instructions became too aggressive.

The AI interpreted:
- "Challenge weak deductions"
- "Consider arguments against the player"

as:
- "Assume the player is wrong"

---

# 14. Detective Prompt 5

## Refinements

Added:
- Explicit correct/incorrect handling
- Red herrings
- Structured response rules
- Immediate failure conditions

---

## Problems

The AI still:
- Occasionally forced disagreement
- Sometimes prioritized surprise over logic

---

# 15. Detective Prompt 6

## Key Refinement

This prompt added:

```text
Do not force disagreement.
Do not force agreement.
If the chosen suspect matches the timeline, motive, and evidence best, the player is correct.
If another suspect fits the evidence better, the player is wrong.
```

---

## Improvements

This was one of the most successful detective prompts because it:
- Balanced fairness
- Reduced forced disagreement
- Improved logical consistency

---

## Important Lesson

Explicit fairness instructions worked better than:
- Heavy anti-bias instructions
- Aggressive contradiction prompts

---

# 16. Detective Prompt 7 (Testing Mode)

## Purpose

Testing mode was created to expose:
- Internal reasoning
- Important clues
- Misleading evidence
- Correct suspect selection

---

## Features

Added:
- Investigation breakdown
- Step-by-step reasoning
- Explicit answer reveal

---

## Result

This became useful for:
- Debugging AI logic
- Improving clue quality
- Testing consistency

---

# 17. Major AI Problems Encountered

## Problem 1 - AI Always Making The Player Correct

### Cause
The model tried to:
- Reward the player
- Continue gameplay
- Maintain positive interaction

### Solution
Added:
- Failure conditions
- Anti-bias rules
- Fixed murderer logic
- Pre-determined solutions

---

## Problem 2 - AI Always Making The Player Incorrect

### Cause
Overly aggressive anti-bias instructions caused the AI to:
- Distrust player reasoning excessively
- Search too hard for contradictions
- Prioritize skepticism

### Solution
Added explicit fairness instructions:

```text
Do not force disagreement.
Do not force agreement.
Judge accusations fairly and objectively using the evidence.
```

---

## Problem 3 - Invalid JSON

### Cause
The model frequently:
- Added markdown
- Added explanations
- Ignored formatting instructions

### Solution
Added:
- "ONLY output raw JSON"
- Strict formatting rules
- Runtime validation
- Retry systems

---

## Problem 4 - Repeated Outputs

### Cause
Small models tend to:
- Reuse structures
- Repeat wording
- Repeat concepts

### Solution
Added:
- Prompt memory
- Random seeds
- Higher temperature
- Theme randomization

---

# 18. Final AI Riddle Prompt Used In Sands Of The Sphinx

## Final Prompt

```text
Generate a UNIQUE Egyptian sphinx riddle.

Theme:
{chosenTheme}

Difficulty:
{difficulty}

IMPORTANT:
Do NOT repeat ideas similar to these previous riddles:

{previousText}

STRICT RULES:
- Every riddle must feel different
- Use different wording each time
- Avoid repeating answers
- EXACTLY 3 answers
- ONLY 1 correct answer
- correctIndex must be 0, 1, or 2

OUTPUT FORMAT:

{
"question":"Your riddle",
"answers":[
"Answer 1",
"Answer 2",
"Answer 3"
],
"correctIndex":0
}

ONLY output raw JSON.
No markdown.
No explanation.
```

---

## Why This Prompt Worked Best

This prompt combined:
- Strict formatting
- Anti-repetition memory
- Theme randomization
- Difficulty scaling
- Controlled creativity
- Runtime validation support

This created the best balance of:
- Stability
- Fairness
- Replayability
- Creativity
- JSON consistency

---

# 19. Final Prompt Engineering Lessons

Key lessons learned during development:

- Smaller models require strict formatting instructions
- Anti-bias prompting can become excessive
- Explicit fairness rules improve gameplay
- Runtime validation is essential
- Prompt memory improves replayability
- Temperature heavily affects creativity
- Simpler prompts are often more reliable
- Local models require defensive programming

---

# 20. Final Outcome

The final prompt system successfully enabled:
- Infinite AI-generated riddles
- Consistent JSON formatting
- Fair answer systems
- Better replayability
- Stable Unity integration
- Dynamic Egyptian-themed gameplay

The project demonstrated how local AI models can be integrated into Unity to create procedural gameplay systems using prompt engineering and runtime validation.
```