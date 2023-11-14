namespace WindowsFormsPrototype
{
    partial class Form2
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
            this.fileName = new System.Windows.Forms.Label();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonRight = new System.Windows.Forms.Button();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.typeOfDocumentLabel = new System.Windows.Forms.Label();
            this.textBoxTypeOfDocument = new System.Windows.Forms.TextBox();
            this.companyLabel = new System.Windows.Forms.Label();
            this.textBoxCompany = new System.Windows.Forms.TextBox();
            this.buttonChangeCompany = new System.Windows.Forms.Button();
            this.addressLabel = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.buttonChangeAddress = new System.Windows.Forms.Button();
            this.buttonSaveFileChanges = new System.Windows.Forms.Button();
            this.itemsLabel = new System.Windows.Forms.Label();
            this.textBoxItems = new System.Windows.Forms.TextBox();
            this.buttonChangeItems = new System.Windows.Forms.Button();
            this.signatureLabel = new System.Windows.Forms.Label();
            this.pictureBoxSignature = new System.Windows.Forms.PictureBox();
            this.buttonSetSignature = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonEditPhoto = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSignature)).BeginInit();
            this.SuspendLayout();
            // 
            // fileName
            // 
            this.fileName.AutoSize = true;
            this.fileName.Location = new System.Drawing.Point(451, 17);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(66, 16);
            this.fileName.TabIndex = 0;
            this.fileName.Text = "FileName";
            // 
            // buttonLeft
            // 
            this.buttonLeft.Location = new System.Drawing.Point(282, 12);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(82, 26);
            this.buttonLeft.TabIndex = 1;
            this.buttonLeft.Text = "<<";
            this.buttonLeft.UseVisualStyleBackColor = true;
            // 
            // buttonRight
            // 
            this.buttonRight.Location = new System.Drawing.Point(595, 12);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(82, 26);
            this.buttonRight.TabIndex = 2;
            this.buttonRight.Text = ">>";
            this.buttonRight.UseVisualStyleBackColor = true;
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(12, 410);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(776, 28);
            this.buttonConfirm.TabIndex = 3;
            this.buttonConfirm.Text = "Confirm all";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(683, 11);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(105, 26);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "Drop file";
            this.buttonDelete.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(282, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(506, 335);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // typeOfDocumentLabel
            // 
            this.typeOfDocumentLabel.AutoSize = true;
            this.typeOfDocumentLabel.Location = new System.Drawing.Point(13, 57);
            this.typeOfDocumentLabel.Name = "typeOfDocumentLabel";
            this.typeOfDocumentLabel.Size = new System.Drawing.Size(118, 16);
            this.typeOfDocumentLabel.TabIndex = 6;
            this.typeOfDocumentLabel.Text = "Type of document:";
            // 
            // textBoxTypeOfDocument
            // 
            this.textBoxTypeOfDocument.Location = new System.Drawing.Point(137, 57);
            this.textBoxTypeOfDocument.Name = "textBoxTypeOfDocument";
            this.textBoxTypeOfDocument.Size = new System.Drawing.Size(140, 22);
            this.textBoxTypeOfDocument.TabIndex = 7;
            // 
            // companyLabel
            // 
            this.companyLabel.AutoSize = true;
            this.companyLabel.Location = new System.Drawing.Point(13, 94);
            this.companyLabel.Name = "companyLabel";
            this.companyLabel.Size = new System.Drawing.Size(68, 16);
            this.companyLabel.TabIndex = 8;
            this.companyLabel.Text = "Company:";
            // 
            // textBoxCompany
            // 
            this.textBoxCompany.Location = new System.Drawing.Point(137, 94);
            this.textBoxCompany.Name = "textBoxCompany";
            this.textBoxCompany.Size = new System.Drawing.Size(100, 22);
            this.textBoxCompany.TabIndex = 9;
            // 
            // buttonChangeCompany
            // 
            this.buttonChangeCompany.Location = new System.Drawing.Point(243, 94);
            this.buttonChangeCompany.Name = "buttonChangeCompany";
            this.buttonChangeCompany.Size = new System.Drawing.Size(33, 21);
            this.buttonChangeCompany.TabIndex = 10;
            this.buttonChangeCompany.Text = "set";
            this.buttonChangeCompany.UseVisualStyleBackColor = true;
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Location = new System.Drawing.Point(13, 131);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(61, 16);
            this.addressLabel.TabIndex = 11;
            this.addressLabel.Text = "Address:";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(137, 131);
            this.textBoxAddress.Multiline = true;
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(100, 60);
            this.textBoxAddress.TabIndex = 12;
            // 
            // buttonChangeAddress
            // 
            this.buttonChangeAddress.Location = new System.Drawing.Point(243, 132);
            this.buttonChangeAddress.Name = "buttonChangeAddress";
            this.buttonChangeAddress.Size = new System.Drawing.Size(33, 21);
            this.buttonChangeAddress.TabIndex = 13;
            this.buttonChangeAddress.Text = "set";
            this.buttonChangeAddress.UseVisualStyleBackColor = true;
            // 
            // buttonSaveFileChanges
            // 
            this.buttonSaveFileChanges.Location = new System.Drawing.Point(13, 357);
            this.buttonSaveFileChanges.Name = "buttonSaveFileChanges";
            this.buttonSaveFileChanges.Size = new System.Drawing.Size(263, 35);
            this.buttonSaveFileChanges.TabIndex = 14;
            this.buttonSaveFileChanges.Text = "Save";
            this.buttonSaveFileChanges.UseVisualStyleBackColor = true;
            // 
            // itemsLabel
            // 
            this.itemsLabel.AutoSize = true;
            this.itemsLabel.Location = new System.Drawing.Point(13, 197);
            this.itemsLabel.Name = "itemsLabel";
            this.itemsLabel.Size = new System.Drawing.Size(42, 16);
            this.itemsLabel.TabIndex = 15;
            this.itemsLabel.Text = "Items:";
            // 
            // textBoxItems
            // 
            this.textBoxItems.Location = new System.Drawing.Point(137, 197);
            this.textBoxItems.Multiline = true;
            this.textBoxItems.Name = "textBoxItems";
            this.textBoxItems.Size = new System.Drawing.Size(100, 68);
            this.textBoxItems.TabIndex = 16;
            // 
            // buttonChangeItems
            // 
            this.buttonChangeItems.Location = new System.Drawing.Point(244, 198);
            this.buttonChangeItems.Name = "buttonChangeItems";
            this.buttonChangeItems.Size = new System.Drawing.Size(33, 21);
            this.buttonChangeItems.TabIndex = 17;
            this.buttonChangeItems.Text = "set";
            this.buttonChangeItems.UseVisualStyleBackColor = true;
            // 
            // signatureLabel
            // 
            this.signatureLabel.AutoSize = true;
            this.signatureLabel.Location = new System.Drawing.Point(13, 276);
            this.signatureLabel.Name = "signatureLabel";
            this.signatureLabel.Size = new System.Drawing.Size(67, 16);
            this.signatureLabel.TabIndex = 18;
            this.signatureLabel.Text = "Signature:";
            // 
            // pictureBoxSignature
            // 
            this.pictureBoxSignature.Location = new System.Drawing.Point(137, 271);
            this.pictureBoxSignature.Name = "pictureBoxSignature";
            this.pictureBoxSignature.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxSignature.TabIndex = 19;
            this.pictureBoxSignature.TabStop = false;
            // 
            // buttonSetSignature
            // 
            this.buttonSetSignature.Location = new System.Drawing.Point(243, 271);
            this.buttonSetSignature.Name = "buttonSetSignature";
            this.buttonSetSignature.Size = new System.Drawing.Size(33, 21);
            this.buttonSetSignature.TabIndex = 20;
            this.buttonSetSignature.Text = "set";
            this.buttonSetSignature.UseVisualStyleBackColor = true;
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(15, 17);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(82, 26);
            this.buttonBack.TabIndex = 21;
            this.buttonBack.Text = "<- Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            // 
            // buttonEditPhoto
            // 
            this.buttonEditPhoto.Location = new System.Drawing.Point(746, 56);
            this.buttonEditPhoto.Name = "buttonEditPhoto";
            this.buttonEditPhoto.Size = new System.Drawing.Size(42, 23);
            this.buttonEditPhoto.TabIndex = 22;
            this.buttonEditPhoto.Text = "Edit";
            this.buttonEditPhoto.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonEditPhoto);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonSetSignature);
            this.Controls.Add(this.pictureBoxSignature);
            this.Controls.Add(this.signatureLabel);
            this.Controls.Add(this.buttonChangeItems);
            this.Controls.Add(this.textBoxItems);
            this.Controls.Add(this.itemsLabel);
            this.Controls.Add(this.buttonSaveFileChanges);
            this.Controls.Add(this.buttonChangeAddress);
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.addressLabel);
            this.Controls.Add(this.buttonChangeCompany);
            this.Controls.Add(this.textBoxCompany);
            this.Controls.Add(this.companyLabel);
            this.Controls.Add(this.textBoxTypeOfDocument);
            this.Controls.Add(this.typeOfDocumentLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.buttonRight);
            this.Controls.Add(this.buttonLeft);
            this.Controls.Add(this.fileName);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSignature)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label fileName;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label typeOfDocumentLabel;
        private System.Windows.Forms.TextBox textBoxTypeOfDocument;
        private System.Windows.Forms.Label companyLabel;
        private System.Windows.Forms.TextBox textBoxCompany;
        private System.Windows.Forms.Button buttonChangeCompany;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Button buttonChangeAddress;
        private System.Windows.Forms.Button buttonSaveFileChanges;
        private System.Windows.Forms.Label itemsLabel;
        private System.Windows.Forms.TextBox textBoxItems;
        private System.Windows.Forms.Button buttonChangeItems;
        private System.Windows.Forms.Label signatureLabel;
        private System.Windows.Forms.PictureBox pictureBoxSignature;
        private System.Windows.Forms.Button buttonSetSignature;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonEditPhoto;
    }
}