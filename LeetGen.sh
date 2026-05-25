#!/bin/bash
set -euo pipefail

currentDir=$(pwd)
projectDir=$currentDir/LeetGen/LeetGen/
outputDir=$currentDir/Problems/
outputDirDebug=$currentDir/.debug-output
templateDir=$currentDir/Templates/
templateDirDebug=$currentDir/.debug-templates

dotnet restore $projectDir

isDebug=false
if [ "${1:-}" == "debug" ]; then
    mkdir -p $outputDirDebug
    mkdir -p $templateDirDebug
    dotnet build $projectDir -c Debug
    isDebug=true
else
    dotnet build $projectDir -c Release
fi

echo "================================"
read -p "Build complete. Create or remove a project? (create(1)/remove(2)) " action
read -p "What is the problem number? " problemNumber
read -p "What is the programming language? " language
echo "================================"

action=$(printf '%s' "$action" | tr '[:upper:]' '[:lower:]')
case "$action" in
    c|create|1)
        action="create"
        ;;
    r|remove|2)
        action="remove"
        ;;
esac

if [ $action == "create" ]; then
    if [ $isDebug == "true" ]; then
        dotnet run --project $projectDir -c Debug -- create --output $outputDirDebug --template $templateDirDebug --problem $problemNumber --language $language --debug
    else
        dotnet run --project $projectDir -c Release -- create --output $outputDir --template $templateDir --problem $problemNumber --language $language
    fi
elif [ $action == "remove" ]; then
    if [ $isDebug == "true" ]; then
        dotnet run --project $projectDir -c Debug -- remove --output $outputDirDebug --template $templateDirDebug --problem $problemNumber --language $language --debug
    else
        dotnet run --project $projectDir -c Release -- remove --output $outputDir --template $templateDir --problem $problemNumber --language $language
    fi
else
    echo "Invalid action. Please choose 'create' or 'remove'."
fi