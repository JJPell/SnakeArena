package game

type Game struct {
	id          int
	entities    map[int]Entity
	players     map[int]Player
	entityCount int
}

func NewGame(id int) *Game {
	return &Game{
		id:       id,
		entities: make(map[int]Entity),
		players:  make(map[int]Player),
	}
}

func (this *Game) Update(delta int64, time int64) {
	for _, entity := range this.entities {
		entity.Update(delta, time)
	}
}

func (this *Game) AddPlayer(connectionId int, x int, y int, length int) {
	entityId := this.createEntityId()
	player := &Player{
		id:           entityId,
		connectionId: connectionId,
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

func (this *Game) RemovePlayer(connectionId int) {

}

func (this *Game) UpdatePlayerInput(connectionId int, playerInput PlayerInput) {

}

func (this *Game) GetState() *GameState {

}

func (this *Game) createEntityId() int {
	id := this.entityCount
	this.entityCount++
	return id
}
