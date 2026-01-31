class_name Inventory
extends Control

@onready var slot_scene:PackedScene = preload("res://slot.tscn")
@onready var grid_container: GridContainer = $Background/MarginContainer/VBoxContainer/ScrollContainer/GridContainer
@onready var item_scene: PackedScene = preload("res://item.tscn")
@onready var mask_scene:PackedScene = preload("res://mask.tscn")
@onready var scroll_container: ScrollContainer = $Background/MarginContainer/VBoxContainer/ScrollContainer
@onready var col_count: int = grid_container.columns #save column number
@onready var grid_container2: GridContainer = $Background2/MarginContainer/VBoxContainer/ScrollContainer/GridContainer
@export var cooldown_per_activation: int = 2

signal mask_placed(damage : int, armor : int)

var grid_array: Array[Slot] = []
var item_held: Item = null
var mask_held: Mask = null
var current_slot: Slot = null
var can_place := false
var icon_anchor: Vector2

# Called when the node enters the scene tree for the first time.
func _ready():
	for i in range(14):
		create_slot()
	

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	if item_held:
		if Input.is_action_just_pressed("mouse_rightclick"):
			rotate_item()
		
		if Input.is_action_just_pressed("mouse_leftclick"):
			if scroll_container.get_global_rect().has_point(get_global_mouse_position()):
				place_item()
	elif mask_held:
		if Input.is_action_just_pressed("mouse_rightclick"):
			rotate_mask()
			
		if Input.is_action_just_pressed("mouse_leftclick"):
			if scroll_container.get_global_rect().has_point(get_global_mouse_position()):
				reduce_cooldowns()
				place_mask()
	else:
		if Input.is_action_just_pressed("mouse_leftclick"):
			if scroll_container.get_global_rect().has_point(get_global_mouse_position()):
				pick_item()
	
	
func create_slot():
	var new_slot = slot_scene.instantiate()
	new_slot.slot_ID = grid_array.size()
	grid_container.add_child(new_slot)
	grid_array.push_back(new_slot)
	new_slot.slot_entered.connect(_on_slot_mouse_entered)
	new_slot.slot_exited.connect(_on_slot_mouse_exited)	
	pass


func _on_slot_mouse_entered(a_Slot):
	icon_anchor = Vector2(10000,100000)
	current_slot = a_Slot
	if item_held:
		check_item_slot_availability(current_slot)
		set_grids.call_deferred(current_slot)
	elif mask_held:
		check_mask_slot_availability(current_slot)
		set_grids.call_deferred(current_slot)
	
func _on_slot_mouse_exited(_a_Slot):
	clear_grid()
	
	if not grid_container.get_global_rect().has_point(get_global_mouse_position()):
		current_slot = null

func _on_button_spawn_pressed():
	var new_item = item_scene.instantiate()
	add_child(new_item)
	new_item.load_item(randi_range(1,3))    #randomize this for different items to spawn
	new_item.selected = true
	item_held = new_item
	
func check_mask_slot_availability(a_Slot):
	for grid in mask_held.mask_grids:
		var grid_to_check = a_Slot.slot_ID + grid[0] + grid[1] * col_count
		var line_switch_check = a_Slot.slot_ID % col_count + grid[0]
		if line_switch_check < 0 or line_switch_check >= col_count:
			can_place = false
			return
		if grid_to_check < 0 or grid_to_check >= grid_array.size():
			can_place = false
			return
	can_place = true
	
func check_item_slot_availability(a_Slot):
	for grid in item_held.item_grids:
		var grid_to_check = a_Slot.slot_ID + grid[0] + grid[1] * col_count
		var line_switch_check = a_Slot.slot_ID % col_count + grid[0]
		if line_switch_check < 0 or line_switch_check >= col_count:
			can_place = false
			return
		if grid_to_check < 0 or grid_to_check >= grid_array.size():
			can_place = false
			return
		if grid_array[grid_to_check].state == grid_array[grid_to_check].States.TAKEN:
			can_place = false
			return
		
	can_place = true
	
