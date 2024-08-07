namespace CalculatorLib
{
    public class Calculator
    {
        public int Calculate(int Paramter1, char Operator, int Parametr2)
        {
            switch (Operator)
            {
                case '+':
                    return Paramter1 + Parametr2;

                case '-':
                    return Paramter1 - Parametr2;
                case '/':
                    if (Parametr2 != 0)
                    {
                        return Paramter1 / Parametr2;
                    }
                    else
                    {
                        throw new ArgumentException();
                    };
                case '*':
                    return Paramter1 * Parametr2;
            }
            return 0;
        }
    }

}