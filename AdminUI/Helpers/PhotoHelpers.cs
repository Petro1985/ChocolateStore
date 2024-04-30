namespace AdminUI.Helpers;

public static class PhotoHelpers
{
    private const string BaseUrl = "http://localhost:5260";
    
    public static string GetPhotoUrl(Guid photoId)
    {
        return $"{BaseUrl}/Photos/{photoId}";
    }
}