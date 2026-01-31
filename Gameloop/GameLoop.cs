using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class GameLoop : Node
{
	private GameSteps activeStep;

	private readonly Dictionary<GameSteps, AGameStep> steps = new(4);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var step in GetChildren().OfType<AGameStep>())
		{
			steps.Add(step.Identifier, step);
		}
	}

	public void StartGame()
	{
		activeStep = GameSteps.Initialize;
		steps[activeStep].Enter(this);
	}

	public void EndGame()
	{
		
	}

	internal void GoToStep(GameSteps nextStep)
	{
		steps[activeStep].Exit();
		activeStep = nextStep;
		steps[activeStep].Enter(this);
	}
}
