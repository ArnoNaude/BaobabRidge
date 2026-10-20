# Baobab Ridge Wildlife Rehabilitation Records System

A Windows Forms application for managing wildlife rehabilitation records. Staff
can add, view, search, update, and delete animal records. The app classifies
animals from their recovery scores and generates a summary report.

## Project team

| Member | Student number |
|---|---:|
| Gavin Cullen | 603971 |
| Tawanda Blessing Mudzamba | 603984 |
| Arno Naude | 603283 |
| Joshua Richards | 601340 |

## Features

- Add animals with input validation and automatically assigned status and
  housing.
- View animal records in a table and search by animal ID.
- Update or delete records, with confirmation before deletion.
- Generate a summary showing total animals, average age and recovery score,
  and counts by rehabilitation status.
- Store records and the generated report in text files.

## File formats

### `animals.txt`

Each non-empty line stores one animal as seven pipe-delimited fields, in this
order:

```text
AnimalID|Name|Species|Age|RecoveryScore|Status|HousingUnit
```

For example:

```text
WR-0007|Tamu|Elephant|12|72|Recovering|Outdoor Enclosure
```

### `summary.txt`

The summary report is plain text with one label and value per line:

```text
Total animals: 8
Average age: 12.50
Average recovery score: 64.25
Critical: 1
Serious: 1
Stable: 2
Recovering: 3
Release-Ready: 1
```

The averages are written to two decimal places, or `N/A` when there are no
animal records.

## Requirements

- Windows
- .NET 8 SDK

## Build and run

From the repository folder, run:

```powershell
dotnet run --project .\BaobabRidge.csproj
```

Alternatively, open `BaobabRidge.sln` in Visual Studio 2022 or later, select
the `BaobabRidge` project as the startup project, and run it.

## Animal record validation

The application reads and writes `animals.txt` in the executable's folder. It
creates an empty file there on first launch if one does not exist. Animal IDs
must use `WR-` followed by four digits. Names and species are
required, may be at most 30 characters, and cannot contain `|`. Age and
recovery score must be whole numbers from 0 to 100. Blank lines are ignored;
malformed lines and duplicate IDs are skipped when loading records.

## Classification

The recovery score determines both the status and housing unit:

| Recovery score | Status | Housing unit |
|---|---|---|
| 0–19 | Critical | Intensive Care Unit |
| 20–39 | Serious | High-Dependency Ward |
| 40–59 | Stable | Recovery Ward |
| 60–79 | Recovering | Outdoor Enclosure |
| 80–100 | Release-Ready | Pre-Release Camp |

## Summary report and file errors

Select **Generate Summary** to update the summary figures in the app and write
`summary.txt` in the executable's folder. The report contains the total number
of animals, average age, average recovery score, and count for each status. If
there are no records, the averages are shown as `N/A`.

If the application cannot read or write its data files, it displays an error
message. Check that the files are not open in another program, are not
read-only, and that the application has permission to access their folder.

## Known limitations

- The application is a Windows Forms desktop app and runs on Windows only.
- Records and reports are local text files beside the executable; the app does
  not provide shared multi-user storage or automatic backups.
- Malformed animal records and duplicate IDs in `animals.txt` are skipped
  during loading rather than repaired automatically.
