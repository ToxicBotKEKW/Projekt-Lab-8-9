using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_Lab_8_9;

namespace Aplikacja_Okienkowa
{
    public partial class ShopPanel : UserControl
    {
        private MineGame mineGame;
        private FlowLayoutPanel pickaxeShopListPanel;

        public ShopPanel()
        {
            mineGame = MineGame.GetInstance();
            InitComponent();
        }

        private void InitComponent()
        {
            Panel shopPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding { Left = 100, Top = 50 },
                AutoScroll = true
            };

            Label pickaxeShopLabel = new Label
            {
                Dock = DockStyle.Top,
                Text = "Kilofy",
                Font = new Font("Arial", 24, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true
            };

            FlowLayoutPanel pickaxeShopSearchPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(10)
            };

            TextBox pickaxeShopSearchTextBox = new TextBox
            {
                PlaceholderText = "Nazwa kilofa",
                Width = 150,
                Height = 35,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Margin = new Padding(5),
            };

            Button pickaxeShopSearchButton = new Button
            {
                Width = 120,
                Height = 25,
                Text = "Wyszukaj",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(25, 135, 84), 
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Margin = new Padding(5)
            };

            pickaxeShopSearchButton.FlatAppearance.BorderSize = 0;
            pickaxeShopSearchButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(20, 100, 60);


            pickaxeShopSearchButton.Click += (s, v) => {
                string name = pickaxeShopSearchTextBox.Text;
                shopPanel.Controls.Remove(pickaxeShopListPanel);
                pickaxeShopListPanel = InitPickaxeListPanel(name);
                shopPanel.Controls.Add(pickaxeShopListPanel);
                shopPanel.Controls.SetChildIndex(pickaxeShopListPanel, 0);
            };

            pickaxeShopSearchPanel.Controls.Add(pickaxeShopSearchTextBox);
            pickaxeShopSearchPanel.Controls.Add(pickaxeShopSearchButton);

            pickaxeShopListPanel = InitPickaxeListPanel(null);

            shopPanel.Controls.Add(pickaxeShopListPanel);
            shopPanel.Controls.Add(pickaxeShopSearchPanel);
            shopPanel.Controls.Add(pickaxeShopLabel);

            this.Controls.Add(shopPanel);
        }

        private FlowLayoutPanel InitPickaxeListPanel(String name)
        {
            FlowLayoutPanel pickaxeShopPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(10)
            };

            foreach (var item in mineGame.GetPickaxesYouCanBuy(name))
            {
                Pickaxe pickaxe = item.Value.Item1;

                Panel pickaxePanel = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(15),
                    Padding = new Padding(10),
                    BackColor = Color.FromArgb(240, 240, 240),
                    MinimumSize = new Size(200, 450)
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
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 25,
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
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 25,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label multiplierIronTextLabel = new Label
                {
                    Text = $"- Żelazo: {pickaxe.Multiplier.GetValueOrDefault(ResourceType.Iron, 0)} %",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label multiplierGoldTextLabel = new Label
                {
                    Text = $"- Złoto: {pickaxe.Multiplier.GetValueOrDefault(ResourceType.Gold, 0)} %",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label multiplierDiamondTextLabel = new Label
                {
                    Text = $"- Diamenty: {pickaxe.Multiplier.GetValueOrDefault(ResourceType.Diamond, 0)} %",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Dictionary<ResourceType, double> cost = item.Value.Item2;


                Label costTextLabel = new Label
                {
                    Text = $"Koszt:",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 25,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label costIronTextLabel = new Label
                {
                    Text = $"- Żelazo: {cost.GetValueOrDefault(ResourceType.Iron, 0)}",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label costGoldTextLabel = new Label
                {
                    Text = $"- Złoto: {cost.GetValueOrDefault(ResourceType.Gold, 0)}",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label costDiamondTextLabel = new Label
                {
                    Text = $"- Diamenty: {cost.GetValueOrDefault(ResourceType.Diamond, 0)}",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Button buyPickaxeButton = new Button
                {
                    Text = "Kup",
                    Dock = DockStyle.Top,
                    Height = 35,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    BackColor = !mineGame.CanBuyPickaxe(pickaxe.Id) ? Color.Gray : Color.FromArgb(25, 135, 84),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = !mineGame.CanBuyPickaxe(pickaxe.Id) ? Cursors.No : Cursors.Hand,
                    Enabled = mineGame.CanBuyPickaxe(pickaxe.Id),
                    Margin = new Padding(0, 10, 0, 0)
                };

                buyPickaxeButton.FlatAppearance.BorderSize = 0;

                buyPickaxeButton.Click += (e, v) =>
                {
                    DialogResult result = MessageBox.Show(
                        $"Żelazo: {cost.GetValueOrDefault(ResourceType.Iron, 0)}, " +
                        $"Złoto: {cost.GetValueOrDefault(ResourceType.Gold, 0)}, " +
                        $"Diamenty: {cost.GetValueOrDefault(ResourceType.Diamond, 0)}",
                        "Ulepsz",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        mineGame.BuyPickaxe(pickaxe.Id);
                        this.Controls.Clear();
                        InitComponent();
                    }
                };

                pickaxePanel.Controls.Add(buyPickaxeButton);
                pickaxePanel.Controls.Add(costDiamondTextLabel);
                pickaxePanel.Controls.Add(costGoldTextLabel);
                pickaxePanel.Controls.Add(costIronTextLabel);
                pickaxePanel.Controls.Add(costTextLabel);
                pickaxePanel.Controls.Add(multiplierDiamondTextLabel);
                pickaxePanel.Controls.Add(multiplierGoldTextLabel);
                pickaxePanel.Controls.Add(multiplierIronTextLabel);
                pickaxePanel.Controls.Add(multiplierTextLabel);
                pickaxePanel.Controls.Add(levelLabel);
                pickaxePanel.Controls.Add(nameLabel);
                pickaxePanel.Controls.Add(pictureBox);

                pickaxeShopPanel.Controls.Add(pickaxePanel);
            }

            return pickaxeShopPanel;
        }
    }
}
