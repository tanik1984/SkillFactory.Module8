namespace Task3;

public class FolderRemover
{
    private readonly FolderObserver _folderObserver;

    public FolderRemover()
    {
        _folderObserver = new FolderObserver();
    }

    public void Process(string path, TimeSpan timeout)
    {
        var folderItems = _folderObserver.Observe(path);

        RemoveFiles(folderItems, timeout);
        RemoveFolders(folderItems, timeout);
    }

    private void RemoveFolders(FolderItem[] folderItems, TimeSpan timeout)
    {
        foreach (var folderItem in folderItems)
            if (folderItem.IsFolder && DateTime.Now - folderItem.LastAccessAt > timeout)
                try
                {
                    Directory.Delete(folderItem.Path);
                }
                catch (IOException)
                {
                    Console.WriteLine($"Папка {folderItem.Path} не пустая");
                }
    }

    private void RemoveFiles(FolderItem[] folderItems, TimeSpan timeout)
    {
        var removedFilesCount = 0;
        var removedFilesSize = 0L;
        var totalSize = 0L;

        foreach (var folderItem in folderItems)
            totalSize += folderItem.Size;

        Console.WriteLine($"Исходный размер папки: {totalSize} байт");

        foreach (var folderItem in folderItems)
            if (!folderItem.IsFolder && DateTime.Now - folderItem.LastAccessAt > timeout)
                try
                {
                    File.Delete(folderItem.Path);
                    removedFilesCount++;
                    removedFilesSize += folderItem.Size;
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"Нет доступа к файлу {folderItem.Path}");
                }

        Console.WriteLine($"Удалено: {removedFilesCount} файлов");
        Console.WriteLine($"Освобождено: {removedFilesSize} байт");
        Console.WriteLine($"Текущий размер папки: {totalSize - removedFilesSize} байт");
    }
}