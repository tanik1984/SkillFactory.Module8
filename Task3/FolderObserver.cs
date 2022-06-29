namespace Task3;

public class FolderObserver
{
    public FolderItem[] Observe(string rootPath)
    {
        var folderItems = new List<FolderItem>();

        ScanFolder(rootPath, folderItems);

        return folderItems.ToArray();
    }

    private void ScanFolder(string path, List<FolderItem> folderItems)
    {
        try
        {
            var directories = Directory.GetDirectories(path);

            foreach (var directory in directories)
            {
                var directoryInfo = new DirectoryInfo(directory);

                var folderItem = new FolderItem
                {
                    IsFolder = true,
                    LastAccessAt = directoryInfo.LastAccessTime,
                    Path = directory,
                    Size = 0
                };

                folderItems.Add(folderItem);

                ScanFolder(directory, folderItems);
            }

            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);

                var folderItem = new FolderItem
                {
                    IsFolder = false,
                    LastAccessAt = fileInfo.LastAccessTime,
                    Path = file,
                    Size = fileInfo.Length
                };

                folderItems.Add(folderItem);
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Нет доступа к папке {path}");
        }
    }
}