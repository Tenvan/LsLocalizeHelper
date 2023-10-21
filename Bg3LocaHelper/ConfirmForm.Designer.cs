using System.ComponentModel;

namespace Bg3LocaHelper;

partial class ConfirmForm
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
    this.labelText = new System.Windows.Forms.Label();
    this.buttonYes = new System.Windows.Forms.Button();
    this.buttonNo  = new System.Windows.Forms.Button();
    this.SuspendLayout();

    // 
    // labelText
    // 
    this.labelText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                   | System.Windows.Forms.AnchorStyles.Left)
                                                                  | System.Windows.Forms.AnchorStyles.Right)));

    this.labelText.Location  = new System.Drawing.Point(12, 9);
    this.labelText.Name      = "labelText";
    this.labelText.Size      = new System.Drawing.Size(350, 82);
    this.labelText.TabIndex  = 0;
    this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

    // 
    // buttonYes
    // 
    this.buttonYes.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
    this.buttonYes.Location                =  new System.Drawing.Point(12, 94);
    this.buttonYes.Name                    =  "buttonYes";
    this.buttonYes.Size                    =  new System.Drawing.Size(75, 23);
    this.buttonYes.TabIndex                =  1;
    this.buttonYes.Text                    =  "Yes";
    this.buttonYes.UseVisualStyleBackColor =  true;
    this.buttonYes.Click                   += new System.EventHandler(this.buttonYes_Click);

    // 
    // buttonNo
    // 
    this.buttonNo.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
    this.buttonNo.DialogResult            =  System.Windows.Forms.DialogResult.Cancel;
    this.buttonNo.Location                =  new System.Drawing.Point(287, 94);
    this.buttonNo.Name                    =  "buttonNo";
    this.buttonNo.Size                    =  new System.Drawing.Size(75, 23);
    this.buttonNo.TabIndex                =  2;
    this.buttonNo.Text                    =  "No";
    this.buttonNo.UseVisualStyleBackColor =  true;
    this.buttonNo.Click                   += new System.EventHandler(this.buttonNo_Click);

    // 
    // ConfirmForm
    // 
    this.AcceptButton        = this.buttonYes;
    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
    this.CancelButton        = this.buttonNo;
    this.ClientSize          = new System.Drawing.Size(374, 129);
    this.Controls.Add(this.buttonNo);
    this.Controls.Add(this.buttonYes);
    this.Controls.Add(this.labelText);
    this.Name = "ConfirmForm";
    this.Text = "ConfirmForm";
    this.ResumeLayout(false);
  }

  private System.Windows.Forms.Button buttonYes;

  private System.Windows.Forms.Button buttonNo;

  private System.Windows.Forms.Label labelText;

  #endregion
}