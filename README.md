# Image Deduplication & Sorting Tool 🖼️

This is a **C# application** that sorts and removes duplicate images based on their **SHA-256 hash**. The program scans a directory, computes the hash for each image, and automatically filters out duplicates while organizing unique images into sorted folders.

## Features 🎯
- 🖼️ **Image Scanning** – Reads images from a specified folder.
- 🔍 **SHA-256 Hashing** – Identifies duplicate images with cryptographic accuracy.
- 🗃️ **Automatic Sorting** – Moves unique images to a structured directory.
- 🗑️ **Duplicate Removal** – Deletes or moves duplicates to a separate folder.
- 📊 **Log Reports** – Generates a log of sorted and removed files.

## How It Works ⚙️
1. The program scans a target directory for images.
2. It calculates the **SHA-256 hash** for each image.
3. Duplicates are identified based on matching hashes.
4. Unique images are sorted into an organized folder structure.
5. Duplicates are either **deleted** or **moved to a duplicates folder**.


## Why Use This Application? 🤔
- **Fast and efficient** – Hash-based deduplication is highly accurate.
- **Automated sorting** – No manual organization needed.
- **Saves disk space** – Eliminates redundant images.
- **Easy to extend** – Can be modified for different image formats and use cases.

## Contribution Guidelines 🤝
Feel free to contribute by submitting issues, feature requests, or pull requests! 🚀

Happy sorting! 🖼️🔥
