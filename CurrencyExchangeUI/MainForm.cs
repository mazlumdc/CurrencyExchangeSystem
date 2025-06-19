using System;
using System.Windows.Forms;
using System.ServiceModel;
using System.Threading.Tasks;
using CurrencyExchangeService;
using System.Drawing;
using System.IO;

namespace CurrencyExchangeUI
{
    public partial class MainForm : Form
    {
        private ICurrencyExchangeService _service;
        private string _currentUser;
        private Panel _loginPanel;
        private Panel _mainPanel;
        private double _usdRate;
        private double _eurRate;
        private double _gbpRate;
        private double _plnRate;

        // Rate labels
        private Label usdRateLabel;
        private Label eurRateLabel;
        private Label gbpRateLabel;
        private Label plnRateLabel;

        // Balance labels
        private Label usdBalanceLabel;
        private Label eurBalanceLabel;
        private Label gbpBalanceLabel;
        private Label plnBalanceLabel;

        public MainForm()
        {
            InitializeComponent();
            InitializeService();
            ShowLoginPanel();
        }

        private void InitializeService()
        {
            // Create connection to our WCF service
            var factory = new ChannelFactory<ICurrencyExchangeService>(
                new BasicHttpBinding(),
                new EndpointAddress("http://localhost:8733/CurrencyExchangeService")
            );
            _service = factory.CreateChannel();
        }

