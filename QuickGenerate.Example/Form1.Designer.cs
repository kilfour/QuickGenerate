namespace QuickGenerate.Example
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
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.detailsGrid = new System.Windows.Forms.DataGridView();
            this.masterGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.detailsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.masterGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // detailsGrid
            // 
            this.detailsGrid.AllowUserToAddRows = false;
            this.detailsGrid.AllowUserToDeleteRows = false;
            this.detailsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                              | System.Windows.Forms.AnchorStyles.Left)
                                                                             | System.Windows.Forms.AnchorStyles.Right)));
            this.detailsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.detailsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.detailsGrid.Location = new System.Drawing.Point(3, 212);
            this.detailsGrid.Name = "detailsGrid";
            this.detailsGrid.ReadOnly = true;
            this.detailsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.detailsGrid.Size = new System.Drawing.Size(912, 248);
            this.detailsGrid.TabIndex = 1;
            // 
            // masterGrid
            // 
            this.masterGrid.AllowUserToAddRows = false;
            this.masterGrid.AllowUserToDeleteRows = false;
            this.masterGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                               | System.Windows.Forms.AnchorStyles.Right)));
            this.masterGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.masterGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.masterGrid.Location = new System.Drawing.Point(3, 1);
            this.masterGrid.Name = "masterGrid";
            this.masterGrid.ReadOnly = true;
            this.masterGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.masterGrid.Size = new System.Drawing.Size(912, 205);
            this.masterGrid.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 461);
            this.Controls.Add(this.masterGrid);
            this.Controls.Add(this.detailsGrid);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.detailsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.masterGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView detailsGrid;
        private System.Windows.Forms.DataGridView masterGrid;
    }
}