using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public class Matrix
    {
        int StockCount = 0;
        int NeedCount = 0;
        double[] Stocks;
        double[] Needs;
        double[,] Prises;
        public double[,] Numbers;
        double[] StocksPotentials;
        double[] NeedsPotentials;
        public double res = 0;
        public Matrix(int sc, int nc, double[] s, double[] n, double[,] p)
        {
            StockCount = sc;
            NeedCount = nc;
            Stocks = s;
            Needs = n;
            Prises = p;
            StocksPotentials = new double[StockCount];
            NeedsPotentials = new double[NeedCount];
            Numbers = new double[StockCount, NeedCount];            
            for (int i = 0; i < StockCount; i++)
                for (int j = 0; j < NeedCount; j++)
                    Numbers[i, j] = 0;
        }
        public void BuildFirstSupportingPlan()
        {            
            var iteration = 0;
            while (true)
            {
                iteration++;
                var check = 1;
                for (int i = 0; i < StockCount; i++)
                {
                    if (Stocks[i] != 0)
                    {
                        check = 0;
                        break;
                    }
                }
                for (int i = 0; i < NeedCount; i++)
                {
                    if (Needs[i] != 0)
                    {
                        check = 0;
                        break;
                    }
                }                
                if (check == 1) break;

                var StocksDifferences = new double[StockCount];
                var NeedsDifferences = new double[NeedCount];
                for (int i = 0; i < StockCount; i++)
                    StocksDifferences[i] = 0;
                for (int i = 0; i < NeedCount; i++)
                    NeedsDifferences[i] = 0;

                // Stocks differences
                for (int i = 0; i < StockCount; i++)
                {
                    var minIndex1 = 0;
                    for (int j = 0; j < NeedCount; j++)                    
                        if (Prises[i, j] < Prises[i, minIndex1] && Needs[j]!=0 || Needs[minIndex1]==0) minIndex1 = j;

                    var minIndex2 = 0;
                    for (int j = 0; j < NeedCount; j++)
                        if (Prises[i, j] < Prises[i, minIndex2] && Needs[j] != 0 && j!=minIndex1 || Needs[minIndex2 ] == 0 || minIndex2 == minIndex1) minIndex2 = j;

                    if (Stocks[i] == 0)
                        StocksDifferences[i] = double.NaN;
                    else
                        StocksDifferences[i] = Math.Abs(Prises[i,minIndex1]-Prises[i,minIndex2]);
                }

                for (int i = 0; i < NeedCount; i++)
                {
                    var minIndex1 = 0;
                    for (int j = 0; j < StockCount; j++)
                        if (Prises[j, i] < Prises[minIndex1,i] && Stocks[j] != 0 || Stocks[minIndex1] == 0) minIndex1 = j;

                    var minIndex2 = 0;
                    for (int j = 0; j < StockCount; j++)
                        if (Prises[j, i] < Prises[minIndex2,i] && Stocks[j] != 0 && j != minIndex1 || Stocks[minIndex2] == 0 || minIndex2==minIndex1) minIndex2 = j;
                    
                    if (Needs[i] == 0)
                        NeedsDifferences[i] = double.NaN;
                    else
                        NeedsDifferences[i] = Math.Abs(Prises[minIndex1,i] - Prises[minIndex2,i]);
                }

                var StocksMaxIndex = 0;
                var NeedsMaxIndex = 0;
                
                for (int i = 0; i < StockCount; i++)                
                    if (StocksDifferences[i] >= StocksDifferences[StocksMaxIndex] || Stocks[StocksMaxIndex]==0) StocksMaxIndex = i;
                for (int i = 0; i < NeedCount; i++)
                    if (NeedsDifferences[i] >= NeedsDifferences[NeedsMaxIndex] || Needs[NeedsMaxIndex]==0) NeedsMaxIndex = i;

                
                if (StocksDifferences[StocksMaxIndex] > NeedsDifferences[NeedsMaxIndex])
                {
                    for (int i = 0; i < NeedCount; i++)
                        if (Prises[StocksMaxIndex, i] < Prises[StocksMaxIndex, NeedsMaxIndex] && Needs[i]!=0 || Needs[NeedsMaxIndex]==0) NeedsMaxIndex = i;
                }
                else
                {
                    for (int i = 0; i < StockCount; i++)
                        if (Prises[i,NeedsMaxIndex] < Prises[StocksMaxIndex, NeedsMaxIndex] && Stocks[i]!=0 || Stocks[StocksMaxIndex]==0) StocksMaxIndex = i;                    
                }

                //Debug.WriteLine(StocksMaxIndex + " " +NeedsMaxIndex);

                if (Stocks[StocksMaxIndex] < Needs[NeedsMaxIndex])
                {
                    Numbers[StocksMaxIndex, NeedsMaxIndex] = Stocks[StocksMaxIndex];
                    Needs[NeedsMaxIndex] -= Stocks[StocksMaxIndex];
                    Stocks[StocksMaxIndex] = 0;                    
                }
                else
                {
                    Numbers[StocksMaxIndex, NeedsMaxIndex] = Needs[NeedsMaxIndex];
                    Stocks[StocksMaxIndex] -= Needs[NeedsMaxIndex];
                    Needs[NeedsMaxIndex] = 0;
                }

                // output                                                
                //for (int i = 0; i < StockCount; i++)
                //{
                //    for (int j = 0; j < NeedCount; j++)
                //        Debug.Write(Prises[i, j] + "[" + Numbers[i, j] + "]" + "\t");
                //    Debug.Write(Stocks[i] + "\t");
                //    Debug.Write(StocksDifferences[i] + "\t");
                //    Debug.WriteLine();
                //}
                //for (int i = 0; i < NeedCount; i++)
                //    Debug.Write(Needs[i] + "\t");
                //Debug.WriteLine();
                //for (int i = 0; i < NeedCount; i++)
                //    Debug.Write(NeedsDifferences[i] + "\t");
                //Debug.WriteLine();
                //Debug.WriteLine();
                if (iteration > 100) return;
            }
        }

        public Point CheckPlan()
        {                      
            var IsStockPotentialsSet = new bool[StockCount];
            var IsNeedPotentialsSet = new bool[NeedCount];
            IsStockPotentialsSet[0] = true;
            var iteration = 0;
            for (int i = 0; i < NeedCount; i++)
                if (Numbers[0, i] != 0)
                {
                    NeedsPotentials[i] = Prises[0, i];
                    IsNeedPotentialsSet[i] = true;
                }
            while (true)
            {
                iteration++;                
                var check = 1;
                for (int i = 0; i < StockCount; i++)
                    for (int j = 0; j < NeedCount; j++)
                        if (Numbers[i, j] != 0 && Prises[i, j] != StocksPotentials[i] + NeedsPotentials[j]) check = 0;                                                    
                if (check == 1) break;

                for(int i=0;i<StockCount;i++)
                    for(int j=0;j<NeedCount;j++)
                    {
                        if(Numbers[i,j]!=0 && IsStockPotentialsSet[i])
                        {
                            for(int k=0;k<NeedCount;k++)
                                if(!IsNeedPotentialsSet[k] && Numbers[i,k]!=0)
                                {
                                    NeedsPotentials[k] = Prises[i, k] - StocksPotentials[i];
                                    IsNeedPotentialsSet[k] = true;                                    
                                }
                        }

                        if (Numbers[i, j] != 0 && IsNeedPotentialsSet[j])
                        {
                            for (int k = 0; k < StockCount; k++)
                                if (!IsStockPotentialsSet[k] && Numbers[k,j]!=0)
                                {
                                    StocksPotentials[k] = Prises[k,j]-NeedsPotentials[j];
                                    IsStockPotentialsSet[k] = true;                                 
                                }
                        }
                    }

                // output                                                
                //for (int i = 0; i < StockCount; i++)
                //{
                //    for (int j = 0; j < NeedCount; j++)
                //        Debug.Write(Prises[i, j] + "[" + Numbers[i, j] + "]" + "\t");
                //    Debug.Write(StocksPotentials[i] + "\t");
                //    Debug.WriteLine();
                //}
                //for (int i = 0; i < NeedCount; i++)
                //    Debug.Write(NeedsPotentials[i] + "\t");
                //Debug.WriteLine();
                //Debug.WriteLine();
                if (iteration > 100) break;
            }
            var pMax = new Point(0, 0);
            for (int i = 0; i < StockCount; i++)
                for (int j = 0; j < NeedCount; j++)
                    if (Numbers[i, j] == 0 && StocksPotentials[i] + NeedsPotentials[j]-Prises[i,j] > StocksPotentials[pMax.X] + NeedsPotentials[pMax.Y] - Prises[pMax.X, pMax.Y])
                        pMax = new Point(i, j);

            return pMax;
        }
        public void Calculate()
        {                        
            var iteration = 0;
            while (true)
            {
                iteration++;
                CheckPlan();
                Debug.Write("\t");
                for (int i = 0; i < NeedCount; i++)
                    Debug.Write("v"+(i+1)+"="+NeedsPotentials[i] + "\t");
                Debug.WriteLine("");
                for (int i = 0; i < StockCount; i++)
                {
                    Debug.Write("u" + (i + 1) + "=" + StocksPotentials[i] + "\t");
                    for (int j = 0; j < NeedCount; j++)
                    {                                                
                        Debug.Write(Prises[i, j] + "[" + Numbers[i, j] + "]" + "\t");
                    }                    
                    Debug.WriteLine("");
                }                                                

                if(StocksPotentials[CheckPlan().X] + NeedsPotentials[CheckPlan().Y] - Prises[CheckPlan().X, CheckPlan().Y] <= 0)
                {
                    Debug.WriteLine("План є оптимальним.");
                    res = 0;
                    for (int i = 0; i < StockCount; i++)
                        for (int j = 0; j < NeedCount; j++)
                            res += Prises[i, j] * Numbers[i, j];
                    Debug.WriteLine("Мiнiмальнi затрати: "+res);
                    break;
                }
                else
                    Debug.WriteLine("План не є оптимальним, обчислюється новий план.");

                
                var stockBasis = CheckPlan().X;
                var needBasis = CheckPlan().Y;
                var isDirectionV = false;
                var isNumberVoid = new bool[StockCount, NeedCount];
                var cycle = new List<Point>();
                cycle.Add(new Point(stockBasis,needBasis));

                while (cycle[0]!=cycle[cycle.Count-1] || cycle.Count < 2)
                {
                    bool isAdded = false;
                    if (isDirectionV)
                    {
                        for (int i = 0; i < StockCount; i++)
                            if (Numbers[i, cycle[cycle.Count - 1].Y] != 0 && i != cycle[cycle.Count - 1].X && !cycle.Contains(new Point(i, cycle[cycle.Count - 1].Y)) && !isNumberVoid[i, cycle[cycle.Count - 1].Y] || i==stockBasis&& cycle[cycle.Count - 1].Y==needBasis&&cycle.Count>1)
                            {
                                cycle.Add(new Point(i, cycle[cycle.Count - 1].Y));
                                isDirectionV = !isDirectionV;
                                isAdded = true;
                                break;
                            }                    
                    }
                    else
                    {
                        for (int i = 0; i < NeedCount; i++)
                            if (Numbers[cycle[cycle.Count - 1].X, i] != 0 && i != cycle[cycle.Count - 1].Y && !cycle.Contains(new Point(cycle[cycle.Count - 1].X, i)) && !isNumberVoid[cycle[cycle.Count - 1].X, i] || i == needBasis && cycle[cycle.Count - 1].X == stockBasis&&cycle.Count>1)
                            {
                                cycle.Add(new Point(cycle[cycle.Count - 1].X, i));
                                isDirectionV = !isDirectionV;
                                isAdded = true;
                                break;
                            }
                    }
                    if(!isAdded)
                    {
                        isDirectionV = !isDirectionV;
                        isNumberVoid[cycle[cycle.Count - 1].X, cycle[cycle.Count - 1].Y] = true;
                        cycle.Remove(cycle[cycle.Count-1]);
                    }

                }                
                var cycleMinIndex = 1;
                for (int i = 1; i < cycle.Count - 1; i++)               
                    if ((i % 2 == 1) && (Numbers[cycle[i].X, cycle[i].Y] < Numbers[cycle[cycleMinIndex].X, cycle[cycleMinIndex].Y])) cycleMinIndex = i;                                    

                var minElement = Numbers[cycle[cycleMinIndex].X, cycle[cycleMinIndex].Y];  
                for (int i = 0; i < cycle.Count - 1; i++)
                {
                    if (i % 2 == 1)                    
                        Numbers[cycle[i].X, cycle[i].Y] -= minElement;                                            
                    else
                        Numbers[cycle[i].X, cycle[i].Y] += minElement;
                }                
            }
        }
    }
}
