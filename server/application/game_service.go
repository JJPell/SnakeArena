package application

import (
	"errors"

	"github.com/JJPell/SnakeArena/domain/game"
)

type GameService struct {
	games        map[int]*game.Game
	gamesCreated int
}

func NewGameService() *GameService {
	return &GameService{
		games:        make(map[int]*game.Game),
		gamesCreated: 0,
	}
}

func (this *GameService) CreateGame() *game.Game {
	game := game.NewGame(this.gamesCreated)
	this.gamesCreated++
	return game
}

func (this *GameService) RemoveGame(id int) {
	delete(this.games, id)
}

func (this *GameService) AddPlayer(gameId int, connectionId int, x int, y int, initialLength int) error {
	game, err := this.getGame(gameId)
	if err != nil {
		return err
	}

	game.AddPlayer(connectionId, x, y, initialLength)
	return nil
}

func (this *GameService) RemovePlayer(gameId int, connectionId int) error {
	game, gameExists := this.games[gameId]
	if gameExists {
		game.RemovePlayer(connectionId)
		return nil
	}

	return errors.New("Game doesn't exist")
}

func (this *GameService) UpdatePlayerInput(gameId int, connectionId int, playerInput game.PlayerInput) error {
	game, gameExists := this.games[gameId]
	if gameExists {
		game.UpdatePlayerInput(connectionId, playerInput)
		return nil
	}

	return errors.New("Game doesn't exist")
}

func (this *GameService) GetState(gameId int) (*game.GameState, error) {
	game, gameExists := this.games[gameId]
	if gameExists {
		gameState := game.GetState()
		return gameState, nil
	}

	err := errors.New("Game doesn't exist")
	return nil, err
}

func (this *GameService) getGame(gameId int) (*game.Game, error) {
	game, gameExists := this.games[gameId]
	if gameExists {
		return game, nil
	}

	err := errors.New("Game doesn't exist")
	return nil, err
}
