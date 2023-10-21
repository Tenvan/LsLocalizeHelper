using System.ComponentModel;

namespace Bg3LocaHelper;

partial class InputBoxForm
{
  /// <summary>
  /// Required designer variable.
  /// </summary>
  private IContainer components = null;

  /// <summary>
  /// Clean up any resources being used.
  /// </summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(
    bool disposing
  )
  {
    if (disposing && (components != null)) { components.Dispose(); }

    base.Dispose(disposing);
  }

  #region Windows Form Designer generated code

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.textBoxInput = new System.Windows.Forms.TextBox();
    this.buttonOk     = new System.Windows.Forms.Button();
    this.SuspendLayout();

    // 
    // textBoxInput
    // 
    this.textBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                     | System.Windows.Forms.AnchorStyles.Right)));

    this.textBoxInput.Location = new System.Drawing.Point(12, 12);
    this.textBoxInput.Name     = "textBoxInput";
    this.textBoxInput.Size     = new System.Drawing.Size(392, 20);
    this.textBoxInput.TabIndex = 0;

    // 
    // buttonOk
    // 
    this.buttonOk.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
    this.buttonOk.Location                =  new System.Drawing.Point(410, 10);
    this.buttonOk.Name                    =  "buttonOk";
    this.buttonOk.Size                    =  new System.Drawing.Size(75, 23);
    this.buttonOk.TabIndex                =  1;
    this.buttonOk.Text                    =  "Ok";
    this.buttonOk.UseVisualStyleBackColor =  true;
    this.buttonOk.Click                   += new System.EventHandler(this.okButton_Click);

    // 
    // InputBoxForm
    // 
    this.AcceptButton        = this.buttonOk;
    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
    this.ClientSize          = new System.Drawing.Size(497, 46);
    this.Controls.Add(this.buttonOk);
    this.Controls.Add(this.textBoxInput);
    this.MaximumSize   = new System.Drawing.Size(800, 85);
    this.MinimumSize   = new System.Drawing.Size(200, 85);
    this.Name          = "InputBoxForm";
    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
    this.Text          = "Input";
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private System.Windows.Forms.TextBox textBoxInput;

  private System.Windows.Forms.Button buttonOk;

  #endregion
}