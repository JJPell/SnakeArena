package user

import "github.com/JJPell/SnakeArena/domain/game"

const (
	UserStateNone    = 0
	UserStateNew     = 1
	UserStateUpdated = 2
	UserStateRemoved = 3
)

type User struct {
	Id        int
	Input     UserInput
	State     int
	GameState game.GameState
}
