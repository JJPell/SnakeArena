package main

import (
	"log"
	"strconv"
)

type Position struct {
	x int
	y int
}

type Player struct {
	userId         int
	position       Position
	networkService *NetworkService
	duration       int64
}

func (this *Player) Update(delta int64, time int64) {
	user := this.networkService.GetUser(this.userId)
	input := user.input

	if input.IsInputDirection() {
		this.duration += delta
	}

	if input.up {
		this.position.y++
	} else if input.down {
		this.position.y--
	} else if input.left {
		this.position.x--
	} else if input.right {
		this.position.x++
	}

	this.duration = 0

	log.Print(strconv.Itoa(this.position.x) + ", " + strconv.Itoa(this.position.y))

}
