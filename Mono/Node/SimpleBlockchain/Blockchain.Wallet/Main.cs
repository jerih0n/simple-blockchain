using Blockchain.Wallet.Components;
using Blockchain.Wallet.NodeConnection;
using Blockchain.Wallet.Services;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain.Wallet
{
    public partial class MainForm : Form
    {
        private readonly NodeHttpClient _nodeHttpClient;
        private readonly LocalDbService _localDbService;
        private readonly SetWalletControl _setWalletControl;
        private WalletInformation _walletInformation;

        private Panel mainInfoPanel;
        private Panel mainInteractionPanel;
        private Panel panel1;
        private Label statusLb;
        private Label label2;
        private Button refreshConnectionBtn;
        private Label nodeInformation;
        private Label label1;
        private Panel panel2;
        private Panel mainPanel;

        public MainForm()
        {
            InitializeComponent();
            _nodeHttpClient = new NodeHttpClient();
            _localDbService = new LocalDbService();
            _setWalletControl = new SetWalletControl();
            _walletInformation = new WalletInformation(_localDbService.Wallet);
            _walletInformation.OnRefreshBtnClick += _walletInformation_OnRefreshBtnClick;
            _setWalletControl.OnWalletCreation += _setWalletControl_OnWalletCreation;
            OnLoadAsync();
        }

        #region Componnents

        private void InitializeComponent()
        {
            this.mainPanel = new System.Windows.Forms.Panel();
            this.mainInteractionPanel = new System.Windows.Forms.Panel();
            this.mainInfoPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusLb = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.refreshConnectionBtn = new System.Windows.Forms.Button();
            this.nodeInformation = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.mainInfoPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // mainPanel
            //
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainPanel.BackColor = System.Drawing.SystemColors.Control;
            this.mainPanel.Controls.Add(this.mainInteractionPanel);
            this.mainPanel.Controls.Add(this.mainInfoPanel);
            this.mainPanel.Location = new System.Drawing.Point(4, 3);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(959, 401);
            this.mainPanel.TabIndex = 0;
            //
            // mainInteractionPanel
            //
            this.mainInteractionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainInteractionPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainInteractionPanel.Location = new System.Drawing.Point(3, 165);
            this.mainInteractionPanel.Name = "mainInteractionPanel";
            this.mainInteractionPanel.Size = new System.Drawing.Size(949, 226);
            this.mainInteractionPanel.TabIndex = 1;
            //
            // mainInfoPanel
            //
            this.mainInfoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainInfoPanel.Controls.Add(this.panel2);
            this.mainInfoPanel.Controls.Add(this.panel1);
            this.mainInfoPanel.Location = new System.Drawing.Point(8, 9);
            this.mainInfoPanel.Name = "mainInfoPanel";
            this.mainInfoPanel.Size = new System.Drawing.Size(944, 135);
            this.mainInfoPanel.TabIndex = 0;
            //
            // panel2
            //
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Location = new System.Drawing.Point(3, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(936, 65);
            this.panel2.TabIndex = 4;
            //
            // panel1
            //
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.statusLb);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.refreshConnectionBtn);
            this.panel1.Controls.Add(this.nodeInformation);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(936, 35);
            this.panel1.TabIndex = 3;
            //
            // statusLb
            //
            this.statusLb.AutoSize = true;
            this.statusLb.Location = new System.Drawing.Point(600, 7);
            this.statusLb.Name = "statusLb";
            this.statusLb.Size = new System.Drawing.Size(39, 15);
            this.statusLb.TabIndex = 4;
            this.statusLb.Text = "Status";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(555, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Status";
            //
            // refreshConnectionBtn
            //
            this.refreshConnectionBtn.Location = new System.Drawing.Point(3, 3);
            this.refreshConnectionBtn.Name = "refreshConnectionBtn";
            this.refreshConnectionBtn.Size = new System.Drawing.Size(75, 23);
            this.refreshConnectionBtn.TabIndex = 0;
            this.refreshConnectionBtn.Text = "Refresh";
            this.refreshConnectionBtn.UseVisualStyleBackColor = true;
            this.refreshConnectionBtn.Click += new System.EventHandler(this.refreshConnectionBtn_Click);
            //
            // nodeInformation
            //
            this.nodeInformation.AutoSize = true;
            this.nodeInformation.Location = new System.Drawing.Point(202, 7);
            this.nodeInformation.Name = "nodeInformation";
            this.nodeInformation.Size = new System.Drawing.Size(38, 15);
            this.nodeInformation.TabIndex = 2;
            this.nodeInformation.Text = "label2";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connected To Node";
            //
            // MainForm
            //
            this.ClientSize = new System.Drawing.Size(968, 406);
            this.Controls.Add(this.mainPanel);
            this.MinimumSize = new System.Drawing.Size(984, 445);
            this.Name = "MainForm";
            this.mainPanel.ResumeLayout(false);
            this.mainInfoPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Componnents

        private async Task OnLoadAsync()
        {
            this.refreshConnectionBtn.Enabled = false;

            if (_localDbService.Wallet == null || string.IsNullOrEmpty(_localDbService.Wallet.PrivateKeyEnc))
            {
                this.panel2.Controls.Add(_setWalletControl);
            }
            else
            {
                this.panel2.Controls.Add(_walletInformation);
            }
            await PingNode();
        }

        private void refreshConnectionBtn_Click(object sender, EventArgs e)
        {
        }

        private void _walletInformation_OnRefreshBtnClick(object? sender, EventArgs e)
        {
            this.panel2.Controls.Clear();
            this.panel2.Controls.Add(_setWalletControl);
        }

        private async Task PingNode()
        {
            try
            {
                var pingNode = await _nodeHttpClient.PingNodeAsync();
                nodeInformation.Text = pingNode.NodeId;
                statusLb.Text = "Success";
                statusLb.ForeColor = Color.Green;
            }
            catch (Exception)
            {
                nodeInformation.Text = "None";
                statusLb.Text = "Not Connected";
                statusLb.ForeColor = Color.Red;
            }
        }

        private void _setWalletControl_OnWalletCreation(object? sender, EventArgs e)
        {
            _walletInformation = new WalletInformation(_localDbService.Wallet);
            this.panel2.Controls.Clear();
            this.panel2.Controls.Add(_walletInformation);
        }
    }
}