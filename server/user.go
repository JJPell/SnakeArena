package main

import "github.com/gorilla/websocket"

type User struct {
	id         int
	connection *websocket.Conn
	input      UserInput
}
