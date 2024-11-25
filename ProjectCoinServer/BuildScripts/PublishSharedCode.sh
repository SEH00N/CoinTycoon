#!/bin/sh

echo "Copy SharedCode"

source_folder="./src/SharedCode/"
target_folder="../ProjectCoinClient/Assets/01. Scripts/SharedCode/"
rm -rf "$target_folder" && cp -rf "$source_folder" "$target_folder"