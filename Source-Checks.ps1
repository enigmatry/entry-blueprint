# TeamCity helper functions example - https://gist.github.com/StevenMcD/4008594

param(
    [parameter(Mandatory=$true)] [int]$TodoThreshold,
    [parameter(Mandatory=$false)] [int]$BuildProblemsThreshold = 0
)
	
$Here = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

# These were borrowed from PSake's contrib modules

function TeamCity-PublishArtifact([string]$path) {
	TeamCity-WriteServiceMessage 'publishArtifacts' $path
}

function TeamCity-SetBuildStatistic([string]$key, [string]$value) {
	TeamCity-WriteServiceMessage 'buildStatisticValue' @{ key=$key; value=$value }
}

function TeamCity-WriteServiceMessage([string]$messageName, $messageAttributes) {
	function escape([string]$value) {
		([char[]] $value | 
				%{ switch ($_) 
						{
								"|" { "||" }
								"'" { "|'" }
								"`n" { "|n" }
								"`r" { "|r" }
								"[" { "|[" }
								"]" { "|]" }
								([char] 0x0085) { "|x" }
								([char] 0x2028) { "|l" }
								([char] 0x2029) { "|p" }
								default { $_ }
						}
				} ) -join ''
		}

	if ($messageAttributes -is [hashtable]) {
		$messageAttributesString = ($messageAttributes.GetEnumerator() | 
			%{ "{0}='{1}'" -f $_.Key, (escape $_.Value) }) -join ' '
	} else {
		$messageAttributesString = ("'{0}'" -f (escape $messageAttributes))
	}

	Write-Output "##teamcity[$messageName $messageAttributesString]"
}

function ContainsAny( [string]$s, [string[]]$items ) {
  $matchingItems = ($items | %{$s.contains($_)}) -contains $true
  [bool]$matchingItems
}

$SolutionRoot = (Split-Path -parent $Here) ## Move one folder up
$SolutionRoot = $Here 

##$SrcPath = Join-Path $SolutionRoot "src" // customize if src is in dedicated folder
$SrcPath = $SolutionRoot

$FoldersToExclude = @("\node_modules\", "\dist\", "\obj\", "\bin\", "\typings\", "\packages\")  ## Folders to exclude from checking, '\' was added since we do part of path checking 
$FileExtensionsToInclude = @("*.cs", "*.ts") ## Extensions to include

$LookFor = @("\bTODO\b", "\bFIXME\b", "\bHACK\b", "\bTEMP\b")
$LookFor_BuildProblem = @("\bTEMP\b")

$Matches = @()
$Matches_BuildProblem = @()

## For each extension file in the src directory
foreach($FileExtension in $FileExtensionsToInclude)
{
	Write-Output "Extension: $FileExtension"
	foreach($File in (Get-ChildItem $SrcPath -Filter $FileExtension -Recurse | Where-Object  { !(ContainsAny $_.FullName $FoldersToExclude) }))
	{
		## Find occurances of TODO, FIXME, HACK comments
		## Output those and their respective files and line numbers

		$Matches = $Matches + (Select-String -Path $File -Pattern $LookFor -AllMatches)
        $Matches_BuildProblem = $Matches_BuildProblem + (Select-String -Path $File -Pattern $LookFor_BuildProblem -AllMatches)
	}
}

## Piped into an HTML file

$Output = Join-Path $SolutionRoot "TODOs.html"
$Output_BuildProblems = Join-Path $SolutionRoot "BuildProblems.html"

function Create-Header([string]$head) {
 $headResult = @"
    <title>$head</title>
    <style>
    th {
	    text-align: left; 
	    border-bottom: 
	    1px solid black;
    }
    </style>
"@
}

$head = Create-Header('TODOS')
$head_buildProblems = Create-Header('Build problems')

$Matches | ConvertTo-Html Path,LineNumber,Line -title "TODOS" -body "<h1>TODOS</h1>" -head $head > $Output
$Matches_BuildProblem | ConvertTo-Html Path,LineNumber,Line -title "Build Problems" -body "<h1>Build problems</h1>" -head $head_buildProblems > $Output_BuildProblems

Write-Output "Output written to: $Output"
Write-Output "Build problems written to: $Output_BuildProblems"

if($env:TEAMCITY_VERSION)
{
	TeamCity-PublishArtifact $Output
    TeamCity-PublishArtifact $Output_BuildProblems

    $TodosCount = $Matches.Count
    $BuildProblemsCount = $Matches_BuildProblem.Count

	TeamCity-SetBuildStatistic -key "TODOs" -value $TodosCount
    TeamCity-SetBuildStatistic -key "BuildProblems" -value $BuildProblemsCount
    TeamCity-WriteServiceMessage -messageName "buildStatus" -messageAttributes @{text="{build.status.text}; TODOs: $TodosCount; BuildProblems: $BuildProblemsCount"}
	
	if($TodosCount -gt $TodoThreshold)
	{
        TeamCity-WriteServiceMessage -messageName "buildProblem" -messageAttributes @{description="Too many TODOs found in the code! Threshold: $TodoThreshold. Found TODOs: $TodosCount."}
	}

    if($BuildProblemsCount -ne $BuildProblemsThreshold)
	{
        TeamCity-WriteServiceMessage -messageName "buildProblem" -messageAttributes @{description="Too many code with: '$LookFor_BuildProblem' found! Threshold: $BuildProblemsThreshold. Found Problems: $BuildProblemsCount."}
    }
}