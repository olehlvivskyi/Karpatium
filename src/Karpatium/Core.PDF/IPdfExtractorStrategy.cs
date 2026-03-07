namespace Karpatium.Core.PDF;

/// <summary>
/// Represents a strategy interface for extracting information from PDF files.
/// </summary>
public interface IPdfExtractorStrategy
{
    /// <summary>
    /// Extracts images from a PDF file at the specified path.
    /// </summary>
    /// <param name="path">The file path to the PDF document from which images will be extracted.</param>
    /// <returns>A dictionary where the key represents the page number, and the value is a list of byte arrays representing images extracted from that page.</returns>
    Dictionary<int, List<byte[]>> GetImages(string path);

    /// <summary>
    /// Extracts text content from a PDF file at the specified path.
    /// </summary>
    /// <param name="path">The file path to the PDF document from which text will be extracted.</param>
    /// <returns>A dictionary where the key represents the page number, and the value is the text content extracted from that page.</returns>
    Dictionary<int, string> GetText(string path);
}