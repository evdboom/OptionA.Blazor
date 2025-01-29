using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Storage;
using System.Text;

namespace OptionA.Blazor.Test.Pages
{
    public partial class Storage
    {
        [Inject]
        public IFileSystem FileSystem { get; set; } = default!;

        private List<FileHandle> _files = [];

        private string? _content;
        private string? _saveContent;

        private async Task OpenFiles()
        {
            var files = await FileSystem.OpenDirectoryAsync();
            _files.AddRange(files);

            _files = _files
                .OrderBy(f => f.Name)
                .ToList();
        }

        private async Task OpenStream(FileHandle handle)
        {
            var stream = await FileSystem.OpenStreamAsync(handle.Key);

            if (stream == null)
            {
                _content = null;
                return;
            }
            using var reader = new StreamReader(stream);
            _content = await reader.ReadToEndAsync();
        }

        private async Task SaveFile()
        {
            if (string.IsNullOrEmpty(_saveContent))
            {
                return;
            }
            var options = new FilePickerOptions("myfile.txt", true, FileAccept.TextFile, FileAccept.JsonFile);

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(_saveContent));
            var fileHandle = await FileSystem.SaveFileAsync(stream, options);

            if (fileHandle == null)
            {
                return;
            }

            _files.Add(fileHandle);

            _files = _files
                .OrderBy(f => f.Name)
                .ToList();
        }
    }
}
