using FCM.DAO;
using FCM.DTO;
using FCM.UserControls;
using System;
using System.Collections.Generic;
using System.Windows;

namespace FCM.View
{
    /// <summary>
    /// Interaction logic for ResultRecordingWindow.xaml
    /// </summary>
    public partial class ResultRecordingWindow : Window
    {
        public Match match;
        public Team team1, team2, teamNow;
        public bool isTeam1 = true;
        public int round;

        public int penaltyTeam1, penaltyTeam2;

        public List<Goal> listGoalsTeam1 = new List<Goal>();
        public List<Goal> listGoalsTeam2 = new List<Goal>();

        public List<Card> listCardsTeam1 = new List<Card>();
        public List<Card> listCardsTeam2 = new List<Card>();

        public List<Lineups> listLineups_Offical_Team1 = new List<Lineups>();
        public List<Lineups> listLineups_Offical_Team2 = new List<Lineups>();

        public List<Lineups> listLineups_Prep_Team1 = new List<Lineups>();
        public List<Lineups> listLineups_Prep_Team2 = new List<Lineups>();

        public List<SwitchedPlayer> listSwitchedPlayerTeam1 = new List<SwitchedPlayer>();
        public List<SwitchedPlayer> listSwitchedPlayerTeam2 = new List<SwitchedPlayer>();

        public List<TypeOfGoal> typeOfGoals = new List<TypeOfGoal>();

        public int ScoreTeam1 = 0;
        public int ScoreTeam2 = 0;

        public ResultRecordingWindow()
        {
            InitializeComponent();
        }

        public ResultRecordingWindow(Match match)
        {
            InitializeComponent();

            Init(match);
        }
        public int GetTimePlayerReceiveRedCard(Player p)
        {
            for (int i = 0; i < listCardsTeam1.Count; i++)
            {
                if (listCardsTeam1[i].idPlayer == p.id && listCardsTeam1[i].typeOfCard == "Thẻ đỏ")
                {
                    return Int32.Parse(listCardsTeam1[i].time);
                }
            }
            for (int i = 0; i < listCardsTeam2.Count; i++)
            {
                if (listCardsTeam2[i].idPlayer == p.id && listCardsTeam2[i].typeOfCard == "Thẻ đỏ")
                {
                    return Int32.Parse(listCardsTeam2[i].time);
                }
            }
            return int.MaxValue;
        }
        public bool HaveAYellowCard(Player p)
        {
            for (int i = 0; i < listCardsTeam1.Count; i++)
            {
                if (listCardsTeam1[i].idPlayer == p.id && listCardsTeam1[i].typeOfCard == "Thẻ vàng")
                {
                    return true;
                }
            }
            for (int i = 0; i < listCardsTeam2.Count; i++)
            {
                if (listCardsTeam2[i].idPlayer == p.id && listCardsTeam2[i].typeOfCard == "Thẻ vàng")
                {
                    return true;
                }
            }
            return false;
        }
        void setCardInfor()
        {
            for (int i = 0; i < this.listLineups_Offical_Team1.Count; i++)
            {
                this.listLineups_Offical_Team1[i].setCardInfor();
            }
            for (int i = 0; i < this.listLineups_Offical_Team2.Count; i++)
            {
                this.listLineups_Offical_Team2[i].setCardInfor();
            }
            for (int i = 0; i < this.listLineups_Prep_Team1.Count; i++)
            {
                this.listLineups_Prep_Team1[i].setCardInfor();
            }
            for (int i = 0; i < this.listLineups_Prep_Team2.Count; i++)
            {
                this.listLineups_Prep_Team2[i].setCardInfor();
            }
        }
        void Init(Match match)
        {
            this.match = match;

            this.round = this.match.round;

            this.team1 = TeamDAO.Instance.GetTeamById(this.match.idTeam01);
            this.team2 = TeamDAO.Instance.GetTeamById(this.match.idTeam02);

            this.tblName1.Text = team1.nameTeam;
            this.tblName2.Text = team2.nameTeam;

            this.imgLogo1.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(team1.logo));
            this.imgLogo2.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(team2.logo));

            this.cbSelectedTeam.Items.Clear();
            this.cbSelectedTeam.Items.Add(team1.nameTeam);
            this.cbSelectedTeam.Items.Add(team2.nameTeam);
            this.cbSelectedTeam.SelectedIndex = 0;

            this.wpCardsTeam1.Children.Clear();
            this.wpCardsTeam2.Children.Clear();

            this.wpGoalsTeam1.Children.Clear();
            this.wpGoalsTeam2.Children.Clear();

            // Lấy dữ liệu lên
            GetDataFromDatabase();

            setCardInfor();

            LoadLineups(0);

