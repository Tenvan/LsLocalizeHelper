namespace Bg3LocaHelper
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

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
      this.buttonFileSource        = new System.Windows.Forms.Button();
      this.labelSource             = new System.Windows.Forms.Label();
      this.labelReference          = new System.Windows.Forms.Label();
      this.buttonFileReference     = new System.Windows.Forms.Button();
      this.openFileDialogSource    = new System.Windows.Forms.OpenFileDialog();
      this.openFileDialogReference = new System.Windows.Forms.OpenFileDialog();
      this.buttonLoad              = new System.Windows.Forms.Button();
      this.buttonSave              = new System.Windows.Forms.Button();
      this.textBoxReference        = new System.Windows.Forms.TextBox();
      this.textBoxSource           = new System.Windows.Forms.TextBox();
      this.textBoxSourceText       = new System.Windows.Forms.TextBox();
      this.textBoxReferenceText    = new System.Windows.Forms.TextBox();
      this.label1                  = new System.Windows.Forms.Label();
      this.label2                  = new System.Windows.Forms.Label();
      this.dataGridViewSource      = new System.Windows.Forms.DataGridView();
      this.buttonPaste             = new System.Windows.Forms.Button();
      this.buttonCopyReference     = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSource)).BeginInit();
      this.SuspendLayout();

      // 
      // buttonFileSource
      // 
      this.buttonFileSource.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonFileSource.Location                =  new System.Drawing.Point(541, 36);
      this.buttonFileSource.Name                    =  "buttonFileSource";
      this.buttonFileSource.Size                    =  new System.Drawing.Size(38, 21);
      this.buttonFileSource.TabIndex                =  0;
      this.buttonFileSource.Text                    =  "...";
      this.buttonFileSource.UseVisualStyleBackColor =  true;
      this.buttonFileSource.Click                   += new System.EventHandler(this.buttonFileSource_Click);

      // 
      // labelSource
      // 
      this.labelSource.Location  = new System.Drawing.Point(4, 35);
      this.labelSource.Name      = "labelSource";
      this.labelSource.Size      = new System.Drawing.Size(110, 21);
      this.labelSource.TabIndex  = 1;
      this.labelSource.Text      = "Source XML File:";
      this.labelSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      // 
      // labelReference
      // 
      this.labelReference.Location  = new System.Drawing.Point(4, 11);
      this.labelReference.Name      = "labelReference";
      this.labelReference.Size      = new System.Drawing.Size(110, 21);
      this.labelReference.TabIndex  = 4;
      this.labelReference.Text      = "Original Reference:";
      this.labelReference.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      // 
      // buttonFileReference
      // 
      this.buttonFileReference.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonFileReference.Location                =  new System.Drawing.Point(541, 6);
      this.buttonFileReference.Name                    =  "buttonFileReference";
      this.buttonFileReference.Size                    =  new System.Drawing.Size(38, 21);
      this.buttonFileReference.TabIndex                =  3;
      this.buttonFileReference.Text                    =  "...";
      this.buttonFileReference.UseVisualStyleBackColor =  true;
      this.buttonFileReference.Click                   += new System.EventHandler(this.buttonFileReferecnce_Click);

      // 
      // openFileDialogSource
      // 
      this.openFileDialogSource.FileName = "openFileDialog1";

      // 
      // openFileDialogReference
      // 
      this.openFileDialogReference.FileName = "openFileDialog1";

      // 
      // buttonLoad
      // 
      this.buttonLoad.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonLoad.Location                =  new System.Drawing.Point(599, 3);
      this.buttonLoad.Name                    =  "buttonLoad";
      this.buttonLoad.Size                    =  new System.Drawing.Size(137, 53);
      this.buttonLoad.TabIndex                =  6;
      this.buttonLoad.Text                    =  "Load Source XML";
      this.buttonLoad.UseVisualStyleBackColor =  true;
      this.buttonLoad.Click                   += new System.EventHandler(this.buttonLoad_Click);

      // 
      // buttonSave
      // 
      this.buttonSave.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonSave.Location                =  new System.Drawing.Point(742, 4);
      this.buttonSave.Name                    =  "buttonSave";
      this.buttonSave.Size                    =  new System.Drawing.Size(130, 51);
      this.buttonSave.TabIndex                =  7;
      this.buttonSave.Text                    =  "Save Source XML";
      this.buttonSave.UseVisualStyleBackColor =  true;
      this.buttonSave.Click                   += new System.EventHandler(this.buttonSave_Click);

      // 
      // textBoxReference
      // 
      this.textBoxReference.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                           | System.Windows.Forms.AnchorStyles.Right)));

      this.textBoxReference.Location = new System.Drawing.Point(147, 7);
      this.textBoxReference.Name     = "textBoxReference";
      this.textBoxReference.Size     = new System.Drawing.Size(388, 20);
      this.textBoxReference.TabIndex = 8;

      // 
      // textBoxSource
      // 
      this.textBoxSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                        | System.Windows.Forms.AnchorStyles.Right)));

      this.textBoxSource.Location = new System.Drawing.Point(147, 36);
      this.textBoxSource.Name     = "textBoxSource";
      this.textBoxSource.Size     = new System.Drawing.Size(388, 20);
      this.textBoxSource.TabIndex = 9;

      // 
      // textBoxSourceText
      // 
      this.textBoxSourceText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                            | System.Windows.Forms.AnchorStyles.Right)));

      this.textBoxSourceText.Location  =  new System.Drawing.Point(147, 446);
      this.textBoxSourceText.Multiline =  true;
      this.textBoxSourceText.Name      =  "textBoxSourceText";
      this.textBoxSourceText.Size      =  new System.Drawing.Size(725, 103);
      this.textBoxSourceText.TabIndex  =  10;
      this.textBoxSourceText.Leave     += new System.EventHandler(this.textBoxSourceText_Leave);

      // 
      // textBoxReferenceText
      // 
      this.textBoxReferenceText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                               | System.Windows.Forms.AnchorStyles.Right)));

      this.textBoxReferenceText.Location  = new System.Drawing.Point(147, 356);
      this.textBoxReferenceText.Multiline = true;
      this.textBoxReferenceText.Name      = "textBoxReferenceText";
      this.textBoxReferenceText.ReadOnly  = true;
      this.textBoxReferenceText.Size      = new System.Drawing.Size(725, 84);
      this.textBoxReferenceText.TabIndex  = 11;

      // 
      // label1
      // 
      this.label1.Anchor    = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.Location  = new System.Drawing.Point(4, 355);
      this.label1.Name      = "label1";
      this.label1.Size      = new System.Drawing.Size(110, 21);
      this.label1.TabIndex  = 14;
      this.label1.Text      = "Refence Text:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      // 
      // label2
      // 
      this.label2.Anchor    = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.Location  = new System.Drawing.Point(4, 446);
      this.label2.Name      = "label2";
      this.label2.Size      = new System.Drawing.Size(110, 21);
      this.label2.TabIndex  = 15;
      this.label2.Text      = "Source Text:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      // 
      // dataGridViewSource
      // 
      this.dataGridViewSource.AllowUserToAddRows      = false;
      this.dataGridViewSource.AllowUserToDeleteRows   = false;
      this.dataGridViewSource.AllowUserToOrderColumns = true;

      this.dataGridViewSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                              | System.Windows.Forms.AnchorStyles.Left)
                                                                             | System.Windows.Forms.AnchorStyles.Right)));

      this.dataGridViewSource.ColumnHeadersHeightSizeMode =  System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewSource.Location                    =  new System.Drawing.Point(147, 63);
      this.dataGridViewSource.Name                        =  "dataGridViewSource";
      this.dataGridViewSource.Size                        =  new System.Drawing.Size(725, 286);
      this.dataGridViewSource.TabIndex                    =  16;
      this.dataGridViewSource.RowEnter                    += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSource_RowEnter);
      this.dataGridViewSource.RowPrePaint                 += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridViewSource_RowPrePaint);

      // 
      // buttonPaste
      // 
      this.buttonPaste.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonPaste.Location                =  new System.Drawing.Point(4, 511);
      this.buttonPaste.Name                    =  "buttonPaste";
      this.buttonPaste.Size                    =  new System.Drawing.Size(137, 38);
      this.buttonPaste.TabIndex                =  17;
      this.buttonPaste.Text                    =  "Paste from Clipboard";
      this.buttonPaste.UseVisualStyleBackColor =  true;
      this.buttonPaste.Click                   += new System.EventHandler(this.buttonPaste_Click);

      // 
      // buttonCopyReference
      // 
      this.buttonCopyReference.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonCopyReference.Location                =  new System.Drawing.Point(4, 402);
      this.buttonCopyReference.Name                    =  "buttonCopyReference";
      this.buttonCopyReference.Size                    =  new System.Drawing.Size(137, 38);
      this.buttonCopyReference.TabIndex                =  18;
      this.buttonCopyReference.Text                    =  "Copy to Clipboard";
      this.buttonCopyReference.UseVisualStyleBackColor =  true;
      this.buttonCopyReference.Click                   += new System.EventHandler(this.buttonCopyReference_Click);

      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize          = new System.Drawing.Size(884, 561);
      this.Controls.Add(this.buttonCopyReference);
      this.Controls.Add(this.buttonPaste);
      this.Controls.Add(this.dataGridViewSource);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBoxReferenceText);
      this.Controls.Add(this.textBoxSourceText);
      this.Controls.Add(this.textBoxSource);
      this.Controls.Add(this.textBoxReference);
      this.Controls.Add(this.buttonSave);
      this.Controls.Add(this.buttonLoad);
      this.Controls.Add(this.labelReference);
      this.Controls.Add(this.labelSource);
      this.Controls.Add(this.buttonFileSource);
      this.Controls.Add(this.buttonFileReference);
      this.MinimumSize   =  new System.Drawing.Size(900, 600);
      this.Name          =  "Form1";
      this.StartPosition =  System.Windows.Forms.FormStartPosition.Manual;
      this.Text          =  "BG3 Loca Helper";
      this.FormClosed    += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSource)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private System.Windows.Forms.Button buttonCopyReference;

    private System.Windows.Forms.Button buttonPaste;

    private System.Windows.Forms.DataGridView dataGridViewSource;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.TextBox textBoxReferenceText;

    private System.Windows.Forms.TextBox textBoxSourceText;

    private System.Windows.Forms.TextBox textBoxReference;

    private System.Windows.Forms.TextBox textBoxSource;

    private System.Windows.Forms.Button buttonLoad;

    private System.Windows.Forms.Button buttonSave;

    private System.Windows.Forms.OpenFileDialog openFileDialogSource;

    private System.Windows.Forms.OpenFileDialog openFileDialogReference;

    private System.Windows.Forms.Label labelReference;

    private System.Windows.Forms.Button buttonFileSource;
    
    private System.Windows.Forms.Button buttonFileReference;

    private System.Windows.Forms.Label labelSource;

    #endregion
  }
}