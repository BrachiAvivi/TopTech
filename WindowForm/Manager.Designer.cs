namespace WindowForm
{
    partial class Manager
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
            this.button1 = new System.Windows.Forms.Button();
            this.open = new System.Windows.Forms.NumericUpDown();
            this.close = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.open)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.close)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(624, 284);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 63);
            this.button1.TabIndex = 0;
            this.button1.Text = "בצע";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(741, 227);
            this.open.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.open.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(53, 22);
            this.open.TabIndex = 1;
            this.open.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(612, 226);
            this.close.Maximum = new decimal(new int[] {
            22,
            0,
            0,
            0});
            this.close.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(62, 22);
            this.close.TabIndex = 2;
            this.close.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(800, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "החל מ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(680, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "עד ל";
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 586);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.open);
            this.Controls.Add(this.button1);
            this.Name = "Manager";
            this.Text = "Manager";
            ((System.ComponentModel.ISupportInitialize)(this.open)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.close)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown open;
        private System.Windows.Forms.NumericUpDown close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}