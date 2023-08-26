namespace FourInARow
{
    internal class Asimon
    {
        private int row;
        private int col;
        private int player;

        public Asimon(int row, int col, int player)
        {
            this.row = row;
            this.col = col;
            this.player = player;
        }

        // set and get methods

        public int GetRow()
        {
            return row;
        }
        public int GetCol()
        {
            return col;
        }
        public void SetRow(int row)
        {
            this.row = row;
        }
        public void SetCol(int col)
        {
            this.col = col;
        }
        public int GetPlayer()
        {
            return player;
        }
        public void SetPlayer(int player)
        {
            this.player = player;
        }
    }
}
