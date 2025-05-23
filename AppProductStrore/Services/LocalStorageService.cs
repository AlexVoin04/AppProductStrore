using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AppProductStrore.Services
{
    public class LocalStorageService
    {
        public static async Task InitializeLocalStorageAsync()
        {
            var localFolder = ApplicationData.Current.LocalFolder;

            StorageFolder jsonFolder = await localFolder.CreateFolderAsync("Data", CreationCollisionOption.OpenIfExists);
            StorageFolder imageFolder = await localFolder.CreateFolderAsync("Images", CreationCollisionOption.OpenIfExists);

            await EnsureJsonDataInitializedAsync(jsonFolder);
            await EnsureImagesCopiedAsync(imageFolder);
        }

        private static async Task EnsureJsonDataInitializedAsync(StorageFolder jsonFolder)
        {
            if (await FileExistsAsync(jsonFolder, "products.json"))
                return;

            StorageFile sourceJson = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Data/products.json"));
            await sourceJson.CopyAsync(jsonFolder, "products.json", NameCollisionOption.ReplaceExisting);
        }

        private static async Task EnsureImagesCopiedAsync(StorageFolder imageFolder)
        {
            StorageFolder installedFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder assetsFolder = await installedFolder.GetFolderAsync("Assets");
            StorageFolder imageSourceFolder = await assetsFolder.GetFolderAsync("Images");

            var allFiles = await imageSourceFolder.GetFilesAsync();

            foreach (StorageFile file in allFiles.Where(f => f.FileType.Equals(".png", StringComparison.OrdinalIgnoreCase)))
            {
                await file.CopyAsync(imageFolder, file.Name, NameCollisionOption.ReplaceExisting);
            }
        }

        /// <summary>
        /// Checks whether the specified file has already been initialized in the given folder.
        /// </summary>
        /// <param name="folder">The folder in which to search for the file.</param>
        /// <param name="fileName">The name of the file to check for existence.</param>
        /// <returns>Returns <c>true</c> if the file is found (already initialized); otherwise, <c>false</c>.</returns>
        private static async Task<bool> FileExistsAsync(StorageFolder folder, string fileName)
        {
            try
            {
                await folder.GetFileAsync(fileName);
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }
    }
}
