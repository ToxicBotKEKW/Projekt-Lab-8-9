using Projekt_Lab_8_9;
using System;
using System.Drawing;
using System.IO.Packaging;
using System.Windows.Forms;

namespace Aplikacja_Okienkowa
{
    public partial class EquipmentPanel : UserControl
    {
        private MineGame mineGame;

        public EquipmentPanel()
        {
            mineGame = MineGame.GetInstance();
            InitComponent();
        }

        private void InitComponent()
        {
            Panel eqPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding { Left = 100, Top = 50},
                AutoScroll = true
            };

            Label improvementsLabel = new Label
            {
                Dock = DockStyle.Top,
                Text = "Ulepszenia",
                Font = new Font("Arial", 24, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true
            };


            FlowLayoutPanel improvementsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(10)
            };

            improvementsPanel.Controls.Add(CreateMineImprovementPanel(mineGame.IronMine, "../../../Resources/iron-mine-2.png"));
            improvementsPanel.Controls.Add(CreateMineImprovementPanel(mineGame.GoldMine, "../../../Resources/gold-mine-2.png"));
            improvementsPanel.Controls.Add(CreateMineImprovementPanel(mineGame.DiamondMine, "../../../Resources/diamond-mine-2.png"));


            /////////////////////////////////

            Label accessivlePickaxesLabel = new Label
            {
                Dock = DockStyle.Top,
                Text = "Dostępne Kilofy",
                Font = new Font("Arial", 24, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true
            };

            FlowLayoutPanel accessiblePickaxesPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(10)
            };