            if (match.allowDraw)
            {
                grdPenalty.Visibility = Visibility.Hidden;

            }
        }
        void GetDataFromDatabase()
        {
            this.listLineups_Offical_Team1 = LineupsDAO.Instance.GetListLineups(this.match.id, this.team1.id, 1);
            this.listLineups_Offical_Team2 = LineupsDAO.Instance.GetListLineups(this.match.id, this.team2.id, 1);

            this.listLineups_Prep_Team1 = LineupsDAO.Instance.GetListLineups(this.match.id, this.team1.id, 0);
            this.listLineups_Prep_Team2 = LineupsDAO.Instance.GetListLineups(this.match.id, this.team2.id, 0);

            // Trận đấu chưa diễn ra
            if (this.match.isStarted == false)
            {
                SetLineupsWhenNotStarted();
            }
            else
            {
                this.listGoalsTeam1 = GoalDAO.Instance.GetListGoals(this.match.id, this.team1.id);
                this.listGoalsTeam2 = GoalDAO.Instance.GetListGoals(this.match.id, this.team2.id);

                this.listCardsTeam1 = CardDAO.Instance.GetListCards(this.match.id, this.team1.id);
                this.listCardsTeam2 = CardDAO.Instance.GetListCards(this.match.id, this.team2.id);

                this.listSwitchedPlayerTeam1 = SwitchedPlayerDAO.Instance.GetListSwitchedPlayer(this.match.id, this.team1.id);
                this.listSwitchedPlayerTeam2 = SwitchedPlayerDAO.Instance.GetListSwitchedPlayer(this.match.id, this.team2.id);

                this.typeOfGoals = TypeOfGoalDAO.Instance.GetListTypeOfGoal(team1.idTournamnt);

                this.penaltyTeam1 = this.match.PenaltyTeam1;
                this.penaltyTeam2 = this.match.PenaltyTeam2;

                this.ScoreTeam1 = this.match.Score1;
                this.ScoreTeam2 = this.match.Score2;

                ShowPenaltyResult();
            }
            LoadLineups(0);
            LoadCard();
            LoadGoalListToWindow();

        }
        public void ShowPenaltyResult()
        {
            this.tblPenaltyScore.Text = this.penaltyTeam1.ToString() + "     -     " + this.penaltyTeam2.ToString();
        }
        public void ShowResultMatch()
        {
            this.tblScore1.Text = this.ScoreTeam1.ToString();
            this.tblScore2.Text = this.ScoreTeam2.ToString();
        }

        public bool isDraw()
        {
            return this.ScoreTeam1 == this.ScoreTeam2;
        }
        // Xóa bỏ dữ liệu cũ
        public void DeleteOldInfor()
        {
            LineupsDAO.Instance.DeleteLineupsByMatchID(this.match.id);
            GoalDAO.Instance.DeleteGoalByMatchID(this.match.id);
            CardDAO.Instance.DeleteCardByMatchID(this.match.id);
            SwitchedPlayerDAO.Instance.DeleteSwitchedPlayersByMatchID(this.match.id);

            this.match.PenaltyTeam1 = 0;
            this.match.PenaltyTeam2 = 0;
            this.match.Score1 = -1;
            this.match.Score2 = -1;
            this.match.isStarted = false;
            MatchDAO.Instance.UpdateMatch(this.match);

        }
        // Cập nhật dữ liệu mới
        public bool UpdateDatabase()
        {
            if (this.match.allowDraw == false)
            {
                if (ScoreTeam1 == ScoreTeam2 && penaltyTeam1 == penaltyTeam2)
                {
                    MessageBoxWindow wd = new MessageBoxWindow(false, "Trận đấu này yêu cầu không được có kết quả hòa!");
                    wd.ShowDialog();
                    return false;
                }

                int idTeamWin = 0;
                if (ScoreTeam1 > ScoreTeam2)
                    idTeamWin = team1.id;
                else
                if (ScoreTeam2 > ScoreTeam1)
                    idTeamWin = team2.id;
                else
                if (ScoreTeam2 == ScoreTeam1 && penaltyTeam1 > penaltyTeam2)
                    idTeamWin = team1.id;
                else
                    idTeamWin = team2.id;
                
                NodeMatchDAO.Instance.UpdateNode(team1.idTournamnt, team1.id, team2.id, idTeamWin);

            }


            // Xóa bỏ dữ liệu cũ
            DeleteOldInfor();

            // Đổ vào dữ liệu mới
            foreach (Lineups l in this.listLineups_Offical_Team1)
            {
                LineupsDAO.Instance.AddLinups(l);
            }
            foreach (Lineups l in this.listLineups_Offical_Team2)
            {
                LineupsDAO.Instance.AddLinups(l);
            }
            foreach (Lineups l in this.listLineups_Prep_Team1)
            {
                LineupsDAO.Instance.AddLinups(l);
            }
            foreach (Lineups l in this.listLineups_Prep_Team2)
            {
                LineupsDAO.Instance.AddLinups(l);
            }

            foreach (Goal g in this.listGoalsTeam1)
            {
                GoalDAO.Instance.AddGoal(g);
            }
            foreach (Goal g in this.listGoalsTeam2)
            {
                GoalDAO.Instance.AddGoal(g);
            }
            foreach (Card c in this.listCardsTeam1)
            {
                CardDAO.Instance.AddCard(c);
            }
            foreach (Card c in this.listCardsTeam2)
            {
                CardDAO.Instance.AddCard(c);
            }
            foreach (SwitchedPlayer s in this.listSwitchedPlayerTeam1)
            {
                SwitchedPlayerDAO.Instance.AddSwitchPlayer(s);
            }
            foreach (SwitchedPlayer s in this.listSwitchedPlayerTeam2)
            {
                SwitchedPlayerDAO.Instance.AddSwitchPlayer(s);
            }

            this.match.PenaltyTeam1 = this.penaltyTeam1;
            this.match.PenaltyTeam2 = this.penaltyTeam2;
            this.match.Score1 = this.ScoreTeam1;
            this.match.Score2 = this.ScoreTeam2;
            this.match.isStarted = true;
            MatchDAO.Instance.UpdateMatch(this.match);
            return true;

        }
        public void WhatTeamIsChosen()
        {
            if (this.cbSelectedTeam.SelectedIndex == 0)
            {
                this.teamNow = team1;
                this.isTeam1 = true;
            }
            else
            {
                this.teamNow = team2;
                this.isTeam1 = false;
            }
        }
        void SetLineupsWhenNotStarted()
        {
            WhatTeamIsChosen();

            this.wpSwitched.Children.Clear();

            List<Lineups> lineupsPrep = new List<Lineups>();
            List<Lineups> lineupsOfficial = new List<Lineups>();


            List<Player> players = PlayerDAO.Instance.GetListPlayer(this.teamNow.id);

            foreach (Player player in players)
            {
                Lineups lineup = new Lineups(this.match.id, player.id, this.teamNow.id, 0, getCardFromPlayer(player));
                lineupsPrep.Add(lineup);
            }

            if (this.isTeam1)
            {
                listLineups_Offical_Team1 = lineupsOfficial;
                listLineups_Prep_Team1 = lineupsPrep;
            }
            else
            {
                listLineups_Offical_Team2 = lineupsOfficial;
                listLineups_Prep_Team2 = lineupsPrep;
            }
        }
        bool haveRedCard(int idPlayer)
        {
            for (int i = 0; i < this.listCardsTeam1.Count; i++)
            {
                if (idPlayer == this.listCardsTeam1[i].idPlayer && this.listCardsTeam1[i].typeOfCard == "Thẻ đỏ")
                {
                    return true;
                }
            }
            for (int i = 0; i < this.listCardsTeam2.Count; i++)
            {
                if (idPlayer == this.listCardsTeam2[i].idPlayer && this.listCardsTeam2[i].typeOfCard == "Thẻ đỏ")
                {
                    return true;
                }
            }

            return false;
        }
        bool haveYellowCard(int idPlayer)
        {
            for (int i = 0; i < this.listCardsTeam1.Count; i++)
            {
                if (idPlayer == this.listCardsTeam1[i].idPlayer && this.listCardsTeam1[i].typeOfCard == "Thẻ vàng")
                {
                    return true;
                }
            }
            for (int i = 0; i < this.listCardsTeam2.Count; i++)
            {
                if (idPlayer == this.listCardsTeam2[i].idPlayer && this.listCardsTeam2[i].typeOfCard == "Thẻ vàng")
                {
                    return true;
                }
            }
            return false;
        }
        void setCardColor(Lineups l)
        {
            int idPlayer = l.idPlayer;
            if (haveRedCard(idPlayer))
            {
                l.card = "Thẻ đỏ";
                return;
            }
            if (haveYellowCard(idPlayer))
            {
                l.card = "Thẻ vàng";
                return;
            }
        }    
        public void LoadLineups(int numTeam)
        {
            WhatTeamIsChosen();

            if (this.isTeam1)
            {
                if (this.listLineups_Prep_Team1.Count == 0 && this.listLineups_Offical_Team1.Count == 0)
                {
                    SetLineupsWhenNotStarted();
                }
            }
            else
            {
                if (this.listLineups_Prep_Team2.Count == 0 && this.listLineups_Offical_Team2.Count == 0)
                {
                    SetLineupsWhenNotStarted();
                }
            }

            List<Lineups> lineupsOfficial = this.isTeam1 ? this.listLineups_Offical_Team1 : this.listLineups_Offical_Team2;
            List<Lineups> lineupsPrep = this.isTeam1 ? this.listLineups_Prep_Team1 : this.listLineups_Prep_Team2;
            List<SwitchedPlayer> switchedPlayers = this.isTeam1 ? this.listSwitchedPlayerTeam1 : this.listSwitchedPlayerTeam2;

            this.wpMainFormation.Children.Clear();
            this.wpReserveFormation.Children.Clear();
            this.wpSwitched.Children.Clear();

            // Thêm vào danh sách "Đội hình trên sân"
            for (int i = 0; i < lineupsOfficial.Count; i++)
            {
                setCardColor(lineupsOfficial[i]);
                ucFootballer ucFootballer = new ucFootballer(lineupsOfficial[i]);
                this.wpMainFormation.Children.Add(ucFootballer);
            }

            // Thêm vào danh sách "đội hình dự bị"
            for (int i = 0; i < lineupsPrep.Count; i++)
            {
                setCardColor(lineupsPrep[i]);
                ucFootballer ucFootballer = new ucFootballer(lineupsPrep[i]);
                this.wpReserveFormation.Children.Add(ucFootballer);
            }

            // Thêm vào danh sách thay người
            if (switchedPlayers != null)
                for (int i = 0; i < switchedPlayers.Count; i++)
                {
                    this.wpSwitched.Children.Add(new ucSwitchedPlayers(switchedPlayers[i], this));
                }

            ResetComboboxAddPlayer();
        }

        public void ResetComboboxAddPlayer()
        {
            WhatTeamIsChosen();

            this.cbSelectPlayerAdd.Items.Clear();

            List<Lineups> lineups;

            if (this.isTeam1)
            {
                lineups = this.listLineups_Prep_Team1;
            }
            else
            {
                lineups = this.listLineups_Prep_Team2;
            }

            for (int i = 0; i < lineups.Count; i++)
            {
                // Thêm các cầu thủ ở đội hình dự bị vào combobox "Thêm người vào sân"
                string numberUniform = PlayerDAO.Instance.GetPlayerById(lineups[i].idPlayer).uniformNumber.ToString();
                string namePlayer = PlayerDAO.Instance.GetPlayerById(lineups[i].idPlayer).namePlayer.ToString();
                this.cbSelectPlayerAdd.Items.Add(numberUniform + ". " + namePlayer);
            }

        }

        public void SwitchPlayer(int indexIn, int indexOut, string minute)
        {
            int idIn, idOut;

            if (this.isTeam1)
            {
                idIn = this.listLineups_Prep_Team1[indexIn].idPlayer;
                idOut = this.listLineups_Offical_Team1[indexOut].idPlayer;

                ChangeFromPrepToOfficial(this.listLineups_Prep_Team1[indexIn]);
                this.listLineups_Offical_Team1.RemoveAt(indexOut);
                this.listLineups_Prep_Team1.RemoveAt(indexIn);
                this.listLineups_Prep_Team1.Add(new Lineups(this.match.id, idOut, team1.id, 0,
                                                            getCardFromPlayer(new Player(this.team1.id,
                                                                                            PlayerDAO.Instance.GetPlayerById(idOut).namePlayer,
                                                                                            PlayerDAO.Instance.GetPlayerById(idOut).uniformNumber,
                                                                                            PlayerDAO.Instance.GetPlayerById(idOut).birthDay,
                                                                                            PlayerDAO.Instance.GetPlayerById(idOut).position,
                                                                                            PlayerDAO.Instance.GetPlayerById(idOut).nationality,
                                                                                            PlayerDAO.Instance.GetPlayerById(idOut).note,
                                                                                            PlayerDAO.Instance.GetPlayerById(idOut).image))));


                SwitchedPlayer switchedPlayer = new SwitchedPlayer(this.match.id, idIn, idOut, team1.id, minute);
                this.listSwitchedPlayerTeam1.Add(switchedPlayer);
            }
            else
            {
                idIn = this.listLineups_Prep_Team2[indexIn].idPlayer;
                idOut = this.listLineups_Offical_Team2[indexOut].idPlayer;

                ChangeFromPrepToOfficial(this.listLineups_Prep_Team2[indexIn]);
                this.listLineups_Offical_Team2.RemoveAt(indexOut);
                this.listLineups_Prep_Team2.RemoveAt(indexIn);
                this.listLineups_Prep_Team2.Add(new Lineups(this.match.id, idOut, team1.id, 0,
                                                            getCardFromPlayer(new Player(this.team1.id,
                                                                                            PlayerDAO.Instance.GetPlayerById(idOut).namePlayer,
                                                                                            PlayerDAO.Instance.GetPlayerById(idOut).uniformNumber,
                                                                                            PlayerDAO.Instance.GetPlayerById(idOut).birthDay,
                                                                                            PlayerDAO.Instance.GetPlayerById(idOut).position,
                                                                                            PlayerDAO.Instance.GetPlayerById(idOut).nationality,
                                                                                            PlayerDAO.Instance.GetPlayerById(idOut).note,
                                                                                            PlayerDAO.Instance.GetPlayerById(idOut).image))));

                SwitchedPlayer switchedPlayer = new SwitchedPlayer(this.match.id, idIn, idOut, team2.id, minute);
                this.listSwitchedPlayerTeam2.Add(switchedPlayer);
            }

            //this.wpSwitched.Children.Add(switchedPlayer);
            LoadLineups(this.cbSelectedTeam.SelectedIndex);
        }

        public void AddPlayerToOfficialLineups()
        {
            WhatTeamIsChosen();

            int index = this.cbSelectPlayerAdd.SelectedIndex;

            if (index < 0) return;

            if (this.isTeam1)
            {
                ChangeFromPrepToOfficial(this.listLineups_Prep_Team1[index]);
                this.listLineups_Prep_Team1.RemoveAt(index);
            }
            else
            {
                ChangeFromPrepToOfficial(this.listLineups_Prep_Team2[index]);
                this.listLineups_Prep_Team2.RemoveAt(index);
            }

            ResetComboboxAddPlayer();
            LoadLineups(this.cbSelectedTeam.SelectedIndex);
        }
        public void ChangeFromPrepToOfficial(Lineups lineup)
        {
            WhatTeamIsChosen();

            lineup.isOfficial = 1;

            if (this.isTeam1)
            {
                this.listLineups_Offical_Team1.Add(lineup);
            }
            else
            {
                this.listLineups_Offical_Team2.Add(lineup);
            }
        }
        public int maxTime(string t1, int t2)
        {
            return t2 > Int32.Parse(t1) ? t2 : Int32.Parse(t1);
        }
        public void AddRedCardWhenHaveTwoYellowCard(Card card)
        {
            if (card.typeOfCard == "Thẻ đỏ")
            {
                return;
            }  

            if (CountYellowCard(card) == 2)
            {
                Card c = new Card(card.idMatchs, card.idPlayer, card.idTeams, card.typeOfCard, getMaxTimeYellowCard(card).ToString());
                AddRedCard(c);
            }
            
        }

        public void DeleteRedCard(Card card)
        {
            for (int i = 0; i < this.listCardsTeam1.Count; i++)
            {
                if (this.listCardsTeam1[i].typeOfCard == "Thẻ đỏ" && this.listCardsTeam1[i].idPlayer == card.idPlayer)
                {
                    this.listCardsTeam1.RemoveAt(i);
                }
            }
            for (int i = 0; i < this.listCardsTeam2.Count; i++)
            {
                if (this.listCardsTeam2[i].typeOfCard == "Thẻ đỏ" && this.listCardsTeam2[i].idPlayer == card.idPlayer)
                {
                    this.listCardsTeam2.RemoveAt(i);
                }
            }
        }
        public void AddRedCard(Card card)
        {
            // Xoá bỏ thẻ đỏ trước đó (nếu có)
            DeleteRedCard(card);

            Card c = new Card(card.idMatchs, card.idPlayer, card.idTeams, "Thẻ đỏ", getMaxTimeYellowCard(card).ToString());
            if (c.idTeams == this.team1.id)
            {
                this.listCardsTeam1.Add(c);
            }
            else
            {
                this.listCardsTeam2.Add(c);
            }

            Player player = PlayerDAO.Instance.GetPlayerById(card.idPlayer);
            DeleteFromOfficial(player);
            InsertIntoPrep(player);

            MessageBoxWindow wd = new MessageBoxWindow(true, "Cầu thủ này đã nhận đủ 2 thẻ vàng, cầu thủ này sẽ tự động thêm thẻ đỏ!");
            wd.ShowDialog();
        }
        public void AddCard(Card card)
        {
            if (CheckCardLogic(card) == false)
            {
                return;
            }

            if (card.idTeams == this.team1.id)
            {
                this.listCardsTeam1.Add(card);
            }
            else
            {
                this.listCardsTeam2.Add(card);
            }

            AddRedCardWhenHaveTwoYellowCard(card);

            LoadCard();
        }
        public void EditCard(Card oldCard, Card newCard)
        {
            if (CheckCardLogic(newCard) == false)
            {
                return;
            }
            
            if (newCard.typeOfCard == "Thẻ đỏ")
            {
                DeleteRedCard(newCard);
            }    

            for (int i = 0; i < this.listCardsTeam1.Count; i++)
            {
                if (this.listCardsTeam1[i].idTeams == oldCard.idTeams &&
                    this.listCardsTeam1[i].idPlayer == oldCard.idPlayer &&
                    this.listCardsTeam1[i].typeOfCard == oldCard.typeOfCard &&
                    this.listCardsTeam1[i].time == oldCard.time)
                {
                    this.listCardsTeam1[i] = newCard;
                }
            }
            for (int i = 0; i < this.listCardsTeam2.Count; i++)
            {
                if (this.listCardsTeam2[i].idTeams == oldCard.idTeams &&
                    this.listCardsTeam2[i].idPlayer == oldCard.idPlayer &&
                    this.listCardsTeam2[i].typeOfCard == oldCard.typeOfCard &&
                    this.listCardsTeam2[i].time == oldCard.time)
                {
                    this.listCardsTeam2[i] = newCard;
                }
            }

            AddRedCardWhenHaveTwoYellowCard(newCard);

            LoadCard();
        }
        public bool CheckCardLogic(Card card)
        {
            if (CountRedCard(card) > 0)
            {
                MessageBoxWindow wd = new MessageBoxWindow(false, "Cầu thủ này đã nhận thẻ đỏ");
                wd.ShowDialog();
                return false;
            }
            if (CountYellowCard(card) == 2)
            {
                MessageBoxWindow wd = new MessageBoxWindow(false, "Cầu thủ này đã nhận 2 thẻ vàng");
                wd.ShowDialog();
                return false;
            }
            return true;
        }
        public void LoadCard()
        {
            this.wpCardsTeam1.Children.Clear();
            this.wpCardsTeam2.Children.Clear();

            for (int i = 0; i < this.listCardsTeam1.Count; i++)
            {
                Card card = this.listCardsTeam1[i];
                int idPlayer = card.idPlayer;
                string uniformNumber = PlayerDAO.Instance.GetPlayerById(idPlayer).uniformNumber.ToString();
                string namePlayer = PlayerDAO.Instance.GetPlayerById(idPlayer).namePlayer.ToString();

                ucCard ucCard = new ucCard(this, new Card(this.match.id, idPlayer, this.team1.id, card.typeOfCard, card.time));

                this.wpCardsTeam1.Children.Add(ucCard);
            }
            for (int i = 0; i < this.listCardsTeam2.Count; i++)
            {
                Card card = this.listCardsTeam2[i];
                int idPlayer = card.idPlayer;
                string uniformNumber = PlayerDAO.Instance.GetPlayerById(idPlayer).uniformNumber.ToString();
                string namePlayer = PlayerDAO.Instance.GetPlayerById(idPlayer).namePlayer.ToString();

                ucCard ucCard = new ucCard(this, new Card(this.match.id, idPlayer, this.team2.id, card.typeOfCard, card.time));

                this.wpCardsTeam2.Children.Add(ucCard);
            }

            LoadLineups(this.cbSelectedTeam.SelectedIndex);
        }
        public int CountYellowCard(Card card)
        {
            int numYellowCard = 0;
            for (int i = 0; i < this.listCardsTeam1.Count; i++)
            {
                if (this.listCardsTeam1[i].idPlayer == card.idPlayer && this.listCardsTeam1[i].typeOfCard == "Thẻ vàng")
                {
                    numYellowCard++;
                }
            }
            for (int i = 0; i < this.listCardsTeam2.Count; i++)
            {
                if (this.listCardsTeam2[i].idPlayer == card.idPlayer && this.listCardsTeam2[i].typeOfCard == "Thẻ vàng")
                {
                    numYellowCard++;
                }
            }
            return numYellowCard;
        }
        public int CountRedCard(Card card)
        {
            int numRedCard = 0;
            for (int i = 0; i < this.listCardsTeam1.Count; i++)
            {
                if (this.listCardsTeam1[i].idPlayer == card.idPlayer && this.listCardsTeam1[i].typeOfCard == "Thẻ đỏ")
                {
                    numRedCard++;
                }
            }
            for (int i = 0; i < this.listCardsTeam2.Count; i++)
            {
                if (this.listCardsTeam2[i].idPlayer == card.idPlayer && this.listCardsTeam2[i].typeOfCard == "Thẻ đỏ")
                {
                    numRedCard++;
                }
            }
            return numRedCard;
        }
        public int getMaxTimeYellowCard(Card card)
        {
            int maxTimeYellowCard = int.MinValue;
            for (int i = 0; i < this.listCardsTeam1.Count; i++)
            {
                if (this.listCardsTeam1[i].idPlayer == card.idPlayer && this.listCardsTeam1[i].typeOfCard == "Thẻ vàng")
                {
                    maxTimeYellowCard = maxTime(listCardsTeam1[i].time, maxTimeYellowCard);
                }
            }
            for (int i = 0; i < this.listCardsTeam2.Count; i++)
            {
                if (this.listCardsTeam2[i].idPlayer == card.idPlayer && this.listCardsTeam2[i].typeOfCard == "Thẻ vàng")
                {
                    maxTimeYellowCard = maxTime(listCardsTeam2[i].time, maxTimeYellowCard);
                }
            }
            return maxTimeYellowCard;
        }
        public void Deletecard(Card card)
        {
            if (CountYellowCard(card) == 2)
            {
                if (card.typeOfCard == "Thẻ đỏ")
                {
                    MessageBoxWindow wd = new MessageBoxWindow(false, "Thẻ đỏ này đi kèm với thẻ vàng thứ 2, không thể xóa!");
                    wd.ShowDialog();
                    return;
                }    
                if (card.typeOfCard == "Thẻ vàng")
                {
                    DeleteRedCard(card);
                    MessageBoxWindow wd = new MessageBoxWindow(true, "Thẻ vàng thứ 2 đã được xóa, thẻ đỏ đi kèm sẽ tự động được xóa theo!");
                    wd.ShowDialog();
                }    
            }
            for (int i = 0; i < this.listCardsTeam1.Count; i++)
            {
                if (this.listCardsTeam1[i].idPlayer == card.idPlayer &&
                    this.listCardsTeam1[i].typeOfCard == card.typeOfCard &&
                    this.team1.id == card.idTeams &&
                    this.listCardsTeam1[i].time == card.time)
                {
                    this.listCardsTeam1.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < this.listCardsTeam2.Count; i++)
            {
                if (this.listCardsTeam2[i].idPlayer == card.idPlayer &&
                    this.listCardsTeam2[i].typeOfCard == card.typeOfCard &&
                    this.team2.id == card.idTeams &&
                    this.listCardsTeam2[i].time == card.time)
                {
                    this.listCardsTeam2.RemoveAt(i);
                    break;
                }
            }
            LoadCard();
        }
        public void AddNewGoal(bool isTeam1, Goal newGoal)
        {
            if (isTeam1)
            {
                this.listGoalsTeam1.Add(newGoal);
                this.ScoreTeam1++;
            }
            else
            {
                this.listGoalsTeam2.Add(newGoal);
                this.ScoreTeam2++;
            }
            if (ScoreTeam1 != ScoreTeam2)
                grdPenalty.Visibility = Visibility.Hidden;
            else
                grdPenalty.Visibility = Visibility.Visible;
            LoadGoalListToWindow();
        }
        public void EditGoal(Goal OldGoal, Goal NewGoal)
        {
            for (int i = 0; i < this.listGoalsTeam1.Count; i++)
            {
                if (this.listGoalsTeam1[i].idPlayerGoals == OldGoal.idPlayerGoals &&
                    this.listGoalsTeam1[i].idPlayerAssist == OldGoal.idPlayerAssist &&
                    this.listGoalsTeam1[i].idTypeOfGoals == OldGoal.idTypeOfGoals &&
                    this.listGoalsTeam1[i].time == OldGoal.time)
                {
                    this.listGoalsTeam1[i] = NewGoal;
                    break;
                }
            }
            for (int i = 0; i < this.listGoalsTeam2.Count; i++)
            {
                if (this.listGoalsTeam2[i].idPlayerGoals == OldGoal.idPlayerGoals &&
                    this.listGoalsTeam2[i].idPlayerAssist == OldGoal.idPlayerAssist &&
                    this.listGoalsTeam2[i].idTypeOfGoals == OldGoal.idTypeOfGoals &&
                    this.listGoalsTeam2[i].time == OldGoal.time)
                {
                    this.listGoalsTeam2[i] = NewGoal;
                    break;
                }
            }
            LoadGoalListToWindow();
        }
        public void DeleteGoal(Goal goal)
        {
            for (int i = 0; i < this.listGoalsTeam1.Count; i++)
            {
                if (this.listGoalsTeam1[i].idPlayerGoals == goal.idPlayerGoals &&
                    this.listGoalsTeam1[i].idPlayerAssist == goal.idPlayerAssist &&
                    this.listGoalsTeam1[i].idTypeOfGoals == goal.idTypeOfGoals &&
                    this.listGoalsTeam1[i].time == goal.time)
                {
                    this.listGoalsTeam1.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < this.listGoalsTeam2.Count; i++)
            {
                if (this.listGoalsTeam2[i].idPlayerGoals == goal.idPlayerGoals &&
                    this.listGoalsTeam2[i].idPlayerAssist == goal.idPlayerAssist &&
                    this.listGoalsTeam2[i].idTypeOfGoals == goal.idTypeOfGoals &&
                    this.listGoalsTeam2[i].time == goal.time)
                {
                    this.listGoalsTeam2.RemoveAt(i);
                    break;
                }
            }
            LoadGoalListToWindow();
        }
        public void LoadGoalListToWindow()
        {
            this.wpGoalsTeam1.Children.Clear();
            this.wpGoalsTeam2.Children.Clear();

            this.tblScore1.Text = this.ScoreTeam1.ToString();
            this.tblScore2.Text = this.ScoreTeam2.ToString();

            for (int i = 0; i < this.listGoalsTeam1.Count; i++)
            {
                ucGoal goal = new ucGoal(this.listGoalsTeam1[i], this);
                this.wpGoalsTeam1.Children.Add(goal);
            }
            for (int i = 0; i < this.listGoalsTeam2.Count; i++)
            {
                ucGoal goal = new ucGoal(this.listGoalsTeam2[i], this);
                this.wpGoalsTeam2.Children.Add(goal);
            }
        }
        public void DeleteSwitched(SwitchedPlayer s)
        {
            Player pIn = new Player();
            Player pOut = new Player();


            // Xóa bỏ khỏi danh sách Switch
            for (int i = 0; i < this.listSwitchedPlayerTeam1.Count; i++)
            {
                if (this.listSwitchedPlayerTeam1[i].idPlayerIn == s.idPlayerIn &&
                    this.listSwitchedPlayerTeam1[i].idPlayerOut == s.idPlayerOut &&
                    this.listSwitchedPlayerTeam1[i].idTeam == s.idTeam &&
                    this.listSwitchedPlayerTeam1[i].time == s.time)
                {
                    pIn = new Player(this.team1.id, PlayerDAO.Instance.GetPlayerById(s.idPlayerIn).namePlayer,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerIn).uniformNumber,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerIn).birthDay,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerIn).position,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerIn).nationality,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerIn).note,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerIn).image);

                    pIn.id = s.idPlayerIn;

                    pOut = new Player(this.team1.id, PlayerDAO.Instance.GetPlayerById(s.idPlayerOut).namePlayer,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerOut).uniformNumber,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerOut).birthDay,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerOut).position,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerOut).nationality,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerOut).note,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerOut).image);

                    pOut.id = s.idPlayerOut;

                    this.listSwitchedPlayerTeam1.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < this.listSwitchedPlayerTeam2.Count; i++)
            {
                if (this.listSwitchedPlayerTeam2[i].idPlayerIn == s.idPlayerIn &&
                    this.listSwitchedPlayerTeam2[i].idPlayerOut == s.idPlayerOut &&
                    this.listSwitchedPlayerTeam2[i].idTeam == s.idTeam &&
                    this.listSwitchedPlayerTeam2[i].time == s.time)
                {
                    pIn = new Player(this.team2.id, PlayerDAO.Instance.GetPlayerById(s.idPlayerIn).namePlayer,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerIn).uniformNumber,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerIn).birthDay,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerIn).position,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerIn).nationality,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerIn).note,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerIn).image);

                    pIn.id = s.idPlayerIn;

                    pOut = new Player(this.team2.id, PlayerDAO.Instance.GetPlayerById(s.idPlayerOut).namePlayer,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerOut).uniformNumber,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerOut).birthDay,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerOut).position,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerOut).nationality,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerOut).note,
                                    PlayerDAO.Instance.GetPlayerById(s.idPlayerOut).image);

                    pOut.id = s.idPlayerOut;

                    this.listSwitchedPlayerTeam2.RemoveAt(i);
                    break;
                }
            }

            // Xóa thằng Out ra khỏi Prep, đẩy lại vào Official
            DeleteFromPrep(pOut);
            InsertIntoOfficial(pOut);

            // Xóa thằng In khỏi Official, đẩy vào lại Prep
            DeleteFromOfficial(pIn);
            InsertIntoPrep(pIn);

            // Xóa dữ liệu thay người khỏi danh sách
            for (int i = 0; i < this.listSwitchedPlayerTeam1.Count; i++)
            {
                if (this.listSwitchedPlayerTeam1[i].idPlayerIn == pIn.id &&
                    this.listSwitchedPlayerTeam1[i].idPlayerOut == pOut.id &&
                    this.team1.id == s.idTeam &&
                    this.listSwitchedPlayerTeam1[i].time == s.time)
                {
                    this.listSwitchedPlayerTeam1.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < this.listSwitchedPlayerTeam2.Count; i++)
            {
                if (this.listSwitchedPlayerTeam2[i].idPlayerIn == pIn.id &&
                    this.listSwitchedPlayerTeam2[i].idPlayerOut == pOut.id &&
                    this.team2.id == s.idTeam &&
                    this.listSwitchedPlayerTeam2[i].time == s.time)
                {
                    this.listSwitchedPlayerTeam2.RemoveAt(i);
                    break;
                }
            }

            // Load lại danh sách Lineups
            LoadLineups(this.cbSelectedTeam.SelectedIndex);
        }
        public void ShowListSwitched()
        {
            for (int i = 0; i < this.listSwitchedPlayerTeam1.Count; i++)
            {
                MessageBoxWindow wd = new MessageBoxWindow(true, TeamDAO.Instance.GetTeamById(this.listSwitchedPlayerTeam1[i].idTeam).nameTeam + "-" +
                    this.listSwitchedPlayerTeam1[i].idPlayerIn + " - " +
                    PlayerDAO.Instance.GetPlayerById(this.listSwitchedPlayerTeam1[i].idPlayerIn).namePlayer + "-" +
                    this.listSwitchedPlayerTeam1[i].idPlayerOut + " - " +
                    PlayerDAO.Instance.GetPlayerById(this.listSwitchedPlayerTeam1[i].idPlayerOut).namePlayer);
                wd.ShowDialog();
            }
            for (int i = 0; i < this.listSwitchedPlayerTeam2.Count; i++)
            {
                MessageBoxWindow wd = new MessageBoxWindow(true, TeamDAO.Instance.GetTeamById(this.listSwitchedPlayerTeam2[i].idTeam).nameTeam + "-" +
                    this.listSwitchedPlayerTeam2[i].idPlayerIn + " - " +
                    PlayerDAO.Instance.GetPlayerById(this.listSwitchedPlayerTeam2[i].idPlayerIn).namePlayer + "-" +
                    this.listSwitchedPlayerTeam2[i].idPlayerOut + " - " +
                    PlayerDAO.Instance.GetPlayerById(this.listSwitchedPlayerTeam2[i].idPlayerOut).namePlayer);
                wd.ShowDialog();
            }
        }
        public void InsertIntoPrep(Player p)
        {
            if (p.idTeam == this.team1.id)
            {
                for (int i = 0; i < this.listLineups_Prep_Team1.Count; i++)
                {
                    if (this.listLineups_Prep_Team1[i].idPlayer == p.id)
                    {
                        return;
                    }
                }

                Lineups l = new Lineups(this.match.id, p.id, this.team1.id, 0, getCardFromPlayer(p));

                this.listLineups_Prep_Team1.Add(l);
            }
            else
            {
                for (int i = 0; i < this.listLineups_Prep_Team2.Count; i++)
                {
                    if (this.listLineups_Prep_Team2[i].idPlayer == p.id)
                    {
                        return;
                    }
                }

                Lineups l = new Lineups(this.match.id, p.id, this.team2.id, 0, getCardFromPlayer(p));

                this.listLineups_Prep_Team2.Add(l);
            }
        }
        public void DeleteFromPrep(Player p)
        {
            for (int i = 0; i < this.listLineups_Prep_Team1.Count; i++)
            {
                if (p.id == this.listLineups_Prep_Team1[i].idPlayer && p.idTeam == this.team1.id)
                {
                    this.listLineups_Prep_Team1.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < this.listLineups_Prep_Team2.Count; i++)
            {
                if (p.id == this.listLineups_Prep_Team2[i].idPlayer && p.idTeam == this.team2.id)
                {
                    this.listLineups_Prep_Team2.RemoveAt(i);
                    break;
                }
            }
        }
        public void InsertIntoOfficial(Player p)
        {
            if (p.idTeam == this.team1.id)
            {
                for (int i = 0; i < this.listLineups_Offical_Team1.Count; i++)
                {
                    if (this.listLineups_Offical_Team1[i].idPlayer == p.id)
                    {
                        return;
                    }
                }

                Lineups l = new Lineups(this.match.id, p.id, this.team1.id, 1, getCardFromPlayer(p));

                this.listLineups_Offical_Team1.Add(l);
            }
            else
            {
                for (int i = 0; i < this.listLineups_Offical_Team2.Count; i++)
                {
                    if (this.listLineups_Offical_Team2[i].idPlayer == p.id)
                    {
                        return;
                    }
                }

                Lineups l = new Lineups(this.match.id, p.id, this.team2.id, 1, getCardFromPlayer(p));

                this.listLineups_Offical_Team2.Add(l);
            }
        }
        public void DeleteFromOfficial(Player p)
        {
            for (int i = 0; i < this.listLineups_Offical_Team1.Count; i++)
            {
                if (p.id == this.listLineups_Offical_Team1[i].idPlayer && p.idTeam == this.team1.id)
                {
                    this.listLineups_Offical_Team1.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < this.listLineups_Offical_Team2.Count; i++)
            {
                if (p.id == this.listLineups_Offical_Team2[i].idPlayer && p.idTeam == this.team2.id)
                {
                    this.listLineups_Offical_Team2.RemoveAt(i);
                    break;
                }
            }
        }
        public string getCardFromPlayer(Player p)
        {
            string cardname = "";

            for (int i = 0; i < this.listCardsTeam1.Count; i++)
            {
                if (p.id == this.listCardsTeam1[i].idPlayer && p.idTeam == this.team1.id)
                {
                    string card = this.listCardsTeam1[i].typeOfCard;
                    cardname = card;
                    if (cardname == "Thẻ đỏ")
                    {
                        break;
                    }
                }
            }
            for (int i = 0; i < this.listCardsTeam2.Count; i++)
            {
                if (p.id == this.listCardsTeam2[i].idPlayer && p.idTeam == this.team1.id)
                {
                    string card = this.listCardsTeam2[i].typeOfCard;
                    cardname = card;
                    if (cardname == "Thẻ đỏ")
                    {
                        break;
                    }
                }
            }
            return cardname;
        }
        public void ChangeLineupsWhenChangeCardInfor(Card card, bool deleteCard = false)
        {
            Lineups lineup = new Lineups(this.match.id, card.idPlayer, card.idTeams, getOfficialInforByPlayerID(card.idPlayer), card.typeOfCard);

            for (int i = 0; i < this.listLineups_Offical_Team1.Count; i++)
            {
                if (this.listLineups_Offical_Team1[i].idPlayer == lineup.idPlayer)
                {
                    this.listLineups_Offical_Team1[i].card = card.typeOfCard;

                    if (deleteCard)
                    {
                        this.listLineups_Offical_Team1[i].card = "";
                    }
                }
            }
            for (int i = 0; i < this.listLineups_Offical_Team2.Count; i++)
            {
                if (this.listLineups_Offical_Team2[i].idPlayer == lineup.idPlayer)
                {
                    this.listLineups_Offical_Team2[i].card = card.typeOfCard;

                    if (deleteCard)
                    {
                        this.listLineups_Offical_Team2[i].card = "";
                    }
                }
            }
            for (int i = 0; i < this.listLineups_Prep_Team1.Count; i++)
            {
                if (this.listLineups_Prep_Team1[i].idPlayer == lineup.idPlayer)
                {
                    this.listLineups_Prep_Team1[i].card = card.typeOfCard;

                    if (deleteCard)
                    {
                        this.listLineups_Prep_Team1[i].card = "";
                    }
                }
            }
            for (int i = 0; i < this.listLineups_Prep_Team2.Count; i++)
            {
                if (this.listLineups_Prep_Team2[i].idPlayer == lineup.idPlayer)
                {
                    this.listLineups_Prep_Team2[i].card = card.typeOfCard;

                    if (deleteCard)
                    {
                        this.listLineups_Prep_Team2[i].card = "";
                    }
                }
            }

            LoadLineups(this.cbSelectedTeam.SelectedIndex);
        }
        int getOfficialInforByPlayerID(int idPlayer)
        {
            for (int i = 0; i < this.listLineups_Offical_Team1.Count; i++)
            {
                if (this.listLineups_Offical_Team1[i].idPlayer == idPlayer)
                {
                    return this.listLineups_Offical_Team1[i].isOfficial;
                }
            }
            for (int i = 0; i < this.listLineups_Offical_Team2.Count; i++)
            {
                if (this.listLineups_Offical_Team2[i].idPlayer == idPlayer)
                {
                    return this.listLineups_Offical_Team2[i].isOfficial;
                }
            }
            for (int i = 0; i < this.listLineups_Prep_Team1.Count; i++)
            {
                if (this.listLineups_Prep_Team1[i].idPlayer == idPlayer)
                {
                    return this.listLineups_Prep_Team1[i].isOfficial;
                }
            }
            for (int i = 0; i < this.listLineups_Prep_Team2.Count; i++)
            {
                if (this.listLineups_Prep_Team2[i].idPlayer == idPlayer)
                {
                    return this.listLineups_Prep_Team2[i].isOfficial;
                }
            }
            return 2;
        }
    }
}
