
namespace SCBPVD
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txb_file_name = new System.Windows.Forms.TextBox();
            this.txb_folder_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_selct_file = new System.Windows.Forms.Button();
            this.btn_selct_folder = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.JobId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Create_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txb_file_name
            // 
            this.txb_file_name.Location = new System.Drawing.Point(159, 59);
            this.txb_file_name.Name = "txb_file_name";
            this.txb_file_name.Size = new System.Drawing.Size(347, 23);
            this.txb_file_name.TabIndex = 0;
            // 
            // txb_folder_name
            // 
            this.txb_folder_name.Location = new System.Drawing.Point(159, 96);
            this.txb_folder_name.Name = "txb_folder_name";
            this.txb_folder_name.Size = new System.Drawing.Size(347, 23);
            this.txb_folder_name.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "ไฟล์";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Path ไฟล์";
            // 
            // btn_selct_file
            // 
            this.btn_selct_file.Location = new System.Drawing.Point(533, 59);
            this.btn_selct_file.Name = "btn_selct_file";
            this.btn_selct_file.Size = new System.Drawing.Size(75, 23);
            this.btn_selct_file.TabIndex = 4;
            this.btn_selct_file.Text = "เลือกไฟล์";
            this.btn_selct_file.UseVisualStyleBackColor = true;
            this.btn_selct_file.Click += new System.EventHandler(this.btn_selct_file_Click);
            // 
            // btn_selct_folder
            // 
            this.btn_selct_folder.Location = new System.Drawing.Point(533, 96);
            this.btn_selct_folder.Name = "btn_selct_folder";
            this.btn_selct_folder.Size = new System.Drawing.Size(75, 23);
            this.btn_selct_folder.TabIndex = 5;
            this.btn_selct_folder.Text = "เลือกโฟลเดอร์";
            this.btn_selct_folder.UseVisualStyleBackColor = true;
            this.btn_selct_folder.Click += new System.EventHandler(this.btn_selct_folder_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(533, 136);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 6;
            this.btn_save.Text = "บันทึก";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JobId,
            this.Create_date,
            this.Total,
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(42, 210);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(629, 202);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // JobId
            // 
            this.JobId.DataPropertyName = "id";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.RoyalBlue;
            this.JobId.DefaultCellStyle = dataGridViewCellStyle1;
            this.JobId.HeaderText = "Job Id";
            this.JobId.Name = "JobId";
            // 
            // Create_date
            // 
            this.Create_date.DataPropertyName = "create_date";
            this.Create_date.HeaderText = "Create date";
            this.Create_date.Name = "Create_date";
            // 
            // Total
            // 
            this.Total.DataPropertyName = "total_company";
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_selct_folder);
            this.Controls.Add(this.btn_selct_file);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_folder_name);
            this.Controls.Add(this.txb_file_name);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_LoadAsync);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txb_file_name;
        private System.Windows.Forms.TextBox txb_folder_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_selct_file;
        private System.Windows.Forms.Button btn_selct_folder;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Create_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}

