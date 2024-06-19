using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

class Program
{
    static void Main()
    {
        
        string directoryPath = @"C:\Users\ab***\OneDrive\Skrivebord\billedeTest";
     
        string[] imageExtensions = new string[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
        
        
        List<string> imageFiles = new List<string>();
        foreach (string ext in imageExtensions)
        {
            imageFiles.AddRange(Directory.GetFiles(directoryPath, "*" + ext));
        }

        // Paths til de 2 mapper der bliver oprettet
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string duplicatesPath = Path.Combine(desktopPath, "Duplicates");
        string originalsPath = Path.Combine(desktopPath, "Original");

        Directory.CreateDirectory(duplicatesPath);
        Directory.CreateDirectory(originalsPath);

        // Samler alle hashes fra billederne
        Dictionary<string, string> imageHashes = new Dictionary<string, string>();

        foreach (string filePath in imageFiles)
        {
            try
            {
                using (Image img = Image.FromFile(filePath))
                {
                    string hash = GetImageHash(img);

                    if (imageHashes.ContainsKey(hash))
                    {
                        // ryk de duplikeret billeder i Duplicate mappen
                        string fileName = Path.GetFileName(filePath);
                        string destPath = Path.Combine(duplicatesPath, fileName);
                        MoveFileWithRetry(filePath, destPath);
                        Console.WriteLine($"Moved duplicate: {fileName} to Duplicates");
                    }
                    else
                    {
                        // Ryk de originale billeder til original mappen
                        string fileName = Path.GetFileName(filePath);
                        string destPath = Path.Combine(originalsPath, fileName);
                        MoveFileWithRetry(filePath, destPath);
                        Console.WriteLine($"Moved original: {fileName} to Original");
                        imageHashes[hash] = filePath;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing image {filePath}: {ex.Message}");
            }
        }
    }

    static string GetImageHash(Image img)
    {
        using (var ms = new MemoryStream())
        {
            img.Save(ms, img.RawFormat);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] imageBytes = ms.ToArray();
                byte[] hashBytes = sha256.ComputeHash(imageBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }

    static void MoveFileWithRetry(string sourcePath, string destinationPath)
    {
        const int maxRetryCount = 5;
        //const int delayMilliseconds = 1000;

        for (int attempt = 1; attempt <= maxRetryCount; attempt++)
        {
            try
            {
                if (File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                }

                File.Copy(sourcePath, destinationPath);
                File.Delete(sourcePath);
                break;
            }
            catch (IOException ex) when (ex.Message.Contains("because it is being used by another process"))
            {
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error moving file: {ex.Message}");
                break;
            }
        }
    }
}
