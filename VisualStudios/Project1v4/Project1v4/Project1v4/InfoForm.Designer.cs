namespace Project1v4
{
    partial class InfoForm
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
            this.player1Name = new System.Windows.Forms.TextBox();
            this.player2Name = new System.Windows.Forms.TextBox();
            this.mapPath = new System.Windows.Forms.TextBox();
            this.player1Label = new System.Windows.Forms.Label();
            this.player2Label = new System.Windows.Forms.Label();
            this.mapLabel = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // player1Name
            // 
            this.player1Name.Location = new System.Drawing.Point(104, 23);
            this.player1Name.Name = "player1Name";
            this.player1Name.Size = new System.Drawing.Size(148, 20);
            this.player1Name.TabIndex = 0;
            this.player1Name.TextChanged += new System.EventHandler(this.player1Name_TextChanged);
            // 
            // player2Name
            // 
            this.player2Name.Location = new System.Drawing.Point(104, 50);
            this.player2Name.Name = "player2Name";
            this.player2Name.Size = new System.Drawing.Size(148, 20);
            this.player2Name.TabIndex = 1;
            // 
            // mapPath
            // 
            this.mapPath.Location = new System.Drawing.Point(104, 76);
            this.mapPath.Name = "mapPath";
            this.mapPath.Size = new System.Drawing.Size(148, 20);
            this.mapPath.TabIndex = 2;
            // 
            // player1Label
            // 
            this.player1Label.AutoSize = true;
            this.player1Label.Location = new System.Drawing.Point(3, 26);
            this.player1Label.Name = "player1Label";
            this.player1Label.Size = new System.Drawing.Size(79, 13);
            this.player1Label.TabIndex = 3;
            this.player1Label.Text = "Player 1 Name:";
            // 
            // player2Label
            // 
            this.player2Label.AutoSize = true;
            this.player2Label.Location = new System.Drawing.Point(3, 53);
            this.player2Label.Name = "player2Label";
            this.player2Label.Size = new System.Drawing.Size(79, 13);
            this.player2Label.TabIndex = 4;
            this.player2Label.Text = "Player 2 Name:";
            // 
            // mapLabel
            // 
            this.mapLabel.AutoSize = true;
            this.mapLabel.Location = new System.Drawing.Point(12, 79);
            this.mapLabel.Name = "mapLabel";
            this.mapLabel.Size = new System.Drawing.Size(50, 13);
            this.mapLabel.TabIndex = 5;
            this.mapLabel.Text = "Map File:";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(331, 19);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 6;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(331, 73);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Play!";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 114);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.mapLabel);
            this.Controls.Add(this.player2Label);
            this.Controls.Add(this.player1Label);
            this.Controls.Add(this.mapPath);
            this.Controls.Add(this.player2Name);
            this.Controls.Add(this.player1Name);
            this.Name = "InfoForm";
            this.Text = "Player Information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox player1Name;
        private System.Windows.Forms.TextBox player2Name;
        private System.Windows.Forms.TextBox mapPath;
        private System.Windows.Forms.Label player1Label;
        private System.Windows.Forms.Label player2Label;
        private System.Windows.Forms.Label mapLabel;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button button2;
    }
}

