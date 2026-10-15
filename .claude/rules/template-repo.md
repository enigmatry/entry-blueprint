# Template repository

This repository is the source of the `blueprint` `dotnet new` template (`.template.config/template.json`). Everything except the paths in `sources.exclude` is copied into every generated project; this file is one of the excluded paths.

## Known identifiers

These are the Known identifiers the `entry-blueprint:jira-branch` and `entry-blueprint:pull-request` skills use in this repository.

- Jira project key: `BP`
- Atlassian cloud ID: `enigmatry.atlassian.net`
- GitHub repository: `enigmatry/entry-blueprint`
- Azure DevOps org/project (pipelines only): `enigmatry` / `Enigmatry Blueprint Template` (ID `b3d80552-b808-495c-9332-82888c12c1bf`)
- Default branch: `master`

## Branches and pull requests

- Branches follow the `entry-blueprint:jira-branch` skill: `features/BP-<id>-<keywords>`.
- Pull requests in this repository are GitHub pull requests on `enigmatry/entry-blueprint` against `master`, created with `gh pr create`. This workflow takes the place of the Azure DevOps creation and completion-option steps of the `entry-blueprint:pull-request` skill:
  - Title: `BP-<id> - <Jira ticket title>`.
  - Description: the Jira link (`https://enigmatry.atlassian.net/browse/BP-<id>`), then a short summary paragraph.
  - A reviewer completes the pull request manually with a squash merge.

## Template authoring

- Text that must follow the generated project's names uses one of the replaced values: `Enigmatry.Entry.Blueprint` (`--projectName`), `enigmatry-entry-blueprint` (`--appProjectName`), `EnigmatryEntryBlueprint` (projectName without dots) or `Entry Blueprint` (`--friendlyName`). Other template-specific text survives generation unchanged.
- Replacement is plain text matching, also inside longer text: "Enigmatry Entry Blueprint" in a copied file becomes "Enigmatry <friendlyName>". Write template-wide names that must stay as they are without the replaced values.
- Files that only serve this repository go in `sources.exclude` in `template.json`.
- Verify a template change by generating a project with multi-word names, e.g. `dotnet new blueprint --name Enigmatry.DrillSergeant --projectName Enigmatry.DrillSergeant --appProjectName enigmatry-drill-sergeant --friendlyName "Drill Sergeant" --no-git --allow-scripts yes`, after `dotnet new install .\` (`templating.md` has the commands).
