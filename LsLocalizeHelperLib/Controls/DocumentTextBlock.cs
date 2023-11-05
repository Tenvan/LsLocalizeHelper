using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace LsLocalizeHelperLib.Controls;

/// <summary>
/// The markup text block is a replacement for <see cref="TextBlock"/> 
/// that allows to specify markup content dynamically.
/// </summary>
[ContentProperty("Document")]
[Localizability(LocalizationCategory.Text)]
public class DocumentTextBlock : TextBlock
{

  public static readonly DependencyProperty DocumentProperty = DependencyProperty.Register(
    name: "Document",
    propertyType: typeof(FlowDocument),
    ownerType: typeof(DocumentTextBlock),
    typeMetadata: new FrameworkPropertyMetadata(
      defaultValue: null,
      flags: FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
      propertyChangedCallback: DocumentTextBlock.OnDocumentChanged
    )
  );

  public DocumentTextBlock(FlowDocument document)
  {
    this.Document = document;
  }

  public FlowDocument Document
  {
    get => (FlowDocument)XamlReader.Parse(this.Inlines.ToString());

    set => this.SetValue(dp: DocumentTextBlock.DocumentProperty, value: value);
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="DocumentTextBlock"/> class.
  /// </summary>
  public DocumentTextBlock() { }

  private static void OnDocumentChanged(DependencyObject dependencyObject,
                                          DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs
  )
  {
    var documentTextBlock = dependencyObject as DocumentTextBlock;

    if (documentTextBlock == null)
    {
      return;
    }
    
    var flowDocument = dependencyPropertyChangedEventArgs.NewValue as FlowDocument;

    try
    {
      var inline = XamlWriter.Save(flowDocument);

      documentTextBlock.Inlines.Clear();
      documentTextBlock.Inlines.Add(inline);
    }
    catch (Exception ex)
    {
      // ignored
    }
  }

}