            mineGame.Equipment.PickaxeList.ForEach(pickaxe =>
            {
                Panel pickaxePanel = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(15),
                    Padding = new Padding(10),
                    BackColor = Color.FromArgb(240, 240, 240),
                    MinimumSize = new Size(200, 350)
                };

                PictureBox pictureBox = new PictureBox
                {
                    Width = 64,
                    Height = 164,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Top,
                    Margin = new Padding(5)
                };

                try
                {
                    pictureBox.Image = Image.FromFile($"../../../Resources/pickaxe/{pickaxe.ImageName}");
                }
                catch
                {
                    pictureBox.Image = null;
                }

                Label nameLabel = new Label
                {
                    Text = pickaxe.Name,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label levelLabel = new Label
                {
                    Text = $"Wymagany poziom: {pickaxe.RequirmentLevel}",
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    ForeColor = Color.FromArgb(220, 53, 69),
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label multiplierTextLabel = new Label
                {
                    Text = $"Mnożniki:",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label multiplierIronTextLabel = new Label
                {
                    Text = $"- Żelazo: {pickaxe.Multiplier.GetValueOrDefault(ResourceType.Iron, 0)}",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label multiplierGoldTextLabel = new Label
                {
                    Text = $"- Złoto: {pickaxe.Multiplier.GetValueOrDefault(ResourceType.Gold, 0)}",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label multiplierDiamondTextLabel = new Label
                {
                    Text = $"- Diamenty: {pickaxe.Multiplier.GetValueOrDefault(ResourceType.Diamond, 0)}",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };


                Button equipPickaxeButton = new Button
                {
                    Text = pickaxe.Id == mineGame.UsedPickaxe.Id ? "Wyposażono" : "Wyposaż",
                    Dock = DockStyle.Top,
                    Height = 35,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    BackColor = pickaxe.Id == mineGame.UsedPickaxe.Id ? Color.Gray : Color.FromArgb(25, 135, 84),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = pickaxe.Id == mineGame.UsedPickaxe.Id ? Cursors.No : Cursors.Hand,
                    Enabled = pickaxe.Id != mineGame.UsedPickaxe.Id,
                    Margin = new Padding(0, 10, 0, 0)
                };

                equipPickaxeButton.FlatAppearance.BorderSize = 0;

                equipPickaxeButton.Click += (e, v) =>
                {
                    mineGame.EquipPickaxe(pickaxe.Id);
                    this.Controls.Clear();
                    InitComponent();
                };

                pickaxePanel.Controls.Add(equipPickaxeButton);
                pickaxePanel.Controls.Add(multiplierDiamondTextLabel);
                pickaxePanel.Controls.Add(multiplierGoldTextLabel);
                pickaxePanel.Controls.Add(multiplierIronTextLabel);
                pickaxePanel.Controls.Add(multiplierTextLabel);
                pickaxePanel.Controls.Add(levelLabel);
                pickaxePanel.Controls.Add(nameLabel);
                pickaxePanel.Controls.Add(pictureBox);

                accessiblePickaxesPanel.Controls.Add(pickaxePanel);
            });

            eqPanel.Controls.Add(accessiblePickaxesPanel);
            eqPanel.Controls.Add(accessivlePickaxesLabel);
            eqPanel.Controls.Add(improvementsPanel);
            eqPanel.Controls.Add(improvementsLabel);

            this.Controls.Add(eqPanel);
        }

        private Panel CreateMineImprovementPanel(Mine mine, string imagePath)
        {
            Panel panel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(15),
                Padding = new Padding(10),
                BackColor = Color.FromArgb(240, 240, 240),
                MinimumSize = new Size(250, 400)
            };

            PictureBox pictureBox = new PictureBox
            {
                Width = 64,
                Height = 164,
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Top,
                Margin = new Padding(5)
            };

            try
            {
                pictureBox.Image = Image.FromFile(imagePath);
            }
            catch
            {
                pictureBox.Image = null;
            }

            Label nameLabel = new Label
            {
                Text = mine.Name,
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Dock = DockStyle.Top,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label levelLabel = new Label
            {
                Text = $"Poziom: {mine.Level}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Dock = DockStyle.Top,
                Height = 20,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label pointForClickLabel = new Label
            {
                Text = $"Punkty za kliknięcie: {mine.GetPointForClick()}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Dock = DockStyle.Top,
                Height = 20,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label pointForTimeLabel = new Label
            {
                Text = $"Punkty za czas: {mine.GetPointsPerInterval()}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Dock = DockStyle.Top,
                Height = 20,
                TextAlign = ContentAlignment.MiddleCenter
            };

            if (mine.RequirmentsForNextLevel() != null)
            {

                Label requirmentsTextLabel = new Label
                {
                    Text = $"Koszt ulepszenia:",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 25,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label ironRequirmentsTextLabel = new Label
                {
                    Text = $"- Żelazo: {mine.RequirmentsForNextLevel().GetValueOrDefault(ResourceType.Iron, 0)}",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label goldRequirmentsTextLabel = new Label
                {
                    Text = $"- Złoto: {mine.RequirmentsForNextLevel().GetValueOrDefault(ResourceType.Gold, 0)}",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label diamondRequirmentsTextLabel = new Label
                {
                    Text = $"- Diamenty: {mine.RequirmentsForNextLevel().GetValueOrDefault(ResourceType.Diamond, 0)}",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Button upgradeButton = new Button
                {
                    Text = "Ulepsz",
                    Dock = DockStyle.Top,
                    Height = 35,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    BackColor = !mineGame.CanUpgradeMine(mine) ? Color.Gray : Color.FromArgb(25, 135, 84),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = !mineGame.CanUpgradeMine(mine) ? Cursors.No : Cursors.Hand,
                    Enabled = mineGame.CanUpgradeMine(mine),
                    Margin = new Padding(0, 10, 0, 0)
                };
                upgradeButton.FlatAppearance.BorderSize = 0;

                upgradeButton.Click += (s, e) =>
                {
                    var requirements = mine.RequirmentsForNextLevel();
                    DialogResult result = MessageBox.Show(
                        $"Żelazo: {requirements.GetValueOrDefault(ResourceType.Iron, 0)}, " +
                        $"Złoto: {requirements.GetValueOrDefault(ResourceType.Gold, 0)}, " +
                        $"Diamenty: {requirements.GetValueOrDefault(ResourceType.Diamond, 0)}",
                        "Ulepsz",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        mineGame.UpgradeMine(mine);
                        this.Controls.Clear();
                        InitComponent();
                    }
                };

                panel.Controls.Add(upgradeButton);
                panel.Controls.Add(diamondRequirmentsTextLabel);
                panel.Controls.Add(goldRequirmentsTextLabel);
                panel.Controls.Add(ironRequirmentsTextLabel);
                panel.Controls.Add(requirmentsTextLabel);
            }

            panel.Controls.Add(pointForTimeLabel);
            panel.Controls.Add(pointForClickLabel);
            panel.Controls.Add(levelLabel);
            panel.Controls.Add(nameLabel);
            panel.Controls.Add(pictureBox);

            return panel;
        }

    }
}
