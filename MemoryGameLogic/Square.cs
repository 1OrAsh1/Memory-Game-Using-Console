using System;
using System.Collections.Generic;
using System.Text;
namespace MemoryGameLogic
{
    public class Square
    {
        private char x;
        private char y;
        private char value;
        private bool showValue = false;
        public void SetXY(int x, int y)
        {
            this.x = (char)(x + '1');
            this.y = (char)(y + 'A');
        }
        public void SetShowValue(bool showValue)
        {
            this.showValue = showValue;
        }
        public bool GetShowValue()
        {
            return this.showValue;
        }
        public void SetValue(char value)
        {
            this.value = value;
        }
        public char GetValue()
        {
            return this.value;
        }
    }
}