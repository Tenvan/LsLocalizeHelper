using System.ComponentModel;

namespace Bg3LocaHelper;

partial class FormImport
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
    this.buttonImport         = new System.Windows.Forms.Button();
    this.openFileDialog       = new System.Windows.Forms.OpenFileDialog();
    this.buttonOpenFileDialog = new System.Windows.Forms.Button();
    this.labelPak             = new System.Windows.Forms.Label();
    this.labelModName         = new System.Windows.Forms.Label();
    this.textBoxPakFile       = new System.Windows.Forms.TextBox();
    this.textBoxModName       = new System.Windows.Forms.TextBox();
    this.SuspendLayout();

    // 
    // buttonImport
    // 
    this.buttonImport.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
    this.buttonImport.Location                =  new System.Drawing.Point(684, 50);
    this.buttonImport.Name                    =  "buttonImport";
    this.buttonImport.Size                    =  new System.Drawing.Size(88, 28);
    this.buttonImport.TabIndex                =  0;
    this.buttonImport.Text                    =  "Import";
    this.buttonImport.UseVisualStyleBackColor =  true;
    this.buttonImport.Click                   += new System.EventHandler(this.buttonImport_Click);

    // 
    // openFileDialog
    // 
    this.openFileDialog.DefaultExt = "pak";
    this.openFileDialog.Title      = "Choose PAK File";

    // 
    // buttonOpenFileDialog
    // 
    this.buttonOpenFileDialog.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
    this.buttonOpenFileDialog.Location                =  new System.Drawing.Point(731, 12);
    this.buttonOpenFileDialog.Name                    =  "buttonOpenFileDialog";
    this.buttonOpenFileDialog.Size                    =  new System.Drawing.Size(41, 23);
    this.buttonOpenFileDialog.TabIndex                =  2;
    this.buttonOpenFileDialog.Text                    =  "...";
    this.buttonOpenFileDialog.UseVisualStyleBackColor =  true;
    this.buttonOpenFileDialog.Click                   += new System.EventHandler(this.buttonOpenFileDialog_Click);

    // 
    // labelPak
    // 
    this.labelPak.Location  = new System.Drawing.Point(12, 12);
    this.labelPak.Name      = "labelPak";
    this.labelPak.Size      = new System.Drawing.Size(97, 23);
    this.labelPak.TabIndex  = 3;
    this.labelPak.Text      = "Choose PAK File:";
    this.labelPak.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

    // 
    // labelModName
    // 
    this.labelModName.Location  = new System.Drawing.Point(12, 53);
    this.labelModName.Name      = "labelModName";
    this.labelModName.Size      = new System.Drawing.Size(97, 23);
    this.labelModName.TabIndex  = 5;
    this.labelModName.Text      = "New Mod Name:";
    this.labelModName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

    // 
    // textBoxPakFile
    // 
    this.textBoxPakFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                       | System.Windows.Forms.AnchorStyles.Right)));

    this.textBoxPakFile.Location = new System.Drawing.Point(115, 12);
    this.textBoxPakFile.Name     = "textBoxPakFile";
    this.textBoxPakFile.Size     = new System.Drawing.Size(610, 20);
    this.textBoxPakFile.TabIndex = 6;

    // 
    // textBoxModName
    // 
    this.textBoxModName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                       | System.Windows.Forms.AnchorStyles.Right)));

    this.textBoxModName.Location = new System.Drawing.Point(115, 55);
    this.textBoxModName.Name     = "textBoxModName";
    this.textBoxModName.Size     = new System.Drawing.Size(563, 20);
    this.textBoxModName.TabIndex = 7;

    // 
    // FormImport
    // 
    this.AcceptButton        = this.buttonImport;
    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
    this.ClientSize          = new System.Drawing.Size(784, 89);
    this.Controls.Add(this.textBoxModName);
    this.Controls.Add(this.textBoxPakFile);
    this.Controls.Add(this.labelModName);
    this.Controls.Add(this.labelPak);
    this.Controls.Add(this.buttonOpenFileDialog);
    this.Controls.Add(this.buttonImport);
    this.MaximumSize   = new System.Drawing.Size(1600, 128);
    this.MinimumSize   = new System.Drawing.Size(600, 128);
    this.Name          = "FormImport";
    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
    this.Text          = "Import Mod";
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private System.Windows.Forms.TextBox textBoxPakFile;

  private System.Windows.Forms.TextBox textBoxModName;

  private System.Windows.Forms.Label labelModName;

  private System.Windows.Forms.Button buttonImport;

  private System.Windows.Forms.OpenFileDialog openFileDialog;

  private System.Windows.Forms.Button buttonOpenFileDialog;

  private System.Windows.Forms.Label labelPak;

  #endregion
}