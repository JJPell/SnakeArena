package game

type Game struct {
	entities    map[int]*Entity
	players     map[int]*Player
	entityCount int
}

func NewGame() *Game {
	return &Game{
		entities: make(map[int]*Entity),
		players:  make(map[int]*Player),
	}
}

func (this *Game) Update(delta int64, time int64) {
	for _, entity := range this.entities {
		entity.Update(delta, time)
	}
}

func (this *Game) AddPlayer(x int, y int, length int) {
	entityId := this.createEntityId()
	player := &Player{
		id: entityId,
		position: Position{
			X: x,
			Y: y,
		},
		length: length,
		input: PlayerInput{
			Up:    false,
			Down:  false,
			Left:  false,
			Right: false,
		},
	}
	this.entities[entityId] = player
	this.players[entityId] = player
}

func (this *Game) createEntityId() int {
	id := this.entityCount
	this.entityCount++
	return id
}
