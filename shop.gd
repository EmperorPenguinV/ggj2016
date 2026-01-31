class_name Shop
extends Node2D

@onready var inventory: Inventory = $Inventory
@onready var texture_rect: TextureRect = $Shop/MarginContainer/VBoxContainer/HBoxContainer/TextureRect
@onready var item_scene: PackedScene = preload("res://item.tscn")
var item_in_shop: Item = null
var item_held := false
var item_bought := false

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var new_item = item_scene.instantiate()
	texture_rect.add_child(new_item)
	new_item.load_item(randi_range(1,3))
	new_item.global_position = texture_rect.global_position + Vector2(texture_rect.custom_minimum_size/2) 
	item_in_shop = new_item
	new_item.IconRect_path.z_index = 3

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	if item_bought:
		return
	if !item_held and item_in_shop.global_position != texture_rect.global_position + Vector2(texture_rect.custom_minimum_size/2) :
		item_bought = true
		return
	if Input.is_action_just_pressed("mouse_leftclick"):
		if item_held:
			item_in_shop.selected = false
			item_held = false
			_reset_item_position()
		elif texture_rect.get_global_rect().has_point(get_global_mouse_position()):
			item_in_shop.selected = true
			item_held = true
			inventory.item_held = item_in_shop

func _reset_item_position() -> void:
	item_in_shop.global_position = texture_rect.global_position + Vector2(texture_rect.custom_minimum_size/2) 
