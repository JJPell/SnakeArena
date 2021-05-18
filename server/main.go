package main

import (
	"fmt"
	"time"

	"github.com/JJPell/SnakeArena/application"
	u "github.com/JJPell/SnakeArena/domain/user"
	"github.com/JJPell/SnakeArena/infrastructure"
	"github.com/gorilla/websocket"
)

var upgrader = websocket.Upgrader{
	ReadBufferSize:  1024,
	WriteBufferSize: 1024,
}

func updatePlayers(gameService *application.GameService, userService *application.UserService) {
	users := userService.GetUsers()

	for _, user := range users {
		if user.State == u.UserStateNew {
			this.Game
		}
	}
}

func main() {
	webServer := infrastructure.NewWebServer(upgrader)
	userRepository := infrastructure.NewUserRepository(webServer)
	gameService := application.NewGameService()
	userService := application.NewUserService(userRepository)

	timeBefore := time.Now().Unix()

	fmt.Println("Starting Game Loop")

	tickRateDuration := 100 * time.Millisecond

	for tick := range time.Tick(tickRateDuration) {
		timeNow := tick.Unix()
		delta := timeNow - timeBefore
		game.Update(timeNow, delta)
		timeBefore = timeNow
	}
}
