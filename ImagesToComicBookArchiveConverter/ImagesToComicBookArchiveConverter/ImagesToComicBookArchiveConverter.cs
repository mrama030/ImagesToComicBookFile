using System;
using System.IO;
using System.IO.Compression;

namespace ImagesToComicBookArchiveConverter
{
    public static class ImagesToComicBookArchiveConverter
    {
        public static int ProcessRequest(string directory, string substringToExclude)
        {
            if (Directory.Exists(directory) == false)
            {
                throw new DirectoryNotFoundException($"The directory '{directory}' was not found.");
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(directory);

            if (directoryInfo.GetDirectories().Length > 0)
            {
                return CreateMultipleComicBookArchives(directory, substringToExclude);
            }
            else if (directoryInfo.GetFiles().Length > 0)
            {
                bool success = CreateComicBookArchive(directory, substringToExclude);
                
                if (success)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                throw new FileNotFoundException("The directory is empty.");
            }
        }

        public static int CreateMultipleComicBookArchives(string directoryOfImageDirectories, string subStringToRemove = null)
        {
            if (Directory.Exists(directoryOfImageDirectories) == false)
            {
                throw new DirectoryNotFoundException($"The directory '{directoryOfImageDirectories}' was not found.");
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(directoryOfImageDirectories);

            DirectoryInfo[] imageDirectoriesToProcess = directoryInfo.GetDirectories();

            if (imageDirectoriesToProcess.Length == 0)
            {
                throw new DirectoryNotFoundException($"Could not locate any folders to process in '{directoryOfImageDirectories}'.");
            }

            int numberOfComicBookArchivesCreated = 0;

            foreach (DirectoryInfo directory in imageDirectoriesToProcess)
            {
                bool success = CreateComicBookArchive(directory.FullName, subStringToRemove);

                if (success)
                {
                    numberOfComicBookArchivesCreated++;
                }
            }

            return numberOfComicBookArchivesCreated;
        }

        public static bool CreateComicBookArchive(string directoryOfImages, string subStringToRemove = null)
        {
            if (Directory.Exists(directoryOfImages) == false)
            {
                throw new DirectoryNotFoundException($"The directory '{directoryOfImages}' was not found.");
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(directoryOfImages);

            string comicName = string.IsNullOrWhiteSpace(subStringToRemove) ? directoryInfo.Name : directoryInfo.Name.Replace(subStringToRemove, string.Empty);
            string zipFileFullPath = Path.Combine(directoryInfo.Parent.FullName, comicName + ".zip");
            string cbzFileFullPath = Path.Combine(directoryInfo.Parent.FullName, comicName + ".cbz");

            if (!File.Exists(cbzFileFullPath))
            {
                try
                {
                    ZipFile.CreateFromDirectory(directoryOfImages, zipFileFullPath);
                    File.Move(zipFileFullPath, cbzFileFullPath);
                }
                catch
                {
                    throw new Exception("Failed to create the comic book archive file.");
                }
            }

            return true;
        }
    }
}
