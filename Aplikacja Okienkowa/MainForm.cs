using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Projekt_Lab_8_9;
using Newtonsoft.Json;

namespace Aplikacja_Okienkowa
{
    public partial class MainForm : Form
    {
        private Panel navPanel;
        private Panel infoPanel;
        private Panel rightPanel;
        private Panel contentPanel;
        private Button ironMineButton;
        private Button goldMineButton;
        private Button diamondMineButton;
        private Button shopButton;
        private Button equipmentButton;
        private Label ironValueLabel;
        private Label goldValueLabel;
        private Label diamonodValueLabel;
        private MineGame mineGame;

        private System.Windows.Forms.Timer periodicTimer;

        public MainForm()
        {
            mineGame = MineGame.GetInstance();

            string projectRoot1 = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
            string primaryFilePath = Path.Combine(projectRoot1, "mineGame.json");
            string projectRoot2 = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @""));
            string backupFilePath = Path.Combine(projectRoot2, "defaultGame.json");

            string jsonFromFile;

            if (File.Exists(primaryFilePath))
            {
                jsonFromFile = File.ReadAllText(primaryFilePath);
            }
            else if (File.Exists(backupFilePath))
            {
                jsonFromFile = File.ReadAllText(backupFilePath);
            }
            else
            {
                throw new FileNotFoundException("Nie znaleziono ani pliku głównego, ani zapasowego.");
            }

            mineGame.LoadDataFromJson(jsonFromFile);

            InitComponent();
            LoadPanel(new IronMinePanel());

            InitializePeriodicTimer();

            this.FormClosing += MainForm_FormClosing;
        }

        private void InitComponent()
        {
            this.Width = 800;
            this.Height = 600;


            navPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 100,
                BackColor = Color.FromArgb(0, 11, 88)
            };

            ironMineButton = new Button
            {
                Text = "",
                Dock = DockStyle.Top,
                Width = 100,
                Height = 100,
                Image = Image.FromFile("../../../Resources/iron-mine.png"),
                Cursor = Cursors.Hand
            };

            ironMineButton.Click += btnIronMine_Click;

            goldMineButton = new Button
            {
                Text = "",
                Dock = DockStyle.Top,
                Width = 100,
                Height = 100,
                Image = Image.FromFile("../../../Resources/gold-mine.png"),
                Cursor = Cursors.Hand
            };

            goldMineButton.Click += btnGoldMine_Click;

            diamondMineButton = new Button 
            {
                Text = "",
                Dock = DockStyle.Top,
                Width = 100,
                Height = 100,
                Image = Image.FromFile("../../../Resources/diamond-mine.png"),
                Cursor = Cursors.Hand
            };

            diamondMineButton.Click += btnDiamondMine_Click;

            shopButton = new Button
            {
                Text = "",
                Dock = DockStyle.Top,
                Width = 100,
                Height = 100,
                Image = Image.FromFile("../../../Resources/store.png"),
                Cursor = Cursors.Hand
            };

            shopButton.Click += btnShop_Click;

            equipmentButton = new Button
            {
                Text = "",
                Dock = DockStyle.Top,
                Width = 100,
                Height = 100,
                Image = Image.FromFile("../../../Resources/pickaxe/pickaxeDefault.png"),
                Cursor = Cursors.Hand
            };

            equipmentButton.Click += btnEquipment_Click;

            navPanel.Controls.Add(equipmentButton);
            navPanel.Controls.Add(shopButton);
            navPanel.Controls.Add(diamondMineButton);
            navPanel.Controls.Add(goldMineButton);
            navPanel.Controls.Add(ironMineButton);

            ///////////////////////////////////////////

          

            rightPanel = new Panel
            {
                Dock = DockStyle.Fill
            };

            infoPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(31, 80, 154)
            };

            Label diamondIconLabel = new Label
            {
                Image = Image.FromFile("../../../Resources/diamond.png"),
                Dock = DockStyle.Right,
                Width = 20,
                Height= 40
            };

            diamonodValueLabel = new Label
            {
                Text = $"{mineGame.Diamond}",
                ForeColor = Color.White,
                Dock = DockStyle.Right,
                AutoSize = true,
                Padding = new Padding(0, 13, 0, 0)
            };

            diamonodValueLabel.DataBindings.Add("Text", mineGame, "Diamond", true, DataSourceUpdateMode.OnPropertyChanged, null, "F1");

            Label goldIconLabel = new Label
            {
                Image = Image.FromFile("../../../Resources/gold.png"),
                Dock = DockStyle.Right,
                Width = 20,
                Height = 40
            };

            goldValueLabel = new Label
            {
                Text = $"{mineGame.Gold}",
                ForeColor = Color.White,
                Dock = DockStyle.Right,
                AutoSize = true,
                Padding = new Padding(0, 13, 0, 0)
            };

            goldValueLabel.DataBindings.Add("Text", mineGame, "Gold", true, DataSourceUpdateMode.OnPropertyChanged, null, "F1");


            Label ironIconLabel = new Label
            {
                Image = new Bitmap(Image.FromFile("../../../Resources/iron.png"), new Size(16, 16)),
                Dock = DockStyle.Right,
                Width = 20,
                Height = 40
            };


            ironValueLabel = new Label
            {
                Text = $"{mineGame.Iron}",
                ForeColor = Color.White,
                Dock = DockStyle.Right,
                AutoSize = true,
                Padding = new Padding(0,13,0,0)
            };

            ironValueLabel.DataBindings.Add("Text", mineGame, "Iron", true, DataSourceUpdateMode.OnPropertyChanged, null, "F1");

            infoPanel.Controls.Add(ironValueLabel);
            infoPanel.Controls.Add(ironIconLabel);
            infoPanel.Controls.Add(goldValueLabel);
            infoPanel.Controls.Add(goldIconLabel);
            infoPanel.Controls.Add(diamonodValueLabel);
            infoPanel.Controls.Add(diamondIconLabel);



            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(10, 57, 129)
            };
            rightPanel.Controls.Add(infoPanel);
            rightPanel.Controls.Add(contentPanel);

            Controls.Add(navPanel);
            Controls.Add(rightPanel);

        }

        private void InitializePeriodicTimer()
        {
            periodicTimer = new System.Windows.Forms.Timer
            {
                Interval = 10000
            };

            periodicTimer.Tick += PeriodicTimer_Tick;

            periodicTimer.Start();
        }

        private async void PeriodicTimer_Tick(object sender, EventArgs e)
        {
            mineGame.PointsPerInterval(ResourceType.Iron);
            mineGame.PointsPerInterval(ResourceType.Gold);
            mineGame.PointsPerInterval(ResourceType.Diamond);
        }

        private void LoadPanel(UserControl panel)
        {
            panel.Dock = DockStyle.Fill;
            contentPanel.Controls.Clear();
            contentPanel.Controls.Add(panel);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
            string filePath = Path.Combine(projectRoot, "mineGame.json");

            string json = mineGame.SaveDataToJson();
            File.WriteAllText(filePath, json);
        }

        private void btnIronMine_Click(object sender, EventArgs e)
        {
            LoadPanel(new IronMinePanel());
        }

        private void btnGoldMine_Click(object sender, EventArgs e)
        {
            LoadPanel(new GoldMinePanel());
        }

        private void btnDiamondMine_Click(object sender, EventArgs e)
        {
            LoadPanel(new DiamondMainPanel());
        }

        private void btnShop_Click(object sender, EventArgs e)
        {
            LoadPanel(new ShopPanel());
        }

        private void btnEquipment_Click(object sender, EventArgs e)
        {
            LoadPanel(new EquipmentPanel());
        }
    }
}
