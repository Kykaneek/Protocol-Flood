namespace Client
{
    partial class Klient
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
            Clean = new Button();
            groupBox1 = new GroupBox();
            PortP = new TextBox();
            SetOptions = new Button();
            IP = new TextBox();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            SendMessage = new Button();
            Odpowiedź = new GroupBox();
            FromServerMessage = new RichTextBox();
            groupBox3 = new GroupBox();
            ToServerMessage = new RichTextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            Odpowiedź.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // Clean
            // 
            Clean.Location = new Point(322, 559);
            Clean.Name = "Clean";
            Clean.Size = new Size(63, 35);
            Clean.TabIndex = 0;
            Clean.Text = "Wyczyść";
            Clean.UseVisualStyleBackColor = true;
            Clean.Click += Clean_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(PortP);
            groupBox1.Controls.Add(SetOptions);
            groupBox1.Controls.Add(IP);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(391, 100);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Dane połączeniowe z serwerem";
            // 
            // PortP
            // 
            PortP.Location = new Point(80, 52);
            PortP.Name = "PortP";
            PortP.Size = new Size(236, 23);
            PortP.TabIndex = 5;
            // 
            // SetOptions
            // 
            SetOptions.Location = new Point(322, 28);
            SetOptions.Name = "SetOptions";
            SetOptions.Size = new Size(63, 52);
            SetOptions.TabIndex = 4;
            SetOptions.Text = "Ustaw";
            SetOptions.UseVisualStyleBackColor = true;
            SetOptions.Click += SetOptions_Click;
            // 
            // IP
            // 
            IP.Location = new Point(80, 25);
            IP.Name = "IP";
            IP.Size = new Size(236, 23);
            IP.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 55);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 1;
            label2.Text = "PORT";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 28);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 0;
            label1.Text = "IP";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(SendMessage);
            groupBox2.Controls.Add(Odpowiedź);
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Controls.Add(Clean);
            groupBox2.Location = new Point(12, 118);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(391, 612);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Dialog";
            // 
            // SendMessage
            // 
            SendMessage.Location = new Point(253, 559);
            SendMessage.Name = "SendMessage";
            SendMessage.Size = new Size(63, 35);
            SendMessage.TabIndex = 5;
            SendMessage.Text = "Wyślij";
            SendMessage.UseVisualStyleBackColor = true;
            SendMessage.Click += SendMessage_Click;
            // 
            // Odpowiedź
            // 
            Odpowiedź.Controls.Add(FromServerMessage);
            Odpowiedź.Location = new Point(22, 301);
            Odpowiedź.Name = "Odpowiedź";
            Odpowiedź.Size = new Size(363, 239);
            Odpowiedź.TabIndex = 4;
            Odpowiedź.TabStop = false;
            Odpowiedź.Text = "Odpowiedź z Serwera";
            // 
            // FromServerMessage
            // 
            FromServerMessage.Location = new Point(6, 22);
            FromServerMessage.Name = "FromServerMessage";
            FromServerMessage.Size = new Size(351, 211);
            FromServerMessage.TabIndex = 1;
            FromServerMessage.Text = "";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(ToServerMessage);
            groupBox3.Location = new Point(22, 22);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(363, 260);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Wiadomość do wysłania";
            // 
            // ToServerMessage
            // 
            ToServerMessage.Location = new Point(12, 22);
            ToServerMessage.Name = "ToServerMessage";
            ToServerMessage.Size = new Size(345, 232);
            ToServerMessage.TabIndex = 0;
            ToServerMessage.Text = "";
            // 
            // Klient
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(423, 742);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Klient";
            Text = "Klient TCP";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            Odpowiedź.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button Clean;
        private GroupBox groupBox1;
        private TextBox IP;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private GroupBox Odpowiedź;
        private GroupBox groupBox3;
        private Button SetOptions;
        private Button SendMessage;
        private RichTextBox FromServerMessage;
        private RichTextBox ToServerMessage;
        private TextBox PortP;
    }
}
