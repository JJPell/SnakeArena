package main

import (
	"log"
	"net/http"

	"github.com/gorilla/websocket"
)

type UserInput struct {
	up    bool
	down  bool
	left  bool
	right bool
}

type EntityState struct {
	Id int
	X  int
	Y  int
}

type GameState = []EntityState

func (this UserInput) IsInputDirection() bool {
	if this.up {
		return true
	}
	if this.down {
		return true
	}
	if this.left {
		return true
	}
	if this.right {
		return true
	}
	return false
}

type User struct {
	id         int
	connection *websocket.Conn
	input      UserInput
}

type NetworkService struct {
	upgrader            websocket.Upgrader
	users               []*User
	totalUsersConnected int
	gameStateNow        GameState
	gameStateBefore     GameState
	test                string
}

func NewNetworkService(upgrader websocket.Upgrader) *NetworkService {
	return &NetworkService{
		upgrader:            upgrader,
		totalUsersConnected: 0,
		users:               make([]*User, 0),
	}
}

func (this *NetworkService) HandleRequest(res http.ResponseWriter, req *http.Request) {
	// The following check origin code disables CORS
	this.upgrader.CheckOrigin = func(r *http.Request) bool {
		return true
	}

	ws, err := this.upgrader.Upgrade(res, req, nil)

	if err != nil {
		log.Fatal(err)
	}

	log.Print("Client succesfully connected...")

	this.users = append(this.users, &User{
		id:         this.totalUsersConnected,
		connection: ws,
		input: UserInput{
			up:    false,
			down:  false,
			left:  false,
			right: false,
		},
	})

	user := this.users[len(this.users)-1]

	this.totalUsersConnected++

	this.ConnectionManager(user)
}

func (this *NetworkService) InputByteToUserInput(number byte) UserInput {
	input := UserInput{
		up:    false,
		down:  false,
		left:  false,
		right: false,
	}

	index := 0
	for number > 0 {
		inputBool := number%2 == 1

		switch index {
		case 0:
			input.right = inputBool
		case 1:
			input.left = inputBool
		case 2:
			input.down = inputBool
		case 3:
			input.up = inputBool
		default:
			break
		}
		number >>= 1
		index++
	}

	return input
}

func (this *NetworkService) ConnectionManager(user *User) {
	connection := user.connection

	for {
		_, message, err := connection.ReadMessage()

		if err != nil {
			log.Print(err)
			break
		}

		input := this.InputByteToUserInput(message[0])
		user.input = input
	}
}

func (this *NetworkService) GetUser(id int) *User {
	for i := 0; i < len(this.users); i++ {
		user := this.users[i]
		if user.id == id {
			return user
		}
	}

	return nil
}

func (this *NetworkService) IsStateDifferent(state1 GameState, state2 GameState) bool {
	state1Len := len(state1)
	state2Len := len(state2)
	if state1Len != state2Len {
		return true
	}

	for i := 0; i < state1Len; i++ {
		entityState1 := state1[i]
		entityState2 := state2[i]

		// TODO: Replace logic below with VisableFields function once @ golang 1.17
		// https://stackoverflow.com/a/66511341/5181480
		if entityState1.Id != entityState2.Id {
			return true
		}
		if entityState1.X != entityState2.X {
			return true
		}
		if entityState1.Y != entityState1.Y {
			return true
		}
	}

	return false
}

func (this *NetworkService) UpdateState(state GameState) {
	this.gameStateBefore = this.gameStateNow
	this.gameStateNow = state

	this.BroadcastState()
	// isStateDifferent := this.IsStateDifferent(this.gameStateNow, this.gameStateBefore)
	// if isStateDifferent {
	// 	this.BroadcastState()
	// }
}

func (this *NetworkService) BroadcastState() {
	for i := 0; i < len(this.users); i++ {
		user := this.users[i]
		user.connection.WriteJSON(this.gameStateNow)
	}
}
