using System;
using System.Collections.Generic;
using System.Text;
namespace MemoryGameLogic
{
    public class Player
    {
        private string m_Name = "computer";
        private short m_Points = 0;
        private bool m_isMyTurn = false;
        public bool Turn
        {
            get { return m_isMyTurn; }
            set { m_isMyTurn = value; }
        }
        public short Points
        {
            get { return m_Points; }
            set { m_Points = value; }
        }
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public Player(string name)
        {
            this.m_Name = name;
        }
        public void SwitchTurns(Player otherPlayer)
        {
            this.Turn = !this.Turn;
            otherPlayer.Turn = !otherPlayer.Turn;
        }
    }
}