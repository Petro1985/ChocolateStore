namespace Services.File;

public interface IFileService
{
    Task<string> SaveFile(Stream fileStream);
    Task<Stream> LoadFile(string fileName);
    void DeleteFile(string fileName);
}

public class FileService : IFileService
{
    private const string Path = "Photos";

    public async Task<string> SaveFile(Stream fileStream)
    {
        var dir = new DirectoryInfo(Path);
        if (!dir.Exists) dir.Create();

        Guid photoName = Guid.NewGuid();
        
        fileStream.Seek(0, SeekOrigin.Begin);

        string filePath = System.IO.Path.Combine(Path, photoName.ToString());
        await using var we = System.IO.File.Create(filePath);
        await fileStream.CopyToAsync(we);
        
        return filePath;
    }

    public async Task<Stream> LoadFile(string fileName)
    {
        try
        {
            FileInfo file = new FileInfo(fileName);
            await using var fileStream = file.OpenRead();

            Stream stream = new MemoryStream(); 
            await fileStream.CopyToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        catch (FileNotFoundException e)
        {
            throw new PhotoFileNotFoundException(fileName);
        }
    }

    public void DeleteFile(string fileName)
    {
        FileInfo file = new FileInfo(fileName);
        if (!file.Exists) return;
        file.Delete();
    }
}