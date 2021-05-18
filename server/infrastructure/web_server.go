package infrastructure

import (
	"fmt"
	"log"
	"net/http"
	"strconv"

	"github.com/gorilla/websocket"
)

type WebServer struct {
	Messages         map[int]byte
	PreviousMessages map[int]byte
	Connections      map[int]*websocket.Conn
	upgrader         websocket.Upgrader
	totalConnections int
}

func NewWebServer(upgrader websocket.Upgrader) *WebServer {
	var messages = make(map[int]byte)
	var connections = make(map[int]*websocket.Conn)

	return &WebServer{
		Messages:         messages,
		PreviousMessages: messages,
		Connections:      connections,
		upgrader:         upgrader,
		totalConnections: 0,
	}
}

func (this *WebServer) Start() {
	http.HandleFunc("/", this.indexHandler)
	http.HandleFunc("/ws", this.websocketHandler)
	fmt.Println("Web Server Started")
	http.ListenAndServe(":8080", nil)
}

func (this *WebServer) Update() {
	this.PreviousMessages = this.Messages

	for id, connection := range this.Connections {
		if connection != nil {
			_, message, err := connection.ReadMessage()

			if err != nil {
				log.Print(err)
				return
			}

			this.Messages[id] = message[0]
		}
	}
}

func (this *WebServer) indexHandler(res http.ResponseWriter, req *http.Request) {
	fmt.Fprint(res, "Hello")
}

func (this *WebServer) websocketHandler(res http.ResponseWriter, req *http.Request) {
	// The following check origin code disables CORS
	this.upgrader.CheckOrigin = func(r *http.Request) bool {
		return true
	}

	ws, err := this.upgrader.Upgrade(res, req, nil)

	if err != nil {
		log.Fatal(err)
	}

	log.Print("New websocket connection ... " + strconv.Itoa(this.totalConnections))

	this.Connections[this.totalConnections] = ws

	this.totalConnections++
}
