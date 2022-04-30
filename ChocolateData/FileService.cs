using System.IO;
using System.Security.Cryptography.X509Certificates;
using ChocolateDomain;
using ChocolateDomain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace ChocolateData;

public class FileService
{
    private const string Path = "Photos";

    public async Task<string>  SaveFile(Stream fileStream)
    {
        var dir = new DirectoryInfo(Path);
        if (!dir.Exists) dir.Create();

        Guid photoName = Guid.NewGuid();
        
        fileStream.Seek(0, SeekOrigin.Begin);

        string filePath = System.IO.Path.Combine(Path, photoName.ToString());
        await using var we = File.Create(filePath);
        await fileStream.CopyToAsync(we);
        
        return filePath;
    }

    public async Task<Stream> LoadFile(Photo photo)
    {
        try
        {
            FileInfo file = new FileInfo(photo.PathToPhoto);
            await using var fileStream = file.OpenRead();

            Stream stream = new MemoryStream(); 
            await fileStream.CopyToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        catch (FileNotFoundException e)
        {
            throw new PhotoFileNotFoundException(photo);
        }
    }

    public void DeleteFile(Photo photo)
    {
        FileInfo file = new FileInfo(photo.PathToPhoto);
        if (!file.Exists) return;
        file.Delete();
    }
}