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
      this.components                               = new System.ComponentModel.Container();
      this.labelSource                              = new System.Windows.Forms.Label();
      this.labelReference                           = new System.Windows.Forms.Label();
      this.textBoxTranslatedText                    = new System.Windows.Forms.TextBox();
      this.textBoxCurrentOriginText                 = new System.Windows.Forms.TextBox();
      this.label1                                   = new System.Windows.Forms.Label();
      this.label2                                   = new System.Windows.Forms.Label();
      this.dataGridViewSource                       = new System.Windows.Forms.DataGridView();
      this.buttonPasteToTranslated                  = new System.Windows.Forms.Button();
      this.buttonCopyFromOrigin                     = new System.Windows.Forms.Button();
      this.label3                                   = new System.Windows.Forms.Label();
      this.label4                                   = new System.Windows.Forms.Label();
      this.label5                                   = new System.Windows.Forms.Label();
      this.buttonCopyFromPrevious                   = new System.Windows.Forms.Button();
      this.label6                                   = new System.Windows.Forms.Label();
      this.textBoxPreviousOriginText                = new System.Windows.Forms.TextBox();
      this.textBoxFilter                            = new System.Windows.Forms.TextBox();
      this.label7                                   = new System.Windows.Forms.Label();
      this.checkBoxAutoClipboard                    = new System.Windows.Forms.CheckBox();
      this.menuStrip1                               = new System.Windows.Forms.MenuStrip();
      this.dateiToolStripMenuItem                   = new System.Windows.Forms.ToolStripMenuItem();
      this.settingsToolStripMenuItem                = new System.Windows.Forms.ToolStripMenuItem();
      this.importModToolStripMenuItem               = new System.Windows.Forms.ToolStripMenuItem();
      this.loadSourceXMLToolStripMenuItem           = new System.Windows.Forms.ToolStripMenuItem();
      this.saveSourceXMLToolStripMenuItem           = new System.Windows.Forms.ToolStripMenuItem();
      this.packingModToolStripMenuItem              = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem                    = new System.Windows.Forms.ToolStripMenuItem();
      this.label8                                   = new System.Windows.Forms.Label();
      this.comboBoxMods                             = new System.Windows.Forms.ComboBox();
      this.buttonRefreshMods                        = new System.Windows.Forms.Button();
      this.comboBoxOriginPreviousFile               = new System.Windows.Forms.ComboBox();
      this.contextMenuStripFileBox                  = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.renameFileToolStripMenuItem              = new System.Windows.Forms.ToolStripMenuItem();
      this.copyFileToolStripMenuItem                = new System.Windows.Forms.ToolStripMenuItem();
      this.copyFileToOtherLanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.deleteFileToolStripMenuItem              = new System.Windows.Forms.ToolStripMenuItem();
      this.comboBoxOriginCurrentFile                = new System.Windows.Forms.ComboBox();
      this.comboBoxTranslatedFile                   = new System.Windows.Forms.ComboBox();
      this.toolStripButton1                         = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton2                         = new System.Windows.Forms.ToolStripButton();
      this.toolStripComboBox1                       = new System.Windows.Forms.ToolStripComboBox();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSource)).BeginInit();
      this.menuStrip1.SuspendLayout();
      this.contextMenuStripFileBox.SuspendLayout();
      this.SuspendLayout();

      // 
      // labelSource
      // 
      this.labelSource.Location  = new System.Drawing.Point(12, 177);
      this.labelSource.Name      = "labelSource";
      this.labelSource.Size      = new System.Drawing.Size(110, 21);
      this.labelSource.TabIndex  = 1;
      this.labelSource.Text      = "Tranlated XML:";
      this.labelSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      // 
      // labelReference
      // 
      this.labelReference.Location  = new System.Drawing.Point(12, 120);
      this.labelReference.Name      = "labelReference";
      this.labelReference.Size      = new System.Drawing.Size(110, 21);
      this.labelReference.TabIndex  = 4;
      this.labelReference.Text      = "Current XML:";
      this.labelReference.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      // 
      // textBoxTranslatedText
      // 
      this.textBoxTranslatedText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));

      this.textBoxTranslatedText.Font       =  new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBoxTranslatedText.Location   =  new System.Drawing.Point(147, 635);
      this.textBoxTranslatedText.Multiline  =  true;
      this.textBoxTranslatedText.Name       =  "textBoxTranslatedText";
      this.textBoxTranslatedText.ScrollBars =  System.Windows.Forms.ScrollBars.Both;
      this.textBoxTranslatedText.Size       =  new System.Drawing.Size(725, 193);
      this.textBoxTranslatedText.TabIndex   =  10;
      this.textBoxTranslatedText.Enter      += new System.EventHandler(this.textBoxTranslatedText_Enter);
      this.textBoxTranslatedText.Leave      += new System.EventHandler(this.textBoxTranslatedText_Leave);

      // 
      // textBoxCurrentOriginText
      // 
      this.textBoxCurrentOriginText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                                   | System.Windows.Forms.AnchorStyles.Right)));

      this.textBoxCurrentOriginText.Font       = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBoxCurrentOriginText.Location   = new System.Drawing.Point(147, 545);
      this.textBoxCurrentOriginText.Multiline  = true;
      this.textBoxCurrentOriginText.Name       = "textBoxCurrentOriginText";
      this.textBoxCurrentOriginText.ReadOnly   = true;
      this.textBoxCurrentOriginText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.textBoxCurrentOriginText.Size       = new System.Drawing.Size(725, 84);
      this.textBoxCurrentOriginText.TabIndex   = 11;

      // 
      // label1
      // 
      this.label1.Anchor    = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.Location  = new System.Drawing.Point(12, 546);
      this.label1.Name      = "label1";
      this.label1.Size      = new System.Drawing.Size(129, 21);
      this.label1.TabIndex  = 14;
      this.label1.Text      = "Current Reference Text:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      // 
      // label2
      // 
      this.label2.Anchor    = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.Location  = new System.Drawing.Point(12, 636);
      this.label2.Name      = "label2";
      this.label2.Size      = new System.Drawing.Size(72, 21);
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
      this.dataGridViewSource.EditMode                    =  System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.dataGridViewSource.Location                    =  new System.Drawing.Point(147, 247);
      this.dataGridViewSource.Name                        =  "dataGridViewSource";
      this.dataGridViewSource.ReadOnly                    =  true;
      this.dataGridViewSource.Size                        =  new System.Drawing.Size(725, 202);
      this.dataGridViewSource.TabIndex                    =  16;
      this.dataGridViewSource.CellPainting                += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewSource_CellPainting);
      this.dataGridViewSource.RowEnter                    += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSource_RowEnter);
      this.dataGridViewSource.RowPrePaint                 += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridViewSource_RowPrePaint);

      // 
      // buttonPasteToTranslated
      // 
      this.buttonPasteToTranslated.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonPasteToTranslated.Location                =  new System.Drawing.Point(4, 790);
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
      this.buttonCopyFromOrigin.Location                =  new System.Drawing.Point(12, 591);
      this.buttonCopyFromOrigin.Name                    =  "buttonCopyFromOrigin";
      this.buttonCopyFromOrigin.Size                    =  new System.Drawing.Size(129, 38);
      this.buttonCopyFromOrigin.TabIndex                =  18;
      this.buttonCopyFromOrigin.Text                    =  "Copy to Clipboard";
      this.buttonCopyFromOrigin.UseVisualStyleBackColor =  true;
      this.buttonCopyFromOrigin.Click                   += new System.EventHandler(this.buttonCopyFromOrigin_Click);

      // 
      // label3
      // 
      this.label3.Location  = new System.Drawing.Point(12, 94);
      this.label3.Name      = "label3";
      this.label3.Size      = new System.Drawing.Size(110, 21);
      this.label3.TabIndex  = 20;
      this.label3.Text      = "Previous XML:";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      // 
      // label4
      // 
      this.label4.Font      = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location  = new System.Drawing.Point(147, 68);
      this.label4.Name      = "label4";
      this.label4.Size      = new System.Drawing.Size(185, 21);
      this.label4.TabIndex  = 22;
      this.label4.Text      = "Origin Mod Files";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      // 
      // label5
      // 
      this.label5.Font      = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location  = new System.Drawing.Point(147, 154);
      this.label5.Name      = "label5";
      this.label5.Size      = new System.Drawing.Size(185, 21);
      this.label5.TabIndex  = 23;
      this.label5.Text      = "Translated Mod File";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      // 
      // buttonCopyFromPrevious
      // 
      this.buttonCopyFromPrevious.Anchor                  =  ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonCopyFromPrevious.Location                =  new System.Drawing.Point(12, 501);
      this.buttonCopyFromPrevious.Name                    =  "buttonCopyFromPrevious";
      this.buttonCopyFromPrevious.Size                    =  new System.Drawing.Size(129, 38);
      this.buttonCopyFromPrevious.TabIndex                =  27;
      this.buttonCopyFromPrevious.Text                    =  "Copy to Clipboard";
      this.buttonCopyFromPrevious.UseVisualStyleBackColor =  true;
      this.buttonCopyFromPrevious.Click                   += new System.EventHandler(this.buttonCopyFromPrevious_Click);

      // 
      // label6
      // 
      this.label6.Anchor    = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label6.Location  = new System.Drawing.Point(12, 455);
      this.label6.Name      = "label6";
      this.label6.Size      = new System.Drawing.Size(129, 21);
      this.label6.TabIndex  = 26;
      this.label6.Text      = "Previous Reference Text:";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      // 
      // textBoxPreviousOriginText
      // 
      this.textBoxPreviousOriginText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                                    | System.Windows.Forms.AnchorStyles.Right)));

      this.textBoxPreviousOriginText.Font       = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBoxPreviousOriginText.Location   = new System.Drawing.Point(147, 455);
      this.textBoxPreviousOriginText.Multiline  = true;
      this.textBoxPreviousOriginText.Name       = "textBoxPreviousOriginText";
      this.textBoxPreviousOriginText.ReadOnly   = true;
      this.textBoxPreviousOriginText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.textBoxPreviousOriginText.Size       = new System.Drawing.Size(725, 80);
      this.textBoxPreviousOriginText.TabIndex   = 25;

      // 
      // textBoxFilter
      // 
      this.textBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                        | System.Windows.Forms.AnchorStyles.Right)));

      this.textBoxFilter.Location    =  new System.Drawing.Point(215, 221);
      this.textBoxFilter.Name        =  "textBoxFilter";
      this.textBoxFilter.Size        =  new System.Drawing.Size(657, 20);
      this.textBoxFilter.TabIndex    =  28;
      this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);

      // 
      // label7
      // 
      this.label7.Location  = new System.Drawing.Point(147, 219);
      this.label7.Name      = "label7";
      this.label7.Size      = new System.Drawing.Size(68, 23);
      this.label7.TabIndex  = 29;
      this.label7.Text      = "Quick Filter:";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      // 
      // checkBoxAutoClipboard
      // 
      this.checkBoxAutoClipboard.Checked                 =  true;
      this.checkBoxAutoClipboard.CheckState              =  System.Windows.Forms.CheckState.Checked;
      this.checkBoxAutoClipboard.Location                =  new System.Drawing.Point(12, 247);
      this.checkBoxAutoClipboard.Name                    =  "checkBoxAutoClipboard";
      this.checkBoxAutoClipboard.Size                    =  new System.Drawing.Size(129, 19);
      this.checkBoxAutoClipboard.TabIndex                =  30;
      this.checkBoxAutoClipboard.Text                    =  "Auto Clipboard";
      this.checkBoxAutoClipboard.UseVisualStyleBackColor =  true;
      this.checkBoxAutoClipboard.CheckedChanged          += new System.EventHandler(this.checkBoxAutoClipboard_CheckedChanged);

      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.dateiToolStripMenuItem });
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name     = "menuStrip1";
      this.menuStrip1.Size     = new System.Drawing.Size(884, 24);
      this.menuStrip1.TabIndex = 32;
      this.menuStrip1.Text     = "menuStrip1";

      // 
      // dateiToolStripMenuItem
      // 
      this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.settingsToolStripMenuItem, this.importModToolStripMenuItem, this.loadSourceXMLToolStripMenuItem, this.saveSourceXMLToolStripMenuItem, this.packingModToolStripMenuItem, this.exitToolStripMenuItem });
      this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
      this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
      this.dateiToolStripMenuItem.Text = "Datei";

      // 
      // settingsToolStripMenuItem
      // 
      this.settingsToolStripMenuItem.Name         =  "settingsToolStripMenuItem";
      this.settingsToolStripMenuItem.ShortcutKeys =  ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
      this.settingsToolStripMenuItem.Size         =  new System.Drawing.Size(208, 22);
      this.settingsToolStripMenuItem.Text         =  "Settings...";
      this.settingsToolStripMenuItem.Click        += new System.EventHandler(this.settingsToolStripMenuItem_Click);

      // 
      // importModToolStripMenuItem
      // 
      this.importModToolStripMenuItem.Name         =  "importModToolStripMenuItem";
      this.importModToolStripMenuItem.ShortcutKeys =  ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
      this.importModToolStripMenuItem.Size         =  new System.Drawing.Size(208, 22);
      this.importModToolStripMenuItem.Text         =  "Import Mod...";
      this.importModToolStripMenuItem.Click        += new System.EventHandler(this.importModToolStripMenuItem_Click);

      // 
      // loadSourceXMLToolStripMenuItem
      // 
      this.loadSourceXMLToolStripMenuItem.Name         =  "loadSourceXMLToolStripMenuItem";
      this.loadSourceXMLToolStripMenuItem.ShortcutKeys =  ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
      this.loadSourceXMLToolStripMenuItem.Size         =  new System.Drawing.Size(208, 22);
      this.loadSourceXMLToolStripMenuItem.Text         =  "Load Source XML";
      this.loadSourceXMLToolStripMenuItem.Click        += new System.EventHandler(this.loadSourceXMLToolStripMenuItem_Click);

      // 
      // saveSourceXMLToolStripMenuItem
      // 
      this.saveSourceXMLToolStripMenuItem.Name         =  "saveSourceXMLToolStripMenuItem";
      this.saveSourceXMLToolStripMenuItem.ShortcutKeys =  ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.saveSourceXMLToolStripMenuItem.Size         =  new System.Drawing.Size(208, 22);
      this.saveSourceXMLToolStripMenuItem.Text         =  "Save Source XML";
      this.saveSourceXMLToolStripMenuItem.Click        += new System.EventHandler(this.saveSourceXMLToolStripMenuItem_Click);

      // 
      // packingModToolStripMenuItem
      // 
      this.packingModToolStripMenuItem.Name         =  "packingModToolStripMenuItem";
      this.packingModToolStripMenuItem.ShortcutKeys =  ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
      this.packingModToolStripMenuItem.Size         =  new System.Drawing.Size(208, 22);
      this.packingModToolStripMenuItem.Text         =  "Packing Mod";
      this.packingModToolStripMenuItem.Click        += new System.EventHandler(this.packingModToolStripMenuItem_Click);

      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name         =  "exitToolStripMenuItem";
      this.exitToolStripMenuItem.ShortcutKeys =  ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
      this.exitToolStripMenuItem.Size         =  new System.Drawing.Size(208, 22);
      this.exitToolStripMenuItem.Text         =  "Exit";
      this.exitToolStripMenuItem.Click        += new System.EventHandler(this.exitToolStripMenuItem_Click);

      // 
      // label8
      // 
      this.label8.Location  = new System.Drawing.Point(12, 29);
      this.label8.Name      = "label8";
      this.label8.Size      = new System.Drawing.Size(129, 35);
      this.label8.TabIndex  = 33;
      this.label8.Text      = "Select Translation Mod:";
      this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

      // 
      // comboBoxMods
      // 
      this.comboBoxMods.DropDownStyle        =  System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBoxMods.FormattingEnabled    =  true;
      this.comboBoxMods.Location             =  new System.Drawing.Point(147, 37);
      this.comboBoxMods.Name                 =  "comboBoxMods";
      this.comboBoxMods.Size                 =  new System.Drawing.Size(261, 21);
      this.comboBoxMods.TabIndex             =  34;
      this.comboBoxMods.SelectedValueChanged += new System.EventHandler(this.comboBoxMods_SelectedValueChanged);

      // 
      // buttonRefreshMods
      // 
      this.buttonRefreshMods.Location                =  new System.Drawing.Point(417, 35);
      this.buttonRefreshMods.Name                    =  "buttonRefreshMods";
      this.buttonRefreshMods.Size                    =  new System.Drawing.Size(60, 23);
      this.buttonRefreshMods.TabIndex                =  35;
      this.buttonRefreshMods.Text                    =  "Refresh";
      this.buttonRefreshMods.UseVisualStyleBackColor =  true;
      this.buttonRefreshMods.Click                   += new System.EventHandler(this.buttonRefreshMods_Click);

      // 
      // comboBoxOriginPreviousFile
      // 
      this.comboBoxOriginPreviousFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                     | System.Windows.Forms.AnchorStyles.Right)));

      this.comboBoxOriginPreviousFile.ContextMenuStrip     =  this.contextMenuStripFileBox;
      this.comboBoxOriginPreviousFile.DropDownStyle        =  System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBoxOriginPreviousFile.FormattingEnabled    =  true;
      this.comboBoxOriginPreviousFile.Location             =  new System.Drawing.Point(147, 94);
      this.comboBoxOriginPreviousFile.Name                 =  "comboBoxOriginPreviousFile";
      this.comboBoxOriginPreviousFile.Size                 =  new System.Drawing.Size(722, 21);
      this.comboBoxOriginPreviousFile.TabIndex             =  36;
      this.comboBoxOriginPreviousFile.SelectedValueChanged += new System.EventHandler(this.comboBoxOriginPreviousFile_SelectedValueChanged);

      // 
      // contextMenuStripFileBox
      // 
      this.contextMenuStripFileBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.renameFileToolStripMenuItem, this.copyFileToolStripMenuItem, this.copyFileToOtherLanguageToolStripMenuItem, this.deleteFileToolStripMenuItem });
      this.contextMenuStripFileBox.Name = "contextMenuStripFileBox";
      this.contextMenuStripFileBox.Size = new System.Drawing.Size(224, 92);

      // 
      // renameFileToolStripMenuItem
      // 
      this.renameFileToolStripMenuItem.Name  =  "renameFileToolStripMenuItem";
      this.renameFileToolStripMenuItem.Size  =  new System.Drawing.Size(223, 22);
      this.renameFileToolStripMenuItem.Text  =  "Rename file";
      this.renameFileToolStripMenuItem.Click += new System.EventHandler(this.renameFileToolStripMenuItem_Click);

      // 
      // copyFileToolStripMenuItem
      // 
      this.copyFileToolStripMenuItem.Name  =  "copyFileToolStripMenuItem";
      this.copyFileToolStripMenuItem.Size  =  new System.Drawing.Size(223, 22);
      this.copyFileToolStripMenuItem.Text  =  "Copy File";
      this.copyFileToolStripMenuItem.Click += new System.EventHandler(this.copyFileToolStripMenuItem_Click);

      // 
      // copyFileToOtherLanguageToolStripMenuItem
      // 
      this.copyFileToOtherLanguageToolStripMenuItem.Name  =  "copyFileToOtherLanguageToolStripMenuItem";
      this.copyFileToOtherLanguageToolStripMenuItem.Size  =  new System.Drawing.Size(223, 22);
      this.copyFileToOtherLanguageToolStripMenuItem.Text  =  "Copy File to other Language";
      this.copyFileToOtherLanguageToolStripMenuItem.Click += new System.EventHandler(this.copyFileToOtherLanguageToolStripMenuItem_Click);

      // 
      // deleteFileToolStripMenuItem
      // 
      this.deleteFileToolStripMenuItem.Name  =  "deleteFileToolStripMenuItem";
      this.deleteFileToolStripMenuItem.Size  =  new System.Drawing.Size(223, 22);
      this.deleteFileToolStripMenuItem.Text  =  "Delete File";
      this.deleteFileToolStripMenuItem.Click += new System.EventHandler(this.deleteFileToolStripMenuItem_Click);

      // 
      // comboBoxOriginCurrentFile
      // 
      this.comboBoxOriginCurrentFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                    | System.Windows.Forms.AnchorStyles.Right)));

      this.comboBoxOriginCurrentFile.ContextMenuStrip     =  this.contextMenuStripFileBox;
      this.comboBoxOriginCurrentFile.DropDownStyle        =  System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBoxOriginCurrentFile.FormattingEnabled    =  true;
      this.comboBoxOriginCurrentFile.Location             =  new System.Drawing.Point(147, 121);
      this.comboBoxOriginCurrentFile.Name                 =  "comboBoxOriginCurrentFile";
      this.comboBoxOriginCurrentFile.Size                 =  new System.Drawing.Size(722, 21);
      this.comboBoxOriginCurrentFile.TabIndex             =  37;
      this.comboBoxOriginCurrentFile.SelectedValueChanged += new System.EventHandler(this.comboBoxOriginCurrentFile_SelectedValueChanged);

      // 
      // comboBoxTranslatedFile
      // 
      this.comboBoxTranslatedFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                 | System.Windows.Forms.AnchorStyles.Right)));

      this.comboBoxTranslatedFile.ContextMenuStrip     =  this.contextMenuStripFileBox;
      this.comboBoxTranslatedFile.DropDownStyle        =  System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBoxTranslatedFile.FormattingEnabled    =  true;
      this.comboBoxTranslatedFile.Location             =  new System.Drawing.Point(147, 178);
      this.comboBoxTranslatedFile.Name                 =  "comboBoxTranslatedFile";
      this.comboBoxTranslatedFile.Size                 =  new System.Drawing.Size(722, 21);
      this.comboBoxTranslatedFile.TabIndex             =  38;
      this.comboBoxTranslatedFile.SelectedValueChanged += new System.EventHandler(this.comboBoxTranslatedFile_SelectedValueChanged);

      // 
      // toolStripButton1
      // 
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
      this.toolStripButton1.Text = "toolStripButton1";

      // 
      // toolStripButton2
      // 
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new System.Drawing.Size(23, 23);
      this.toolStripButton2.Text = "toolStripButton2";

      // 
      // toolStripComboBox1
      // 
      this.toolStripComboBox1.Name = "toolStripComboBox1";
      this.toolStripComboBox1.Size = new System.Drawing.Size(121, 21);

      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize          = new System.Drawing.Size(884, 840);
      this.Controls.Add(this.comboBoxTranslatedFile);
      this.Controls.Add(this.comboBoxOriginCurrentFile);
      this.Controls.Add(this.comboBoxOriginPreviousFile);
      this.Controls.Add(this.buttonRefreshMods);
      this.Controls.Add(this.comboBoxMods);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.checkBoxAutoClipboard);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.textBoxFilter);
      this.Controls.Add(this.buttonCopyFromPrevious);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.textBoxPreviousOriginText);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.buttonCopyFromOrigin);
      this.Controls.Add(this.buttonPasteToTranslated);
      this.Controls.Add(this.dataGridViewSource);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBoxCurrentOriginText);
      this.Controls.Add(this.textBoxTranslatedText);
      this.Controls.Add(this.labelReference);
      this.Controls.Add(this.labelSource);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip =  this.menuStrip1;
      this.MinimumSize   =  new System.Drawing.Size(900, 800);
      this.Name          =  "FormMain";
      this.StartPosition =  System.Windows.Forms.FormStartPosition.Manual;
      this.Text          =  "BG3 Loca Helper";
      this.ResizeEnd     += new System.EventHandler(this.FormMain_ResizeEnd);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSource)).EndInit();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.contextMenuStripFileBox.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private System.Windows.Forms.ContextMenuStrip contextMenuStripFileBox;

    private System.Windows.Forms.ToolStripMenuItem renameFileToolStripMenuItem;

    private System.Windows.Forms.ToolStripMenuItem copyFileToolStripMenuItem;

    private System.Windows.Forms.ToolStripMenuItem deleteFileToolStripMenuItem;

    private System.Windows.Forms.ToolStripMenuItem copyFileToOtherLanguageToolStripMenuItem;

    private System.Windows.Forms.ToolStripButton toolStripButton2;

    private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;

    private System.Windows.Forms.ToolStripMenuItem importModToolStripMenuItem;

    private System.Windows.Forms.ToolStripButton toolStripButton1;

    private System.Windows.Forms.ComboBox comboBoxOriginPreviousFile;

    private System.Windows.Forms.ComboBox comboBoxOriginCurrentFile;

    private System.Windows.Forms.ComboBox comboBoxTranslatedFile;

    private System.Windows.Forms.Label label8;

    private System.Windows.Forms.ComboBox comboBoxMods;

    private System.Windows.Forms.Button buttonRefreshMods;

    private System.Windows.Forms.MenuStrip menuStrip1;

    private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;

    private System.Windows.Forms.ToolStripMenuItem loadSourceXMLToolStripMenuItem;

    private System.Windows.Forms.ToolStripMenuItem saveSourceXMLToolStripMenuItem;

    private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;

    private System.Windows.Forms.ToolStripMenuItem packingModToolStripMenuItem;

    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

    private System.Windows.Forms.CheckBox checkBoxAutoClipboard;

    private System.Windows.Forms.TextBox textBoxFilter;

    private System.Windows.Forms.Label label7;

    private System.Windows.Forms.Button buttonCopyFromPrevious;

    private System.Windows.Forms.Label label6;

    private System.Windows.Forms.TextBox textBoxPreviousOriginText;

    private System.Windows.Forms.Label label5;

    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.Label label4;

    private System.Windows.Forms.Button buttonCopyFromOrigin;

    private System.Windows.Forms.Button buttonPasteToTranslated;

    private System.Windows.Forms.DataGridView dataGridViewSource;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.TextBox textBoxCurrentOriginText;

    private System.Windows.Forms.TextBox textBoxTranslatedText;

    private System.Windows.Forms.Label labelReference;

    private System.Windows.Forms.Label labelSource;

    #endregion
  }
}