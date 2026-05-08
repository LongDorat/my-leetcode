#!/bin/bash

currentDir=$(pwd)
projectDir=$currentDir/LeetGen/LeetGen/
outputDir=$currentDir/Problems/
outputDirDebug=$currentDir/.debug-output/
templateDir=$currentDir/Templates/
templateDirDebug=$currentDir/.debug-templates/

dotnet restore $projectDir

if [ $1 == "debug" ]; then
    mkdir -p $outputDirDebug
    mkdir -p $templateDirDebug
    dotnet build $projectDir -c Debug
    isDebug=true
else
    dotnet build $projectDir -c Release
fi

echo "================================"
read -p "Build complete. Do you want create or remove a project? (create/remove) " action
read -p "What is the problem number? " problemNumber
read -p "What is the programming language? " language

if [ $action == "create" ]; then
    if [ $isDebug ]; then
        dotnet run --project $projectDir -c Debug -- create --output $outputDirDebug --template $templateDirDebug --problem $problemNumber --language $language --debug
    else
        dotnet run --project $projectDir -c Release -- create --output $outputDir --template $templateDir --problem $problemNumber --language $language
    fi
fi
