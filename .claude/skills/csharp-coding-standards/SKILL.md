---
name: csharp-coding-standards
description: C# 13 coding standards for the YOS project — naming, formatting, nullability, and testing conventions. Use this when writing, reviewing, or refactoring any C# (.cs) file.
---

# C# Coding Standards

## Language Version

Use C# 13 features wherever applicable.

## General

- Make only high-confidence suggestions when reviewing code changes.
- Handle edge cases and write clear exception handling.

## Naming Conventions

- **PascalCase** for component names, method names, and public members.
- **camelCase** for private fields and local variables.
- Prefix interface names with `I` (e.g., `IUserService`).

## Formatting

- Apply the code-formatting style defined in `.editorconfig`.
- Prefer file-scoped namespace declarations and single-line using directives.
- Insert a newline before the opening curly brace of any code block (after `if`, `for`, `while`, `foreach`, `using`, `try`, etc.).
- **Always use braces for every control-flow clause** (`if`, `else`, `for`, `foreach`, `while`, etc.) — even single-line bodies. Never omit braces.
- Ensure the final return statement of a method is on its own line.
- Use pattern matching and switch expressions wherever possible.
- Use `nameof` instead of string literals when referring to member names.
- Add XML doc comments on all public APIs; include `<example>` and `<code>` sections where applicable.

## Nullable Reference Types

- Declare variables non-nullable; check for `null` only at entry points.
- Always use `is null` / `is not null` instead of `== null` / `!= null`.
- Trust C# null annotations — do not add null checks when the type system guarantees a value cannot be null.

## Testing

- Always include test cases for critical paths.
- Do not emit `// Arrange`, `// Act`, or `// Assert` comments.
- Match the naming style and capitalization of nearby test files.
