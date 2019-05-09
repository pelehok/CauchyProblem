using System;

namespace CauchyProblem
{
	class GauseMethod
    {
        private decimal[,] primaryMatrix;
        private decimal[] yVector;
        private int systemSize;

        private void Initialization(decimal[,] matrixParam, decimal[] vectorParam)
        {
            systemSize = vectorParam.Length;
            primaryMatrix = new decimal[systemSize, systemSize];
            yVector = new decimal[systemSize];

            Array.Copy(matrixParam, primaryMatrix, matrixParam.Length);
            Array.Copy(vectorParam, yVector, vectorParam.Length);
        }

        private int FindMaxElement(int elementIndex)
        {
            var maxElement = elementIndex;
            for (int i = elementIndex; i < systemSize; i++)
            {
                if (Math.Abs(primaryMatrix[i, elementIndex]) > Math.Abs(primaryMatrix[maxElement, elementIndex]))
                {
                    maxElement = i;
                }
            }
            return maxElement;
        }

        private void DoSums(int rowIndex, int mainRowIndex, int matrixStep)
        {
            var sumElement = (-1) * (primaryMatrix[rowIndex, matrixStep] / primaryMatrix[mainRowIndex, matrixStep]);
            for (int i = matrixStep; i < systemSize; i++)
            {
                primaryMatrix[rowIndex, i] += primaryMatrix[mainRowIndex, i] * sumElement;
            }
            yVector[rowIndex] += yVector[mainRowIndex] * sumElement;
        }

        private void Reshuffle(int mainRowIndex, int matrixStep)
        {
            var tempValue = 0M;
            for (int i = matrixStep; i < systemSize; i++)
            {
                tempValue = primaryMatrix[matrixStep, i];
                primaryMatrix[matrixStep, i] = primaryMatrix[mainRowIndex, i];
                primaryMatrix[mainRowIndex, i] = tempValue;
            }

            tempValue = yVector[mainRowIndex];
            yVector[mainRowIndex] = yVector[matrixStep];
            yVector[matrixStep] = tempValue;
        }

        private decimal[] ResolveSystem()
        {
            var sum = 0M;
            var tempArray = new decimal[systemSize];

            for (int i = systemSize - 1; i >= 0; i--)
            {
                for (int j = systemSize - 1; j >= 0; j--)
                {
                    sum += primaryMatrix[i, j] * tempArray[j];
                }

                yVector[i] += sum * (-1);

                tempArray[i] = yVector[i] = yVector[i] / primaryMatrix[i, i];
                sum = 0M;
            }
            return yVector;
        }

        public decimal[] FindSolution(decimal[,] matrixParam, decimal[] vectorParam)
        {
            Initialization(matrixParam, vectorParam);

            for (int i = 0; i < systemSize; i++)
            {
                var maxElement = FindMaxElement(i);
                for (int j = i; j < systemSize; j++)
                {
                    if (j != maxElement && primaryMatrix[j, i] != 0)
                    {
                        DoSums(j, maxElement, i);
                    }
                }
                if (maxElement != i)
                {
                    Reshuffle(maxElement, i);
                }
            }
            return ResolveSystem();
        }
    }
}
