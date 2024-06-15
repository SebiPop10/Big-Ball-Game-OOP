// See https://aka.ms/new-console-template for more information

//Ball b = new Ball();
Simulation sim = new Simulation(2, 2, 2);


while (!sim.IsFinished())
{
    sim.Turn(); 
    sim.PrintState();
    sim.Delay(); /
}

Console.WriteLine("Simulare terminată!");
    
