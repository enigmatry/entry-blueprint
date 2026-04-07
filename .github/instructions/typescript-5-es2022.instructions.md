---
description: 'Guidelines for TypeScript Development targeting TypeScript 5.x and ES2022 output'
applyTo: '**/*.ts'
---

# TypeScript Development

> These instructions assume projects are built with TypeScript 5.x (or newer) compiling to an ES2022 JavaScript baseline.

## Core Intent

- Respect the existing architecture and coding standards.
- Prefer readable, explicit solutions over clever shortcuts.
- Extend current abstractions before inventing new ones.
- Prioritize maintainability and clarity, short methods and classes, clean code.

## General Guardrails

- Target TypeScript 5.x / ES2022 and prefer native features over polyfills.
- Use pure ES modules; never emit `require`, `module.exports`, or CommonJS helpers.
- Rely on the project's build, lint, and test scripts unless asked otherwise.

## Project Organization

- Follow the repository's folder and responsibility layout for new code.
- Use kebab-case filenames (e.g., `user-session.ts`, `data-service.ts`) unless told otherwise.
- **Always define each class and interface in its own dedicated file.** Never declare them inline inside another file.

## Naming & Style

- Use PascalCase for classes, interfaces, enums, and type aliases; camelCase for everything else.
- Skip interface prefixes like `I`; rely on descriptive names.

## Formatting & Style

- Run the repository's lint/format scripts (e.g., `npm run lint`) before submitting.
- Match the project's indentation, quote style, and trailing comma rules.
- **Always use braces for every control-flow clause** (`if`, `else`, `for`, `for...of`, `while`, etc.) — even single-line bodies. Never omit braces.
- Keep functions focused; extract helpers when logic branches grow.
- Favor immutable data and pure functions when practical.

## Type System Expectations

- Avoid `any` (implicit or explicit); prefer `unknown` plus narrowing.
- Use discriminated unions for realtime events and state machines.
- Centralize shared contracts instead of duplicating shapes.
- Express intent with TypeScript utility types (e.g., `Readonly`, `Partial`, `Record`).

## Async, Events & Error Handling

- Use `async/await`; wrap awaits in `try/catch` with meaningful error handling — **never use an empty catch clause**.
- Always `await` async calls at the call site; **never use `void` to discard a Promise**.
- Guard edge cases early to avoid deep nesting.

## Architecture & Patterns

- Follow the repository's dependency injection or composition pattern; keep modules single-purpose.
- Keep transport, domain, and presentation layers decoupled with clear interfaces.
- **Always declare class methods using the readonly arrow-function property syntax:**
  ```ts
  // ✅ correct
  readonly myMethod = (): void => { ... };
  private readonly myHelper = (x: number): string => { ... };

  // ❌ avoid
  myMethod(): void { ... }
  private myHelper(x: number): string { ... }
  ```
  **Exception — framework lifecycle hooks** (e.g., Angular's `ngOnInit`, `ngOnDestroy`, `ngOnChanges`, etc.) must be declared as plain methods:
  ```ts
  // ✅ correct for lifecycle hooks
  ngOnInit(): void { ... }
  ngOnDestroy(): void { ... }
  ```

## Security Practices

- Validate and sanitize external input with schema validators or type guards.
- Avoid dynamic code execution and untrusted template rendering.
- Never hardcode secrets; load them from secure sources.
