using System;

namespace GUI

{
    partial class MainForm
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
            this.checkBoxFoodAutomation = new System.Windows.Forms.CheckBox();
            this.checkBoxDrinkAutomation = new System.Windows.Forms.CheckBox();
            this.checkBoxUseScrolls = new System.Windows.Forms.CheckBox();
            this.textBoxFood = new System.Windows.Forms.TextBox();
            this.textBoxDrink = new System.Windows.Forms.TextBox();
            this.labelFood = new System.Windows.Forms.Label();
            this.labelDrink = new System.Windows.Forms.Label();
            this.textBoxFoodPercent = new System.Windows.Forms.TextBox();
            this.textBoxDrinkPercent = new System.Windows.Forms.TextBox();
            this.labelEatAt = new System.Windows.Forms.Label();
            this.labelDrinkAt = new System.Windows.Forms.Label();
            this.labelPercentFood = new System.Windows.Forms.Label();
            this.labelPercentDrink = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(197, 226);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Event_GUIHandler);
            // 
            // checkBoxFoodAutomation
            // 
            this.checkBoxFoodAutomation.AutoSize = true;
            this.checkBoxFoodAutomation.Location = new System.Drawing.Point(13, 44);
            this.checkBoxFoodAutomation.Name = "checkBoxFoodAutomation";
            this.checkBoxFoodAutomation.Size = new System.Drawing.Size(103, 17);
            this.checkBoxFoodAutomation.TabIndex = 1;
            this.checkBoxFoodAutomation.Text = "Autoselect Food";
            this.checkBoxFoodAutomation.UseVisualStyleBackColor = true;
            this.checkBoxFoodAutomation.CheckedChanged += new System.EventHandler(this.Event_GUIHandler);
            // 
            // checkBoxDrinkAutomation
            // 
            this.checkBoxDrinkAutomation.AutoSize = true;
            this.checkBoxDrinkAutomation.Location = new System.Drawing.Point(13, 68);
            this.checkBoxDrinkAutomation.Name = "checkBoxDrinkAutomation";
            this.checkBoxDrinkAutomation.Size = new System.Drawing.Size(104, 17);
            this.checkBoxDrinkAutomation.TabIndex = 2;
            this.checkBoxDrinkAutomation.Text = "Autoselect Drink";
            this.checkBoxDrinkAutomation.UseVisualStyleBackColor = true;
            this.checkBoxDrinkAutomation.CheckedChanged += new System.EventHandler(this.Event_GUIHandler);
            // 
            // checkBoxUseScrolls
            // 
            this.checkBoxUseScrolls.AutoSize = true;
            this.checkBoxUseScrolls.Location = new System.Drawing.Point(13, 92);
            this.checkBoxUseScrolls.Name = "checkBoxUseScrolls";
            this.checkBoxUseScrolls.Size = new System.Drawing.Size(79, 17);
            this.checkBoxUseScrolls.TabIndex = 3;
            this.checkBoxUseScrolls.Text = "Use Scrolls";
            this.checkBoxUseScrolls.UseVisualStyleBackColor = true;
            this.checkBoxUseScrolls.CheckedChanged += new System.EventHandler(this.Event_GUIHandler);
            // 
            // textBoxFood
            // 
            this.textBoxFood.Location = new System.Drawing.Point(12, 151);
            this.textBoxFood.Name = "textBoxFood";
            this.textBoxFood.Size = new System.Drawing.Size(100, 20);
            this.textBoxFood.TabIndex = 4;
            this.textBoxFood.TextChanged += new System.EventHandler(this.Event_GUIHandler);
            // 
            // textBoxDrink
            // 
            this.textBoxDrink.Location = new System.Drawing.Point(12, 177);
            this.textBoxDrink.Name = "textBoxDrink";
            this.textBoxDrink.Size = new System.Drawing.Size(100, 20);
            this.textBoxDrink.TabIndex = 5;
            this.textBoxDrink.TextChanged += new System.EventHandler(this.Event_GUIHandler);
            // 
            // labelFood
            // 
            this.labelFood.AutoSize = true;
            this.labelFood.Location = new System.Drawing.Point(118, 154);
            this.labelFood.Name = "labelFood";
            this.labelFood.Size = new System.Drawing.Size(31, 13);
            this.labelFood.TabIndex = 6;
            this.labelFood.Text = "Food";
            // 
            // labelDrink
            // 
            this.labelDrink.AutoSize = true;
            this.labelDrink.Location = new System.Drawing.Point(117, 180);
            this.labelDrink.Name = "labelDrink";
            this.labelDrink.Size = new System.Drawing.Size(32, 13);
            this.labelDrink.TabIndex = 7;
            this.labelDrink.Text = "Drink";
            // 
            // textBoxFoodPercent
            // 
            this.textBoxFoodPercent.Location = new System.Drawing.Point(197, 44);
            this.textBoxFoodPercent.Name = "textBoxFoodPercent";
            this.textBoxFoodPercent.Size = new System.Drawing.Size(19, 20);
            this.textBoxFoodPercent.TabIndex = 8;
            this.textBoxFoodPercent.TextChanged += new System.EventHandler(this.Event_GUIHandler);
            // 
            // textBoxDrinkPercent
            // 
            this.textBoxDrinkPercent.Location = new System.Drawing.Point(197, 70);
            this.textBoxDrinkPercent.Name = "textBoxDrinkPercent";
            this.textBoxDrinkPercent.Size = new System.Drawing.Size(19, 20);
            this.textBoxDrinkPercent.TabIndex = 9;
            this.textBoxDrinkPercent.TextChanged += new System.EventHandler(this.Event_GUIHandler);
            // 
            // labelEatAt
            // 
            this.labelEatAt.AutoSize = true;
            this.labelEatAt.Location = new System.Drawing.Point(156, 47);
            this.labelEatAt.Name = "labelEatAt";
            this.labelEatAt.Size = new System.Drawing.Size(35, 13);
            this.labelEatAt.TabIndex = 10;
            this.labelEatAt.Text = "Eat at";
            // 
            // labelDrinkAt
            // 
            this.labelDrinkAt.AutoSize = true;
            this.labelDrinkAt.Location = new System.Drawing.Point(147, 73);
            this.labelDrinkAt.Name = "labelDrinkAt";
            this.labelDrinkAt.Size = new System.Drawing.Size(44, 13);
            this.labelDrinkAt.TabIndex = 11;
            this.labelDrinkAt.Text = "Drink at";
            // 
            // labelPercentFood
            // 
            this.labelPercentFood.AutoSize = true;
            this.labelPercentFood.Location = new System.Drawing.Point(222, 48);
            this.labelPercentFood.Name = "labelPercentFood";
            this.labelPercentFood.Size = new System.Drawing.Size(15, 13);
            this.labelPercentFood.TabIndex = 12;
            this.labelPercentFood.Text = "%";
            // 
            // labelPercentDrink
            // 
            this.labelPercentDrink.AutoSize = true;
            this.labelPercentDrink.Location = new System.Drawing.Point(222, 73);
            this.labelPercentDrink.Name = "labelPercentDrink";
            this.labelPercentDrink.Size = new System.Drawing.Size(15, 13);
            this.labelPercentDrink.TabIndex = 13;
            this.labelPercentDrink.Text = "%";
            // 
            // MainForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.labelPercentDrink);
            this.Controls.Add(this.labelPercentFood);
            this.Controls.Add(this.labelDrinkAt);
            this.Controls.Add(this.labelEatAt);
            this.Controls.Add(this.textBoxDrinkPercent);
            this.Controls.Add(this.textBoxFoodPercent);
            this.Controls.Add(this.labelDrink);
            this.Controls.Add(this.labelFood);
            this.Controls.Add(this.textBoxDrink);
            this.Controls.Add(this.textBoxFood);
            this.Controls.Add(this.checkBoxUseScrolls);
            this.Controls.Add(this.checkBoxDrinkAutomation);
            this.Controls.Add(this.checkBoxFoodAutomation);
            this.Controls.Add(this.button1);
            this.Name = "MainForm";
            this.Text = "ShadyPriest Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Event_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Event_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.CheckBox checkBoxFoodAutomation;
        public System.Windows.Forms.CheckBox checkBoxDrinkAutomation;
        public System.Windows.Forms.CheckBox checkBoxUseScrolls;
        public System.Windows.Forms.TextBox textBoxFood;
        public System.Windows.Forms.TextBox textBoxDrink;
        public System.Windows.Forms.Label labelFood;
        public System.Windows.Forms.Label labelDrink;
        public System.Windows.Forms.TextBox textBoxFoodPercent;
        public System.Windows.Forms.TextBox textBoxDrinkPercent;
        public System.Windows.Forms.Label labelEatAt;
        public System.Windows.Forms.Label labelDrinkAt;
        public System.Windows.Forms.Label labelPercentFood;
        public System.Windows.Forms.Label labelPercentDrink;
    }
}