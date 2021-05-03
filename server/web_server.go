package main

import (
	"fmt"
	"net/http"
)

type WebServer struct {
	networkService *NetworkService
}

func NewWebServer(networkService *NetworkService) *WebServer {
	return &WebServer{
		networkService: networkService,
	}
}

func (this *WebServer) Start() {
	http.HandleFunc("/", this.homePage)
	http.HandleFunc("/ws", this.wsEndPoint)
	fmt.Println("Web Server Started")
	http.ListenAndServe(":8080", nil)
}

func (this *WebServer) homePage(res http.ResponseWriter, req *http.Request) {
	fmt.Fprint(res, "Hello World")
}

func (this *WebServer) wsEndPoint(res http.ResponseWriter, req *http.Request) {
	this.networkService.HandleRequest(res, req)
}
