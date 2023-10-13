namespace Bg3LocaHelper
{
  partial class FormMain
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
      this.buttonFileTranslated      = new System.Windows.Forms.Button();
      this.labelSource               = new System.Windows.Forms.Label();
      this.labelReference            = new System.Windows.Forms.Label();
      this.buttonFileOriginCurrent   = new System.Windows.Forms.Button();
      this.openFileDialog            = new System.Windows.Forms.OpenFileDialog();
      this.buttonLoad                = new System.Windows.Forms.Button();
      this.buttonSave                = new System.Windows.Forms.Button();
      this.textBoxOriginCurrentFile  = new System.Windows.Forms.TextBox();
      this.textBoxTranslatedFile     = new System.Windows.Forms.TextBox();
      this.textBoxTranslatedText     = new System.Windows.Forms.TextBox();
      this.textBoxCurrentOriginText  = new System.Windows.Forms.TextBox();
      this.label1                    = new System.Windows.Forms.Label();
      this.label2                    = new System.Windows.Forms.Label();
      this.dataGridViewSource        = new System.Windows.Forms.DataGridView();
      this.buttonPasteToTranslated   = new System.Windows.Forms.Button();
      this.buttonCopyFromOrigin      = new System.Windows.Forms.Button();
      this.textBoxOriginPreviousFile = new System.Windows.Forms.TextBox();
      this.label3                    = new System.Windows.Forms.Label();
      this.buttonFileOriginPrevious  = new System.Windows.Forms.Button();
      this.label4                    = new System.Windows.Forms.Label();
      this.label5                    = new System.Windows.Forms.Label();
      this.buttonCopyFromPrevious    = new System.Windows.Forms.Button();
      this.label6                    = new System.Windows.Forms.Label();
      this.textBoxPreviousOriginText = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSource)).BeginInit();
      this.SuspendLayout();

      //
      // buttonFileTranslated
      //
      this.buttonFileTranslated.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonFileTranslated.Location                =  new System.Drawing.Point(691, 112);
      this.buttonFileTranslated.Name                    =  "buttonFileTranslated";
      this.buttonFileTranslated.Size                    =  new System.Drawing.Size(38, 21);
      this.buttonFileTranslated.TabIndex                =  0;
      this.buttonFileTranslated.Text                    =  "...";
      this.buttonFileTranslated.UseVisualStyleBackColor =  true;
      this.buttonFileTranslated.Click                   += new System.EventHandler(this.buttonFileTranslated_Click);

      //
      // labelSource
      //
      this.labelSource.Location  = new System.Drawing.Point(4, 112);
      this.labelSource.Name      = "labelSource";
      this.labelSource.Size      = new System.Drawing.Size(110, 21);
      this.labelSource.TabIndex  = 1;
      this.labelSource.Text      = "Tranlated XML:";
      this.labelSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      //
      // labelReference
      //
      this.labelReference.Location  = new System.Drawing.Point(4, 59);
      this.labelReference.Name      = "labelReference";
      this.labelReference.Size      = new System.Drawing.Size(110, 21);
      this.labelReference.TabIndex  = 4;
      this.labelReference.Text      = "Current XML:";
      this.labelReference.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      //
      // buttonFileOriginCurrent
      //
      this.buttonFileOriginCurrent.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonFileOriginCurrent.Location                =  new System.Drawing.Point(691, 54);
      this.buttonFileOriginCurrent.Name                    =  "buttonFileOriginCurrent";
      this.buttonFileOriginCurrent.Size                    =  new System.Drawing.Size(38, 21);
      this.buttonFileOriginCurrent.TabIndex                =  3;
      this.buttonFileOriginCurrent.Text                    =  "...";
      this.buttonFileOriginCurrent.UseVisualStyleBackColor =  true;
      this.buttonFileOriginCurrent.Click                   += new System.EventHandler(this.buttonFileOriginCurrent_Click);

      //
      // openFileDialog
      //
      this.openFileDialog.FileName = "openFileDialog1";

      //
      // buttonLoad
      //
      this.buttonLoad.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonLoad.Location                =  new System.Drawing.Point(735, 28);
      this.buttonLoad.Name                    =  "buttonLoad";
      this.buttonLoad.Size                    =  new System.Drawing.Size(137, 32);
      this.buttonLoad.TabIndex                =  6;
      this.buttonLoad.Text                    =  "Load Source XML";
      this.buttonLoad.UseVisualStyleBackColor =  true;
      this.buttonLoad.Click                   += new System.EventHandler(this.buttonLoad_Click);

      //
      // buttonSave
      //
      this.buttonSave.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonSave.Location                =  new System.Drawing.Point(735, 102);
      this.buttonSave.Name                    =  "buttonSave";
      this.buttonSave.Size                    =  new System.Drawing.Size(137, 31);
      this.buttonSave.TabIndex                =  7;
      this.buttonSave.Text                    =  "Save Source XML";
      this.buttonSave.UseVisualStyleBackColor =  true;
      this.buttonSave.Click                   += new System.EventHandler(this.buttonSave_Click);

      //
      // textBoxOriginCurrentFile
      //
      this.textBoxOriginCurrentFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                   | System.Windows.Forms.AnchorStyles.Right)));

      this.textBoxOriginCurrentFile.Location = new System.Drawing.Point(147, 55);
      this.textBoxOriginCurrentFile.Name     = "textBoxOriginCurrentFile";
      this.textBoxOriginCurrentFile.Size     = new System.Drawing.Size(538, 20);
      this.textBoxOriginCurrentFile.TabIndex = 8;

      //
      // textBoxTranslatedFile
      //
      this.textBoxTranslatedFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));

      this.textBoxTranslatedFile.Location = new System.Drawing.Point(147, 113);
      this.textBoxTranslatedFile.Name     = "textBoxTranslatedFile";
      this.textBoxTranslatedFile.Size     = new System.Drawing.Size(538, 20);
      this.textBoxTranslatedFile.TabIndex = 9;

      //
      // textBoxTranslatedText
      //
      this.textBoxTranslatedText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));

      this.textBoxTranslatedText.Font      =  new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBoxTranslatedText.Location  =  new System.Drawing.Point(147, 556);
      this.textBoxTranslatedText.Multiline =  true;
      this.textBoxTranslatedText.Name      =  "textBoxTranslatedText";
      this.textBoxTranslatedText.Size      =  new System.Drawing.Size(725, 193);
      this.textBoxTranslatedText.TabIndex  =  10;
      this.textBoxTranslatedText.Enter     += new System.EventHandler(this.textBoxTranslatedText_Enter);
      this.textBoxTranslatedText.Leave     += new System.EventHandler(this.textBoxTranslatedText_Leave);

      //
      // textBoxCurrentOriginText
      //
      this.textBoxCurrentOriginText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                                   | System.Windows.Forms.AnchorStyles.Right)));

      this.textBoxCurrentOriginText.Font      = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBoxCurrentOriginText.Location  = new System.Drawing.Point(147, 466);
      this.textBoxCurrentOriginText.Multiline = true;
      this.textBoxCurrentOriginText.Name      = "textBoxCurrentOriginText";
      this.textBoxCurrentOriginText.ReadOnly  = true;
      this.textBoxCurrentOriginText.Size      = new System.Drawing.Size(725, 84);
      this.textBoxCurrentOriginText.TabIndex  = 11;

      //
      // label1
      //
      this.label1.Anchor    = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.Location  = new System.Drawing.Point(4, 470);
      this.label1.Name      = "label1";
      this.label1.Size      = new System.Drawing.Size(137, 21);
      this.label1.TabIndex  = 14;
      this.label1.Text      = "Current Reference Text:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      //
      // label2
      //
      this.label2.Anchor    = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.Location  = new System.Drawing.Point(4, 557);
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
      this.dataGridViewSource.Location                    =  new System.Drawing.Point(147, 139);
      this.dataGridViewSource.Name                        =  "dataGridViewSource";
      this.dataGridViewSource.Size                        =  new System.Drawing.Size(725, 231);
      this.dataGridViewSource.TabIndex                    =  16;
      this.dataGridViewSource.RowEnter                    += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSource_RowEnter);
      this.dataGridViewSource.RowPrePaint                 += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridViewSource_RowPrePaint);

      //
      // buttonPasteToTranslated
      //
      this.buttonPasteToTranslated.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonPasteToTranslated.Location                =  new System.Drawing.Point(4, 711);
      this.buttonPasteToTranslated.Name                    =  "buttonPasteToTranslated";
      this.buttonPasteToTranslated.Size                    =  new System.Drawing.Size(137, 38);
      this.buttonPasteToTranslated.TabIndex                =  17;
      this.buttonPasteToTranslated.Text                    =  "Paste from Clipboard";
      this.buttonPasteToTranslated.UseVisualStyleBackColor =  true;
      this.buttonPasteToTranslated.Click                   += new System.EventHandler(this.buttonPasteToTranslated_Click);

      //
      // buttonCopyFromOrigin
      //
      this.buttonCopyFromOrigin.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonCopyFromOrigin.Location                =  new System.Drawing.Point(4, 516);
      this.buttonCopyFromOrigin.Name                    =  "buttonCopyFromOrigin";
      this.buttonCopyFromOrigin.Size                    =  new System.Drawing.Size(137, 38);
      this.buttonCopyFromOrigin.TabIndex                =  18;
      this.buttonCopyFromOrigin.Text                    =  "Copy to Clipboard";
      this.buttonCopyFromOrigin.UseVisualStyleBackColor =  true;
      this.buttonCopyFromOrigin.Click                   += new System.EventHandler(this.buttonCopyFromOrigin_Click);

      //
      // textBoxOriginPreviousFile
      //
      this.textBoxOriginPreviousFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                    | System.Windows.Forms.AnchorStyles.Right)));

      this.textBoxOriginPreviousFile.Location =  new System.Drawing.Point(147, 29);
      this.textBoxOriginPreviousFile.Name     =  "textBoxOriginPreviousFile";
      this.textBoxOriginPreviousFile.Size     =  new System.Drawing.Size(538, 20);
      this.textBoxOriginPreviousFile.TabIndex =  21;

      //
      // label3
      //
      this.label3.Location  = new System.Drawing.Point(4, 33);
      this.label3.Name      = "label3";
      this.label3.Size      = new System.Drawing.Size(110, 21);
      this.label3.TabIndex  = 20;
      this.label3.Text      = "Previous XML:";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      //
      // buttonFileOriginPrevious
      //
      this.buttonFileOriginPrevious.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonFileOriginPrevious.Location                =  new System.Drawing.Point(691, 28);
      this.buttonFileOriginPrevious.Name                    =  "buttonFileOriginPrevious";
      this.buttonFileOriginPrevious.Size                    =  new System.Drawing.Size(38, 21);
      this.buttonFileOriginPrevious.TabIndex                =  19;
      this.buttonFileOriginPrevious.Text                    =  "...";
      this.buttonFileOriginPrevious.UseVisualStyleBackColor =  true;
      this.buttonFileOriginPrevious.Click                   += new System.EventHandler(this.buttonFileOriginPrevious_Click);

      //
      // label4
      //
      this.label4.Font      = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location  = new System.Drawing.Point(147, 3);
      this.label4.Name      = "label4";
      this.label4.Size      = new System.Drawing.Size(185, 21);
      this.label4.TabIndex  = 22;
      this.label4.Text      = "Origin Mod Files";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      //
      // label5
      //
      this.label5.Font      = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location  = new System.Drawing.Point(147, 89);
      this.label5.Name      = "label5";
      this.label5.Size      = new System.Drawing.Size(185, 21);
      this.label5.TabIndex  = 23;
      this.label5.Text      = "Translated Mod File";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      //
      // buttonCopyFromPrevious
      //
      this.buttonCopyFromPrevious.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonCopyFromPrevious.Location                =  new System.Drawing.Point(4, 422);
      this.buttonCopyFromPrevious.Name                    =  "buttonCopyFromPrevious";
      this.buttonCopyFromPrevious.Size                    =  new System.Drawing.Size(137, 38);
      this.buttonCopyFromPrevious.TabIndex                =  27;
      this.buttonCopyFromPrevious.Text                    =  "Copy to Clipboard";
      this.buttonCopyFromPrevious.UseVisualStyleBackColor =  true;
      this.buttonCopyFromPrevious.Click                   += new System.EventHandler(this.buttonCopyFromPrevious_Click);

      //
      // label6
      //
      this.label6.Anchor    = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label6.Location  = new System.Drawing.Point(4, 376);
      this.label6.Name      = "label6";
      this.label6.Size      = new System.Drawing.Size(137, 21);
      this.label6.TabIndex  = 26;
      this.label6.Text      = "Previous Reference Text:";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      //
      // textBoxPreviousOriginText
      //
      this.textBoxPreviousOriginText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                                    | System.Windows.Forms.AnchorStyles.Right)));

      this.textBoxPreviousOriginText.Font      = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBoxPreviousOriginText.Location  = new System.Drawing.Point(147, 376);
      this.textBoxPreviousOriginText.Multiline = true;
      this.textBoxPreviousOriginText.Name      = "textBoxPreviousOriginText";
      this.textBoxPreviousOriginText.ReadOnly  = true;
      this.textBoxPreviousOriginText.Size      = new System.Drawing.Size(725, 80);
      this.textBoxPreviousOriginText.TabIndex  = 25;

      //
      // FormMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize          = new System.Drawing.Size(884, 761);
      this.Controls.Add(this.buttonCopyFromPrevious);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.textBoxPreviousOriginText);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.textBoxOriginPreviousFile);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.buttonFileOriginPrevious);
      this.Controls.Add(this.buttonCopyFromOrigin);
      this.Controls.Add(this.buttonPasteToTranslated);
      this.Controls.Add(this.dataGridViewSource);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBoxCurrentOriginText);
      this.Controls.Add(this.textBoxTranslatedText);
      this.Controls.Add(this.textBoxTranslatedFile);
      this.Controls.Add(this.textBoxOriginCurrentFile);
      this.Controls.Add(this.buttonSave);
      this.Controls.Add(this.buttonLoad);
      this.Controls.Add(this.labelReference);
      this.Controls.Add(this.labelSource);
      this.Controls.Add(this.buttonFileTranslated);
      this.Controls.Add(this.buttonFileOriginCurrent);
      this.MinimumSize   =  new System.Drawing.Size(900, 800);
      this.Name          =  "FormMain";
      this.StartPosition =  System.Windows.Forms.FormStartPosition.Manual;
      this.Text          =  "BG3 Loca Helper";
      this.FormClosed    += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSource)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private System.Windows.Forms.Button buttonCopyFromPrevious;

    private System.Windows.Forms.Label label6;

    private System.Windows.Forms.TextBox textBoxPreviousOriginText;

    private System.Windows.Forms.Label label5;

    private System.Windows.Forms.TextBox textBoxOriginPreviousFile;

    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.Button buttonFileOriginPrevious;

    private System.Windows.Forms.Label label4;

    private System.Windows.Forms.Button buttonCopyFromOrigin;

    private System.Windows.Forms.Button buttonPasteToTranslated;

    private System.Windows.Forms.DataGridView dataGridViewSource;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.TextBox textBoxCurrentOriginText;

    private System.Windows.Forms.TextBox textBoxTranslatedText;

    private System.Windows.Forms.TextBox textBoxOriginCurrentFile;

    private System.Windows.Forms.TextBox textBoxTranslatedFile;

    private System.Windows.Forms.Button buttonLoad;

    private System.Windows.Forms.Button buttonSave;

    private System.Windows.Forms.OpenFileDialog openFileDialog;

    private System.Windows.Forms.Label labelReference;

    private System.Windows.Forms.Button buttonFileTranslated;
    
    private System.Windows.Forms.Button buttonFileOriginCurrent;

    private System.Windows.Forms.Label labelSource;

    #endregion
  }
}