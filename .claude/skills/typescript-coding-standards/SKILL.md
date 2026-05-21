---
name: typescript-coding-standards
description: TypeScript 5.x / ES2022 coding standards for the YOS Angular project — naming, formatting, type system, async patterns, and architecture conventions. Use this when writing, reviewing, or refactoring any TypeScript (.ts) file.
---

# TypeScript Coding Standards

Targets **TypeScript 5.x** compiling to an **ES2022** baseline.

## Core Intent

- Respect the existing architecture and coding standards.
- Prefer readable, explicit solutions over clever shortcuts.
- Extend current abstractions before inventing new ones.
- Prioritize maintainability and clarity: short methods and classes, clean code.

## General Guardrails

- Use native TypeScript 5.x / ES2022 features; never use polyfills.
- Use pure ES modules — never emit `require`, `module.exports`, or CommonJS helpers.
- Rely on the project's build, lint, and test scripts unless told otherwise.

## Project Organization

- Follow the repository's folder and responsibility layout for new files.
- Use **kebab-case filenames** (e.g., `user-session.ts`, `data-service.ts`) unless told otherwise.
- **Always define each class and interface in its own dedicated file.** Never declare them inline inside another file.

## Naming & Style

- **PascalCase** for classes, interfaces, enums, and type aliases.
- **camelCase** for everything else.
- Skip `I` interface prefixes; rely on descriptive names instead.

## Formatting

- Run `npm run lint` before submitting changes.
- Match the project's indentation, quote style, and trailing comma rules.
- **Always use braces for every control-flow clause** (`if`, `else`, `for`, `for...of`, `while`, etc.) — even single-line bodies. Never omit braces.
- Keep functions focused; extract helpers when logic branches grow.
- Favor immutable data and pure functions when practical.

## Type System

- Avoid `any` (implicit or explicit); prefer `unknown` with narrowing.
- Use discriminated unions for realtime events and state machines.
- Centralize shared contracts instead of duplicating shapes.
- Express intent with TypeScript utility types (`Readonly`, `Partial`, `Record`, etc.).

## Async, Events & Error Handling

- **Prefer RxJS observables** over `async/await` and Promises to keep async flows unified across the project.
- Use `async/await` only when Promise interop is required by external libraries or APIs.
- Wrap `await` in `try/catch` with meaningful error handling — **never use an empty catch clause**.
- Always `await` async calls at the call site; **never use `void` to discard a Promise**.
- Guard edge cases early to avoid deep nesting.

## Architecture & Patterns

- Follow the repository's dependency injection/composition pattern; keep modules single-purpose.
- Keep transport, domain, and presentation layers decoupled with clear interfaces.
- **Declare class methods using the `readonly` arrow-function property syntax:**

  ```ts
  // correct
  readonly myMethod = (): void => { ... };
  private readonly myHelper = (x: number): string => { ... };

  // avoid
  myMethod(): void { ... }
  private myHelper(x: number): string { ... }
  ```

  **Exception — Angular lifecycle hooks** must be plain methods:

  ```ts
  // correct for lifecycle hooks
  ngOnInit(): void { ... }
  ngOnDestroy(): void { ... }
  ```

## Security

- Validate and sanitize external input with schema validators or type guards.
- Avoid dynamic code execution and untrusted template rendering.
- Never hardcode secrets; load them from secure sources.