func set_grids(a_Slot):
	var slots = []
	if item_held:
		slots = item_held.item_grids
	elif mask_held:
		slots = mask_held.mask_grids
	
	for grid in slots:
		var grid_to_check = a_Slot.slot_ID + grid[0] + grid[1] * col_count
		if grid_to_check < 0 or grid_to_check >= grid_array.size():
			continue
		#make sure the check don't wrap around boarders
		var line_switch_check = a_Slot.slot_ID % col_count + grid[0]
		if line_switch_check <0 or line_switch_check >= col_count:
			continue
		
		if can_place:
			grid_array[grid_to_check].set_color(grid_array[grid_to_check].States.FREE)
			#save anchor for snapping
			if grid[1] < icon_anchor.x: icon_anchor.x = grid[1]
			if grid[0] < icon_anchor.y: icon_anchor.y = grid[0]
				
		else:
			grid_array[grid_to_check].set_color(grid_array[grid_to_check].States.TAKEN)

func clear_grid():
	for grid in grid_array:
		grid.set_color(grid.States.DEFAULT)

func rotate_item():
	item_held.rotate_item()
	clear_grid()
	if current_slot:
		_on_slot_mouse_entered(current_slot)

func rotate_mask():
	mask_held.rotate_mask()
	clear_grid()
	if current_slot:
		_on_slot_mouse_entered(current_slot)

func place_item():
	if not can_place or not current_slot: 
		return #put indication of placement failed, sound or visual here
		
	#for changing scene tree
	item_held.get_parent().remove_child(item_held)
	grid_container.add_child(item_held)
	item_held.global_position = get_global_mouse_position()
	####
	var calculated_grid_id = current_slot.slot_ID + icon_anchor.x * col_count + icon_anchor.y
	item_held._snap_to(grid_array[calculated_grid_id].global_position)
	item_held.grid_anchor = current_slot
	for grid in item_held.item_grids:
		var grid_to_check = current_slot.slot_ID + grid[0] + grid[1] * col_count
		grid_array[grid_to_check].state = grid_array[grid_to_check].States.TAKEN 
		grid_array[grid_to_check].item_stored = item_held
	
	#put item into a data storage here
	item_held = null
	clear_grid()

func place_mask():
	if not can_place or not current_slot: 
		return #put sound here
		
	mask_held.get_parent().remove_child(mask_held)
	mask_held.global_position = get_global_mouse_position()
	
	var masked_items = []
	var grid_ids = []
	for grid in mask_held.mask_grids:
		var grid_to_check = current_slot.slot_ID + grid[0] + grid[1] * col_count
		if grid_array[grid_to_check].States.TAKEN:
			if grid_array[grid_to_check].cooldown_timer.cooldown == 0:
				masked_items.append(grid_array[grid_to_check].item_stored)
				grid_ids.append(grid_to_check)
			grid_array[grid_to_check].cooldown_timer.cooldown += cooldown_per_activation
			print(grid_array[grid_to_check].cooldown_timer.cooldown)
	
	mask_held = null
	clear_grid()
	print("Masked Items:")
	print(masked_items)
	print("Grid Ids:")
	print(grid_ids)

func pick_item():
	if not current_slot or not current_slot.item_stored: 
		return
	item_held = current_slot.item_stored
	item_held.selected = true
	#move node in the scene tree
	item_held.get_parent().remove_child(item_held)
	add_child(item_held)
	item_held.global_position = get_global_mouse_position()
	####
	
	for grid in item_held.item_grids:
		var grid_to_check = item_held.grid_anchor.slot_ID + grid[0] + grid[1] * col_count # use grid anchor instead of current slot to prevent bug
		grid_array[grid_to_check].state = grid_array[grid_to_check].States.FREE 
		grid_array[grid_to_check].item_stored = null
	
	check_item_slot_availability(current_slot)
	set_grids.call_deferred(current_slot)



func _on_add_slot_pressed():
	create_slot()


func _on_get_mask_pressed() -> void:
	var new_mask = mask_scene.instantiate()
	add_child(new_mask)
	new_mask.load_mask(randi_range(1,6))    #randomize this for different items to spawn
	new_mask.selected = true
	mask_held = new_mask
	pass # Replace with function body.


func reduce_cooldowns() -> void:
	for slot in grid_array:
		if slot.cooldown_timer.cooldown > 0:
			slot.cooldown_timer.cooldown -= 1
