// See https://aka.ms/new-console-template for more information
public class Simulation
{
    private List<Ball> balls;
    private Random rnd;

    public Simulation(int regularCount,int monsterCount,int repelentCount)
    {
        balls= new List<Ball>();
        rnd= new Random();
        for(int i=0;i<regularCount;i++)
        {
            balls.Add(new RegularBall(rnd));
        }
        for (int i = 0; i < monsterCount; i++)
        {
            balls.Add(new MonsterBall(rnd));
        }
        for (int i = 0; i < repelentCount; i++)
        {
            balls.Add(new RepelentBall(rnd));
        }
    }
    public void Turn()
    {
       
        List<Ball> ballsCopy = new List<Ball>(balls);

        foreach (Ball ball in ballsCopy)
        {
            ball.Move(); 
            ball.CheckCollision(balls); 
            ball.CheckWallCollision(); 
        }
    }
    public bool IsFinished()
    {
      
        foreach (Ball ball in balls)
        {
            if (ball is RegularBall)
            {
                return false;
            }
        }
        return true;
    }
    public void PrintState()
    {
       
        Console.WriteLine("Starea sistemului:");
        foreach (Ball ball in balls)
        {
            Console.WriteLine(ball);
        }
        Console.WriteLine();
    }
    public void Delay()
    {
       
        System.Threading.Thread.Sleep(1000);
    }
}