using System.ComponentModel;

namespace Bg3LocaHelper;

partial class FormSettings
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
    this.label4                      = new System.Windows.Forms.Label();
    this.textBoxModsPath             = new System.Windows.Forms.TextBox();
    this.labelModsPath               = new System.Windows.Forms.Label();
    this.buttonFileModsPath          = new System.Windows.Forms.Button();
    this.folderBrowserDialogModsPath = new System.Windows.Forms.FolderBrowserDialog();
    this.buttonApply                 = new System.Windows.Forms.Button();
    this.buttonAbort                 = new System.Windows.Forms.Button();
    this.SuspendLayout();

    // 
    // label4
    // 
    this.label4.Font      = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.label4.Location  = new System.Drawing.Point(137, 9);
    this.label4.Name      = "label4";
    this.label4.Size      = new System.Drawing.Size(185, 21);
    this.label4.TabIndex  = 26;
    this.label4.Text      = "Origin Mod Files";
    this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

    // 
    // textBoxModsPath
    // 
    this.textBoxModsPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                        | System.Windows.Forms.AnchorStyles.Right)));

    this.textBoxModsPath.Location = new System.Drawing.Point(137, 48);
    this.textBoxModsPath.Name     = "textBoxModsPath";
    this.textBoxModsPath.Size     = new System.Drawing.Size(300, 20);
    this.textBoxModsPath.TabIndex = 25;

    // 
    // labelModsPath
    // 
    this.labelModsPath.Location  = new System.Drawing.Point(2, 40);
    this.labelModsPath.Name      = "labelModsPath";
    this.labelModsPath.Size      = new System.Drawing.Size(110, 35);
    this.labelModsPath.TabIndex  = 24;
    this.labelModsPath.Text      = "Path to Mod-Working Folder:";
    this.labelModsPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

    // 
    // buttonFileModsPath
    // 
    this.buttonFileModsPath.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
    this.buttonFileModsPath.Location                =  new System.Drawing.Point(443, 47);
    this.buttonFileModsPath.Name                    =  "buttonFileModsPath";
    this.buttonFileModsPath.Size                    =  new System.Drawing.Size(38, 21);
    this.buttonFileModsPath.TabIndex                =  23;
    this.buttonFileModsPath.Text                    =  "...";
    this.buttonFileModsPath.UseVisualStyleBackColor =  true;
    this.buttonFileModsPath.Click                   += new System.EventHandler(this.buttonFileModsPath_Click);

    // 
    // buttonApply
    // 
    this.buttonApply.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
    this.buttonApply.Location                =  new System.Drawing.Point(325, 109);
    this.buttonApply.Name                    =  "buttonApply";
    this.buttonApply.Size                    =  new System.Drawing.Size(75, 23);
    this.buttonApply.TabIndex                =  27;
    this.buttonApply.Text                    =  "Apply";
    this.buttonApply.UseVisualStyleBackColor =  true;
    this.buttonApply.Click                   += new System.EventHandler(this.buttonApply_Click);

    // 
    // buttonAbort
    // 
    this.buttonAbort.Anchor                  = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
    this.buttonAbort.DialogResult            = System.Windows.Forms.DialogResult.Cancel;
    this.buttonAbort.Location                = new System.Drawing.Point(406, 109);
    this.buttonAbort.Name                    = "buttonAbort";
    this.buttonAbort.Size                    = new System.Drawing.Size(75, 23);
    this.buttonAbort.TabIndex                = 28;
    this.buttonAbort.Text                    = "Abort";
    this.buttonAbort.UseVisualStyleBackColor = true;

    // 
    // FormSettings
    // 
    this.AcceptButton        = this.buttonApply;
    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
    this.CancelButton        = this.buttonAbort;
    this.ClientSize          = new System.Drawing.Size(493, 144);
    this.Controls.Add(this.buttonAbort);
    this.Controls.Add(this.buttonApply);
    this.Controls.Add(this.label4);
    this.Controls.Add(this.textBoxModsPath);
    this.Controls.Add(this.labelModsPath);
    this.Controls.Add(this.buttonFileModsPath);
    this.Name          = "FormSettings";
    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
    this.Text          = "Settings";
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private System.Windows.Forms.Button buttonApply;

  private System.Windows.Forms.Button buttonAbort;

  private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogModsPath;

  private System.Windows.Forms.Label label4;

  private System.Windows.Forms.TextBox textBoxModsPath;

  private System.Windows.Forms.Label labelModsPath;

  private System.Windows.Forms.Button buttonFileModsPath;

  #endregion
}