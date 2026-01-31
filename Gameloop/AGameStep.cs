using Godot;
using System;

public abstract partial class AGameStep : Node
{
	public virtual GameSteps Identifier {get;}

	public abstract void Enter();

	public abstract void Exit();
}