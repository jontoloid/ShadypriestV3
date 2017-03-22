using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            checkBoxDrinkAutomation.Checked = ShadySettings.autoSelectDrink;
            checkBoxFoodAutomation.Checked = ShadySettings.autoSelectFood;
            checkBoxUseScrolls.Checked = ShadySettings.useScrolls;
            textBoxDrinkPercent.Text = ShadySettings.drinkAt.ToString();
            textBoxFoodPercent.Text = ShadySettings.eatAt.ToString();
            textBoxFood.Text = ShadySettings.food.ToString();
            textBoxDrink.Text = ShadySettings.drink.ToString();
        }

        void Event_GUIHandler(object Sender, EventArgs Args)
        {
            if (Sender == checkBoxDrinkAutomation)
            {
                ShadySettings.autoSelectDrink = checkBoxDrinkAutomation.Checked;
            }
            else if (Sender == checkBoxFoodAutomation)
            {
                ShadySettings.autoSelectFood = checkBoxFoodAutomation.Checked;
            }
            else if (Sender == checkBoxUseScrolls)
            {
                ShadySettings.useScrolls = checkBoxUseScrolls.Checked;
            }
            else if (Sender == textBoxDrinkPercent)
            {
                ShadySettings.drinkAt = int.Parse(textBoxDrinkPercent.Text);
            }
            else if (Sender == textBoxFoodPercent)
            {
                ShadySettings.eatAt = int.Parse(textBoxFoodPercent.Text);
            }
            else if (Sender == textBoxFood)
            {
                ShadySettings.food = textBoxFood.Text;
            }
            else if (Sender == textBoxDrink)
            {
                ShadySettings.drink = textBoxDrink.Text;
            }
            else if (Sender == button1)
            {//tbd

            }
        }

        private void Event_FormClosing(object Sender, FormClosingEventArgs Args)
        {
            Args.Cancel = true;
            this.Hide();
        }

        private void checkBoxUseScrolls_CheckedChanged(object sender, EventArgs e)
        {
            ShadySettings.useScrolls = checkBoxUseScrolls.Checked;

        }

        private void textBoxFood_TextChanged(object sender, EventArgs e)
        {
            ShadySettings.food = textBoxFood.Text;
        }

        private void textBoxDrink_TextChanged(object sender, EventArgs e)
        {
            ShadySettings.drink = textBoxDrink.Text;
        }

        private void checkBoxDrinkAutomation_CheckedChanged(object sender, EventArgs e)
        {
            ShadySettings.autoSelectDrink = checkBoxDrinkAutomation.Checked;
        }

        private void checkBoxFoodAutomation_CheckedChanged(object sender, EventArgs e)
        {
            ShadySettings.autoSelectFood = checkBoxFoodAutomation.Checked;
        }

        private void textBoxDrinkPercent_TextChanged(object sender, EventArgs e)
        {
            ShadySettings.drinkAt = int.Parse(textBoxDrinkPercent.Text);

        }

        private void textBoxFoodPercent_TextChanged(object sender, EventArgs e)
        {
            ShadySettings.eatAt = int.Parse(textBoxFoodPercent.Text);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void Event_KeyPress(object Sender, KeyPressEventArgs Args)
        {
            if (Args.KeyChar == '\r')
            {
                if (Sender == textBoxFood || Sender == textBoxDrink)
                {

                }
            }
            else
            {
                Args.Handled = !char.IsDigit(Args.KeyChar);
            }

        }
    }
}