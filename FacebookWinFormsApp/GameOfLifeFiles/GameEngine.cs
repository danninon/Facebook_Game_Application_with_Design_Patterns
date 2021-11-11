﻿namespace BasicFacebookFeatures.GameOfLifeFiles
{
    public class GameEngine
    {
        private GameBoard m_GameBoard;

        public GameBoard GameBoard => m_GameBoard;

        public int GameRows => k_GameRows;

        public int GameColumns => k_GameColumns;

        private readonly int k_GameRows;
        private readonly int k_GameColumns;

        public GameEngine(int i_GameRows, int i_GameColumns)
        {
            Rounds = 0;
            k_GameRows = i_GameRows;
            k_GameColumns = i_GameColumns;
            m_GameBoard = new GameBoard(k_GameRows,k_GameColumns);
        }

        private int getNeighbors(int i_RowIndex, int i_ColumnIndex, bool[,] i_Tmp)
        {
            int numOfAliveNeighbors = 0;
            for (int rowIndex = i_RowIndex - 1; rowIndex < i_RowIndex + 2; rowIndex++)
            {
                for (int columnIndex = i_ColumnIndex - 1; columnIndex < i_ColumnIndex + 2; columnIndex++)
                {
                    if (!(i_RowIndex == rowIndex && i_ColumnIndex == columnIndex))//if not the caller
                    {
                        if (!((rowIndex < 0 || columnIndex < 0) || (rowIndex >= k_GameRows || columnIndex >= k_GameColumns)))
                        {
                            if (i_Tmp[rowIndex, columnIndex] == true)
                            {
                                numOfAliveNeighbors++;
                            }
                        }
                    }
                }
            }
            return numOfAliveNeighbors; //always counts itself
        }

        public void UpdateToNextGeneration()
        {

            bool[,] tmp = new bool[k_GameRows, k_GameColumns];
            for (int i = 0; i < k_GameRows; ++i)
            {
                for (int j = 0; j < k_GameColumns; ++j)
                {
                    tmp[i, j] = m_GameBoard.GameMatrix[i, j];
                }
            }


            for (int rowIndex = 0; rowIndex < k_GameRows; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < k_GameColumns; columnIndex++)
                {
                    int numOfAliveNeighbors = getNeighbors(rowIndex, columnIndex, tmp);

                    if (m_GameBoard.GameMatrix[rowIndex, columnIndex] == true)
                    {
                        if (numOfAliveNeighbors < 2)
                        {
                            m_GameBoard.GameMatrix[rowIndex, columnIndex] = false;
                        }

                        if (numOfAliveNeighbors > 3)
                        {
                            m_GameBoard.GameMatrix[rowIndex, columnIndex] = false;
                        }
                    }
                    else
                    {
                        if (numOfAliveNeighbors == 3)
                        {
                            m_GameBoard.GameMatrix[rowIndex, columnIndex] = true;
                        }
                    }
                }
            }
            m_Rounds++;
        }
        private int m_Rounds;
        public int Rounds
        {
            get => m_Rounds;
            private set => m_Rounds = value;
        }

        public void Restart()
        {
            m_GameBoard.CleanBoard();
        }
    }
}
