
namespace SCBPVD
{
    partial class SendEmail
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
            this.lb_total = new System.Windows.Forms.Label();
            this.progressBar_send_email = new System.Windows.Forms.ProgressBar();
            this.btn_send_email = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_send = new System.Windows.Forms.Label();
            this.lb_job = new System.Windows.Forms.Label();
            this.txb_file_name = new System.Windows.Forms.TextBox();
            this.btn_read_log = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_create_report = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "จำนวนทั้งหมด";
            // 
            // lb_total
            // 
            this.lb_total.AutoSize = true;
            this.lb_total.Location = new System.Drawing.Point(198, 63);
            this.lb_total.Name = "lb_total";
            this.lb_total.Size = new System.Drawing.Size(18, 29);
            this.lb_total.TabIndex = 1;
            this.lb_total.Text = "-";
            // 
            // progressBar_send_email
            // 
            this.progressBar_send_email.Location = new System.Drawing.Point(42, 118);
            this.progressBar_send_email.Name = "progressBar_send_email";
            this.progressBar_send_email.Size = new System.Drawing.Size(524, 37);
            this.progressBar_send_email.TabIndex = 2;
            // 
            // btn_send_email
            // 
            this.btn_send_email.Location = new System.Drawing.Point(582, 118);
            this.btn_send_email.Name = "btn_send_email";
            this.btn_send_email.Size = new System.Drawing.Size(93, 37);
            this.btn_send_email.TabIndex = 3;
            this.btn_send_email.Text = "Send Email";
            this.btn_send_email.UseVisualStyleBackColor = true;
            this.btn_send_email.Click += new System.EventHandler(this.btn_send_email_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(314, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "จำนวนที่ส่งแล้ว";
            // 
            // lb_send
            // 
            this.lb_send.AutoSize = true;
            this.lb_send.Location = new System.Drawing.Point(459, 63);
            this.lb_send.Name = "lb_send";
            this.lb_send.Size = new System.Drawing.Size(18, 29);
            this.lb_send.TabIndex = 5;
            this.lb_send.Text = "-";
            // 
            // lb_job
            // 
            this.lb_job.AutoSize = true;
            this.lb_job.Location = new System.Drawing.Point(42, 21);
            this.lb_job.Name = "lb_job";
            this.lb_job.Size = new System.Drawing.Size(96, 29);
            this.lb_job.TabIndex = 6;
            this.lb_job.Text = "จำนวนทั้งหมด";
            // 
            // txb_file_name
            // 
            this.txb_file_name.Location = new System.Drawing.Point(42, 183);
            this.txb_file_name.Name = "txb_file_name";
            this.txb_file_name.Size = new System.Drawing.Size(524, 37);
            this.txb_file_name.TabIndex = 7;
            // 
            // btn_read_log
            // 
            this.btn_read_log.Location = new System.Drawing.Point(582, 183);
            this.btn_read_log.Name = "btn_read_log";
            this.btn_read_log.Size = new System.Drawing.Size(93, 37);
            this.btn_read_log.TabIndex = 8;
            this.btn_read_log.Text = "Select Log";
            this.btn_read_log.UseVisualStyleBackColor = true;
            this.btn_read_log.Click += new System.EventHandler(this.btn_read_log_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(582, 229);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(93, 39);
            this.btn_save.TabIndex = 9;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_create_report
            // 
            this.btn_create_report.Location = new System.Drawing.Point(582, 287);
            this.btn_create_report.Name = "btn_create_report";
            this.btn_create_report.Size = new System.Drawing.Size(93, 36);
            this.btn_create_report.TabIndex = 10;
            this.btn_create_report.Text = "Create report";
            this.btn_create_report.UseVisualStyleBackColor = true;
            this.btn_create_report.Click += new System.EventHandler(this.btn_create_report_Click);
            // 
            // SendEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 362);
            this.Controls.Add(this.btn_create_report);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_read_log);
            this.Controls.Add(this.txb_file_name);
            this.Controls.Add(this.lb_job);
            this.Controls.Add(this.lb_send);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_send_email);
            this.Controls.Add(this.progressBar_send_email);
            this.Controls.Add(this.lb_total);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Cordia New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "SendEmail";
            this.Text = "SendEmail";
            this.Load += new System.EventHandler(this.SendEmail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_total;
        private System.Windows.Forms.ProgressBar progressBar_send_email;
        private System.Windows.Forms.Button btn_send_email;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_send;
        private System.Windows.Forms.Label lb_job;
        private System.Windows.Forms.TextBox txb_file_name;
        private System.Windows.Forms.Button btn_read_log;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_create_report;
    }
}