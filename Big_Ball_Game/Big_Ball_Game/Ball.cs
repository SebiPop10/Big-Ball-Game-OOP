// See https://aka.ms/new-console-template for more information

abstract class Ball
{
    protected Random rnd;
    public int Raza {  get; set; }
    public int X { get; protected set; }
    public int Y { get; protected set; }
    public int dX { get; protected set; }
    public int dY { get; protected set; }
    public ConsoleColor Color { get;  set; }

    public Ball(Random rnd)
    {
        this.rnd = rnd;
        X = rnd.Next(1, 60);
        Y= rnd.Next(1, 30);
        Color = (ConsoleColor)rnd.Next(1, 10);
        Raza=rnd.Next(1, 4);
        dX= rnd.Next(-1, 2);
        dY= rnd.Next(-1, 2);
    }
    public abstract void Move();
    public abstract void CheckCollision(List<Ball> balls);
    public abstract void CheckWallCollision();
}
  class RegularBall: Ball
{
    public RegularBall(Random random) : base(random) { }
    public override void Move()
    {
        X += dX;
        Y += dY;
    }
    public override void CheckCollision(List<Ball> balls)
    {
        foreach (Ball ball in balls)
        {
            if (ball != this && Math.Sqrt(Math.Pow(X - ball.X, 2) + Math.Pow(Y - ball.Y, 2)) <= Raza + ball.Raza)
            {
                
                if (ball is RegularBall)
                {
                    
                    if (Raza >= ball.Raza)
                    {
                        Raza += ball.Raza;
                        Color = CombineColors(Color, ball.Color, Raza, ball.Raza);
                        balls.Remove(ball);
                        return;
                    }
                }
                else if (ball is MonsterBall)
                {
                   
                    ball.Raza += Raza;
                    balls.Remove(this);
                    return;
                }
                else if (ball is RepelentBall)
                {
                    
                    ball.Color = Color;
                    dX *= -1; // Schimbarea direcției
                    dY *= -1;
                    return;
                }
            }
        }
    }
    public override void CheckWallCollision()
    {
        if (X <= 0 || X >= 80) 
        {
            dX *= -1; 
        }
        if (Y <= 0 || Y >= 20) 
        {
            dY *= -1; 
        }
    }
    //private ConsoleColor CombineColors(ConsoleColor color1, ConsoleColor color2, int size1, int size2)
    //{
    //    // Combinarea culorilor în funcție de dimensiuni
    //    
    //    return (ConsoleColor)((int)(color1+color2) / 2);
    //}
    private ConsoleColor CombineColors(ConsoleColor color1, ConsoleColor color2, int size1, int size2)
    {
       
        ConsoleColor newColor = color1; 

       
        int r1 = (int)color1 % 16;
        int g1 = ((int)color1 / 16) % 16;
        int b1 = ((int)color1 / 256) % 16;
        int r2 = (int)color2 % 16;
        int g2 = ((int)color2 / 16) % 16;
        int b2 = ((int)color2 / 256) % 16;

       
        int r = (r1 * size1 + r2 * size2) / (size1 + size2);
        int g = (g1 * size1 + g2 * size2) / (size1 + size2);
        int b = (b1 * size1 + b2 * size2) / (size1 + size2);

       
        newColor = (ConsoleColor)(r + g * 16 + b * 256);
        return newColor;
    }
    public override string ToString()
    {
        return $"Regular Ball: Position({X},{Y}), Radius({Raza}), Color({Color}), Direction({dX},{dY})";
    }
}
 class MonsterBall: Ball
{
    public MonsterBall(Random random) : base(random)
    {
        dX = 0;
        dY = 0;
    }

    public override void Move() { } 

    public override void CheckCollision(List<Ball> balls)
    {
        foreach (Ball ball in balls)
        {
            if (ball != this && Math.Sqrt(Math.Pow(X - ball.X, 2) + Math.Pow(Y - ball.Y, 2)) <= Raza + ball.Raza)
            {
                
                if (ball is RegularBall)
                {
                   
                    Raza += ball.Raza;
                    balls.Remove(ball);
                    return;
                }
                else if (ball is RepelentBall)
                {
                  
                    Raza /= 2;
                    return;
                }
            }
        }
    }

    public override void CheckWallCollision() { } 
    public override string ToString()
    {
        return $"Monster Ball: Position({X},{Y}), Radius({Raza}), Color({Color}), Direction({dX},{dY})";
    }
}
  class RepelentBall : Ball
{
    public RepelentBall(Random random) : base(random) { }
    public override void Move()
    {
        X += dX;
        Y += dY;
    }

    public override void CheckCollision(List<Ball> balls)
    {
        foreach (Ball ball in balls)
        {
            if (ball != this && Math.Sqrt(Math.Pow(X - ball.X, 2) + Math.Pow(Y - ball.Y, 2)) <= Raza + ball.Raza)
            {
               
                if (ball is RegularBall)
                {
                   
                    Color = ball.Color;
                    return;
                }
                else if (ball is RepelentBall)
                {
                    
                    ConsoleColor tempColor = Color;
                    Color = ball.Color;
                    ball.Color = tempColor;
                    return;
                }
            }
        }
    }

    public override void CheckWallCollision()
    {
        if (X <= 0 || X >= 80) 
        {
            dX *= -1; 
        }
        if (Y <= 0 || Y >= 20) 
        {
            dY *= -1;
        }
    }
    public override string ToString()
    {
        return $"Repelent Ball: Position({X},{Y}), Radius({Raza}), Color({Color}), Direction({dX} , {dY})";
    }
}