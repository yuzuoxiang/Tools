namespace CreateModelFile
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBuilder = new System.Windows.Forms.Button();
            this.lblModelNameList = new System.Windows.Forms.Label();
            this.txtModelNames = new System.Windows.Forms.TextBox();
            this.txtExportPath = new System.Windows.Forms.TextBox();
            this.lblExportPath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.lblNameSpace = new System.Windows.Forms.Label();
            this.lblDBName = new System.Windows.Forms.Label();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnBuilder
            // 
            this.btnBuilder.Location = new System.Drawing.Point(499, 287);
            this.btnBuilder.Name = "btnBuilder";
            this.btnBuilder.Size = new System.Drawing.Size(75, 23);
            this.btnBuilder.TabIndex = 0;
            this.btnBuilder.Text = "生成";
            this.btnBuilder.UseVisualStyleBackColor = true;
            this.btnBuilder.Click += new System.EventHandler(this.btnBuilder_Click);
            // 
            // lblModelNameList
            // 
            this.lblModelNameList.AutoSize = true;
            this.lblModelNameList.Location = new System.Drawing.Point(31, 22);
            this.lblModelNameList.Name = "lblModelNameList";
            this.lblModelNameList.Size = new System.Drawing.Size(125, 12);
            this.lblModelNameList.TabIndex = 1;
            this.lblModelNameList.Text = "需要导出的模型名称：";
            // 
            // txtModelNames
            // 
            this.txtModelNames.Location = new System.Drawing.Point(162, 19);
            this.txtModelNames.Multiline = true;
            this.txtModelNames.Name = "txtModelNames";
            this.txtModelNames.Size = new System.Drawing.Size(301, 57);
            this.txtModelNames.TabIndex = 2;
            // 
            // txtExportPath
            // 
            this.txtExportPath.Location = new System.Drawing.Point(164, 245);
            this.txtExportPath.Multiline = true;
            this.txtExportPath.Name = "txtExportPath";
            this.txtExportPath.Size = new System.Drawing.Size(301, 65);
            this.txtExportPath.TabIndex = 0;
            // 
            // lblExportPath
            // 
            this.lblExportPath.AutoSize = true;
            this.lblExportPath.Location = new System.Drawing.Point(69, 248);
            this.lblExportPath.Name = "lblExportPath";
            this.lblExportPath.Size = new System.Drawing.Size(89, 12);
            this.lblExportPath.TabIndex = 3;
            this.lblExportPath.Text = "文件导出路径：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(469, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "（以英文逗号分隔）";
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Location = new System.Drawing.Point(162, 92);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(298, 21);
            this.txtNameSpace.TabIndex = 5;
            // 
            // lblNameSpace
            // 
            this.lblNameSpace.AutoSize = true;
            this.lblNameSpace.Location = new System.Drawing.Point(91, 95);
            this.lblNameSpace.Name = "lblNameSpace";
            this.lblNameSpace.Size = new System.Drawing.Size(65, 12);
            this.lblNameSpace.TabIndex = 6;
            this.lblNameSpace.Text = "命名空间：";
            // 
            // lblDBName
            // 
            this.lblDBName.AutoSize = true;
            this.lblDBName.Location = new System.Drawing.Point(80, 131);
            this.lblDBName.Name = "lblDBName";
            this.lblDBName.Size = new System.Drawing.Size(77, 12);
            this.lblDBName.TabIndex = 8;
            this.lblDBName.Text = "数据库名称：";
            // 
            // txtDBName
            // 
            this.txtDBName.Location = new System.Drawing.Point(162, 128);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(189, 21);
            this.txtDBName.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 339);
            this.Controls.Add(this.lblDBName);
            this.Controls.Add(this.txtDBName);
            this.Controls.Add(this.lblNameSpace);
            this.Controls.Add(this.txtNameSpace);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblExportPath);
            this.Controls.Add(this.txtExportPath);
            this.Controls.Add(this.txtModelNames);
            this.Controls.Add(this.lblModelNameList);
            this.Controls.Add(this.btnBuilder);
            this.Name = "Form1";
            this.Text = "数据库模型生成器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuilder;
        private System.Windows.Forms.Label lblModelNameList;
        private System.Windows.Forms.TextBox txtModelNames;
        private System.Windows.Forms.TextBox txtExportPath;
        private System.Windows.Forms.Label lblExportPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.Label lblNameSpace;
        private System.Windows.Forms.Label lblDBName;
        private System.Windows.Forms.TextBox txtDBName;
    }
}

