package main

type World struct {
	players        map[int]*Player
	networkManager *NetworkManager
}

func NewWorld(networkManager *NetworkManager) *World {
	return &World{
		players:        make(map[int]*Player),
		networkManager: networkManager,
	}
}

func (this *World) Update(delta int64, time int64) {
	userCount := len(this.networkManager.users)
	for i := 0; i < userCount; i++ {
		user := this.networkManager.users[i]

		player, playerExists := this.players[user.id]

		if !playerExists {
			this.players[user.id] = &Player{
				userId: user.id,
				position: Position{
					x: 0,
					y: 0,
				},
				networkManager: this.networkManager,
				duration:       0,
			}

			player = this.players[user.id]
		}
		player.Update(delta, time)
	}

	state := this.CreateState()
	this.networkManager.UpdateState(state)
}

func (this *World) CreateState() GameState {
	var state GameState

	for i := 0; i < len(this.players); i++ {
		player := this.players[i]
		entityState := EntityState{
			id: player.userId,
			x:  player.position.x,
			y:  player.position.y,
		}

		state = append(state, entityState)
	}

	return state
}
