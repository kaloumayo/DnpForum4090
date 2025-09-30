using System.Text.Json;

namespace FileRepositories;

internal static class FileDb
{
    private static readonly JsonSerializerOptions Opts = new() { WriteIndented = true };

    public static async Task SaveAsync<T>(string path, IEnumerable<T> data)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        await File.WriteAllTextAsync(path, JsonSerializer.Serialize(data, Opts));
    }

    public static async Task<List<T>> LoadAsync<T>(string path)
    {
        if (!File.Exists(path)) return new List<T>();
        var json = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }
}