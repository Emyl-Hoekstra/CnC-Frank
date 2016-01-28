namespace CnC_Player
{
    partial class PlayerDashboard
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbxGames = new System.Windows.Forms.ListBox();
            this.btnJoin = new System.Windows.Forms.Button();
            this.lbxCommands = new System.Windows.Forms.ListBox();
            this.lbxPlayers = new System.Windows.Forms.ListBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtNickName = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbxMyGames = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbxCommandFeedback = new System.Windows.Forms.ListBox();
            this.btnLeaveGame = new System.Windows.Forms.Button();
            this.lbxUnits = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbxPlayersInGame = new System.Windows.Forms.ListBox();
            this.btnAddVehicle = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnRotateVehicleLeft = new System.Windows.Forms.Button();
            this.btnRotateVehicleRight = new System.Windows.Forms.Button();
            this.btnBackward = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.lbxPlayerUnits = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtForward = new System.Windows.Forms.TextBox();
            this.txtRotateVehicle = new System.Windows.Forms.TextBox();
            this.txtMoveBackward = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtAuthToken = new System.Windows.Forms.TextBox();
            this.btnSignIn = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.pnlMap = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnFire = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lbxPlayerMapTiles = new System.Windows.Forms.ListBox();
            this.txtPlayerUnitSelected = new System.Windows.Forms.TextBox();
            this.txtGameTestEnvironment = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Games";
            // 
            // lbxGames
            // 
            this.lbxGames.FormattingEnabled = true;
            this.lbxGames.Location = new System.Drawing.Point(268, 74);
            this.lbxGames.Name = "lbxGames";
            this.lbxGames.Size = new System.Drawing.Size(208, 95);
            this.lbxGames.TabIndex = 2;
            this.lbxGames.SelectedIndexChanged += new System.EventHandler(this.lbxGames_SelectedIndexChanged);
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(267, 12);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(124, 30);
            this.btnJoin.TabIndex = 3;
            this.btnJoin.Text = "Join";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // lbxCommands
            // 
            this.lbxCommands.FormattingEnabled = true;
            this.lbxCommands.Location = new System.Drawing.Point(268, 344);
            this.lbxCommands.Name = "lbxCommands";
            this.lbxCommands.Size = new System.Drawing.Size(208, 95);
            this.lbxCommands.TabIndex = 4;
            // 
            // lbxPlayers
            // 
            this.lbxPlayers.FormattingEnabled = true;
            this.lbxPlayers.Location = new System.Drawing.Point(41, 227);
            this.lbxPlayers.Name = "lbxPlayers";
            this.lbxPlayers.Size = new System.Drawing.Size(208, 43);
            this.lbxPlayers.TabIndex = 5;
            this.lbxPlayers.SelectedIndexChanged += new System.EventHandler(this.lbxPlayers_SelectedIndexChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(135, 51);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(114, 20);
            this.txtName.TabIndex = 6;
            // 
            // txtNickName
            // 
            this.txtNickName.Location = new System.Drawing.Point(135, 77);
            this.txtNickName.Name = "txtNickName";
            this.txtNickName.Size = new System.Drawing.Size(114, 20);
            this.txtNickName.TabIndex = 7;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(135, 103);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(114, 20);
            this.txtId.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Games";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(265, 328);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "My commands";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Players (should be one)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(265, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(171, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "My Games (should be one for now)";
            // 
            // lbxMyGames
            // 
            this.lbxMyGames.FormattingEnabled = true;
            this.lbxMyGames.Location = new System.Drawing.Point(268, 201);
            this.lbxMyGames.Name = "lbxMyGames";
            this.lbxMyGames.Size = new System.Drawing.Size(208, 121);
            this.lbxMyGames.TabIndex = 12;
            this.lbxMyGames.SelectedIndexChanged += new System.EventHandler(this.lbxMyGames_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 276);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Command feedback";
            // 
            // lbxCommandFeedback
            // 
            this.lbxCommandFeedback.FormattingEnabled = true;
            this.lbxCommandFeedback.Location = new System.Drawing.Point(41, 292);
            this.lbxCommandFeedback.Name = "lbxCommandFeedback";
            this.lbxCommandFeedback.Size = new System.Drawing.Size(208, 173);
            this.lbxCommandFeedback.TabIndex = 14;
            this.lbxCommandFeedback.SelectedIndexChanged += new System.EventHandler(this.lbxCommandFeedback_SelectedIndexChanged);
            // 
            // btnLeaveGame
            // 
            this.btnLeaveGame.Location = new System.Drawing.Point(397, 10);
            this.btnLeaveGame.Name = "btnLeaveGame";
            this.btnLeaveGame.Size = new System.Drawing.Size(124, 30);
            this.btnLeaveGame.TabIndex = 16;
            this.btnLeaveGame.Text = "Leave";
            this.btnLeaveGame.UseVisualStyleBackColor = true;
            this.btnLeaveGame.Click += new System.EventHandler(this.btnLeaveGame_Click);
            // 
            // lbxUnits
            // 
            this.lbxUnits.FormattingEnabled = true;
            this.lbxUnits.Location = new System.Drawing.Point(493, 74);
            this.lbxUnits.Name = "lbxUnits";
            this.lbxUnits.Size = new System.Drawing.Size(208, 95);
            this.lbxUnits.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(490, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Available units";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(490, 185);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Players in game";
            // 
            // lbxPlayersInGame
            // 
            this.lbxPlayersInGame.FormattingEnabled = true;
            this.lbxPlayersInGame.Location = new System.Drawing.Point(493, 201);
            this.lbxPlayersInGame.Name = "lbxPlayersInGame";
            this.lbxPlayersInGame.Size = new System.Drawing.Size(208, 95);
            this.lbxPlayersInGame.TabIndex = 19;
            // 
            // btnAddVehicle
            // 
            this.btnAddVehicle.Location = new System.Drawing.Point(577, 38);
            this.btnAddVehicle.Name = "btnAddVehicle";
            this.btnAddVehicle.Size = new System.Drawing.Size(124, 30);
            this.btnAddVehicle.TabIndex = 21;
            this.btnAddVehicle.Text = "Add vehicle";
            this.btnAddVehicle.UseVisualStyleBackColor = true;
            this.btnAddVehicle.Click += new System.EventHandler(this.btnAddVehicle_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(716, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Game map";
            // 
            // btnRotateVehicleLeft
            // 
            this.btnRotateVehicleLeft.Location = new System.Drawing.Point(622, 533);
            this.btnRotateVehicleLeft.Name = "btnRotateVehicleLeft";
            this.btnRotateVehicleLeft.Size = new System.Drawing.Size(42, 30);
            this.btnRotateVehicleLeft.TabIndex = 25;
            this.btnRotateVehicleLeft.Text = "<";
            this.btnRotateVehicleLeft.UseVisualStyleBackColor = true;
            this.btnRotateVehicleLeft.Click += new System.EventHandler(this.btnRotateVehicleLeft_Click);
            // 
            // btnRotateVehicleRight
            // 
            this.btnRotateVehicleRight.Location = new System.Drawing.Point(670, 533);
            this.btnRotateVehicleRight.Name = "btnRotateVehicleRight";
            this.btnRotateVehicleRight.Size = new System.Drawing.Size(42, 30);
            this.btnRotateVehicleRight.TabIndex = 27;
            this.btnRotateVehicleRight.Text = ">";
            this.btnRotateVehicleRight.UseVisualStyleBackColor = true;
            this.btnRotateVehicleRight.Click += new System.EventHandler(this.btnRotateVehicleRight_Click);
            // 
            // btnBackward
            // 
            this.btnBackward.Location = new System.Drawing.Point(623, 569);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(89, 30);
            this.btnBackward.TabIndex = 28;
            this.btnBackward.Text = "\\/";
            this.btnBackward.UseVisualStyleBackColor = true;
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(623, 459);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(89, 30);
            this.btnForward.TabIndex = 30;
            this.btnForward.Text = "/\\";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(38, 478);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "Player Units";
            // 
            // lbxPlayerUnits
            // 
            this.lbxPlayerUnits.FormattingEnabled = true;
            this.lbxPlayerUnits.Location = new System.Drawing.Point(41, 494);
            this.lbxPlayerUnits.Name = "lbxPlayerUnits";
            this.lbxPlayerUnits.Size = new System.Drawing.Size(208, 95);
            this.lbxPlayerUnits.TabIndex = 31;
            this.lbxPlayerUnits.SelectedIndexChanged += new System.EventHandler(this.lbxPlayerUnits_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(501, 468);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "Move forward";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(501, 582);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 13);
            this.label13.TabIndex = 34;
            this.label13.Text = "Move backward";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(501, 542);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 13);
            this.label15.TabIndex = 36;
            this.label15.Text = "Rotate vehicle";
            // 
            // txtForward
            // 
            this.txtForward.Location = new System.Drawing.Point(593, 465);
            this.txtForward.Name = "txtForward";
            this.txtForward.Size = new System.Drawing.Size(24, 20);
            this.txtForward.TabIndex = 37;
            // 
            // txtRotateVehicle
            // 
            this.txtRotateVehicle.Location = new System.Drawing.Point(592, 539);
            this.txtRotateVehicle.Name = "txtRotateVehicle";
            this.txtRotateVehicle.Size = new System.Drawing.Size(24, 20);
            this.txtRotateVehicle.TabIndex = 39;
            // 
            // txtMoveBackward
            // 
            this.txtMoveBackward.Location = new System.Drawing.Point(593, 579);
            this.txtMoveBackward.Name = "txtMoveBackward";
            this.txtMoveBackward.Size = new System.Drawing.Size(24, 20);
            this.txtMoveBackward.TabIndex = 40;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(586, 442);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(43, 13);
            this.label16.TabIndex = 41;
            this.label16.Text = "Amount";
            // 
            // txtAuthToken
            // 
            this.txtAuthToken.Location = new System.Drawing.Point(135, 129);
            this.txtAuthToken.Name = "txtAuthToken";
            this.txtAuthToken.Size = new System.Drawing.Size(114, 20);
            this.txtAuthToken.TabIndex = 42;
            // 
            // btnSignIn
            // 
            this.btnSignIn.Location = new System.Drawing.Point(114, 10);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(124, 30);
            this.btnSignIn.TabIndex = 43;
            this.btnSignIn.Text = "Sign in";
            this.btnSignIn.UseVisualStyleBackColor = true;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(38, 54);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(35, 13);
            this.label17.TabIndex = 44;
            this.label17.Text = "Name";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(40, 80);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 13);
            this.label18.TabIndex = 45;
            this.label18.Text = "Nick Name";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(40, 106);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(41, 13);
            this.label19.TabIndex = 46;
            this.label19.Text = "User Id";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(40, 133);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(38, 13);
            this.label20.TabIndex = 47;
            this.label20.Text = "Token";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(42, 156);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(25, 13);
            this.label21.TabIndex = 48;
            this.label21.Text = "Key";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(135, 156);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(114, 20);
            this.txtKey.TabIndex = 49;
            // 
            // pnlMap
            // 
            this.pnlMap.Location = new System.Drawing.Point(719, 77);
            this.pnlMap.Name = "pnlMap";
            this.pnlMap.Size = new System.Drawing.Size(650, 650);
            this.pnlMap.TabIndex = 50;
            // 
            // btnFire
            // 
            this.btnFire.Location = new System.Drawing.Point(625, 500);
            this.btnFire.Name = "btnFire";
            this.btnFire.Size = new System.Drawing.Size(86, 27);
            this.btnFire.TabIndex = 52;
            this.btnFire.Text = "Fire!";
            this.btnFire.UseVisualStyleBackColor = true;
            this.btnFire.Click += new System.EventHandler(this.btnFire_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(38, 644);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(639, 13);
            this.label14.TabIndex = 53;
            this.label14.Text = "1. sign in, 2. select game + join 3. add unit 4. select my game 5.  select availa" +
                                 "ble vehicle + add 6. select my vehicle 7. move and rotate";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(265, 452);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(82, 13);
            this.label22.TabIndex = 55;
            this.label22.Text = "Player MapTiles";
            // 
            // lbxPlayerMapTiles
            // 
            this.lbxPlayerMapTiles.FormattingEnabled = true;
            this.lbxPlayerMapTiles.Location = new System.Drawing.Point(268, 468);
            this.lbxPlayerMapTiles.Name = "lbxPlayerMapTiles";
            this.lbxPlayerMapTiles.Size = new System.Drawing.Size(208, 95);
            this.lbxPlayerMapTiles.TabIndex = 54;
            // 
            // txtPlayerUnitSelected
            // 
            this.txtPlayerUnitSelected.Location = new System.Drawing.Point(515, 404);
            this.txtPlayerUnitSelected.Name = "txtPlayerUnitSelected";
            this.txtPlayerUnitSelected.Size = new System.Drawing.Size(195, 20);
            this.txtPlayerUnitSelected.TabIndex = 56;
            // 
            // txtGameTestEnvironment
            // 
            this.txtGameTestEnvironment.Location = new System.Drawing.Point(135, 182);
            this.txtGameTestEnvironment.Name = "txtGameTestEnvironment";
            this.txtGameTestEnvironment.Size = new System.Drawing.Size(114, 20);
            this.txtGameTestEnvironment.TabIndex = 57;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(40, 185);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 58;
            this.label9.Text = "Testenvironment";
            // 
            // PlayerDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 711);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtGameTestEnvironment);
            this.Controls.Add(this.txtPlayerUnitSelected);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.lbxPlayerMapTiles);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnFire);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pnlMap);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.btnSignIn);
            this.Controls.Add(this.txtAuthToken);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtMoveBackward);
            this.Controls.Add(this.txtRotateVehicle);
            this.Controls.Add(this.txtForward);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lbxPlayerUnits);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnBackward);
            this.Controls.Add(this.btnRotateVehicleRight);
            this.Controls.Add(this.btnRotateVehicleLeft);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnAddVehicle);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lbxPlayersInGame);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbxUnits);
            this.Controls.Add(this.btnLeaveGame);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbxCommandFeedback);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbxMyGames);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.txtNickName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lbxPlayers);
            this.Controls.Add(this.lbxCommands);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.lbxGames);
            this.Controls.Add(this.label1);
            this.Name = "PlayerDashboard";
            this.Text = "CnC Player";
            this.Load += new System.EventHandler(this.PlayerDashboard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbxGames;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.ListBox lbxCommands;
        private System.Windows.Forms.ListBox lbxPlayers;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtNickName;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lbxMyGames;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lbxCommandFeedback;
        private System.Windows.Forms.Button btnLeaveGame;
        private System.Windows.Forms.ListBox lbxUnits;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox lbxPlayersInGame;
        private System.Windows.Forms.Button btnAddVehicle;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnRotateVehicleLeft;
        private System.Windows.Forms.Button btnRotateVehicleRight;
        private System.Windows.Forms.Button btnBackward;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox lbxPlayerUnits;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtForward;
        private System.Windows.Forms.TextBox txtRotateVehicle;
        private System.Windows.Forms.TextBox txtMoveBackward;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtAuthToken;
        private System.Windows.Forms.Button btnSignIn;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Panel pnlMap;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnFire;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ListBox lbxPlayerMapTiles;
        private System.Windows.Forms.TextBox txtPlayerUnitSelected;
        private System.Windows.Forms.TextBox txtGameTestEnvironment;
        private System.Windows.Forms.Label label9;
    }
}

