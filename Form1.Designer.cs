
namespace LK.Tool
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lb_Count = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Context = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_LoadDB = new System.Windows.Forms.Button();
            this.btn_TestConnect = new System.Windows.Forms.Button();
            this.cbb_DB = new System.Windows.Forms.ComboBox();
            this.cbb_DBType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_Pwd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_UserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_IP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lb_Tables = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.生成文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tbPort);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.lb_Count);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tb_Context);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btn_LoadDB);
            this.panel1.Controls.Add(this.btn_TestConnect);
            this.panel1.Controls.Add(this.cbb_DB);
            this.panel1.Controls.Add(this.cbb_DBType);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.tb_Pwd);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tb_UserName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tb_IP);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(991, 143);
            this.panel1.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.ContextMenuStrip = this.contextMenuStrip2;
            this.listBox1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 19;
            this.listBox1.Location = new System.Drawing.Point(764, 18);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(214, 99);
            this.listBox1.TabIndex = 20;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(101, 26);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // lb_Count
            // 
            this.lb_Count.AutoSize = true;
            this.lb_Count.Location = new System.Drawing.Point(211, 106);
            this.lb_Count.Name = "lb_Count";
            this.lb_Count.Size = new System.Drawing.Size(0, 12);
            this.lb_Count.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "表名模糊";
            // 
            // tb_Context
            // 
            this.tb_Context.Location = new System.Drawing.Point(87, 101);
            this.tb_Context.Name = "tb_Context";
            this.tb_Context.Size = new System.Drawing.Size(100, 21);
            this.tb_Context.TabIndex = 1;
            this.tb_Context.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(440, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Star一下";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_LoadDB
            // 
            this.btn_LoadDB.Location = new System.Drawing.Point(633, 55);
            this.btn_LoadDB.Name = "btn_LoadDB";
            this.btn_LoadDB.Size = new System.Drawing.Size(75, 23);
            this.btn_LoadDB.TabIndex = 12;
            this.btn_LoadDB.Text = "加载所有表";
            this.btn_LoadDB.UseVisualStyleBackColor = true;
            this.btn_LoadDB.Click += new System.EventHandler(this.btn_LoadDB_Click);
            // 
            // btn_TestConnect
            // 
            this.btn_TestConnect.Location = new System.Drawing.Point(440, 18);
            this.btn_TestConnect.Name = "btn_TestConnect";
            this.btn_TestConnect.Size = new System.Drawing.Size(75, 23);
            this.btn_TestConnect.TabIndex = 11;
            this.btn_TestConnect.Text = "加载所有库";
            this.btn_TestConnect.UseVisualStyleBackColor = true;
            this.btn_TestConnect.Click += new System.EventHandler(this.btn_TestConnect_Click);
            // 
            // cbb_DB
            // 
            this.cbb_DB.FormattingEnabled = true;
            this.cbb_DB.Location = new System.Drawing.Point(598, 18);
            this.cbb_DB.Name = "cbb_DB";
            this.cbb_DB.Size = new System.Drawing.Size(110, 20);
            this.cbb_DB.TabIndex = 10;
            // 
            // cbb_DBType
            // 
            this.cbb_DBType.FormattingEnabled = true;
            this.cbb_DBType.Location = new System.Drawing.Point(87, 58);
            this.cbb_DBType.Name = "cbb_DBType";
            this.cbb_DBType.Size = new System.Drawing.Size(100, 20);
            this.cbb_DBType.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(539, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "选择库";
            // 
            // tb_Pwd
            // 
            this.tb_Pwd.Location = new System.Drawing.Point(286, 58);
            this.tb_Pwd.Name = "tb_Pwd";
            this.tb_Pwd.Size = new System.Drawing.Size(100, 21);
            this.tb_Pwd.TabIndex = 7;
            this.tb_Pwd.Text = "123456";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(229, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "密码";
            // 
            // tb_UserName
            // 
            this.tb_UserName.Location = new System.Drawing.Point(286, 18);
            this.tb_UserName.Name = "tb_UserName";
            this.tb_UserName.Size = new System.Drawing.Size(100, 21);
            this.tb_UserName.TabIndex = 5;
            this.tb_UserName.Text = "root";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(226, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "用户名";
            // 
            // tb_IP
            // 
            this.tb_IP.Location = new System.Drawing.Point(88, 18);
            this.tb_IP.Name = "tb_IP";
            this.tb_IP.Size = new System.Drawing.Size(100, 21);
            this.tb_IP.TabIndex = 3;
            this.tb_IP.Text = "192.168.50.240";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "服务器IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "数据库类型";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lb_Tables);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(329, 568);
            this.panel2.TabIndex = 1;
            // 
            // lb_Tables
            // 
            this.lb_Tables.ContextMenuStrip = this.contextMenuStrip1;
            this.lb_Tables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Tables.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_Tables.FormattingEnabled = true;
            this.lb_Tables.ItemHeight = 14;
            this.lb_Tables.Location = new System.Drawing.Point(0, 0);
            this.lb_Tables.Name = "lb_Tables";
            this.lb_Tables.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lb_Tables.Size = new System.Drawing.Size(329, 568);
            this.lb_Tables.TabIndex = 0;
            this.lb_Tables.DoubleClick += new System.EventHandler(this.lb_Tables_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.生成文件ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // 生成文件ToolStripMenuItem
            // 
            this.生成文件ToolStripMenuItem.Name = "生成文件ToolStripMenuItem";
            this.生成文件ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.生成文件ToolStripMenuItem.Text = "生成文件";
            this.生成文件ToolStripMenuItem.Click += new System.EventHandler(this.生成文件ToolStripMenuItem_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.richTextBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(658, 568);
            this.panel3.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(658, 568);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 143);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(991, 568);
            this.splitContainer1.SplitterDistance = 329;
            this.splitContainer1.TabIndex = 3;
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(286, 101);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(100, 21);
            this.tbPort.TabIndex = 22;
            this.tbPort.Text = "3306";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(229, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "端口";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 711);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "软件简称：TGE  全称：Table Generate Entity（.Net版本数据实体生成工具）";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_UserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_IP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lb_Tables;
        private System.Windows.Forms.ComboBox cbb_DB;
        private System.Windows.Forms.ComboBox cbb_DBType;
        private System.Windows.Forms.Button btn_LoadDB;
        private System.Windows.Forms.Button btn_TestConnect;
        private System.Windows.Forms.TextBox tb_Pwd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 生成文件ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tb_Context;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_Count;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label7;
    }
}

