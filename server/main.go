package main

import (
	"fmt"
	"time"

	"github.com/gorilla/websocket"
)

var upgrader = websocket.Upgrader{
	ReadBufferSize:  1024,
	WriteBufferSize: 1024,
}

func main() {
	networkManager := NewNetworkManager(upgrader)
	webServer := NewWebServer(networkManager)
	world := NewWorld(networkManager)

	go webServer.Start()

	timeBefore := time.Now().Unix()

	fmt.Println("Starting Game Loop")

	// tickRate := 8
	// tickRateDuration := time.Duration(time.Duration((1000 / tickRate)) * time.Millisecond)
	tickRateDuration := 1 * time.Second

	for tick := range time.Tick(tickRateDuration) {
		timeNow := tick.Unix()
		delta := timeNow - timeBefore
		world.Update(timeNow, delta)
		timeBefore = timeNow
	}
}
