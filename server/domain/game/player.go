package game

type Player struct {
	id       int
	position Position
	length   int
	input    PlayerInput
}

func (this *Player) GetState() *EntityState {
	return &EntityState{
		Id:     this.id,
		X:      this.position.X,
		Y:      this.position.Y,
		Length: this.length,
	}
}

func (this *Player) Update(delta int64, time int64) {
	if this.input.Up {
		this.position.Y++
	} else if this.input.Down {
		this.position.Y--
	} else if this.input.Left {
		this.position.X--
	} else if this.input.Right {
		this.position.X++
	}
}

func (this *Player) SetInput() {

}
