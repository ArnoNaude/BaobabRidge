namespace BaobabRidge
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAnimalId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtSpecies = new System.Windows.Forms.TextBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.txtRecoveryScore = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvAnimals = new System.Windows.Forms.DataGridView();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimals)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 334);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Animal ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 365);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 396);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Species:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 425);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Age (years):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 452);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Recovery Score:";
            // 
            // txtAnimalId
            // 
            this.txtAnimalId.Location = new System.Drawing.Point(74, 330);
            this.txtAnimalId.Margin = new System.Windows.Forms.Padding(2);
            this.txtAnimalId.Name = "txtAnimalId";
            this.txtAnimalId.Size = new System.Drawing.Size(117, 20);
            this.txtAnimalId.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(57, 363);
            this.txtName.Margin = new System.Windows.Forms.Padding(2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(112, 20);
            this.txtName.TabIndex = 6;
            // 
            // txtSpecies
            // 
            this.txtSpecies.Location = new System.Drawing.Point(74, 396);
            this.txtSpecies.Margin = new System.Windows.Forms.Padding(2);
            this.txtSpecies.Name = "txtSpecies";
            this.txtSpecies.Size = new System.Drawing.Size(117, 20);
            this.txtSpecies.TabIndex = 7;
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(74, 425);
            this.txtAge.Margin = new System.Windows.Forms.Padding(2);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(43, 20);
            this.txtAge.TabIndex = 8;
            // 
            // txtRecoveryScore
            // 
            this.txtRecoveryScore.Location = new System.Drawing.Point(101, 450);
            this.txtRecoveryScore.Margin = new System.Windows.Forms.Padding(2);
            this.txtRecoveryScore.Name = "txtRecoveryScore";
            this.txtRecoveryScore.Size = new System.Drawing.Size(73, 20);
            this.txtRecoveryScore.TabIndex = 9;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(74, 491);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(50, 23);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvAnimals
            // 
            this.dgvAnimals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnimals.Location = new System.Drawing.Point(1, 2);
            this.dgvAnimals.Margin = new System.Windows.Forms.Padding(2);
            this.dgvAnimals.Name = "dgvAnimals";
            this.dgvAnimals.RowHeadersWidth = 62;
            this.dgvAnimals.RowTemplate.Height = 28;
            this.dgvAnimals.Size = new System.Drawing.Size(615, 311);
            this.dgvAnimals.TabIndex = 11;
            this.dgvAnimals.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAnimals_CellClick);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(228, 334);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(106, 13);
            this.lblSearch.TabIndex = 12;
            this.lblSearch.Text = "Search by Animal ID:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(340, 331);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 13;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(355, 357);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 23);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Enabled = false;
            this.btnSaveChanges.Location = new System.Drawing.Point(139, 491);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(89, 23);
            this.btnSaveChanges.TabIndex = 15;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(248, 491);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(340, 491);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 567);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.dgvAnimals);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtRecoveryScore);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.txtSpecies);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtAnimalId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAnimalId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtSpecies;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.TextBox txtRecoveryScore;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvAnimals;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
    }
}

