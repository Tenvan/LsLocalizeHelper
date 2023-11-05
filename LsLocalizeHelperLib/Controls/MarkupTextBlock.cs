using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace LsLocalizeHelperLib.Controls;

/// <summary>
/// The markup text block is a replacement for <see cref="TextBlock"/> 
/// that allows to specify markup content dynamically.
/// </summary>
[ContentProperty("MarkupText")]
[Localizability(LocalizationCategory.Text)]
public class MarkupTextBlock : TextBlock
{

  /// <summary>
  /// The markup text property.
  /// </summary>
  public static readonly DependencyProperty MarkupTextProperty = DependencyProperty.Register(
    name: "MarkupText",
    propertyType: typeof(string),
    ownerType: typeof(MarkupTextBlock),
    typeMetadata: new FrameworkPropertyMetadata(
      defaultValue: string.Empty,
      flags: FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
      propertyChangedCallback: MarkupTextBlock.OnTextMarkupChanged
    )
  );

  private const string FlowDocumentPrefix
    = "<FlowDocument xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'><Paragraph><Span>";

  private const string FlowDocumentSuffix = "</Span></Paragraph></FlowDocument>";

  /// <summary>
  /// Initializes a new instance of the <see cref="MarkupTextBlock"/> class.
  /// </summary>
  /// <param name="markupText">
  /// The markup text.
  /// </param>
  public MarkupTextBlock(string markupText) { this.MarkupText = markupText; }

  /// <summary>
  /// Initializes a new instance of the <see cref="MarkupTextBlock"/> class.
  /// </summary>
  public MarkupTextBlock() { }

  /// <summary>
  /// Gets or sets content of the <see cref="MarkupTextBlock"/>.
  /// </summary>
  [Localizability(LocalizationCategory.Text)]
  public string MarkupText
  {
    get { return this.Inlines.ToString()!; }

    set { this.SetValue(dp: MarkupTextBlock.MarkupTextProperty, value: value); }
  }

  /// <summary>
  /// Process the changed text markup.
  /// </summary>
  /// <param name="dependencyObject">The dependency object.</param>
  /// <param name="dependencyPropertyChangedEventArgs">The event arguments.</param>
  private static void OnTextMarkupChanged(DependencyObject dependencyObject,
                                          DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
  {
    if (!(dependencyObject is MarkupTextBlock markupTextBlock))
    {
      return;
    }

    var flowDocument = new StringBuilder(dependencyPropertyChangedEventArgs.NewValue?.ToString() ?? string.Empty);

    try
    {
      var text = flowDocument.ToString();
      var document = (FlowDocument)XamlReader.Parse(text);

      if (!(document.Blocks.FirstBlock is Paragraph paragraph)) 
      {
        return;
      }

      markupTextBlock.Inlines.Clear();

      foreach (var inline in paragraph.Inlines.ToArray()) // ToArray to avoid collection modified exception
      {
        if (inline == null) 
        {
          continue;
        }

        paragraph.Inlines.Remove(inline);
        markupTextBlock.Inlines.Add(inline);
      }
    }
    catch (Exception ex)
    {
      // Log or handle exception.
    }
  }
}
