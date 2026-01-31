class_name CooldownTimer
extends ColorRect


var cooldown: int = 0 : set = _set_cooldown
@onready var label: Label = $Label

func _ready() -> void:
	visible = false

func _set_cooldown(cd: int) -> void:
	cooldown = max(0,cd)
	label.text = str(cooldown)
	
	if cooldown == 0:
		visible = false
	else:
		visible = true
