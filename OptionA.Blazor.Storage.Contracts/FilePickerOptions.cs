using System.Collections.Generic;
using System.Linq;

namespace OptionA.Blazor.Storage;

/// <summary>
/// Options for file pickers
/// </summary>    
public class FilePickerOptions
{
    /// <summary>
    /// Constructor with name
    /// </summary>
    /// <param name="suggestedName"></param>
    /// <param name="excludeDifferentTypes"></param>
    /// <param name="accepts"></param>
    public FilePickerOptions(string? suggestedName, bool excludeDifferentTypes, params FileAccept[] accepts)
    {
        SuggestedName = suggestedName;
        ExcludeDifferentTypes = excludeDifferentTypes;
        Accepts = accepts.ToList();
    }

    /// <summary>
    /// Constructor without name
    /// </summary>
    /// <param name="excludeDifferentTypes"></param>
    /// <param name="accepts"></param>
    public FilePickerOptions(bool excludeDifferentTypes, params FileAccept[] accepts) : this(null, excludeDifferentTypes, accepts)
    {

    }

    /// <summary>
    /// Suggested name for save files
    /// </summary>
    public string? SuggestedName { get; set; }
    /// <summary>
    /// Set to true to only allow the provided accepts
    /// </summary>
    public bool ExcludeDifferentTypes { get; set; }
    /// <summary>
    /// Filetype to accept
    /// </summary>
    public List<FileAccept> Accepts { get; set; }

}
