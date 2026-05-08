#!/bin/bash

projectDir=./LeetGen/LeetGen/
outputDir=./Problems/
templateDir=./Templates/

dotnet restore $projectDir

if [ $1 == "debug" ]; then
    dotnet build $projectDir -c Debug
    isDebug=true
else
    dotnet build $projectDir -c Release
fi

read -p "Build complete. Do you want create or remove a project? (create/remove) " action
read -p "What is the problem number? " problemNumber
read -p "What is the programming language? " language

if [ $action == "create" ]; then
    if [ $isDebug ]; then
        dotnet run --project $projectDir -- create --output $outputDir --template $templateDir --problem $problemNumber --language $language --debug
    else
        dotnet run --project $projectDir -- create --output $outputDir --template $templateDir --problem $problemNumber --language $language
    fi
fi
