---
description: 'Guidelines for Angular development in this project'
applyTo: '**/*.ts'
---

# Angular Development

## Async / Await over RxJS

- **For any Observable that emits a single value and completes (e.g., HTTP calls, `TranslateService.get()`, `firstValueFrom`-compatible sources), always use `async/await` with `firstValueFrom()` instead of `.subscribe()`.**
- Reserve `.subscribe()` exclusively for **true streams** — Subjects, event buses, or any Observable that emits multiple values over time (e.g., `kernel.ready`, route events, WebSocket streams).
- Never store a subscription just to call `.unsubscribe()` on it when `firstValueFrom` would work instead.

```ts
// ✅ correct — one-shot HTTP / translate call
const label = await firstValueFrom(this.translate.get('some.key'));
const data  = await firstValueFrom(this.http.get<MyType>('/api/data'));

// ❌ avoid
this.translate.get('some.key').subscribe(text => this.label = text);
this.http.get<MyType>('/api/data').subscribe(data => this.data = data);

// ✅ correct — true stream (Subject, fires multiple times)
this.kernel.ready.subscribe(ready => { ... });
```

## Error Handling

- Always handle errors from `async` calls with `try/catch`. Never use an empty catch block.
- Log errors with `console.error` for infrastructure/background failures; use the app's `ToastrService` for user-facing errors.

## i18n / Translation Keys

- **Reuse before adding.** Before adding a translation key, search for an existing key with the same meaning. For generic labels (e.g. "OK", "Yes", "No", "Cancel") always use `shared.*` keys.
- **No duplication.** If the same string would appear under multiple keys, introduce it once in `shared` and reference that key everywhere.
- **No abbreviations.** Use full words — `configuration`, not `config`; `button`, not `btn`.
- **Key names must be 4–5 words maximum.** If you need more, introduce a nested object to group related keys.
  ```json
  // ✅ correct
  "shared": { "ok": "OK", "yes": "Yes", "no": "No", "cancel": "Cancel" }
  "configurationMismatch": { "title": "...", "body": "...", "fileLocation": "..." }

  // ❌ avoid
  "configurationMismatch": { "confirmButtonText": "OK" }  // duplicate of shared.ok
  "configurationMismatchDialogConfirmButtonLabel": "OK"   // too long, and duplicate
  ```

- Prefer standalone components (`standalone: true`); avoid NgModules where possible.
- Keep components thin — delegate business logic to injected services.
- Use `inject()` for dependency injection rather than constructor injection.
- Use the `readonly` arrow-function property syntax for all class methods **except** Angular lifecycle hooks (`ngOnInit`, `ngOnDestroy`, `ngOnChanges`, etc.), which must be plain methods so the framework can bind them through the interface:
  ```ts
  // ✅ correct
  ngOnInit(): void { ... }
  readonly myMethod = (): void => { ... };

  // ❌ avoid
  readonly ngOnInit = (): void => { ... };  // framework won't call this
  myMethod(): void { ... }                   // use readonly arrow instead
  ```
