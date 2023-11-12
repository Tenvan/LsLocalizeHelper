using System.Windows;
using System.Windows.Controls;

namespace LsLocalizeHelper.Controls;

public partial class LsTextBox : UserControl
{

  #region Constructors

  public LsTextBox()
  {
    InitializeComponent();
    this.CustomTextBox.SelectionChanged += (sender, e) => MoveCustomCaret();
    this.CustomTextBox.LostFocus += (sender, e) => Caret.Visibility = Visibility.Collapsed;
    this.CustomTextBox.GotFocus += (sender, e) => Caret.Visibility = Visibility.Visible;
  }

  #endregion

  #region Properties

  public TextBox TextBox
  {
    get => this.CustomTextBox;

    set => this.CustomTextBox = value;
  }

  public string Text
  {
    get => this.CustomTextBox.Text;

    set => this.CustomTextBox.Text = value;
  }

  #endregion

  #region Methods

  /// <summary>
  /// Moves the custom caret on the canvas.
  /// </summary>
  private void MoveCustomCaret()
  {
    var caretLocation = CustomTextBox.GetRectFromCharacterIndex(CustomTextBox.CaretIndex).Location;

    if (!double.IsInfinity(caretLocation.X))
    {
      Canvas.SetLeft(Caret, caretLocation.X);
    }

    if (!double.IsInfinity(caretLocation.Y))
    {
      Canvas.SetTop(Caret, caretLocation.Y);
    }
  }

  #endregion

}
