using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoBang_Lib
{
    public class Tools
    {
        private  static readonly int Max =100000000; //＋∞
        public static int deepMax=4;
        public static int count = 0,cut=0;

        public static bool IsWin(ChessBoard chessBoard, int x, int y)
        {
            Type[,] bd = new Type[15, 15];

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    bd[i, j] = chessBoard.Board[i, j].type;
                }
            }
            Type chess = bd[x, y];

            //横------------------------------------------------------------------------------------------------------------------------------------------
            int left, right;
            left = x >= 4 ? x - 4 : 0;
            right = x <= 10 ? x + 4 : 14;
            for (int i = left; i <= right - 4; i++)
            {
                if (bd[i, y] == chess && bd[i + 1, y] == chess && bd[i + 2, y] == chess && bd[i + 3, y] == chess && bd[i + 4, y] == chess)
                    return true;
            }
            //竖------------------------------------------------------------------------------------------------------------------------------------------
            int top, bot;
            top = y >= 4 ? y - 4 : 0;
            bot = y <= 10 ? y + 4 : 14;
            for (int i = top; i <= bot - 4; i++)
            {
                if (bd[x, i] == chess && bd[x, i + 1] == chess && bd[x, i + 2] == chess && bd[x, i + 3] == chess && bd[x, i + 4] == chess)
                    return true;
            }
            //捺------------------------------------------------------------------------------------------------------------------------------------------
            int count = 1;
            bool iscontinue = false;
            for (int i = -4; i <= 4; i++)
            {
                if (0 <= x + i && x + i <= 14 && 0 <= y + i && y + i <= 14)
                {
                    if (bd[x + i, y + i] == chess)
                    {

                        if (iscontinue)
                        { count++; }
                        iscontinue = true;
                    }
                    else
                    {
                        iscontinue = false;
                        count = 1;
                    }
                    if (count == 5) return true;
                }
            }
            //撇------------------------------------------------------------------------------------------------------------------------------------------
            count = 1; iscontinue = false;
            for (int i = -4; i <= 4; i++)
            {
                if (0 <= x + i && x + i <= 14 && 0 <= y - i && y - i <= 14)
                {
                    if (bd[x + i, y - i] == chess)
                    {
                        if (iscontinue)
                            count++;
                        iscontinue = true;
                    }
                    else
                    {
                        iscontinue = false;
                        count = 1;
                    }
                    if (count == 5) return true;
                }
            }
            return false;
        }

        public static Point AI_1(ChessBoard chessBoard, Type typeofcom)    //level_1;只会防御(只对人的空白模拟下棋评估取最高下棋)
        {
            Type[,] typeboard  = ChessBoard_Trans_TypeBoard(chessBoard);

            Type type = typeofcom == GoBang_Lib.Type.White ? GoBang_Lib.Type.Black : GoBang_Lib.Type.White;

            int[,] scoretable = GetScoreTable(typeboard,type);

            Point[] bestpointarray = new Point[225];

            int best = 0, count = 1;

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    int score = scoretable[i, j];
                    if (score > best)
                    {
                        best = score;
                        bestpointarray = new Point[225];
                        bestpointarray[0] = new Point(i, j);
                        count = 1;
                    }
                    else if (score == best)
                    {
                        bestpointarray[count] = new Point(i,j);
                        count++;
                    }
                }
            }
            Random random = new Random();
            int rd = random.Next(0, count);
            return bestpointarray[rd];

        }
   
        public static Point AI_2(ChessBoard chessBoard, Type typeofcom)  //level_2:会进攻了(从人和电脑的空白模拟下棋评分取最高分下棋)
        {
            Type[,] typeboard = ChessBoard_Trans_TypeBoard(chessBoard);

            Type typeofman = typeofcom == GoBang_Lib.Type.White ? GoBang_Lib.Type.Black : GoBang_Lib.Type.White;

            int[,] scoretableofman = GetScoreTable(typeboard, typeofman), scoretableofcom = GetScoreTable(typeboard, typeofcom);

            Point[] bestpointarrayofman = new Point[225], bestpointarrayofcom = new Point[225];


            int bestofman = 0, countofman = 1;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    int scoreofman = scoretableofman[i, j];
                    if (scoreofman > bestofman)
                    {
                        bestofman = scoreofman;
                        bestpointarrayofman = new Point[225];
                        bestpointarrayofman[0] = new Point(i, j);
                        countofman = 1;
                    }
                    else if (scoreofman == bestofman)
                    {
                        bestpointarrayofman[countofman] = new Point(i, j);
                        countofman++;
                    }
                }
            }


            int bestofcom = 0, countofcom = 1;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    int scoreofcom = scoretableofcom[i, j];
                    if (scoreofcom > bestofcom)
                    {
                        bestofcom = scoreofcom;
                        bestpointarrayofcom = new Point[225];
                        bestpointarrayofcom[0] = new Point(i, j);
                        countofcom = 1;
                    }
                    else if (scoreofcom == bestofcom)
                    {
                        bestpointarrayofcom[countofcom] = new Point(i, j);
                        countofcom++;
                    }
                }
            }
            Random random = new Random();
            if(bestofman>=bestofcom)
            {
                
                int rd = random.Next(0, countofman);
                return bestpointarrayofman[rd];        
            }
            else
            {
                int rd = random.Next(0, countofcom);
                return bestpointarrayofcom[rd];
            }
        }

        public static Point AI_3(ChessBoard chessBoard, Type typeofcom)  //level_3:攻防兼备(从人和电脑的空白模拟下棋评分取其之和的最高分下棋)
        {
            Type[,] typeboard = ChessBoard_Trans_TypeBoard(chessBoard);

            Type typeofman = typeofcom == GoBang_Lib.Type.White ? GoBang_Lib.Type.Black : GoBang_Lib.Type.White;

            int[,] scoretableofman = GetScoreTable(typeboard, typeofman), scoretableofcom = GetScoreTable(typeboard, typeofcom), scoretabel = new int[15, 15];

            Point[] bestpointarray = new Point[225];

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    scoretabel[i, j] = scoretableofcom[i, j] + scoretableofman[i, j];
                }
            }

            int best = 0, count = 0;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    int score = scoretabel[i, j];
                    if (score > best)
                    {
                        best = score;
                        bestpointarray = new Point[225];
                        bestpointarray[0] = new Point(i, j);
                        count = 1;
                    }
                    else if (score == best)
                    {
                        bestpointarray[count] = new Point(i, j);
                        count++;
                    }
                }
            }

            Random random = new Random();

            int rd = random.Next(0, count);
            return bestpointarray[rd];
        }

        public static Point MaxMin(ChessBoard chessBoard,Type type)
        {
            count = 0;cut = 0;
            int best = -Max;

            Type typeofman = type == Type.Black ? Type.White : Type.Black;

            Type[,] typeboard = ChessBoard_Trans_TypeBoard(chessBoard);

            Point[] nextposition = GeneraterNextPosition(typeboard, type);

            Stack<Point>  bestposition =new Stack<Point>(225);

            for(int i=0;i<nextposition.Length;i++)
            {
                count++;//-----------------------------------
                Point newpoint = nextposition[i];
                typeboard[newpoint.X, newpoint.Y] = type;
                int value = Min_value(typeboard, typeofman, deepMax - 1,-Max,Max);

                if (value == best) { bestposition.Push(newpoint); }
                else if (value > best) { best = value;bestposition.Clear();bestposition.Push(newpoint); }
                typeboard[newpoint.X, newpoint.Y] = Type.Empety;
            }

            Random random = new Random();
            return (bestposition.ToArray())[random.Next(0, bestposition.ToArray().Length)];
        }

        public static int Max_value(Type[,] typeboard,Type type,int deep,int alpha,int beta)
        {
            int value = GetScore_chessboard(typeboard);
            if (deep <= 0 || value >= (int)Score.Five) return value;

            int best = -Max;
            Point[] nextposition = GeneraterNextPosition(typeboard, type);
            Type anothertype = type == Type.Black ? Type.White : Type.Black;

            for (int i=0;i<nextposition.Length;i++)
            {
                count++;//-----------------------------------
                Point newpoint = nextposition[i];

                typeboard[newpoint.X, newpoint.Y] = type;    
                int v = Min_value(typeboard,anothertype,deep-1,alpha,beta);
                typeboard[newpoint.X, newpoint.Y] = Type.Empety;

                if (v > best)
                    best = v;
                if (best > alpha)
                    alpha = best;
                if (beta <= alpha)
                {
                    cut++;
                    break;
                }
            }

            return best;
        }
        public static int Min_value(Type[,] typeboard, Type type, int deep, int alpha, int beta)
        {
            int value = GetScore_chessboard(typeboard);
            if (deep <= 0 || value >= (int)Score.Five) return value;

            int best = Max;
            Point[] nextposition = GeneraterNextPosition(typeboard, type);
            Type anothertype = type == Type.Black ? Type.White : Type.Black;

            for (int i = 0; i < nextposition.Length; i++)
            {
                count++;//-----------------------------------
                Point newpoint = nextposition[i];
                typeboard[newpoint.X, newpoint.Y] = type;
                int v = Max_value(typeboard, anothertype, deep - 1,alpha,beta);
                typeboard[newpoint.X, newpoint.Y] = Type.Empety;
                if (v < best) best = v;
                if (best < beta) beta = best;
                if (beta <= alpha)
                {
                    cut++;
                    break;
                }
            }
            return best;
        }

        //启发式搜索评估函数
        public static Point[] GeneraterNextPosition(Type[,] typeboard, Type typeofcom)//type是电脑的棋色,返回当前棋局最值得下的地方
        {        
            Stack<Point>
                five = new Stack<Point>(5),
                four = new Stack<Point>(10),

                twothree = new Stack<Point>(10), 
                three = new Stack<Point>(225), 
                two = new Stack<Point>(225), 
                nei = new Stack<Point>(225);

            Point[] points = new Point[1] { new Point(7, 7) };

            
            Point[] originalarray = GetChessArray_HasNeighbor(typeboard);//按照远近邻居排序成队列的可行棋
            if(originalarray.Length==0)
            {
                return points;
            }
            Type typeofhum = typeofcom == Type.Black ? Type.White : Type.Black;

            for (int i = 0; i < originalarray.Length; i++)
            {
                int
                scoreofcom = GetScore_emptyPoint(typeboard, typeofcom, originalarray[i]),
                scoreofhum = GetScore_emptyPoint(typeboard, typeofhum, originalarray[i]);

                    if (scoreofcom >= (int)Score.Five)
                    {
                        five.Push(originalarray[i]);
                        return five.ToArray();
                    }
                    else if (scoreofhum >= (int)Score.Five)
                    {
                        five.Push(originalarray[i]);
                    }
                    else if (scoreofcom >= (int)Score.Four)
                    {
                        four.Push(originalarray[i]);
                    }
                    else if (scoreofhum >= (int)Score.Four)
                    {
                        four.Push(originalarray[i]);
                    }                   
                    else if (scoreofcom >= 2 * (int)Score.Three)
                    {
                        twothree.Reverse();
                        twothree.Push(originalarray[i]);
                        twothree.Reverse();
                    }
                    else if (scoreofhum >= 2 * (int)Score.Three)
                    {
                        twothree.Push(originalarray[i]);
                    }

                    else if (scoreofcom >= (int)Score.Three)
                    {
                        three.Reverse();
                        three.Push(originalarray[i]);
                        three.Reverse();
                    }
                    else if (scoreofhum >= (int)Score.Three)
                    {

                        three.Push(originalarray[i]);
                    }

                    else if (scoreofcom >= (int)Score.Two)
                    {
                        two.Reverse();
                        two.Push(originalarray[i]);
                        two.Reverse();
                    }
                    else if (scoreofhum >= (int)Score.Two)
                    {
                        two.Push(originalarray[i]);
                    }
                    else nei.Push(originalarray[i]);
            }
            if (five.Count != 0)
            {
                return five.ToArray();
            }
            if (four.Count != 0)
            {
                return four.ToArray();
            }
            if (twothree.Count != 0)
            {
                return twothree.ToArray();
            }
            return three.Concat(two.Concat(nei)).ToArray();
        }

        //对局面打分---------------------------------------------------------------------------------------------
        public static int GetScore_chessboard(Type[,] typeboard)//对当前局面打分 站在白棋角度，用白色分-黑色分。返回值越高对白色越有利
        {
            Type[][] heng = new Type[15][];
            Type[][] shu = new Type[15][];
            Type[][] pie = new Type[29][];
            Type[][] na = new Type[29][];

            for(int i=0;i<29;i++)
            {
                if (i < 15)
                {
                    heng[i] = new Type[15];
                    shu[i] = new Type[15];
                }
            }


            for (int i=0;i<15;i++)
            {
                for(int j=0;j<15;j++)
                {
                    heng[i][j] = typeboard[i, j];
                    shu[i][j] = typeboard[j, i];
                }              
            }
            for(int i=-14;i<15;i++)
            {
                List<Type> natemp = new List<Type>(15);
                for (int j=0;j<15;j++)
                {

                    if (j + i >= 0 && j + i < 15)
                    {
                        natemp.Add(typeboard[j + i, j]);
                    }
                }
                na[i + 14] = natemp.ToArray();
            }
            for (int i = 0; i < 29; i++)
            {
                List<Type> pietemp = new List<Type>(15);
                for (int j = 0; j < 15; j++)
                {
                    if (i - j >= 0 && i - j < 15)
                    {
                        pietemp.Add(typeboard[i - j, j]);
                    }
                }
                pie[i] = pietemp.ToArray();
            }

            int value=0,value_white = 0,value_black=0;  
            for(int i=0;i<29;i++)//  value = ∑ eachlinevalue;
            {
                if (i < 15)
                {
                    value_white += Score_Line(heng[i], Type.White);
                    value_white += Score_Line(shu[i], Type.White);
                    value_white += Score_Line(pie[i], Type.White);
                    value_white += Score_Line(na[i], Type.White);
                    value_black += Score_Line(heng[i], Type.Black);
                    value_black += Score_Line(shu[i], Type.Black);
                    value_black += Score_Line(pie[i], Type.Black);
                    value_black += Score_Line(na[i], Type.Black);
                }
                else
                {
                    value_white += Score_Line(pie[i], Type.White);
                    value_white += Score_Line(na[i], Type.White);
                    value_black += Score_Line(pie[i], Type.Black);
                    value_black += Score_Line(na[i], Type.Black);
                }
            }
            value = value_white - value_black;
            return value;
        }
        public static int Score_Line(Type[] types,Type type)//对一个数组中的某色棋子打分
        {
            int blocked = 0, count = 0, value = 0;
            for(int i=0;i<types.Length;i++)
            {
                if (types[i] == type)
                {
                    count = 1;
                    blocked = 0;
                    if (i == 0)
                        blocked++;
                    if (i > 0 && types[i - 1] != Type.Empety)
                        blocked++;
                    for (i = i + 1; i < types.Length && types[i] == type; ++i)
                        count++;
                    if (i == types.Length)
                        blocked++;
                    if (i < types.Length && types[i] != Type.Empety)
                        blocked++;
                    value += (int)GetScore(count, blocked);
                }
            }
            return value;
        }

        //对空白打分---------------------------------------------------------------------------------------------
        public static int[,] GetScoreTable(Type[,] typeboard, Type type) //得出当前棋局适当空位分数（模拟下棋）
        {
            int[,] scoretable = new int[15, 15];
            Point[] pointarray = GetChessArray_HasNeighbor(typeboard);

            for (int i = 0; i <pointarray.Length; i++)
            {          
                scoretable[pointarray[i].X,pointarray[i].Y]= GetScore_emptyPoint(typeboard, type, pointarray[i]); //对当前棋局对该空位模拟下某色棋子后形成的局面得分
            }
            return scoretable;
        }
        public static int GetScore_emptyPoint(Type[,] typeboard, Type type, Point point)//对一个位置下某色棋子时进行打分
        {
            Score[] scores = JudgeType_emptyPoint(typeboard, type, point);
            int score = 0;
            for(int i=0;i<=3;i++)
            {
                score += (int)scores[i];
            }
            return score;
        }
        public static Score[] JudgeType_emptyPoint(Type[,] typeboard, Type type, Point point)//判断一个位置下某色棋子时横竖撇捺四个方向的棋形
        {
            Score[] scoretypes = new Score[4];

            int count = 1, blocked = 0, x = point.X, y = point.Y,i;
            //横------------------------------------------------------------------------------------
            for (i = 1; i + x < 15 && typeboard[x+i, y] == type; i++)
            {
                count++;
            }
            if (i + x >= 15 || typeboard[x+i, y] != GoBang_Lib.Type.Empety)
            {
                blocked++;
            }
            for ( i = 1; x-i >= 0&&typeboard[x-i,y]==type; i++)
            {
                count++;
            }
            if (x - i < 0 || typeboard[x - i, y] != GoBang_Lib.Type.Empety)
            {
                blocked++;
            }
            scoretypes[0] = GetScore(count, blocked);
            //竖------------------------------------------------------------------------------------
            count = 1; blocked = 0;
            for (i = 1; i + y < 15 && typeboard[x, y+i] == type; i++)
            {
                count++;
            }
            if (i + y >= 15 || typeboard[x , y+i] != GoBang_Lib.Type.Empety)
            {
                blocked++;
            }
            for (i = 1; y - i >= 0 && typeboard[x, y-i] == type; i++)
            {
                count++;
            }
            if (y - i < 0 || typeboard[x , y-i] != GoBang_Lib.Type.Empety)
            {
                blocked++;
            }
            scoretypes[1]=GetScore(count,blocked);
            //撇------------------------------------------------------------------------------------
            count = 1; blocked = 0;
            for (i = 1; i + x < 15 && y - i >= 0 && typeboard[x + i, y - i] == type; i++)
            {
                count++;
            }
            if ((i + x > 14 || y - i < 0) || typeboard[x + i, y - i] != GoBang_Lib.Type.Empety)
            {
                blocked++;
            }
            for (i = 1;  x-i >= 0 && y + i <15 && typeboard[x - i, y + i] == type; i++)
            {
                count++;
            }
            if ((x-i <0 || y + i > 14) || typeboard[x - i, y + i] != GoBang_Lib.Type.Empety)
            {
                blocked++;
            }
            scoretypes[2] = GetScore(count, blocked);
            //捺------------------------------------------------------------------------------------
            count = 1; blocked = 0;
            for (i = 1; i + x < 15 && y +i<15 && typeboard[x + i, y + i] == type; i++)
            {
                count++;
            }
            if ((i + x > 14 || y + i >14) || typeboard[x + i, y + i] != GoBang_Lib.Type.Empety)
            {
                blocked++;
            }
            for (i = 1; x - i >= 0 && y - i >=0 && typeboard[x - i, y - i] == type; i++)
            {
                count++;
            }
            if ((x - i < 0 || y - i <0) || typeboard[x - i, y - i] != GoBang_Lib.Type.Empety)
            {
                blocked++;
            }
            scoretypes[3] = GetScore(count, blocked);

            return scoretypes;
        }

        // 得到有邻居的空白位置(已经按远近排序)目前只计算一步的。
        public static Point[] GetChessArray_HasNeighbor(Type[,] typeboard)
        {
            List<Point> onestepneighbour = new List<Point>(120), twostepneighbour=new List<Point>(120);
            Point[] hasNeighbourArray;

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Point p = new Point(i, j);
                    if (typeboard[i, j] == Type.Empety)
                    {
                        if (HasNstepNeighbor(typeboard, p, 1))
                        {
                            onestepneighbour.Add(p);
                        }
                       /* if (HasNstepNeighbor(typeboard, p, 2))
                        {
                            twostepneighbour.Add(p);
                        }*/
                    }
                }
            }
            hasNeighbourArray = onestepneighbour.Concat(twostepneighbour).ToArray();
            return hasNeighbourArray;
        }
        public static bool HasNstepNeighbor(Type[,] typeboard,Point point,int n)
        {
            int x = point.X, y = point.Y;
            if (x + n < 15 && typeboard[x + n, y] != Type.Empety)
                return true;
            if (x - n >= 0 && typeboard[x - n, y] != Type.Empety)
                return true;

            if (y + n < 15 && typeboard[x, y + n] != Type.Empety)
                return true;
            if (y - n >= 0 && typeboard[x, y - n] != Type.Empety)
                return true;

            if ((y + n < 15 && x + n < 15) && typeboard[x + n, y + n] != Type.Empety)
                return true;
            if ((y - n >= 0 && x - n >= 0) && typeboard[x - n, y - n] != Type.Empety)
                return true;

            if ((y + n < 15 && x - n >= 0) && typeboard[x - n, y + n] != Type.Empety)
                return true;
            if ((y - n >= 0 && x + n < 15) && typeboard[x + n, y - n] != Type.Empety)
                return true;

            return false;
        }

        //根据连子数和封闭数
        public static Score GetScore(int count, int blocked)
        {
            Score score=GoBang_Lib.Score.One;
            switch (blocked)
            {
                case 0:
                    {
                        switch (count)
                        {
                            case 1:
                                {
                                    score= Score.One;
                                    break;
                                }
                            case 2:
                                {
                                    score = Score.Two;
                                    break;
                                }
                            case 3:
                                {
                                    score = Score.Three;
                                    break;
                                }
                            case 4:
                                {
                                    score = Score.Four;
                                    break;
                                }
                            default:
                                {
                                    score = Score.Five;
                                    break;
                                }
                        }
                        break;
                    }
                case 1:
                    {
                        switch (count)
                        {
                            case 1:
                                {
                                    score = Score.blocked_One;
                                    break;
                                }
                            case 2:
                                {
                                    score = Score.blocked_Two;
                                    break;
                                }
                            case 3:
                                {
                                    score = Score.blocked_Three;
                                    break;
                                }
                            case 4:
                                {
                                    score = Score.blocked_Four;
                                    break;
                                }
                            default:
                                {
                                    score = Score.Five;
                                    break;
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        if(count>4)
                        {
                            score =Score.Five;
                        }
                        break;
                    }
            }
            return score;
        }

        //将chessboard数组转换为typeboard数组
        public static Type[,] ChessBoard_Trans_TypeBoard(ChessBoard chessBoard)
        {
            Type[,] typeboard = new Type[15, 15];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    typeboard[i, j] = chessBoard.Board[i, j].type;
                }
            }
            return typeboard;
        }
    }
}
