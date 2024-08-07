using Serilog;
Serilog.Core.Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
try
{
    
    Array.ForEach(args, (elem) =>
    {
        File.WriteAllText("alll.txt", elem);
    });
    string opearationi = File.ReadAllText("alll.txt");
Calculator Calculator = new Calculator(log);

    Calculator.StringToExpression(opearationi);

    Console.WriteLine(Calculator.Calculate());
    log.Information("Sucsess!");
    Console.ReadLine();
}
catch 
{
    log.Warning("Wrongggg");
}



public class Calculator
{
    int _parametr1 { get; set; }
    int _parametr2 { get; set; }
    char _operator { get; set; }
    Serilog.Core.Logger _configuration { get; set; }
    public Calculator(Serilog.Core.Logger loggerConfiguration)
    {
        _configuration = loggerConfiguration;
    }
    public int Calculate()
    {
        
        switch (_operator)
        {
            case '+':
                return _parametr1 + _parametr2;
                break;
            case '-':
                return _parametr1 - _parametr2;
                break;
            case '/':
                if (_parametr2 != 0) return _parametr1 / _parametr2  ;
                break;
            case '*':
                return _parametr1 * _parametr2; 

        }
        return default;
    }
    public void StringToExpression(string value)
    {
        int ll = 0;
        string p1="";
        string p2="";
        for (int i = 0; i < value.Length; i++)
        {
          
            if(value[i] == '+')
            {
                _operator = '+';
                ll = i+1;
                break;
            }if(value[i] == '-')
            {
                _operator = '-';
                ll = i + 1;
                break;
            }if(value[i] == '*')
            {
                _operator = '*';
                ll = i + 1;
                break;
            }if(value[i] == '/')
            {
                _operator = '/';
                ll = i + 1;
                break;
            }
            p1 += value[i];
        }
        for (int i =ll; i <value.Length ; i++)
        {
            p2+=value[i]; 
        }

        try
        {
            _parametr1 = Convert.ToInt32(p1);
            _parametr2 = Convert.ToInt32(p2);
        } 
        catch 
        {
            _configuration.Warning("Parametrs are worong");
        }

    }

    public static bool IsNumeric(string s)
    {
        int output;
        return int.TryParse(s, out output);
    }
}