        private void InitializeComponent()
        {
            // Form setup
            this.Text = "Currency Exchange Office";
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Create login panel
            _loginPanel = new Panel
            {
                Size = new Size(400, 500),
                Location = new Point((this.ClientSize.Width - 400) / 2, (this.ClientSize.Height - 500) / 2),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Fotoğraf eklemek için PictureBox kullanın
            var pictureBox = new PictureBox
            {
                Size = new Size(200, 200),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point((_loginPanel.Width - 200) / 2, 50)
            };
            try
            {
                string imagePath = Path.Combine(Application.StartupPath, "Resources", "vizja.png");
                if (File.Exists(imagePath))
                {
                    pictureBox.Image = Image.FromFile(imagePath);
                }
                else
                {
                    MessageBox.Show("Image file not found: " + imagePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
            _loginPanel.Controls.Add(pictureBox);

            // Username input
            var usernameLabel = new Label
            {
                Text = "Username:",
                AutoSize = true,
                Location = new Point((_loginPanel.Width - 200) / 2, 270),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular)
            };
            _loginPanel.Controls.Add(usernameLabel);

            var usernameTextBox = new TextBox
            {
                Size = new Size(200, 30),
                Location = new Point((_loginPanel.Width - 200) / 2, 300),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular)
            };
            _loginPanel.Controls.Add(usernameTextBox);

            // Password input
            var passwordLabel = new Label
            {
                Text = "Password:",
                AutoSize = true,
                Location = new Point((_loginPanel.Width - 200) / 2, 340),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular)
            };
            _loginPanel.Controls.Add(passwordLabel);

            var passwordTextBox = new TextBox
            {
                Size = new Size(200, 30),
                Location = new Point((_loginPanel.Width - 200) / 2, 370),
                PasswordChar = '•',
                Font = new Font("Segoe UI", 10F, FontStyle.Regular)
            };
            _loginPanel.Controls.Add(passwordTextBox);

            // Login button
            var loginButton = new Button
            {
                Text = "Login",
                Size = new Size(200, 40),
                Location = new Point((_loginPanel.Width - 200) / 2, 420),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.Click += LoginButton_Click;
            _loginPanel.Controls.Add(loginButton);

            // Create main panel (initially hidden)
            _mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                Visible = false
            };

            // Add logout button
            Button logoutButton = new Button
            {
                Text = "Logout",
                Size = new Size(100, 30),
                Location = new Point(650, 10),
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            logoutButton.Click += (s, e) => ShowLoginPanel();
            _mainPanel.Controls.Add(logoutButton);

            // Currency exchange controls
            var fromCurrencyLabel = new Label
            {
                Text = "From Currency:",
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };
            var fromCurrencyBox = new ComboBox
            {
                Location = new System.Drawing.Point(120, 20),
                Width = 100,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            fromCurrencyBox.Items.AddRange(new string[] { "PLN", "USD", "EUR", "GBP" });

            var toCurrencyLabel = new Label
            {
                Text = "To Currency:",
                Location = new System.Drawing.Point(20, 50),
                AutoSize = true
            };
            var toCurrencyBox = new ComboBox
            {
                Location = new System.Drawing.Point(120, 50),
                Width = 100,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            toCurrencyBox.Items.AddRange(new string[] { "PLN", "USD", "EUR", "GBP" });

            var amountLabel = new Label
            {
                Text = "Amount:",
                Location = new System.Drawing.Point(20, 80),
                AutoSize = true
            };
            var amountBox = new NumericUpDown
            {
                Location = new System.Drawing.Point(120, 80),
                Width = 100,
                Minimum = 1,
                Maximum = 1000000,
                DecimalPlaces = 2
            };

            // Exchange rate display
            var rateLabel = new Label
            {
                Text = "Current Rate:",
                Location = new System.Drawing.Point(20, 110),
                AutoSize = true
            };
            var rateValue = new Label
            {
                Location = new System.Drawing.Point(120, 110),
                AutoSize = true
            };

            // Update rate button
            var updateRateButton = new Button
            {
                Text = "Update Rate",
                Location = new System.Drawing.Point(20, 140),
                Width = 100
            };
            updateRateButton.Click += (s, e) =>
            {
                try
                {
                    var rate = _service.GetExchangeRate(fromCurrencyBox.Text, toCurrencyBox.Text);
                    rateValue.Text = string.Format("1 {0} = {1:N4} {2}", 
                        fromCurrencyBox.Text, 
                        rate, 
                        toCurrencyBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error getting rate: {0}", ex.Message));
                }
            };

            // Exchange button
            var exchangeButton = new Button
            {
                Text = "Exchange",
                Location = new System.Drawing.Point(20, 170),
                Width = 100
            };
            exchangeButton.Click += (s, e) =>
            {
                try
                {
                    var result = _service.BuyCurrency(
                        _currentUser,
                        fromCurrencyBox.Text,
                        toCurrencyBox.Text,
                        (double)amountBox.Value
                    );
                    MessageBox.Show("Exchange successful!");
                    ShowMainPanel();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error exchanging currency: {0}", ex.Message));
                }
            };

            // --- KUR LABEL'LARI ---
            usdRateLabel = new Label { Text = "USD Rate: -", Location = new System.Drawing.Point(300, 20), AutoSize = true };
            eurRateLabel = new Label { Text = "EUR Rate: -", Location = new System.Drawing.Point(300, 50), AutoSize = true };
            gbpRateLabel = new Label { Text = "GBP Rate: -", Location = new System.Drawing.Point(300, 80), AutoSize = true };
            plnRateLabel = new Label { Text = "PLN Rate: -", Location = new System.Drawing.Point(300, 110), AutoSize = true };
            _mainPanel.Controls.Add(usdRateLabel);
            _mainPanel.Controls.Add(eurRateLabel);
            _mainPanel.Controls.Add(gbpRateLabel);
            _mainPanel.Controls.Add(plnRateLabel);

            // --- BAKİYE LABEL'LARI ---
            usdBalanceLabel = new Label { Text = "USD Balance: -", Location = new System.Drawing.Point(500, 20), AutoSize = true };
            eurBalanceLabel = new Label { Text = "EUR Balance: -", Location = new System.Drawing.Point(500, 50), AutoSize = true };
            gbpBalanceLabel = new Label { Text = "GBP Balance: -", Location = new System.Drawing.Point(500, 80), AutoSize = true };
            plnBalanceLabel = new Label { Text = "PLN Balance: -", Location = new System.Drawing.Point(500, 110), AutoSize = true };
            _mainPanel.Controls.Add(usdBalanceLabel);
            _mainPanel.Controls.Add(eurBalanceLabel);
            _mainPanel.Controls.Add(gbpBalanceLabel);
            _mainPanel.Controls.Add(plnBalanceLabel);

            // Add controls to main panel
            _mainPanel.Controls.AddRange(new Control[] {
                fromCurrencyLabel, fromCurrencyBox,
                toCurrencyLabel, toCurrencyBox,
                amountLabel, amountBox,
                rateLabel, rateValue,
                updateRateButton, exchangeButton
            });

            // Add panels to form
            this.Controls.Add(_loginPanel);
            this.Controls.Add(_mainPanel);
        }

        private void ShowLoginPanel()
        {
            _loginPanel.BringToFront();
            _loginPanel.Visible = true;
            _mainPanel.Visible = false;
        }

        private void ShowMainPanel()
        {
            _mainPanel.BringToFront();
            _mainPanel.Visible = true;
            _loginPanel.Visible = false;
            try
            {
                // Get exchange rates
                var rates = _service.GetExchangeRates().Result;
                if (rates != null)
                {
                    _usdRate = rates.USD;
                    _eurRate = rates.EUR;
                    _gbpRate = rates.GBP;
                    _plnRate = rates.PLN;

                    // Update rate labels
                    usdRateLabel.Text = string.Format("USD Rate: {0:F2}", _usdRate);
                    eurRateLabel.Text = string.Format("EUR Rate: {0:F2}", _eurRate);
                    gbpRateLabel.Text = string.Format("GBP Rate: {0:F2}", _gbpRate);
                    plnRateLabel.Text = string.Format("PLN Rate: {0:F2}", _plnRate);
                }
                else
                {
                    MessageBox.Show("Could not get exchange rates");
                }

                // Get user balances
                var balances = _service.GetBalances(_currentUser);
                if (balances != null)
                {
                    usdBalanceLabel.Text = string.Format("USD Balance: {0:F2}", balances.ContainsKey("USD") ? balances["USD"] : 0);
                    eurBalanceLabel.Text = string.Format("EUR Balance: {0:F2}", balances.ContainsKey("EUR") ? balances["EUR"] : 0);
                    gbpBalanceLabel.Text = string.Format("GBP Balance: {0:F2}", balances.ContainsKey("GBP") ? balances["GBP"] : 0);
                    plnBalanceLabel.Text = string.Format("PLN Balance: {0:F2}", balances.ContainsKey("PLN") ? balances["PLN"] : 0);
                }
                else
                {
                    MessageBox.Show("Could not get user balances");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading main panel: " + ex.Message);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_loginPanel != null)
            {
                // Update login panel position when form is resized
                _loginPanel.Location = new Point((this.ClientSize.Width - _loginPanel.Width) / 2, 
                                                (this.ClientSize.Height - _loginPanel.Height) / 2);
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                var usernameTextBox = _loginPanel.Controls[2] as TextBox;
                var passwordTextBox = _loginPanel.Controls[4] as TextBox;
                var result = _service.CreateAccount(usernameTextBox.Text, passwordTextBox.Text);
                if (result)
                {
                    _currentUser = usernameTextBox.Text;
                    ShowMainPanel();
                }
                else
                {
                    MessageBox.Show("Account already exists");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
} 