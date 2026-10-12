# Template repository

This repository is the source of the `blueprint` `dotnet new` template (`.template.config/template.json`). Everything except the paths in `sources.exclude` is copied into every generated project; this file is one of the excluded paths.

## Known identifiers

- Jira project key: `BP`
- Atlassian cloud ID: `enigmatry.atlassian.net`
- GitHub repository: `enigmatry/entry-blueprint`
- Azure DevOps org/project (pipelines only): `enigmatry` / `Enigmatry Blueprint Template` (ID `b3d80552-b808-495c-9332-82888c12c1bf`)
- Default branch: `master`

## Branches and pull requests

- Branches follow the `entry-blueprint:jira-branch` skill: `features/BP-<id>-<keywords>`.
- Pull requests are GitHub pull requests on `enigmatry/entry-blueprint` against `master`, created with `gh pr create`:
  - Title: `BP-<id> - <Jira ticket title>`.
  - Description: the Jira link (`https://enigmatry.atlassian.net/browse/BP-<id>`), then a short summary paragraph.
  - Squash merge, completed manually after review (no auto-merge).

## Template authoring

- Text that must follow the generated project's names uses one of the replaced values: `Enigmatry.Entry.Blueprint` (`--projectName`), `enigmatry-entry-blueprint` (`--appProjectName`), `EnigmatryEntryBlueprint` (projectName without dots) or `Enigmatry Blueprint` (`--friendlyName`). Other template-specific text survives generation unchanged.
- Files that only serve this repository go in `sources.exclude` in `template.json`.
- Verify a template change by generating a project with multi-word names, e.g. `dotnet new blueprint --name Enigmatry.DrillSergeant --projectName Enigmatry.DrillSergeant --appProjectName enigmatry-drill-sergeant --friendlyName "Drill Sergeant" --no-git --allow-scripts yes`, after `dotnet new install .\` (`templating.md` has the commands).
